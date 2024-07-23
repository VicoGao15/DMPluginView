<template>
				
	<!-- Projects Table Card -->
	<a-card :bordered="false" class="header-solid h-full" :bodyStyle="{padding: 0,}">
		<template #title>
			<a-row type="flex" align="middle">
				<a-col :span="24" :md="3">
						<h5>{{pluginServer.pluginserversdx}}.{{pluginServer.pluginserver}}.com</h5>
				</a-col>
        <a-col :span="24" :md="9">
						<a-select 
              :value="currentVersion"
              @change="versionChange"
              :options="allsdxVersion"
              style="width:120px"/>
            <a-input 
              v-model="inputVersion"
              placeholder="Build号, 例：25.0.0.1364"
              @pressEnter="inputVersionChange()"
              style="width:200px"/>
				</a-col>
				<a-col :span="24" :md="0">
					<!--<a-button style="height:30px" @click="change()">切换服务器</a-button>-->
					<!--<a-dropdown>
						<a class="ant-dropdown-link" @click=""></a>
					</a-dropdown>-->
				</a-col>
				<a-col :span="24" :md="12" style="display: flex; align-items: center; justify-content: flex-end">
					<!--<a-input-search class="header-search" v-model:value="searchInput" loading :class="searchLoading ? 'loading' : ''" placeholder=" Search" @input="onSearch()" :loading='searchLoading' size="small">
						<svg slot="prefix" width="16" height="16" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
							<path fill-rule="evenodd" clip-rule="evenodd" d="M8 4C5.79086 4 4 5.79086 4 8C4 10.2091 5.79086 12 8 12C10.2091 12 12 10.2091 12 8C12 5.79086 10.2091 4 8 4ZM2 8C2 4.68629 4.68629 2 8 2C11.3137 2 14 4.68629 14 8C14 9.29583 13.5892 10.4957 12.8907 11.4765L17.7071 16.2929C18.0976 16.6834 18.0976 17.3166 17.7071 17.7071C17.3166 18.0976 16.6834 18.0976 16.2929 17.7071L11.4765 12.8907C10.4957 13.5892 9.29583 14 8 14C4.68629 14 2 11.3137 2 8Z" fill="#111827"/>
						</svg>
					</a-input-search> -->
					<a-radio-group v-model="projectHeaderBtns" size="small">
            <a-radio-button value="all" @click="pluginTypeSwitch(4)"><a-icon type="slack" style="color:green;font-size:15px;"></a-icon>&nbsp;&nbsp;&nbsp;所有包</a-radio-button>
            <a-radio-button value="installer" @click="pluginTypeSwitch(7)"><img src="images/logos/DevMagic_Studio_Installer.png" width="18" class="mr-10">Installer</a-radio-button>
						<a-radio-button value="product" @click="pluginTypeSwitch(0)"><img src="images/logos/DevMagic_Studio.png" width="18" class="mr-10">产品包</a-radio-button>
						<a-radio-button value="features" @click="pluginTypeSwitch(1)"><img src="images/logos/sdx_core.png" width="18" class="mr-10">核心包</a-radio-button>
						<a-radio-button value="extentions" @click="pluginTypeSwitch(2)"><img src="images/logos/sdx_ext.png" width="18" class="mr-10">插件包</a-radio-button>
						<a-radio-button value="environments" @click="pluginTypeSwitch(3)"><img src="images/logos/sdx_env.png" width="18" class="mr-10">环境包</a-radio-button>
            <a-radio-button value="workload" @click="pluginTypeSwitch(6)"><img src="images/logos/logo-jira.svg" width="18" class="mr-10">工作负载</a-radio-button>
						<a-radio-button value="notnormal" @click="pluginTypeSwitch(5)"><a-icon type="api" style="color:red;font-size:15px;"></a-icon>&nbsp;&nbsp;&nbsp;非正常包</a-radio-button>
					</a-radio-group>
				</a-col>
			</a-row>
		</template>
		<a-table 
			:columns="tableColumns" 
			:data-source="tableData" 
      :pagination="pagination"
      :scroll="{y:740}"
			row-key="id" 
			:customRow="tableRowClick"
			:showHeader="true" 
			style="margin-right:20px;"
			>
			
			<template slot="pluginid" slot-scope="pluginid">
				<span class="font-bold text-muted text-sm">{{ pluginid.label ? pluginid.label : pluginid }}</span>
			</template>

			<template slot="type" slot-scope="type">
					<img :src="logoType(type)" width="23" class="mr-10">
			</template>
			
			<template slot="company" slot-scope="company">
				<h6 class="m-0">
					<!--<img :src="pluginicon" width="25" class="mr-10">-->
					{{ company }}
				</h6>
			</template>

			<template slot="version" slot-scope="version">
				<span class="font-bold text-muted text-sm" v-if="version===this.currentVersion">{{ version }}</span>
				<span class="font-bold text-muted text-sm" v-else>{{ version }} - Not Normal</span>
			</template>
			
			<template slot="publishTime" slot-scope="publishTime">
				<span class="font-bold text-muted text-sm" >{{ timeFormate(publishTime) }}</span>
			</template>

			<template slot="zipSpace" slot-scope="zipSpace">
				{{calculateSize(zipSpace)}} MB
			</template>

			<template slot="completion" slot-scope="completion">
				<span class="font-bold text-muted text-sm">{{ completion.label ? completion.label : completion }}</span>
				<a-progress :percent="completion.value ? completion.value : completion" :show-info="false" size="small" :status="completion.status ? completion.status : 'normal'" />
			</template>

		</a-table>
		<PluginDetailDrawer></PluginDetailDrawer>
    
    
    <div style="margin:0 20px 20px 20px;">
      <a-divider/>
  		<a-button type="dasked" size="small" @click="generateDeleteCommand()">版本删除命令</a-button>
      <a-button type="dasked" size="small" @click="InfoBackup()" style="margin-left:10px;">版本信息备份</a-button>
      
      <p></p><b>所有版本Url：</b> <a :href="allVersionSite" target="_blank">{{allVersionSite}}</a>
      <p></p><b>接口数据Api：</b>  <a :href="showURI" target="_blank">{{showURI}}</a>
    </div>
    
    <AddBackupPluginInfoConfirm></AddBackupPluginInfoConfirm>
	</a-card>
	<!-- / Projects Table Card -->

