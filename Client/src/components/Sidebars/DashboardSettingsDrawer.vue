<template>

	<!-- Settings Drawer -->
	<a-drawer
		class="settings-drawer"
		:class="[ rtl ? 'settings-drawer-rtl' : '' ]"
		:placement="rtl ? 'left' : 'right'"
		:closable="false"
		:visible="showSettingsDrawer"
		width="400"
		:getContainer="() => wrapper"
		@close="$emit('toggleSettingsDrawer', false)"
	>

		<!-- Settings Drawer Close Button -->
		<a-button type="link" class="btn-close" @click="$emit('toggleSettingsDrawer', false)">
			<svg xmlns="http://www.w3.org/2000/svg" width="9" height="9" viewBox="0 0 9 9">
				<g id="close" transform="translate(0.75 0.75)">
					<path id="Path" d="M7.5,0,0,7.5" fill="none" stroke="#000" stroke-linecap="round" stroke-linejoin="round" stroke-miterlimit="10" stroke-width="1.5"/>
					<path id="Path-2" data-name="Path" d="M0,0,7.5,7.5" fill="none" stroke="#000" stroke-linecap="round" stroke-linejoin="round" stroke-miterlimit="10" stroke-width="1.5"/>
				</g>
			</svg>
		</a-button>
		<!-- / Settings Drawer Close Button -->
		
		<!-- Settings Drawer Content -->
		<div class="drawer-content">
			<h6>设置</h6>
			<p>See our dashboard options.</p>
			<hr>
			<div class="sidebar-color">
				<h6>Sidebar Color</h6>
				<a-radio-group v-model="sidebarColorModel" @change="$emit('updateSidebarColor', $event.target.value)" defaultValue="primary">
					<a-radio-button value="primary" class="bg-primary"></a-radio-button>
					<a-radio-button value="secondary" class="bg-secondary"></a-radio-button>
					<a-radio-button value="success" class="bg-success"></a-radio-button>
					<a-radio-button value="danger" class="bg-danger"></a-radio-button>
					<a-radio-button value="warning" class="bg-warning"></a-radio-button>
					<a-radio-button value="black" class="bg-dark"></a-radio-button>
				</a-radio-group>
			</div>
			<div class="sidenav-type">
				<h6>Sidenav Type</h6>
				<p>选择不同的sidenav类型</p>
				<a-radio-group button-style="solid" v-model="sidebarThemeModel" @change="$emit('updateSidebarTheme', $event.target.value)" defaultValue="primary">
					<a-radio-button style="width:110px;" value="light">TRANSPARENT</a-radio-button>
					<a-radio-button style="width:100px;" value="white">WHITE</a-radio-button>
					<a-radio-button style="width:100px;" value="dark">DARK</a-radio-button>
				</a-radio-group>
			</div>
			<p></p>
			<hr>
				<div class="sidenav-type">
				<h6>数据来源</h6>
				<p>从SDX服务器接口获取最新版本或者从数据库获取备份版本信息</p>
				<a-radio-group button-style="solid" v-model="dataFrom" @change="changeDataFrom()" defaultValue="primary">
					<a-radio-button style="width:110px;" value="sdx">SDX</a-radio-button>
					<a-radio-button style="width:100px;" value="backup">数据库备份</a-radio-button>
				</a-radio-group>
			</div>
			<p></p>
			<hr>
			<div v-if="dataFrom=='sdx'">
				<div class="sidenav-type">
					<p><b>插件服务器</b></p>
					<p></p>
					<a-select
					:default-value="{key:'sdplugin'}"
					v-model="pluginServer_sdx"
					style="width:140px"
					@change="pluginServerChange()"
					>
						<a-select-option value="sdplugin">sdplugin</a-select-option>
						<a-select-option value="sdplugin-release">sdplugin-release</a-select-option>
					</a-select>.
					<a-select
					:default-value="{key:'aipuyang'}"
					v-model="pluginServer"
					style="width:130px"
					@change="pluginServerChange()"
					>
						<a-select-option value="devmagic">devmagic</a-select-option>
						<a-select-option value="aipuyang">aipuyang</a-select-option>
					</a-select>.com
					<!--<a-radio-group button-style="solid" v-model="pluginServer_sdx" @change="pluginServerChange()" defaultValue="primary">
						<a-radio-button value="sdplugin">sdplugin</a-radio-button>
						<a-radio-button value="sdpluginvico">sdpluginvico</a-radio-button>
					</a-radio-group>-->
					<!--<p></p>
					<p>选择服务器类型：</p>-->
					<!--<a-radio-group button-style="solid" v-model="pluginServer" @change="pluginServerChange()" defaultValue="primary">
						<a-radio-button value="aipuyang">中文版本(aipuyang)</a-radio-button>
						<a-radio-button value="devmagic">英文版本(devmagic)</a-radio-button>
					</a-radio-group>-->
					<!--<a-button @click="settingOK()">OK</a-button>-->
				</div>
				<div class="sidenav-type">
					<p></p>
					<p><b>版本</b></p>
					<p></p>
					<!--<a-radio-group button-style="solid" defaultValue="primary">
						<a-input @pressEnter="setVersion()" placeholder="默认：22.0" v-model="pluginVersion"></a-input>
					</a-radio-group>-->
					<a-select
					v-model="pluginVersion"
					style="width:130px"
					@change="setVersion()"
					>
						<a-select-option v-for="item in productVersions" :key="item.value" :value="item.value">{{item.text}}</a-select-option>
					</a-select>
					<p>uri:</p>
				</div>
				<div class="sidenav-type">
					<p></p>
					<p><b>API地址</b></p>
					<p></p>
					<a-radio-group button-style="solid" defaultValue="primary">
						<a-input @pressEnter="settingOK()" placeholder="默认：http://172.25.13.72/api.pluginViewer/api" v-model="pluginServerApi"></a-input>
					</a-radio-group>
				</div>
				<p></p>	
			</div>
			<div v-else>
				<div class="sidenav-type">
					<p></p>
					<p><b>备份版本</b></p>
					<p></p>
					<a-select
					v-model="selectBackupVersionId"
					style="width:150px"
					@change="setVersion()"
					>
						<a-select-option v-for="item in allBackupVersion" :key="item.value" :value="item.value">{{item.text}}</a-select-option>
					</a-select>
					<a-button style="margin-left:10px;" @click="searchBackupVersionInfo()">检索</a-button>
				</div>
			</div>
			<hr>
			<div class="sidenav-type">
				<h6>其他设置</h6>
				<p></p>
				<p>非正常插件检查时间</p>
				<a-radio-group button-style="solid" defaultValue="primary">
					<a-input @pressEnter="settingCheckTime()" placeholder="5000" v-model="checkNormalPlugin_time"></a-input>
				</a-radio-group>
				<p></p>
				<!--<a-button @click="settingCheckTime()">OK</a-button>-->
			</div>
			

		</div>
		<!-- / Settings Drawer Content -->

	</a-drawer>
	<!-- / Settings Drawer -->

