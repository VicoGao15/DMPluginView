<template>

	<!-- Billing Information Card -->
	<a-card :bordered="false" class="header-solid h-full" :bodyStyle="{paddingTop: 0, paddingBottom: '16px' }">
		<template #title>
			<!--<h6 class="font-semibold m-0">组件(Components)</h6>-->
			<span style="font-size:17px">组件(Components)</span>
			<a-button type="dashed" size="small" shape="round" style="float:right;" @click="saveExcelFile()">数据导出到为Excel文件</a-button>
		</template>
		<a-row :gutter="[24, 24]">

			<a-col :span="24" v-if="kind_Debugger.pluginList.length!=0">
			<a-card :bordered="false" class="card-billing-info">
				<div class="col-info">
					<a-descriptions :column="2">
						<template #title>
							<span class="span_title">{{kind_Debugger.categoryName}}</span>
						</template>
						<a-descriptions-item v-for="(item,id) in kind_Debugger.pluginList" :key=id >
							<a-tooltip>
								<template #title>{{item.description}}</template>
								<a-tag class="exampleClass1" style="margin-left:10px;margin-top:5px;" @click="pluginDetail(item)" color="pink">{{item.title}} </a-tag>
								<a-icon type="check" style="color:#52c41a" v-if="getCheckedStatus(item.id)"></a-icon>
							</a-tooltip>
						</a-descriptions-item>
					</a-descriptions>
				</div>
			</a-card>
			</a-col>
			<a-col :span="24" v-if="kind_ORMFramework.pluginList.length!=0">
			<a-card :bordered="false" class="card-billing-info">
				<div class="col-info">
					<a-descriptions :column="2">
						<template #title>
							<h6>{{kind_ORMFramework.categoryName}}</h6>
						</template>
						<a-descriptions-item v-for="(item,id) in kind_ORMFramework.pluginList" :key=id>
							<a-tooltip>
								<template #title>{{item.description}}</template>
								<a-tag class="exampleClass1" style="margin-left:10px;margin-top:5px;" @click="pluginDetail(item)" color="orange">{{item.title}} </a-tag>
								<a-icon type="check" style="color:#52c41a" v-if="getCheckedStatus(item.id)"></a-icon>
							</a-tooltip>
						</a-descriptions-item>
					</a-descriptions>
				</div>
			</a-card>
			</a-col>
			<a-col :span="24" v-if="kind_LowCode.pluginList.length!=0">
			<a-card :bordered="false" class="card-billing-info">
				<div class="col-info">
					<a-descriptions :column="2">
						<template #title>
							<h6>{{kind_LowCode.categoryName}}</h6>
						</template>
						<a-descriptions-item v-for="(item,id) in kind_LowCode.pluginList" :key=id >
							<a-tooltip>
								<template #title>{{item.description}}</template>
								<a-tag class="exampleClass1" style="margin-left:10px;margin-top:5px;" @click="pluginDetail(item)" color="green">{{item.title}} </a-tag>
								<a-icon type="check" style="color:#52c41a" v-if="getCheckedStatus(item.id)"></a-icon>
							</a-tooltip>
						</a-descriptions-item>
					</a-descriptions>
				</div>
			</a-card>
			</a-col>
			<a-col :span="24" v-if="kind_Utilities.pluginList.length!=0">
			<a-card :bordered="false" class="card-billing-info">
				<div class="col-info">
					<a-descriptions :column="2">
						<template #title>
							<h6>{{kind_Utilities.categoryName}}</h6>
						</template>
						<a-descriptions-item v-for="(item,id) in kind_Utilities.pluginList" :key=id >
							<a-tooltip>
								<template #title>{{item.description}}</template>
								<a-tag class="exampleClass1" style="margin-left:10px;margin-top:5px;" @click="pluginDetail(item)" color="cyan">{{item.title}} </a-tag>
								<a-icon type="check" style="color:#52c41a" v-if="getCheckedStatus(item.id)"></a-icon>
							</a-tooltip>
						</a-descriptions-item>
					</a-descriptions>
				</div>
			</a-card>
			</a-col>
			<a-col :span="24" v-if="kind_TestPlatform.pluginList.length!=0">
			<a-card :bordered="false" class="card-billing-info">
				<div class="col-info">
					<a-descriptions :column="2">
						<template #title>
							<h6>{{kind_TestPlatform.categoryName}}</h6>
						</template>
						<a-descriptions-item v-for="(item,id) in kind_TestPlatform.pluginList" :key=id >
							<a-tooltip>
								<template #title>{{item.description}}</template>
								<a-tag class="exampleClass1" style="margin-left:10px;margin-top:5px;" @click="pluginDetail(item)" color="blue">{{item.title}} </a-tag>
								<a-icon type="check" style="color:#52c41a" v-if="getCheckedStatus(item.id)"></a-icon>
							</a-tooltip>
						</a-descriptions-item>
					</a-descriptions>
				</div>
			</a-card>
			</a-col>
			<a-col :span="24" v-if="kind_CodingTools.pluginList.length!=0">
			<a-card :bordered="false" class="card-billing-info">
				<div class="col-info">
					<a-descriptions :column="2">
						<template #title>
							<h6>{{kind_CodingTools.categoryName}}</h6>
						</template>
						<a-descriptions-item v-for="(item,id) in kind_CodingTools.pluginList" :key=id >
							<a-tooltip>
								<template #title>{{item.description}}</template>
								<a-tag class="exampleClass1" style="margin-left:10px;margin-top:5px;" @click="pluginDetail(item)" color="purple">{{item.title}} </a-tag>
								<a-icon type="check" style="color:#52c41a" v-if="getCheckedStatus(item.id)"></a-icon>
							</a-tooltip>
						</a-descriptions-item>
					</a-descriptions>
				</div>
			</a-card>
			</a-col>
			<a-col :span="24" v-if="kind_DatabaseSupport.pluginList.length!=0">
			<a-card :bordered="false" class="card-billing-info">
				<div class="col-info">
					<a-descriptions :column="2">
						<template #title>
							<h6>{{kind_DatabaseSupport.categoryName}}</h6>
						</template>
						<a-descriptions-item v-for="(item,id) in kind_DatabaseSupport.pluginList" :key=id>
							<a-tooltip>
								<template #title>{{item.description}}</template>
								<a-tag class="exampleClass1" style="margin-left:10px;margin-top:5px;" @click="pluginDetail(item)" color="red">{{item.title}} </a-tag>
								<a-icon type="check" style="color:#52c41a" v-if="getCheckedStatus(item.id)"></a-icon>
							</a-tooltip>
						</a-descriptions-item>
					</a-descriptions>
				</div>
			</a-card>
			</a-col>
			<a-col :span="24" v-if="kind_DevelopmentSupport.pluginList.length!=0">
			<a-card :bordered="false" class="card-billing-info">
				<div class="col-info">
					<a-descriptions :column="2">
						<template #title>
							<h6>{{kind_DevelopmentSupport.categoryName}}</h6>
						</template>
						<a-descriptions-item v-for="(item,id) in kind_DevelopmentSupport.pluginList" :key=id>
							<a-tooltip>
								<template #title>{{item.description}}</template>
								<a-tag class="exampleClass1" style="margin-left:10px;margin-top:5px;" @click="pluginDetail(item)" color="yellow">{{item.title}} </a-tag>
								<a-icon type="check" style="color:#52c41a" v-if="getCheckedStatus(item.id)"></a-icon>
							</a-tooltip>
						</a-descriptions-item>
					</a-descriptions>
				</div>
			</a-card>
			</a-col>
			<a-col :span="24" v-if="kind_Others.pluginList.length!=0">
			<a-card :bordered="false" class="card-billing-info">
				<div class="col-info">
					<a-descriptions :column="2">
						<template #title>
							<h6>{{kind_Others.categoryName}}</h6>
						</template>
						<a-descriptions-item v-for="(item,id) in kind_Others.pluginList" :key=id>
							<a-tooltip>
								<template #title>{{item.description}}</template>
								<a-tag class="exampleClass1" style="margin-left:10px;margin-top:5px;" @click="pluginDetail(item)" color="blue">{{item.title}} </a-tag>
								<a-icon type="check" style="color:#52c41a" v-if="getCheckedStatus(item.id)"></a-icon>
							</a-tooltip>
						</a-descriptions-item>
					</a-descriptions>
				</div>
			</a-card>
			</a-col>
			
		</a-row>
	</a-card>
	<!-- / Billing Information Card -->
	

