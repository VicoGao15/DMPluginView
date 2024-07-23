<template>
  <div>
			
      <a-drawer
			placement="right"
			:closable="false"
			:visible="detailDrawerVisible"
			:width="600"
			@close="onClodeDrawer"
		>
      <a-tabs v-model="activeKey">
				<a-tab-pane key="1" tab="查看处理数据">
			<div class="drawer-content">
				<img style="margin-top:-8px;" :src="logoType(selectPlugin.packageType)" width="23" class="mr-10">
				<span style="font-size:20px;margin-top:20px;"><b>{{selectPlugin.title}}</b></span>
        <a-tooltip title="Copy">
          <a-icon type="copy" style="margin-left:10px;" theme="twoTone" two-tone-color="#52c41a" @click="doCopy(selectPlugin.title)"></a-icon>
        </a-tooltip>
        <p></p>
        <span class="drawer_des">{{selectPlugin.description}}</span>
        <a-tooltip title="Copy">
          <a-icon type="copy" style="margin-left:10px;" theme="twoTone" two-tone-color="#52c41a" @click="doCopy(selectPlugin.description)"></a-icon>
        </a-tooltip>
        <a-divider/>


				<a-descriptions :column="1" bordered size="small">
					<a-descriptions-item label="id">
						{{selectPlugin.id}}
            <a-tooltip title="Copy">
							<a-icon type="copy" style="margin-left:10px;" theme="twoTone" two-tone-color="#52c41a" @click="doCopy(selectPlugin.id)"></a-icon>
            </a-tooltip>
            
					</a-descriptions-item>
					<a-descriptions-item label="version">
						{{selectPlugin.version}}
					</a-descriptions-item>
					<a-descriptions-item label="packageType">
						{{selectPlugin.packageType}}
					</a-descriptions-item>
					
					<a-descriptions-item label="releaseNotes">
						{{selectPlugin.releaseNotes}}
					</a-descriptions-item>
					<a-descriptions-item label="isRequireLicenseAcceptance">
						{{selectPlugin.isRequireLicenseAcceptance}}
					</a-descriptions-item>
					<a-descriptions-item label="hasLicense">
						{{selectPlugin.hasLicense}}
					</a-descriptions-item>
					<a-descriptions-item label="license">
						{{selectPlugin.license}}
					</a-descriptions-item>
					<a-descriptions-item label="licenseUrl">
						{{selectPlugin.licenseUrl}}
					</a-descriptions-item>
					<a-descriptions-item label="md5">
						{{selectPlugin.md5}}
					</a-descriptions-item>
					<a-descriptions-item label="installBytes">
						{{selectPlugin.installBytes}}
					</a-descriptions-item>
					<a-descriptions-item label="zipBytes">
						{{selectPlugin.zipBytes}}
					</a-descriptions-item>
					
					<a-descriptions-item label="published">
						{{timeFormate(selectPlugin.published)}}
					</a-descriptions-item>
					<a-descriptions-item label="kind">
						{{selectPlugin.kind}}
					</a-descriptions-item>
					<a-descriptions-item label="tags">
						<a-tag color="cyan" v-for="(item,id) in selectPlugin.tags" :key="id">
							{{item}}
						</a-tag>
						
					</a-descriptions-item>
					<a-descriptions-item label="authors">
						{{selectPlugin.authors}}
					</a-descriptions-item>
					<a-descriptions-item label="minClientVersion">
						{{selectPlugin.minClientVersion}}
					</a-descriptions-item>
					<a-descriptions-item label="compatibleVersion">
						{{selectPlugin.compatibleVersion}}
					</a-descriptions-item>
					
					<a-descriptions-item label="summary">
						{{selectPlugin.summary}}
					</a-descriptions-item>
					<a-descriptions-item label="downloads">
						{{selectPlugin.downloads}}
					</a-descriptions-item>
					<a-descriptions-item label="isPrerelease">
						{{selectPlugin.isPrerelease}}
					</a-descriptions-item>
					<a-descriptions-item label="hasEmbeddedIcon">
						{{selectPlugin.hasEmbeddedIcon}}
					</a-descriptions-item>
					<a-descriptions-item label="hasReadme">
						{{selectPlugin.hasReadme}}
					</a-descriptions-item>
					<a-descriptions-item label="envDependencies">
						<div v-for="(item,id) in selectPlugin.envDependencies" :key="id">
							<a-tag color="blue" @click="tagViewDetail(item.id)">
								{{item.id}}
							</a-tag>	
						</div>
					</a-descriptions-item>
					
					<a-descriptions-item label="preDependencies">
						<div v-for="(item,id) in selectPlugin.preDependencies" :key="id">
							<a-tag color="pink" @click="tagViewDetail(item.id)">
								{{item.id}}
							</a-tag>	
						</div>
					</a-descriptions-item>
					<a-descriptions-item label="includePackages">
						<div v-for="(item,id) in selectPlugin.includePackages" :key="id">
							<a-tag color="green" v-if="tagIsExist(item.id)" @click="tagViewDetail(item.id)">
								<!-- {{getPluginByID(item.id).title}} -->
								{{item.id}}
							</a-tag>
							<a-tag color="red" v-else @click="tagViewDetail(item.id)">
								<!-- <template #icon>
									<close-circle-outlined />
								</template> -->
								{{item.id}}
							</a-tag>	
						</div>
					</a-descriptions-item>
          <!--<a-descriptions-item label="all version">
						<div v-for="(item,id) in selectPluginAllVersion" :key="id">
              <a-tooltip>
                <template #title>
									<span>publisedTime:{{timeFormate(item.publisedTime)}}</span>
                </template>
								<a-tag color="blue" @click="tagViewDetail_Version(item.id,item.version)">{{item.version}} </a-tag><a-icon type="star" style="color:#eb2f96" v-if="item.version===selectPlugin.version"></a-icon>
              </a-tooltip>
						</div>
            <a-button type="dasked" size="small" @click="generateDeleteCommand()">生成删除命令</a-button>
          </a-descriptions-item>-->
				</a-descriptions>
			</div>
        </a-tab-pane>
        <a-tab-pane key="2" tab="查看完整Json数据">
					<div>
          <json-viewer 
            :value="selectPlugin"
            :expand-depth="5"
            copyable
            boxed
            expanded="true"
            >
          </json-viewer>
        </div>
        </a-tab-pane>
        <a-tab-pane key="3" tab="所有版本">
					<div>
            <a-tabs v-model="activeKey_version">
              <a-tab-pane key="1" tab="Tag">
                <a-button type="dasked" size="small" @click="generateDeleteCommand()">生成删除命令</a-button>
                <a-descriptions :column="1" bordered size="small">
                  <a-descriptions-item label="all version">
                    <div v-for="(item,id) in selectPluginAllVersion" :key="id">
                      <a-tooltip>
                        <template #title>
                    			<span>publisedTime:{{timeFormate(item.publisedTime)}}</span>
                        </template>
                    		<a-tag color="blue" @click="tagViewDetail_Version(item.id,item.version)">{{item.version}} </a-tag><a-icon type="star" style="color:#eb2f96" v-if="item.version===selectPlugin.version"></a-icon>
                      </a-tooltip>
                    </div>
                  </a-descriptions-item>
                </a-descriptions>
              </a-tab-pane>
              <a-tab-pane key="2" tab="Json">
                <json-viewer 
                  :value="JsonSort(selectPluginAllVersion,'version')"
                  :expand-depth="5"
                  copyable
                  boxed
                  expanded="true"
                  sort
                  >
                </json-viewer>
              </a-tab-pane>
            </a-tabs>
          
        </div>
        </a-tab-pane>
      </a-tabs>
      
		</a-drawer>
    
    <a-drawer
			placement="left"
			:closable="false"
			:visible="sonplugin_detailDrawerVisible"
			:width="600"
			@close="onClodeDrawer_son"
		>
    <a-tabs v-model="activeKey_Son">
			<a-tab-pane key="1" tab="查看处理数据">
			<div class="drawer-content">
				<img style="margin-top:-8px;" :src="logoType(selectPlugin_son.packageType)" width="23" class="mr-10">
				<span style="font-size:20px;margin-top:20px;"><b>{{selectPlugin_son.title}}</b></span>
        <a-tooltip title="Copy">
          <a-icon type="copy" style="margin-left:10px;" theme="twoTone" two-tone-color="#52c41a" @click="doCopy(selectPlugin_son.title)"></a-icon>
        </a-tooltip>
				<p></p>
				<span class="drawer_des">{{selectPlugin_son.description}}</span>
        <a-tooltip title="Copy">
          <a-icon type="copy" style="margin-left:10px;" theme="twoTone" two-tone-color="#52c41a" @click="doCopy(selectPlugin_son.description)"></a-icon>
        </a-tooltip>
				<a-divider/>


				<a-descriptions :column="1" bordered size="small">
					<a-descriptions-item label="id">
						{{selectPlugin_son.id}}
            <a-tooltip title="Copy">
							<a-icon type="copy" style="margin-left:10px;" theme="twoTone" two-tone-color="#52c41a" @click="doCopy(selectPlugin_son.id)"></a-icon>
            </a-tooltip>
					</a-descriptions-item>
					<a-descriptions-item label="version">
						{{selectPlugin_son.version}}
					</a-descriptions-item>
					<a-descriptions-item label="packageType">
						{{selectPlugin_son.packageType}}
					</a-descriptions-item>
					
					<a-descriptions-item label="releaseNotes">
						{{selectPlugin_son.releaseNotes}}
					</a-descriptions-item>
					<a-descriptions-item label="isRequireLicenseAcceptance">
						{{selectPlugin_son.isRequireLicenseAcceptance}}
					</a-descriptions-item>
					<a-descriptions-item label="hasLicense">
						{{selectPlugin_son.hasLicense}}
					</a-descriptions-item>
					<a-descriptions-item label="license">
						{{selectPlugin_son.license}}
					</a-descriptions-item>
					<a-descriptions-item label="licenseUrl">
						{{selectPlugin_son.licenseUrl}}
					</a-descriptions-item>
					<a-descriptions-item label="md5">
						{{selectPlugin_son.md5}}
					</a-descriptions-item>
					<a-descriptions-item label="installBytes">
						{{selectPlugin_son.installBytes}}
					</a-descriptions-item>
					<a-descriptions-item label="zipBytes">
						{{selectPlugin_son.zipBytes}}
					</a-descriptions-item>
					
					<a-descriptions-item label="published">
						{{selectPlugin_son.published}}
					</a-descriptions-item>
					<a-descriptions-item label="kind">
						{{selectPlugin_son.kind}}
					</a-descriptions-item>
					<a-descriptions-item label="tags">
						<a-tag color="cyan" v-for="(item,id) in selectPlugin_son.tags" :key="id">
							{{item}}
						</a-tag>
						
					</a-descriptions-item>
					<a-descriptions-item label="authors">
						{{selectPlugin_son.authors}}
					</a-descriptions-item>
					<a-descriptions-item label="minClientVersion">
						{{selectPlugin_son.minClientVersion}}
					</a-descriptions-item>
					<a-descriptions-item label="compatibleVersion">
						{{selectPlugin_son.compatibleVersion}}
					</a-descriptions-item>
					
					<a-descriptions-item label="summary">
						{{selectPlugin_son.summary}}
					</a-descriptions-item>
					<a-descriptions-item label="downloads">
						{{selectPlugin_son.downloads}}
					</a-descriptions-item>
					<a-descriptions-item label="isPrerelease">
						{{selectPlugin_son.isPrerelease}}
					</a-descriptions-item>
					<a-descriptions-item label="hasEmbeddedIcon">
						{{selectPlugin_son.hasEmbeddedIcon}}
					</a-descriptions-item>
					<a-descriptions-item label="hasReadme">
						{{selectPlugin_son.hasReadme}}
					</a-descriptions-item>
					<a-descriptions-item label="envDependencies">
						<div v-for="(item,id) in selectPlugin_son.envDependencies" :key="id">
							<a-tag color="blue" @click="tagViewDetail_sonDrawer(item.id)">
								{{item.id}}
							</a-tag>	
						</div>
					</a-descriptions-item>
					
					<a-descriptions-item label="preDependencies">
						<div v-for="(item,id) in selectPlugin_son.preDependencies" :key="id">
							<a-tag color="pink" @click="tagViewDetail_sonDrawer(item.id)">
								{{item.id}}
							</a-tag>	
						</div>
					</a-descriptions-item>
					<a-descriptions-item label="includePackages">
						<div v-for="(item,id) in selectPlugin_son.includePackages" :key="id">
							<a-tag color="green" v-if="tagIsExist(item.id)" @click="tagViewDetail_sonDrawer(item.id)">
								{{item.id}}
							</a-tag>
							<a-tag color="red" v-else>
								{{item.id}}
							</a-tag>	
						</div>
					</a-descriptions-item>
				</a-descriptions>
			</div>
      </a-tab-pane>
      <a-tab-pane  key="2" tab="查看完整Json数据">
				<div>
  				<json-viewer 
            :value="selectPlugin_son"
            :expand-depth="5"
            copyable
            boxed
            expanded="true"
            >
          </json-viewer>
        </div>
      </a-tab-pane>
      <a-tab-pane key="3" tab="所有版本">
				<div>
        <json-viewer 
          :value="JsonSort(selectPlugin_sonAllVersion,'version')"
          :expand-depth="5"
          copyable
          boxed
          expanded="true"
          sort
          >
        </json-viewer>
      </div>
      </a-tab-pane>
    </a-tabs>
      
		</a-drawer>
  </div>