</template>

<script>
	import {EventBus} from '../../event-bus.js'
	import 'vue-github-buttons/dist/vue-github-buttons.css'; // Stylesheet
	import VueGitHubButtons from 'vue-github-buttons';
	import SdpliginService from '../service/GetSDPluginService';
	import SdxBackupService from '../service/GetSdxBackupService';
	import Vue from 'vue';
	Vue.use(VueGitHubButtons, { useCache: true });

	export default ({
		props: {
			// Settings drawer visiblility status.
			showSettingsDrawer: {
				type: Boolean,
				default: false,
			},
			
			// Main sidebar color.
			sidebarColor: {
				type: String,
				default: "primary",
			},
			
			// Main sidebar theme : light, white, dark.
			sidebarTheme: {
				type: String,
				default: "light",
			},

			// Header fixed status.
			navbarFixed: {
				type: Boolean,
				default: false,
			},

			// Drawer direction.
			rtl: {
				type: Boolean,
				default: false,
			},
		},
		data() {
			return {
				// The wrapper element to attach dropdowns to.
				wrapper: document.body,
				
				// Main sidebar color.
				sidebarColorModel: this.sidebarColor,
				
				// Main sidebar theme : light, white, dark.
				sidebarThemeModel: this.sidebarTheme,
				
				//获取数据来源方式
				dataFrom: 'sdx',

				// Header fixed status.
				navbarFixedModel: this.navbarFixed,
				
				pluginServer:this.GLOBAL.pluginServer,
				pluginServer_sdx:this.GLOBAL.pluginServer_sdx,
				pluginVersion:'',
				
				//数据库备份版本
				selectBackupVersionId:'',
				allBackupVersion:[],
				
				pluginServerApi:this.GLOBAL.apiAddress,
				
				checkNormalPlugin_time:this.GLOBAL.checkNormalPlugin_time,
				sdpliginService:new SdpliginService(),
				sdxbackupService:new SdxBackupService(),
				
				productVersions:[]
			}
		},
		mounted: function(){
			// Set the wrapper to the proper element, layout wrapper.
			this.wrapper = document.getElementById('layout-dashboard') ;
			
			this.getProductVersions()
		},
		
		methods:{
			settingOK(){
				this.GLOBAL.apiAddress=this.pluginServerApi
				
				EventBus.$emit('pluginServerApi',this.pluginServerApi)
			},
			
			setVersion(){
				this.GLOBAL.pluginVersion = this.pluginVersion
				
				EventBus.$emit('pluginVersion', this.pluginVersion)
			},
			
			settingCheckTime(){
				this.GLOBAL.checkNormalPlugin_time=this.checkNormalPlugin_time
				
				EventBus.$emit('checkNormalPlugin_time',this.checkNormalPlugin_time)
			},
			
			pluginServerChange(){
				//this.$router.push('/')
				if(this.pluginServer_sdx==='sdpluginvico' && this.pluginServer==='aipuyang'){
					this.$message.info("服务器未上传！")
					return
				}

				var server = {
					pluginserversdx:this.pluginServer_sdx,
					pluginserver:this.pluginServer,
					pluginversion:this.pluginVersion
				}
				//事件总线
				EventBus.$emit('pluginServer',server)
				this.GLOBAL.pluginServer=this.pluginServer
				this.GLOBAL.pluginServer_sdx=this.pluginServer_sdx
				this.GLOBAL.pluginVersion=this.pluginVersion
				
				this.getProductVersions()
			},
			
			async getProductVersions(){
				var server = {
					pluginserversdx:this.pluginServer_sdx,
					pluginserver:this.pluginServer,
					pluginversion:this.pluginVersion
				}
				
				this.productVersions=[]
				
				await this.sdpliginService.getProducts(server).then((res)=>{
					res.data.forEach(element=>{
						var productVersionOption={
							value:element.version,
							text:element.version
						}
						this.productVersions.push(productVersionOption)
					})
					
					this.pluginVersion = this.productVersions[0].value
					this.GLOBAL.pluginVersion=this.pluginVersion
				})
			},
			
			changeDataFrom(){
				if(this.dataFrom!='sdx'){
					this.sdxbackupService.getAllBackupVersion().then(res=>{
						res.data.forEach(element=>{
							var allBackupVersionOption={
								value:element.id,
								text:element.build
							}
							this.allBackupVersion.push(allBackupVersionOption)
						})
						this.selectBackupVersionId = this.allBackupVersion[0].value
					})
				}
			},
			
			searchBackupVersionInfo(){
				this.sdxbackupService.getBackupVersionJsonDataById(this.selectBackupVersionId).then(res=>{
					if(res.data){
						//var y = JSON.parse(JSON.stringify(res.data.jsonData))
						
						EventBus.$emit('sdxBackupData',res.data)	
					}
					
				})
			}
			
		}
	})

</script>
