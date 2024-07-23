<template>

	<!-- Billing Information Card -->
	<a-card :bordered="false" class="header-solid h-full" :bodyStyle="{paddingTop: 0, paddingBottom: '16px' }">
		<template #title>
			<!--<h6 class="font-semibold m-0">工作负载(Jobs)</h6>-->
			<span style="font-size:17px">工作负载(Jobs)</span>
			<a-button type="dashed" size="small" shape="round" style="float:right;">数据导出到Excel文件</a-button>
		</template>
		<a-row :gutter="[24, 24]">
			<a-col :span="24" v-for="(item,id) in allWorkloadData" :key=id>
				<a-card :bordered="false" class="card-billing-info">
					<div class="col-info">
						<a-tooltip>
							<template #title>{{InitWorkload(item.id).description}}</template>
							<a-checkbox @change="cb_change(item.title,true)" :checked="judgeIsChecked(item.id,false)">
								<span class="span_title">{{item.title}}</span>
							</a-checkbox>	
						</a-tooltip>
						<a-icon type="more" @click="pluginDetail(item.id,false)"></a-icon>
						<p></p>
						<a-descriptions title="Required" :column="2" v-if="judgeIsOptional(item.includePackages,true).length!=0" style="margin-left:10px;">
							<a-descriptions-item v-for="(itemtag,id) in judgeIsOptional(item.includePackages,true)" :key=id>
								<a-tooltip color="green" v-if="!itemtag.isOptional">
									<template #title >{{InitpluginTag(itemtag.id).description}}</template>
									<!--定义临时变量-->
									<span v-show="false">
										{{plugin=InitpluginTag(itemtag.id)}}
									</span>
									<a-icon type="close" v-if="plugin.id==='not know'"></a-icon>
									<a-checkbox v-else disabled :checked="judgeIsChecked(itemtag.id,true)"></a-checkbox>
									<a-tag class="exampleClass1" @click="pluginDetail(itemtag.id,true)" :color="InitpluginTagColor(itemtag.id)" style="margin-left:5px;">{{plugin.title}} </a-tag>
								</a-tooltip>
							</a-descriptions-item>
						</a-descriptions>
						<p></p>
						<a-descriptions title="Optional" :column="2" style="margin-left:10px;" v-if="judgeIsOptional(item.includePackages,flase).length!=0">
							<a-descriptions-item v-for="(itemtag,id) in judgeIsOptional(item.includePackages,false)" :key=id>
								<a-tooltip color="green" v-if="itemtag.isOptional">
									<template #title >{{InitpluginTag(itemtag.id).description}}</template>
									<!--定义临时变量-->
									<span v-show="false">
										{{plugin=InitpluginTag(itemtag.id)}}
									</span>
									<a-icon type="close" v-if="plugin.id==='not know'"></a-icon>
									<a-checkbox v-else @change="cb_change(InitpluginTag(itemtag.id).title,false)" :checked="judgeIsChecked(itemtag.id,true)"></a-checkbox>
									<a-tag class="exampleClass1" @click="pluginDetail(itemtag.id,true)" :color="InitpluginTagColor(itemtag.id)" style="margin-left:5px;">{{plugin.title}} </a-tag>
									
								</a-tooltip>
							</a-descriptions-item>
						</a-descriptions>
					</div>
				</a-card>
			</a-col>
			<a-button style="margin-left:10px;" size="small" @click="selectAll(true)">Select All</a-button>
			<a-button style="margin-left:20px;" size="small" @click="selectAll(false)">Unselect All</a-button>
			<span style="margin-left:20px;">Total space required: <b>{{totalSpace_view}}</b> {{totalSpaceUnit}}</span><p></p>
			<p style="margin-left:10px;">* Select All or Unselect All does not change the selection of the "Environment dependency" component (runtime components).
			</p>
		</a-row>
		<a-drawer
			placement="right"
			:closable="false"
			:visible="false"
			:width="600"
			@close="onClodeDrawer"
		>
			<div class="drawer-content">
				<p>{{selectPlugin.description}}</p>

			</div>
			
		</a-drawer> 
	</a-card>
	<!-- / Billing Information Card -->
	

</template>