</template>
		
<script>
import PluginDetailDrawer from '../Drawer/PluginDetailDrawer.vue'
import AddBackupPluginInfoConfirm from '../Dialog/AddBackupPluginInfoConfirm.vue'
	import SdpliginService from '../service/GetSDPluginService'
  import SdxBackupService from '../service/GetSdxBackupService';
	import {EventBus} from '../../event-bus.js'
	import {notification} from 'ant-design-vue'
	import GLOBAL from '../../Global.vue'
	//import {CloseCircleOutlined,ExclamationCircleOutlined} from '@ant-design/icons-vue'

	export default ({
		components:{
			// CloseCircleOutlined,
			// ExclamationCircleOutlined
			PluginDetailDrawer,
      AddBackupPluginInfoConfirm
		},
		data() {

			// const pagination = computed(()=>({
			// 	total:200,
			// 	current:current.value,
			// 	pageSize:pageSize.value
			// }))
			
			var selectRow

			var pluginicon= 'images/logos/DevMagic_Studio.png';
			const sdpliginService=new SdpliginService();
      const sdxBackupService = new SdxBackupService();
			var detailDrawerVisible=false;
			
			return {
				projectHeaderBtns: 'all',

				tableData:[],
				tableData_Copy:[],
        notNormalList:[],
        pagination:{
					total:0,
          pageSize:10,
          showSizeChanger:true,
          pageSizeOptions:['10','15','20','50'],
          showTotal:total => `共 ${total} 条数据`,
          showQuickJumper:true,//快速跳转
          showLessItems:true
        },
				tableColumns:[
					{
						title: '插件ID',
						dataIndex: 'id',
						class: 'font-bold text-muted text-sm',
						width: 270,
					},
					{
						title: '类型',
						dataIndex: 'packageType',
						scopedSlots: { customRender: 'type' },
						width: 80,
					},
					{
						title: '插件名称',
						dataIndex: 'title',
						scopedSlots: { customRender: 'company' },
						width: 290,
					},
					{
						title: '插件描述',
						dataIndex: 'description',
						width: 480,
					},
					{
						title: '版本',
						dataIndex: 'version',
						class: 'font-bold text-muted text-sm',
            width: 120,
					},
					{
						title: '上传时间',
						dataIndex: 'published',
						scopedSlots: { customRender: 'publishTime' },
						class: 'font-bold text-muted text-sm',
            width: 180,
					},
					{
						title: 'zip包大小',
						dataIndex: 'zipBytes',
						scopedSlots: { customRender: 'zipSpace' },
						class: 'font-bold text-muted text-sm',
            width: 120,
					},
					{
						title: '下载数量',
						scopedSlots: { customRender: 'completion' },
						dataIndex: 'downloads',
            width: 240,
					}
				],
				
				allData:[],

				selectRow,
				currentVersion:"",
        inputVersion:"",
        allsdxVersion:[
          {value: '25.0.0.1373', label:'API Null'},
          {value: '25.0.0.1373', label:'25.0.0.1373'},
          {value: '25.0.0.1372', label:'23.0.0.1372'},
          {value: '25.0.0.1364', label:'23.0.0.1364'},
        ],

				pluginicon,
				sdpliginService,
        sdxBackupService,
				detailDrawerVisible,
				selectItem:{},
				pluginServer:{
					pluginserver:this.GLOBAL.pluginServer,
          pluginserversdx:this.GLOBAL.pluginServer_sdx,
          pluginversion:this.GLOBAL.pluginVersion,
        },
        allVersionSite:"https://sdplugin."+this.GLOBAL.pluginServer+".com/packages/snapdevelop",
        showURI:'http://sdplugin.aipuyang.com/api/v3/product/getpackages?packageType=;SDSDK-INSTALLER;SDSDK;SDSDK-WORKLOAD;SDSDK-CORE;SDSDK-EXTENSION;SDSDK-ENVIRONMENT&TargetVersionIncludeOnly=True&version=25.0.0.1372',
        
				currentShowType:'4',
				searchInput:'',
				
				checkNormalPlugin_time:this.GLOBAL.checkNormalPlugin_time

				//pagination,//分页
			}
		},
		mounted(){
			this.getall()
			this.getAllInit()
			
			EventBus.$on('searchplugin',data=>{
				this.searchInput=data
				this.onSearch()
			})
			
			EventBus.$on('pluginServer',Server=>{
				this.pluginServer=Server
        this.currentVersion = ''
        this.allVersionSite="https://sdplugin."+this.pluginServer.pluginserver+".com/packages/snapdevelop",
				this.getproducts()
				this.getAllInit()
			})
      
			EventBus.$on('pluginVersion', pluginVersion=>{
				this.pluginServer.pluginversion = pluginVersion
        this.getproducts()
				this.getAllInit()
      })
      
			EventBus.$on('pluginServerApi',pluginServerApi=>{
				this.getproducts()
				this.getAllInit()
				//this.$message.info(pluginServerApi)
			})
			
			EventBus.$on('checkNormalPlugin_time',checkNormalPlugin_time=>{
				this.checkNormalPlugin_time=checkNormalPlugin_time
				
				//销毁定时器，重置时间
				clearInterval(this.timer);
				this.timer = null
				this.timer = setInterval(this.JudgePackageisNormal, this.checkNormalPlugin_time)
			
				this.$message.success("成功将检查时间设置为："+checkNormalPlugin_time)
			})
      
      EventBus.$on('sdxBackupData',data=>{
				
        var backupdata = eval("(" + JSON.parse(JSON.stringify(data.jsonData)) + ")") 
        console.log(backupdata)
        this.allData = backupdata
      })	
		},
		beforeUnmounte(){
			//销毁定时器
			clearInterval(this.timer);
			this.timer = null
		},
		created(){
			//计时器,判断是否存在非正常包
			this.timer = setInterval(this.JudgePackageisNormal, this.checkNormalPlugin_time)
		},
		methods:{
			
			async getAllInit(){
				var headerUri_product="/api/v3/product/getpackages?packageType=SDSDK;SDSDK-WORKLOAD;SDSDK-CORE;SDSDK-EXTENSION;SDSDK-ENVIRONMENT&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer, this.currentVersion).then((res)=>{
					this.allData=res.data
          
          for(var i=0;i<this.allData.length;i++){
  					if(this.allData[i].id==='SnapDevelop'){
  						this.currentVersion=this.allData[i].version
  						break
  					}
  				}
				}).catch(err=>{
					console.log(err.message)
				})
			},

			async getproducts(){
				var headerUri_product="/api/v3/product/getpackages?packageType=SDSDK&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer, this.currentVersion).then((res)=>{
					this.tableData=res.data
					this.tableData_Copy=res.data
					this.selectRow=res.data[0]
					this.currentVersion=this.selectRow.version
				})
			},

			async getfeatures(){
        console.log(this.currentVersion)
				var headerUri_product="/api/v3/product/getpackages?packageType=SDSDK-CORE&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer, this.currentVersion).then((res)=>{
					this.tableData=res.data
					this.tableData_Copy=res.data
				})
			},

			async getplugins(){
				var headerUri_product="/api/v3/product/getpackages?packageType=SDSDK-EXTENSION&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer, this.currentVersion).then((res)=>{
					this.tableData=res.data
					this.tableData_Copy=res.data
				})
			},

			async getenvironments(){
				var headerUri_product="/api/v3/product/getpackages?packageType=SDSDK-ENVIRONMENT&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer, this.currentVersion).then((res)=>{
					this.tableData=res.data
					this.tableData_Copy=res.data
				})
			},
      
      async getworkloads(){
				var headerUri_product="/api/v3/product/getpackages?packageType=	SDSDK-WORKLOAD&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer, this.currentVersion).then((res)=>{
					this.tableData=res.data
					this.tableData_Copy=res.data
				})
			},
      
      async getInstaller(){
				await this.sdpliginService.getInstaller(this.pluginServer).then((res)=>{
					this.tableData=res.data
					this.tableData_Copy=res.data
				})
      },
			
			async getall(){
				var headerUri_product="/api/v3/product/getpackages?packageType=;SDSDK-INSTALLER;SDSDK;SDSDK-WORKLOAD;SDSDK-CORE;SDSDK-EXTENSION;SDSDK-ENVIRONMENT&"
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer, this.currentVersion).then((res)=>{
					this.tableData=res.data
					this.tableData_Copy=res.data
				})
			},
	
			/*
			//isClock区别定时器
			*/
			async getnotNormal(isClock){
				var headerUri_product="/api/v3/product/getpackages?packageType=SDSDK;SDSDK-WORKLOAD;SDSDK-CORE;SDSDK-EXTENSION;SDSDK-ENVIRONMENT&"
				
				await this.sdpliginService.getPlugin(headerUri_product,this.pluginServer, this.currentVersion).then((res)=>{
					if(!isClock){
						this.tableData=[]
					}
					this.tableData_Copy=res.data
				})
				
				for(var i=0;i<this.tableData_Copy.length;i++){
					if(this.tableData_Copy[i].id==='SnapDevelop'){
						this.currentVersion=this.tableData_Copy[i].version
						break
					}
				}
				
        this.notNormalList=[]
				this.tableData_Copy.forEach(element => {
					if(element.version!=this.currentVersion){
						if(!isClock){
							this.tableData.push(element)
						}else{
							this.notNormalList.push(element)
						}
					}
				});
				//this.$message.info(this.currentVersion)
				//this.tableData_Copy=res.data
			},

			pluginTypeSwitch(type){
				switch(type){
					case 0:
						this.getproducts()
						this.currentShowType='0'
						break;
					case 1:
						this.getfeatures()
						this.currentShowType='1'
						break;
					case 2:
						this.getplugins()
						this.currentShowType='2'
						break;
					case 3:
						this.getenvironments()
						this.currentShowType='3'
						break;
					case 4:
						this.getall()
						this.currentShowType='4'
						break;
					case 5:
						this.getnotNormal(false)
						this.currentShowType='5'
						break;
          case 6:
            this.getworkloads()
						this.currentShowType='6'
						break;
          case 7:
            this.getInstaller()
            this.currentShowType='7'
						break;
				}
			},

			viewMore(raw){
				this.detailDrawerVisible=true
				this.selectRow=this.tableData[0]
			},
			
			tableRowClick(record,index){
				return{
					on:{
						click:()=>{
							//传递事件总线参数
							EventBus.$emit('pluginDetailVisible',true)
							EventBus.$emit('plugin',record)
							EventBus.$emit('allPlugin',this.allData)
						}
					}	
				}
				
			},
			
			change(){
				this.pluginServer=this.pluginServer==='devmagic'?'aipuyang':'devmagic'
				switch(this.currentShowType){
					case '0':
						this.pluginicon = (this.pluginServer==='devmagic' || this.pluginServer==='devmagic-release')?
                                'images/logos/DevMagic_Studio.png':'images/logos/snapdevelop.png';
						this.getproducts()
						break;
					case '1':
						this.pluginicon='images/logos/nuget.png';
						this.getfeatures()
						currentShowType='1'
						break;
					case '2':
						this.pluginicon='images/logos/sdx.icon';
						this.getplugins()
						currentShowType='2'
						break;
					case '3':
						this.pluginicon='images/logos/logo-google-drive.svg';
						this.getenvironments()
						currentShowType='3'
						break;
					case '4':
						this.pluginicon='images/logos/logo-slack.svg';
						this.getall()
						currentShowType='4'
						break;
					case '5':
						this.pluginicon='images/logos/logo-slack.svg';
						this.getnotNormal()
						currentShowType='5'
						break;
				}

				this.GLOBAL.pluginServer=this.pluginServer
			},
      
      versionChange(val){
        this.currentVersion = val
        this.pluginTypeSwitch(4)
      },
      inputVersionChange(){
				this.currentVersion = this.inputVersion
        this.pluginTypeSwitch(4)
      },
			
			logoType(type){
				var logoString;
				switch(type){
					case 'SDSDK':
						logoString= (this.pluginServer.pluginserver ==='devmagic' || this.pluginServer.pluginserver ==='devmagic-release')?
                                'images/logos/DevMagic_Studio.png':'images/logos/snapdevelop.png';
						break;
					case 'SDSDK-CORE':
						logoString= 'images/logos/sdx_core.png';
						break;
					case 'SDSDK-EXTENSION':
						logoString= 'images/logos/sdx_ext.png';
						break;
					case 'SDSDK-ENVIRONMENT':
						logoString= 'images/logos/sdx_env.png';
						break;
          case 'SDSDK-INSTALLER':
						logoString= 'images/logos/DevMagic_Studio_Installer.png';
						break;
					default:
						logoString= 'images/logos/logo-jira.svg';
						break;
				}
				return logoString
			},
			
			onSearch(){
				var searchTable=[]
				this.tableData_Copy.forEach(element => {
//					if(element.id.toLowerCase().indexOf(this.searchInput.toLowerCase())!=-1){
//						searchTable.push(element)
//					}
          if(element.title.toLowerCase().indexOf(this.searchInput.toLowerCase())!=-1){
						searchTable.push(element)
					}else if(element.description.toLowerCase().indexOf(this.searchInput.toLowerCase())!=-1){
						searchTable.push(element)
					}else if(element.version.indexOf(this.searchInput)!=-1){
						searchTable.push(element)
					}
				});

				this.tableData=searchTable
				
			},

			getPluginByID(value){
				var plugin
				for (let index = 0; index < this.allData.length; index++) {
					const element = this.allData[index];
					if(value===this.allData[index].id){
						plugin=this.allData[index]
						break;
					}
				}

				return plugin
			},

			calculateSize(value){
				//var num=value/(1024*1024)
				return (value/(1024*1024)).toFixed(2)
			},
			
			JudgePackageisNormal(){
				this.getnotNormal(true)
				var message="存在 "+this.notNormalList.length+" 个不正常的包！"
				var des="点击前往查看"
				
				if(this.notNormalList.length!=0){
					
					notification.open({
						type:'warning',
						message:message,
						description:des,
						duration:5,
						onClick:()=>{
							this.$router.push('/')
							this.pluginTypeSwitch(5)
						}, 
						style:{
							width:'500px',
							marginLeft:`${335-500}px`,
						}
					})
				}
			},
			
			timeFormate(publishtime){
			var day=(publishtime || '').split('T')
      
      var hms = ((day[1] ||'').split('.')[0] || '').split(':')
      if((parseInt(hms[0])+8)<10){
				var _h = '0'+ (parseInt(hms[0])+8)
      }else if((parseInt(hms[0])+8)>24){
				var _h = (parseInt(hms[0])+8)-24
      }else{
				var _h = parseInt(hms[0])+8
      }
      
      return day[0] + ' ' + _h + ':' + hms[1] + ':' + hms[2]
      //return day[0]+' ' + (day[1] ||'').split('.')[0]
      },
      
      generateDeleteCommand()
      {
        var deleteCommand=[]
  			//var templatecommand="dotnet nuget delete xxxName xxxVersion -s xxxURL -k xxxKey --non-interactive"
        var templatecommand="dotnet nuget delete xxxName 22.0.0.1857 -s http://sdpluginvico.devmagic.com/ -k SAME98A2-CC36-4B0E-8E3D-F8996B227A2D --non-interactive"
        //templatecommand = templatecommand.replace("xxxVersion",this.currentVersion)
        
        this.allData.forEach(item=>{
          var templatecommand_ver = templatecommand
          var templatecommand_ver = templatecommand_ver.replace('xxxName',item.id)
          
          deleteCommand.push(templatecommand_ver)
        })
        
        var deleteString = deleteCommand.toString().replaceAll(",","\r\n")
        this.$copyText(deleteString).then(()=>{
  				this.$message.success("删除命令已复制到粘贴板！")
        })
      },
      
      InfoBackup()
      {
        var _date = new Date()
				var newJsonData = {
          build: this.currentVersion,
          version: "23",
          isKeyversion: false,
          keyversion: "-",
          remark: _date.getFullYear()+'/'+ (_date.getMonth()+1).toString() + '/'+_date.getDate() + " 备份版本",
          offlineInstaller:"",
          jsondata: this.tableData_Copy
        }
        EventBus.$emit('addbackupPluginInfo',newJsonData)
      },
      
		}
	})

</script>