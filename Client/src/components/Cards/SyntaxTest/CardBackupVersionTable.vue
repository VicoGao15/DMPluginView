<template>
				
	<!-- Projects Table Card -->
	<a-card :bordered="false" class="header-solid h-full" :bodyStyle="{padding: 0,}">
		<template #title>
			<a-row type="flex" align="middle">
				<a-col :span="24" :md="4">
					<a-select
          show-search
          :allowClear="true"
          :filterOption="filterOption"
					v-model="selectBackupVersionId"
					style="width:250px"
					@change="searchBackupVersionInfo()"
					>
						<a-select-option v-for="item in allBackupVersion" :key="item.value" :value="item.value">{{item.text}}</a-select-option>
					</a-select>
				</a-col>
        <a-col :span="24" :md="10">
					<span>备注：{{selectBackupVersionRemark}}</span>
				</a-col>
				<a-col :span="24" :md="10" style="display: flex; align-items: center; justify-content: flex-end">
					<a-radio-group v-model="projectHeaderBtns" size="small">
            <a-radio-button value="all" @click="pluginTypeSwitch(4)"><a-icon type="slack" style="color:green;font-size:15px;"></a-icon>&nbsp;&nbsp;&nbsp;所有包</a-radio-button>
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
    <div>
		  <a-button type="dasked" size="small" @click="deleteBackupVersion()">删除备份版本</a-button>
      <a-button type="dasked" size="small" style="margin-left:10px;" @click="editBackupInfo()">编辑信息</a-button>
      <a style="margin-left:10px;" v-show="selectBackupVersionOfflineInstaller!=''" :href="selectBackupVersionOfflineInstaller">离线安装</a>
    </div>
    <p></p>
    <EditBackupPluginInfoConfirm></EditBackupPluginInfoConfirm>
    </a-card>
	<!-- / Projects Table Card -->

</template>
		
<script>
import PluginDetailDrawer from '../Drawer/PluginDetailDrawer.vue'
import EditBackupPluginInfoConfirm from '../Dialog/EditBackupPluginInfoConfirm.vue'
import SdxBackupService from '../service/GetSdxBackupService';
import {EventBus} from '../../event-bus.js'
import {notification} from 'ant-design-vue'
import GLOBAL from '../../Global.vue'
  