<script>
import SdpliginService from '../service/GetSDPluginService'
import {EventBus} from '../../event-bus.js'
import {CloseOutlined,MoreOutlined,FullscreenOutlined} from "@ant-design/icons-vue"
import {notification} from 'ant-design-vue'
	
	export default ({
		components:{ CloseOutlined,MoreOutlined,FullscreenOutlined },
		data() {
			const sdpliginService=new SdpliginService();
			var totalSpace=0;
			var selectPlugin;
			
			return {
				sdpliginService,		//接口服务
				totalSpace,					//总大小
				totalSpace_view:0,
				totalSpaceUnit:'MB',
				isCalculatePlugin:[],//已经计算空间的插件
				productSelectPlugin:[],//主产品包默认添加的插件
				
				allData:[],
				allWorkloadData:[],	//获取所有Workload存储
				allExtentionData:[],//获取所有插件存储
				allExtentionData_copy:[],
				
				allWorkloadData_check:[],
				allExtentionData_check:[],
				
				selectPlugin,				//选择插件
				
				pluginServer:{
					pluginserver:this.GLOBAL.pluginServer,
          pluginserversdx:this.GLOBAL.pluginServer_sdx,
          pluginversion:this.GLOBAL.pluginVersion,
        },//全局变量控制中英文服务器切换
			}
		},
		
		mounted(){
			this.getAll()
			this.getworkloads()
			this.getplugins()
			
			//监听设置项
			EventBus.$on('pluginServer',pluginServer=>{
				this.pluginServer=pluginServer
				this.getworkloads()
				this.getplugins()
			})
      
			EventBus.$on('pluginVersion', pluginVersion=>{
				this.pluginServer.pluginversion = pluginVersion
				this.getworkloads()
				this.getplugins()
      })
			
			EventBus.$on('searchplugin',searchInput=>{
				this.fliterSearchPlugin(searchInput)
			})

			EventBus.$on('pluginServerApi',pluginServerApi=>{
				this.getAll()
				this.getworkloads()
				this.getplugins()
				//this.$message.info(pluginServerApi)
			})
			
			EventBus.$emit('JobDate',this.allData)
			
		},
		
		methods:{
			//插件tag点击事件，事件总线将点击插件信息传递给drawer
			pluginDetail(value,isExtention){
				if(isExtention){
					for(var i=0;i<this.allExtentionData.length;i++){
						if(value===this.allExtentionData[i].id){
							EventBus.$emit('pluginServer',this.pluginServer)
							EventBus.$emit('pluginDetailVisible',true)
							EventBus.$emit('plugin',this.allExtentionData[i])
							EventBus.$emit('allPlugin',this.allExtentionData)
							break;
						}
					}	
				}
				else{
					for(var i=0;i<this.allWorkloadData.length;i++){
						if(value===this.allWorkloadData[i].id){
							EventBus.$emit('pluginServer',this.pluginServer)
							EventBus.$emit('pluginDetailVisible',true)
							EventBus.$emit('plugin',this.allWorkloadData[i])
							EventBus.$emit('allPlugin',this.allExtentionData)
							break;
						}
					}	
				}
			},
			
			getAll(){
				this.sdpliginService.getAll().then(res=>{
					this.allData=res.data
					EventBus.$emit('JobDate',this.allData)
				})
			},
			
			//初始化空间大小
			initSpace(){
				
				//获取主产品包及必安装的核心包计算大小
				var prohuctplugin=this.getPluginByIdfromLocalData('SnapDevelop')
				
				this.calculateSpace(prohuctplugin, true) 
			},
			
			//传入插件，计算空间
			calculateSpace(prohuctplugin, isSelect){
				//勾选
				if(isSelect){
					if(this.isCalculatePlugin.indexOf(prohuctplugin.id)===-1){
						this.totalSpace += (prohuctplugin.installBytes +prohuctplugin.zipBytes)
						this.isCalculatePlugin.push(prohuctplugin.id)//加入已计算列表
						
						//console.log(prohuctplugin.id)
						
						//include的插件进行计算
						prohuctplugin.includePackages.forEach(item=>{
							if(item.isSelected){
								var plugin=this.getPluginByIdfromLocalData(item.id)
								if(plugin!=null){
									this.calculateSpace(plugin, isSelect)
								}
							}
						})	
						
						//preDependencies前置依赖计算空间
						prohuctplugin.preDependencies.forEach(item=>{
							if(item.isSelected){
								var plugin=this.getPluginByIdfromLocalData(item.id)
								if(plugin!=null){
									this.calculateSpace(plugin, isSelect)
								}
							}
						})
					}
				}
				//反勾选
				else{
					if(this.isCalculatePlugin.indexOf(prohuctplugin.id)!=-1){
						this.totalSpace -= (prohuctplugin.installBytes +prohuctplugin.zipBytes)
						this.isCalculatePlugin=this.isCalculatePlugin.filter((item)=>{
							return item!==prohuctplugin.id
						})
						
						//TODO 反勾选时还需要考虑依赖插件
					}
				}
			},
			
			//获取所有工作负载
			async getworkloads(){
				var headerUri_product="/api/v3/product/getpackages?packageType=SDSDK-WORKLOAD&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer).then((res)=>{
					this.allWorkloadData=res.data
					this.selectPlugin=res.data[0]
					//初始化插件勾选状态
					this.allWorkloadData.forEach(item=>{
						var workload={
							id:item.id,
							title:item.title,
							checked:false
						}
						this.allWorkloadData_check.push(workload)
					}) 
				}).catch(err=>{
					this.$message.info(err.message)
				})
			},
			
			//获取所有插件
			async getplugins(){
				var headerUri_product="/api/v3/product/getpackages?packageType=SDSDK-EXTENSION&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer).then((res)=>{
					this.allExtentionData=res.data
					this.allExtentionData_copy=res.data
					
					//初始化插件勾选状态
					this.allExtentionData.forEach(item=>{
						var extention={
							id:item.id,
							title:item.title,
							checked:false
						}
						this.allExtentionData_check.push(extention)
					})
				})
				
				
				
				//从产品包开始计算插件空间
				this.calculateSpaceForProduct()
				//console.log(this.isCalculatePlugin)
				this.isCalculatePlugin.forEach(item=>{
					this.productSelectPlugin.push(item)
				})
				
				//默认勾选项
				this.initExtentionPluginisSelected()
				
				this.spaceView()
			},
			
			//计算主产品包空间
			calculateSpaceForProduct(){
				this.initSpace()
				
				this.spaceView()
			},
			
			//显示计算的空间
			spaceView(){
				this.totalSpace_view=(this.totalSpace/(1024*1024)).toFixed(2)
				
				if(this.totalSpace_view>1000){
					this.totalSpace_view=(this.totalSpace_view/1024).toFixed(2)
					this.totalSpaceUnit="GB"
				}
				
				//console.log(this.isCalculatePlugin)
			},
			
			//默认勾选的插件
			initExtentionPluginisSelected(){
				this.allWorkloadData.forEach(workload=>{
					var isSelectNumber=0
					workload.includePackages.forEach(include=>{
						if(include.isSelected){
							isSelectNumber+=1
							
							for (var i = 0; i < this.allExtentionData_check.length; i++) {
								if(include.id===this.allExtentionData_check[i].id){
									this.allExtentionData_check[i].checked=true
									//计算空间
									var plugin=this.InitpluginTag(this.allExtentionData_check[i].id)
									if(plugin!=null){
										this.calculateSpace(plugin, true)
									}
									break
								}
							}
						}
					})
					
					//是否Job类别全部勾选
					if(isSelectNumber===workload.includePackages.length){
						for (var i = 0; i < this.allWorkloadData_check.length; i++) {
							if(workload.id===this.allWorkloadData_check[i].id){
								this.allWorkloadData_check[i].checked=true
								break
							}
						}
					}
				})
				
				EventBus.$emit('pluginList_check',this.allExtentionData_check)
			},
			
			
			//根据id获取插件实体
			InitpluginTag(value){
				var tagPlugin=null
				var isExist=false
				
				for(var i=0;i<this.allExtentionData.length;i++){
					if(value===this.allExtentionData[i].id){
						tagPlugin=this.allExtentionData[i]
						isExist=true
						break;
					}
				}
				
				if(!isExist){
					tagPlugin=JSON.parse(JSON.stringify(this.allExtentionData[0]))
					tagPlugin.title=value
					tagPlugin.description='无法找到id'
					tagPlugin.id='not know'
				}
				return tagPlugin
			},
			
			getPluginByIdfromLocalData(id){
				
				var plugin
				for (var i = 0; i < this.allData.length; i++) {
					if(this.allData[i].id===id){
						plugin=this.allData[i]
						break
					}
				}
				if(plugin==null){
					console.log(id+"不存在！")
				}
				
				return plugin
			},
			
			InitWorkload(value){
				var workload=null
				var isExist=false
				
				for(var i=0;i<this.allWorkloadData.length;i++){
					if(value===this.allWorkloadData[i].id){
						workload=this.allWorkloadData[i]
						isExist=true
						break;
					}
				}
				
				if(!isExist){
					workload=JSON.parse(JSON.stringify(this.allWorkloadData[0]))
					workload.title=value
					workload.description='无法找到Workload'
					workload.id='not know'
				}
				return workload
			},
			
			//根据插件id判断返回显示tag颜色
			InitpluginTagColor(value){
				var tagplugin
				for(var i=0;i<this.allExtentionData.length;i++){
					if(value===this.allExtentionData[i].id){
						tagplugin=this.allExtentionData[i]
						break;
					}
				}
				var tagColor='red'
				if(!tagplugin){
					tagColor = 'red'
				}
				else{
					switch(tagplugin.kind){
						case 'Debugger':
						case '调试器':
							tagColor = 'pink'
							break;
						case 'ORM framework':
						case 'ORM 框架':
							tagColor = 'orange'
							break;
						case 'LowCode':
						case 'Low-Code':
							tagColor = 'green'
							break;
						case 'Utilities':
						case '工具组':
							tagColor = 'cyan'
							break;
						case 'Test platform':
						case '测试平台':
							tagColor = 'blue'
							break;
						case 'Coding tools':
						case '编码工具':
							tagColor = 'purple'
							break;
						case 'Database support':
						case '数据库支持':
							tagColor = 'red'
							break;
						case 'Development support':
						case '开发支持':
							tagColor = 'yellow'
							break;
						default:
							tagColor = 'red'
							break;
					}	
				}
				
				return tagColor
			},

			//判断是否为可选插件
			judgeIsOptional(value, isRequiredType){
				var isOptionalIncludes=[]
				var isRequiredIncludes=[]
				var returnIncludes=[]

				value.forEach(element => {
					if(element.isOptional){
						isOptionalIncludes.push(element)
					}
					else{
						isRequiredIncludes.push(element)
					}
				});

				returnIncludes = isRequiredType?isRequiredIncludes:isOptionalIncludes
				return returnIncludes
			},
			
			//根据id获取勾选状态
			judgeIsChecked(value,isOptional){
				var isCheck=false
				
				if(isOptional){
					for (var i = 0; i < this.allExtentionData_check.length; i++) {
						if(value===this.allExtentionData_check[i].id){
							isCheck=this.allExtentionData_check[i].checked
							break
						}
					}
				}else{
					for (var i = 0; i < this.allWorkloadData_check.length; i++) {
						if(value===this.allWorkloadData_check[i].id){
							isCheck=this.allWorkloadData_check[i].checked
							break
						}
					}
				}
				
				return isCheck
			},
			
			//插件勾选、反勾选事件
			cb_change(value,isJob){
				var isCheck=false
				var opretion="反勾选"
				var icon=''
				var notification_des=''
				
				if(isJob){
					for (var i = 0; i < this.allWorkloadData_check.length; i++) {
						if(value===this.allWorkloadData_check[i].title){
							this.allWorkloadData_check[i].checked=this.allWorkloadData_check[i].checked?false:true
							isCheck=this.allWorkloadData_check[i].checked
							break	
						}
					}
				}
				else{
					for (var i = 0; i < this.allExtentionData_check.length; i++) {
						if(value===this.allExtentionData_check[i].title){
							this.allExtentionData_check[i].checked=this.allExtentionData_check[i].checked?false:true
							isCheck=this.allExtentionData_check[i].checked
							var plugin=this.InitpluginTag(this.allExtentionData_check[i].id)
							
							this.calculateSpace(plugin, isCheck)
							this.spaceView()
							
							break	
						}
					}
				}
				
				if(isCheck){
					opretion="勾选"
					icon='success'
				}else{
					opretion="反勾选"
					icon='error'
				}
				
				if(isJob){
					notification_des=opretion+'插件：'+this.relationPluginChecked(value,isJob,isCheck)
				}else{
					this.relationPluginChecked(value,isJob,isCheck)
				}
				
				notification.open({
					type:icon,
					message:opretion+value,
					description:notification_des,
					duration:5,
					onClick:()=>{
						this.$message.info(value)
					},
					style:{
						width:'500px',
						marginLeft:`${335-500}px`,
					}
				})	
				
			},
			
			//插件勾选反勾选时其关联插件的操作
			relationPluginChecked(value,isJob,isCheck){
				var pluginList_title=[]
				var pluginList=[]
				
				if(isJob){
					var data=this.allWorkloadData
					
					for(var i=0;i<data.length;i++){
						if(value===data[i].title){
							pluginList=data[i].includePackages
//							data[i].includePackages.forEach(item=>{
//								item.includePackages.forEach(element=>{
//									pluginList.push(element)
//								})
//							})
							break;
						}
					}
	
					pluginList.forEach(item=>{
						pluginList_title.push(this.InitpluginTag(item.id).title)
					})
					pluginList_title.forEach(item=>{
						for (var i = 0; i < this.allExtentionData_check.length; i++) {
							if(item===this.allExtentionData_check[i].title){
								this.allExtentionData_check[i].checked=isCheck
								this.calculateSpace(this.InitpluginTag(this.allExtentionData_check[i].id), isCheck)
								break
							}
						}
					})
					
					this.spaceView()
				}
				else{
					var data=this.allExtentionData
					
					for(var i=0;i<data.length;i++){
						if(value===data[i].title){
							pluginList=data[i].preDependencies
							break;
						}
					}
					
					pluginList.forEach(item=>{
						pluginList_title.push(this.InitpluginTag(item.id).title)
					})
					pluginList_title.forEach(item=>{
						for (var i = 0; i < this.allExtentionData_check.length; i++) {
							if(item===this.allExtentionData_check[i].title){
								this.allExtentionData_check[i].checked=isCheck
//								if(isCheck===true){
//									this.allExtentionData_check[i].checked=isCheck
//								}else{
//									this.opretionRequiredPluginCheck(this.allExtentionData_check[i])
//								}
								break
							}
						}
					})
				}
				
				//传递到component组件 
				EventBus.$emit('pluginCheck',isCheck)
				EventBus.$emit('pluginList_check',this.allExtentionData_check)
				
				return pluginList_title
			},
			
			//Todo
			opretionRequiredPluginCheck(requiredPlugin_check){
				for (var i = 0; i < this.allExtentionData_check.length; i++) {
					if(requiredPlugin_check.id===this.allExtentionData_check[i].id){
						
					}
				}
			},
			
			fliterSearchPlugin(searchInput){
				var searchTable=[]
				this.allExtentionData_copy.forEach(element => {
					if(element.title.toLowerCase().indexOf(searchInput.toLowerCase())!=-1){
						searchTable.push(element)
					}
				});

				this.allExtentionData=searchTable
			},
			
			//选择勾选/反勾选所有插件
			selectAll(isSelectAll){
				//select All
				if(isSelectAll){
					this.totalSpace=0
					this.isCalculatePlugin=[]
				}
				
				for (var i = 0; i < this.allExtentionData_check.length; i++) {
					this.allExtentionData_check[i].checked=isSelectAll
					
					var plugin = this.InitpluginTag(this.allExtentionData_check[i].id)
					
					if(isSelectAll){
						this.calculateSpace(plugin, isSelectAll)
					}
					else if(this.productSelectPlugin.indexOf(plugin.id)===-1){
						//console.log(this.productSelectPlugin)
						this.calculateSpace(plugin, isSelectAll)
					}
				}
				
				console.log(this.productSelectPlugin)
				console.log(this.isCalculatePlugin)

				for (var i = 0; i < this.allWorkloadData_check.length; i++) {
					this.allWorkloadData_check[i].checked=isSelectAll
				}
				
				this.initSpace()				
				this.spaceView()
				
				EventBus.$emit('pluginList_check',this.allExtentionData_check)
			},
			
		}
	})

</script>
<style>
.span_title{
	/*color:blue;*/
	font-weight: bold; 
	font-size: 15px;
}

.exampleClass1 {
cursor: pointer;
transition: transform 250ms cubic-bezier(.2,.8,.4,1);
}
.exampleClass1:hover {
transform: scale(1.10);
}
.exampleClass1:active {
transform: scale(0.9);
}
</style>