</template>

<script>
import {EventBus} from '../../event-bus.js'
import {CopyOutlined} from '@ant-design/icons-vue'
import SdpliginService from '../service/GetSDPluginService'
import JsonViewer from 'vue-json-viewer'

export default {
  props:{
	  //detailDrawerVisible
  },
  components:{
		CopyOutlined,
    JsonViewer
  },
  data(){
	  
    return{
      //pluginServer:'devmagic',
      pluginServer:{
				pluginserver:this.GLOBAL.pluginServer,
        pluginserversdx:this.GLOBAL.pluginServer_sdx
      },
      sdpliginService:new SdpliginService(),		//接口服务
		  detailDrawerVisible:this.GLOBAL.detailDrawerVisible,
		  //selectPlugin:this.GLOBAL.selectPlugin,
      allData:[],
      
      selectPlugin:{},
      selectPluginAllVersion:[],
      detailDrawerVisible:false,
      
      activeKey:'1',
      activeKey_Son:'1',
      activeKey_version:'1',
      
      
      selectPlugin_son:{},
      selectPlugin_sonAllVersion:[],
      sonplugin_detailDrawerVisible:false
    }
  },
  
  mounted(){
		//事件总线
		EventBus.$on('pluginDetailVisible',visible=>{
			this.detailDrawerVisible=visible
		})
    EventBus.$on('plugin',data=>{
			this.selectPlugin=data
      this.getAllVersionById(this.selectPlugin.id,false)
		})
    EventBus.$on('allPlugin',data=>{
			this.allData=data
		})
    EventBus.$on('pluginServer',pluginServer=>{
			this.pluginServer=pluginServer
    })
  },
  
  methods:{
		onClodeDrawer(){
			this.detailDrawerVisible=false
		},
    onClodeDrawer_son(){
			this.sonplugin_detailDrawerVisible=false
		},
    
    async getAllVersionById(id, isSonDrawer){
      await this.sdpliginService.getallExtentiomVersionById(this.pluginServer, id).then((res)=>{
        if(isSonDrawer){
					this.selectPlugin_sonAllVersion=res.data
        }else{
					this.selectPluginAllVersion=res.data
        }
      })
      
    },
    
    tagViewDetail(value){
			var isExist=false
			for (let index = 0; index < this.allData.length; index++) {
				if(value===this.allData[index].id){
          this.getAllVersionById(value, true)
          
          this.sonplugin_detailDrawerVisible=true
          this.selectPlugin_son=this.allData[index]

					isExist=true
					break;
				}
			}

			if(!isExist){
				this.$message.error("插件不存在："+value);
			}

			return isExist
		},
    
    async tagViewDetail_Version(id,version){
      await this.sdpliginService.getPluginById_Version(this.pluginServer, id, version).then((res)=>{
        this.selectPlugin_son=res.data[0]
				this.sonplugin_detailDrawerVisible=true
        
        //console.log(this.selectPlugin_son)
      })
    },
    
    tagViewDetail_sonDrawer(value){
			var isExist=false
			for (let index = 0; index < this.allData.length; index++) {
				if(value===this.allData[index].id){
					this.sonplugin_detailDrawerVisible=false
					this.selectPlugin_son=this.allData[index]
					this.sonplugin_detailDrawerVisible=true

					isExist=true
					break;
				}
			}

			if(!isExist){
				this.$message.error("插件不存在："+value);
			}

			return isExist
		},
    
    tagIsExist(value){
			var isExist=false
			for (let index = 0; index < this.allData.length; index++) {
				const element = this.allData[index];
				if(value===this.allData[index].id){
					isExist=true
					break;
				}
			}

			return isExist
		},
    
    logoType(type){
			var logoString;
			switch(type){
				case 'SDSDK':
					logoString= 'images/logos/DevMagic_Studio.png';
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
    
    timeFormate(publishtime){
			var day=(publishtime || '').split('T')
      
      return day[0]+' ' + (day[1] || '').split('.')[0]
    },
    
    generateDeleteCommand()
    {
      var deleteCommand=[]
			//var templatecommand="dotnet nuget delete xxxName xxxVersion -s xxxURL -k xxxKey --non-interactive"
      var templatecommand="dotnet nuget delete xxxName xxxVersion -s https://sdplugin.devmagic.com/ -k AD6F1047-E641-4FB4-890F-17DE088FD646 --non-interactive"
      templatecommand = templatecommand.replace("xxxName",this.selectPlugin.id)
      console.log(this.selectPluginAllVersion)
      
      this.selectPluginAllVersion.forEach(item=>{
        var templatecommand_ver = templatecommand
        var templatecommand_ver = templatecommand_ver.replace('xxxVersion',item.version)
        console.log(item.version)
        
        deleteCommand.push(templatecommand_ver)
      })
      
      var deleteString = deleteCommand.toString().replaceAll(",","\r\n")
      this.$copyText(deleteString).then(()=>{
				this.$message.success("删除命令已复制到粘贴板！")
      })
    },
    
    /*
    @function JsonSort 对json排序
    @param json 用来排序的json
    @param key 排序的键值
    */
    JsonSort(json,key){
      //console.log(json);
      for(var j=1,jl=json.length;j < jl;j++){
        var temp = json[j],
        val = temp[key],
        i = j-1;
        while(i >=0 && json[i][key]<val){
          json[i+1] = json[i];
          i = i-1;
        }
        json[i+1] = temp;
      }
      //console.log(json);
      return json;
    }

  }
}
</script>

<style>
.drawer_des{
  /*color:blue;*/
	font-weight: bold; 
	font-size: 15px;
}
</style>