</template>

<script>
import SdpliginService from '../../service/GetSDPluginService'
import {EventBus} from '../../../event-bus'
import {CheckOutlined,CheckCircleOutlined} from '@ant-design/icons-vue'
import * as XLSX from 'xlsx'
	
	export default ({
		components:{
			CheckOutlined,
			CheckCircleOutlined
		},
		data() {
			const sdpliginService=new SdpliginService();
			var selectPlugin;
			
			return {
				searchPlugin_name:'',
				sdpliginService,		//接口服务
				
				allExtentionData:[],//获取所有插件存储
				allPluginData:[],//获取所有组件存储
				//插件类别
				kind_Debugger:{categoryName:'',pluginList:[]},
				kind_ORMFramework:{categoryName:'',pluginList:[]},
				kind_LowCode:{categoryName:'',pluginList:[]},
				kind_Utilities:{categoryName:'',pluginList:[]},
				kind_TestPlatform:{categoryName:'',pluginList:[]},
				kind_EnviromentDependency:{categoryName:'',pluginList:[]},
				kind_CodingTools:{categoryName:'',pluginList:[]},
				kind_DatabaseSupport:{categoryName:'',pluginList:[]},
				kind_DevelopmentSupport:{categoryName:'',pluginList:[]},
				kind_Others:{categoryName:'Others',pluginList:[]},
				
				selectPlugin,				//选择插件
				
				pluginServer:{
					pluginserver:this.GLOBAL.pluginServer,
          pluginserversdx:this.GLOBAL.pluginServer_sdx,
          pluginversion:this.GLOBAL.pluginVersion,
        },//全局变量控制中英文服务器切换
        pluginVersion:this.GLOBAL.pluginVersion,
				
				//插件勾选相关
				isCheck:false,
				allExtentionData_check:[],
			}
		},
		
		mounted(){
			this.getExtentionplugins()
			this.getAllplugins()
			
			//事件总线
			EventBus.$on('searchplugin',(data)=>{
				this.searchPlugin_name=data
			})
			
			EventBus.$on('pluginServer',pluginServer=>{
				this.pluginServer=pluginServer
				this.getExtentionplugins()
				this.getAllplugins()
			})
      
      EventBus.$on('pluginVersion', pluginVersion=>{
				this.pluginServer.pluginversion = pluginVersion
				this.getExtentionplugins()
				this.getAllplugins()
      })
			
			EventBus.$on('pluginCheck',isCheck=>{
				this.isCheck=isCheck
			})
			EventBus.$on('pluginList_check',allExtentionData_check=>{
				this.allExtentionData_check=allExtentionData_check
			})
			
			EventBus.$on('pluginServerApi',pluginServerApi=>{
				this.getExtentionplugins()
				this.getAllplugins()
			})
		},
		
		methods:{
			pluginDetail(value){
						EventBus.$emit('pluginDetailVisible',true)
						EventBus.$emit('plugin',value)
						EventBus.$emit('allPlugin',this.allPluginData)
			},
			
			async getExtentionplugins(){
				var headerUri_product="/api/v3/product/getpackages?packageType=SDSDK-EXTENSION;SDSDK-ENVIRONMENT&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer).then((res)=>{
					this.allExtentionData=res.data
					this.selectPlugin=res.data[0]
					
					//执行分类
					this.categoryData()
				})
			},

			async getAllplugins(){
				var headerUri_product="/api/v3/product/getpackages?packageType=SDSDK;SDSDK-WORKLOAD;SDSDK-CORE;SDSDK-EXTENSION;SDSDK-ENVIRONMENT&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer).then((res)=>{
					this.allPluginData=res.data
				})
			},
			initCategory(){
				//插件类别
				this.kind_Debugger={categoryName:'',pluginList:[]},
				this.kind_ORMFramework={categoryName:'',pluginList:[]},
				this.kind_LowCode={categoryName:'',pluginList:[]},
				this.kind_Utilities={categoryName:'',pluginList:[]},
				this.kind_TestPlatform={categoryName:'',pluginList:[]},
				this.kind_EnviromentDependency={categoryName:'',pluginList:[]},
				this.kind_CodingTools={categoryName:'',pluginList:[]},
				this.kind_DatabaseSupport={categoryName:'',pluginList:[]},
				this.kind_DevelopmentSupport={categoryName:'',pluginList:[]},
				this.kind_Others={categoryName:'Others',pluginList:[]}
			},
			categoryData(){
				this.initCategory()
				this.allExtentionData.forEach(item=>{
					switch(item.kind){
						case 'Debugger':
						case '调试器':
							this.kind_Debugger.categoryName=item.kind
							this.kind_Debugger.pluginList.push(item)
							break;
						case 'ORM framework':
						case 'ORM 框架':
							this.kind_ORMFramework.categoryName=item.kind
							this.kind_ORMFramework.pluginList.push(item)
							break;
						case 'LowCode':
						case 'Low-Code':
							this.kind_LowCode.categoryName=item.kind
							this.kind_LowCode.pluginList.push(item)
							break;
						case 'Utilities':
						case '工具组':
							this.kind_Utilities.categoryName=item.kind
							this.kind_Utilities.pluginList.push(item)
							break;
						case 'Test platform':
						case '测试平台':
							this.kind_TestPlatform.categoryName=item.kind
							this.kind_TestPlatform.pluginList.push(item)
							break;
						case 'Coding tools':
						case '编码工具':
							this.kind_CodingTools.categoryName=item.kind
							this.kind_CodingTools.pluginList.push(item)
							break;
						case 'Database support':
						case '数据库支持':
							this.kind_DatabaseSupport.categoryName=item.kind
							this.kind_DatabaseSupport.pluginList.push(item)
							break;
						case 'Development support':
						case '开发支持':
							this.kind_DevelopmentSupport.categoryName=item.kind
							this.kind_DevelopmentSupport.pluginList.push(item)
							break;
						case 'Environment dependency':
						case '环境依赖':
							this.kind_EnviromentDependency.categoryName=item.kind
							this.kind_EnviromentDependency.pluginList.push(item)
							break;
						default:
							//this.kind_Others.categoryName=this.pluginServer=='devmagic'?'Others':'其他'
							this.kind_Others.pluginList.push(item)
							break;
					}
				})
				this.kind_Others.categoryName=this.pluginServer=='devmagic'?'Others':'其他'
			},

			tagViewDetail(value){
				var isExist=false
				for (let index = 0; index < this.allPluginData.length; index++) {
					const element = this.allPluginData[index];
					if(value===this.allPluginData[index].id){
						this.detailDrawerVisible=false
						this.selectPlugin=this.allPluginData[index]
						this.detailDrawerVisible=true

						isExist=true
						break;
					}
				}

				if(!isExist){
					this.$message.error("插件不存在："+value);
				}
			},
			
			getCheckedStatus(id){
				var checked=false
				for (var i = 0; i < this.allExtentionData_check.length; i++) {
					if(id===this.allExtentionData_check[i].id){
						checked=this.allExtentionData_check[i].checked
						break
					}
				}
				
				return checked
			},
			
			saveExcelFile(){
				
				// 自定义下载的header，注意是数组中的数组哦
				const Header = [['插件名称', '插件描述']]
		
				// 将JS数据数组转换为工作表。
				const headerWs = XLSX.utils.aoa_to_sheet(Header);
				
				//获取Component数据
				var exportDate_Component = this.getExportDate()
				const ws_Components = XLSX.utils.sheet_add_json(headerWs, exportDate_Component, {skipHeader: true, origin: 'A2'})
				//获取Job数据
				//var exportDate_Job = this.getExportDate()
				//const ws_Jobs = XLSX.utils.sheet_add_json(headerWs, exportDate_Job, {skipHeader: true, origin: 'A2'})
		
				/* 新建空的工作表 */
				const wb = XLSX.utils.book_new()
				// 可以自定义下载之后的sheetname
				//XLSX.utils.book_append_sheet(wb, ws_Jobs, 'Job分类')
				XLSX.utils.book_append_sheet(wb, ws_Components, 'Component分类')
		
				/* 生成xlsx文件 */
				XLSX.writeFile(wb, 'dmplugin.xlsx')
		  },
			
			getExportDate(){
				var exportDate=[]
				var allKind = [
					this.kind_Debugger,
					this.kind_ORMFramework,
					this.kind_LowCode,
					this.kind_Utilities,
					this.kind_TestPlatform,
					this.kind_EnviromentDependency,
					this.kind_CodingTools,
					this.kind_DatabaseSupport,
					this.kind_DevelopmentSupport
				]
				allKind.forEach(item=>{
					var categoryType={name:item.categoryName,Description:''}
					exportDate.push(categoryType)
					item.pluginList.forEach(pluginitem=>{
						var plugin = {name:pluginitem.title,Description:pluginitem.description}
						exportDate.push(plugin)
					})
				})
				
				return exportDate
			}
			
		}
	})

</script>

<style>
.span_title{
	/*color:blue;*/
	font-weight: bold; 
	font-size: 15px;
}

</style>