import { myNotification } from '../../mixin/notification'

	export default {
    mixins:[myNotification],
    props:{},
		components:{
			PluginDetailDrawer,
      EditBackupPluginInfoConfirm
		},
		data() {
			
			var selectRow

      const sdxbackupService = new SdxBackupService()
			var detailDrawerVisible=false
			
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
          showTotal:total => `共 ${total} 条数据`
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
						width: 40,
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
					},
					{
						title: '上传时间',
						dataIndex: 'published',
						scopedSlots: { customRender: 'publishTime' },
						class: 'font-bold text-muted text-sm',
					},
					{
						title: 'zip包大小',
						dataIndex: 'zipBytes',
						scopedSlots: { customRender: 'zipSpace' },
						class: 'font-bold text-muted text-sm',
					}
				],
				
				allData:[],

				selectRow,
				currentVersion:"",

        sdxbackupService,
				detailDrawerVisible,
				selectItem:{},
				pluginServer:{
					pluginserver:this.GLOBAL.pluginServer,
          pluginserversdx:this.GLOBAL.pluginServer_sdx,
          pluginversion:this.GLOBAL.pluginVersion,
        },
        
				currentShowType:'4',
				searchInput:'',
				
				checkNormalPlugin_time:this.GLOBAL.checkNormalPlugin_time,

        //****backup***
        //数据库备份版本
        selectBackup:{},
				selectBackupVersionId:'',
        selectBackupVersionRemark:'',
        selectBackupVersionOfflineInstaller:'',
        allBackupVersion:[],
        
			}
		},
		mounted(){
      this.dataFromBackupInit()
			
			EventBus.$on('searchplugin',data=>{
				this.searchInput=data
				this.onSearch()
			})
      
      EventBus.$on('sdxBackupData',data=>{
        var backupdata = eval("(" + JSON.parse(JSON.stringify(data.jsonData)) + ")") 
        this.allData = backupdata
      })	
      
      EventBus.$on('reloadBackupVersionInfo',()=>{
				this.dataFromBackupInit()
      })
		},
		beforeUnmounte(){},
		created(){},
		methods:{

			getPluginByType(type){
				for(var i=0;i<this.tableData_Copy.length;i++){
  				if(this.tableData_Copy[i].packageType==type){
  					this.tableData.push(this.tableData_Copy[i])
          }	
        }
			},

			/*
			//
			*/
			getnotNormal(isClock){
				this.tableData = []
				for(var i=0;i<this.tableData_Copy.length;i++){
					if(this.tableData_Copy[i].id==='SnapDevelop'){
						this.currentVersion=this.tableData_Copy[i].version
						break
					}
				}
				
				this.tableData_Copy.forEach(element => {
					if(element.version!=this.currentVersion){
						this.tableData.push(element)
					}
				});
				//this.$message.info(this.currentVersion)
			},

      
			pluginTypeSwitch(type){
        this.tableData=[]
				switch(type){
					case 0:
						//this.getproducts()
            this.getPluginByType('SDSDK')
						this.currentShowType='0'
						break;
					case 1:
						this.getPluginByType('SDSDK-CORE')
						this.currentShowType='1'
						break;
					case 2:
						this.getPluginByType('SDSDK-EXTENSION')
						this.currentShowType='2'
						break;
					case 3:
						this.getPluginByType('SDSDK-ENVIRONMENT')
						this.currentShowType='3'
						break;
					case 4:
						//this.getall()
            this.tableData = this.tableData_Copy
						this.currentShowType='4'
						break;
					case 5:
						this.getnotNormal(false)
						this.currentShowType='5'
						break;
          case 6:
            this.getPluginByType('SDSDK-WORKLOAD')
						this.currentShowType='6'
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
      
      deleteBackupVersion()
      {
        if(this.selectBackup.isKeyVersion){
          this.myNotification_info_withTitle(this.selectBackup.build,'关键版本不允许删除！')
					return
        }else{
					this.sdxbackupService.deleteBackupVersionJsonData(this.selectBackup.id).then(res=>{
						if(res.data){
              this.myNotification_success_withTitle(this.selectBackup.build,' 删除成功!')
              //页面数据重新加载
              this.dataFromBackupInit()
            }
            else{
              this.myNotification_error_withTitle(this.selectBackup.version,'删除失败!')
            }
          })
        }
      },
      
      dataFromBackupInit(){
        this.allBackupVersion = []
				this.sdxbackupService.getAllBackupVersion().then(res=>{
          //版本排序
          res.data = res.data.sort(function(a,b){
          	return b.build.localeCompare(a.build)
          })
					res.data.forEach(element=>{
            var optiontest = ''
            if(element.isKeyVersion){ optiontest = element.build + ' - ' + element.keyVersion }
            else{ optiontest = element.build }
            if(element.offlineinstaller!=''){ optiontest = optiontest + ' * ' }//有离线安装中下载链接的标*
            
						var allBackupVersionOption={
							value:element.id,
							text:optiontest
						}
						this.allBackupVersion.push(allBackupVersionOption)
					})
					this.selectBackupVersionId = this.allBackupVersion[0].value
          
          this.searchBackupVersionInfo()
				})
			},
      
      searchBackupVersionInfo(){
        
				this.sdxbackupService.getBackupVersionJsonDataById(this.selectBackupVersionId).then(res=>{
					if(res.data){
            this.selectBackup = res.data
						var backupdata = eval("(" + JSON.parse(JSON.stringify(res.data.jsonData)) + ")") 
            
            this.selectBackupVersionRemark = res.data.remark
            this.selectBackupVersionOfflineInstaller = res.data.offlineinstaller
            this.tableData=backupdata
					  this.tableData_Copy=backupdata
					}
				})
			},
      
      //编辑
      editBackupInfo(){
				EventBus.$emit('editbackupPluginInfo',this.selectBackup)
      },
      
      //根据input过滤版本
      filterOption(input,option){
				return (
          option.componentOptions.children[0].text.toLowerCase().indexOf(input.toLowerCase()) >= 0
        )
      },
      
		}
	}

</script>