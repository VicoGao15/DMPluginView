<template>

	<!-- Billing Information Card -->
	<a-card :bordered="false" class="header-solid h-full" :bodyStyle="{paddingTop: 0, paddingBottom: '0px' }">
		<template #title>
			<div>
			<span class="font-semibold m-0">服务监控</span>
			<!--<a-icon type="fullscreen" style="color:blue;font-size:18px;"></a-icon>-->
			<a-button  style="margin-left:20px;" size="small" @click="showSetting()">隐藏/显示设置选项</a-button>	
			</div>
			
			<a-row :gutter="[24, 24]" style="margin-top:10px;">
				
				<a-col :span="col_span.setting">
					<a-card :bordered="false" style="height:calc(85vh);backgroud-color:'#ffffff';">
						<div class="col-info">
							
							<a-button type="link" ref="secondarySidebarTriggerBtn" style="margin-left:-30px;" @click="collaspeSetting.nodeSelect=!collaspeSetting.nodeSelect">
								<a-icon type="caret-down" v-if="collaspeSetting.nodeSelect" style="font-size:20px;"></a-icon>
								<a-icon type="caret-up" v-else style="font-size:20px;"></a-icon>
							</a-button>
							<span style="margin-left:-10px;">显示节点类型</span><br/>
								<a-checkbox-group @change="selectPlugin_checkboxgroup" :defaultValue="defaultCheckedList" v-if="collaspeSetting.nodeSelect">
									<a-checkbox value="Product" checked="true" disabled defaultChecked="true">
										<a-tag style="width:135px;" :color="nodeTypeColor.product">产品包(Products)</a-tag>
									</a-checkbox>
									<a-tooltip placement="bottom" title="自定义颜色">
											<colorPicker v-model="nodeTypeColor.product" defaultColor="#0000ff" style="margin-top:0px;" @change="changeColor()"/>
									</a-tooltip><br/>
									<a-checkbox style="margin-top:5px;" value="Featrue">
										<a-tag style="width:135px;" :color="nodeTypeColor.feature">核心包(Features)</a-tag>
									</a-checkbox>
									<a-tooltip placement="bottom" title="自定义颜色">
											<colorPicker v-model="nodeTypeColor.feature" defaultColor="#008000" @change="changeColor()"/>
									</a-tooltip><br/>
									<a-checkbox style="margin-top:5px;" defaultChecked="true" value="Extention">
										<a-tag style="width:135px;" :color="nodeTypeColor.extension">插件包(Extentions)</a-tag>
									</a-checkbox>
									<a-tooltip placement="bottom" title="自定义颜色">
											<colorPicker v-model="nodeTypeColor.extension" defaultColor="#ffae1a" @change="changeColor()"/>
									</a-tooltip><br/>
									<a-checkbox style="margin-top:5px;" defaultChecked="true" value="Environment">
										<a-tag style="width:135px;" :color="nodeTypeColor.environment">环境包(Environments)</a-tag>
									</a-checkbox>
									<a-tooltip placement="bottom" title="自定义颜色">
											<colorPicker v-model="nodeTypeColor.environment" defaultColor="#51dcf3" @change="changeColor()"/>
									</a-tooltip><br/>
									<a-checkbox style="margin-top:5px;" value="Workload">
										<a-tag style="width:135px;" :color="nodeTypeColor.workload">工作负载(Workloads)</a-tag>
									</a-checkbox>
									<a-tooltip placement="bottom" title="自定义颜色">
											<colorPicker v-model="nodeTypeColor.workload" defaultColor="#800080" @change="changeColor()"/>
									</a-tooltip><br/>
								</a-checkbox-group><p></p>
							<a-button type="link" ref="secondarySidebarTriggerBtn" style="margin-left:-30px;" @click="collaspeSetting.linkSelect=!collaspeSetting.linkSelect">
								<a-icon type="caret-down" v-if="collaspeSetting.linkSelect" style="font-size:20px;"></a-icon>
								<a-icon type="caret-up" v-else style="font-size:20px;"></a-icon>
							</a-button>
							<span style="margin-left:-10px;">关系刷选</span><br/>
								<a-checkbox-group @change="reletion_checkboxgroup" :defaultValue="CheckedList_relation" v-if="collaspeSetting.linkSelect">
									<a-checkbox style="margin-top:0px;" value="includePackages">
										<a-tag style="width:135px;" :color="linkTypeColor.inc">includePackages</a-tag>
									</a-checkbox>
									<a-tooltip placement="bottom" title="自定义颜色">
											<colorPicker v-model="linkTypeColor.inc" defaultColor="#000000" @change="changeColor()"/>
									</a-tooltip><br/>
									<a-checkbox style="margin-top:5px;" value="preDependencies">
										<a-tag style="width:135px;" :color="linkTypeColor.pre">preDependencies</a-tag>
									</a-checkbox>
									<a-tooltip placement="bottom" title="自定义颜色">
											<colorPicker v-model="linkTypeColor.pre" defaultColor="#800080" @change="changeColor()"/>
									</a-tooltip><br/>
									<a-checkbox style="margin-top:5px;" value="envDependencies" >
										<a-tag style="width:135px;" :color="linkTypeColor.env">envDependencies</a-tag>
									</a-checkbox>
									<a-tooltip placement="bottom" title="自定义颜色">
											<colorPicker v-model="linkTypeColor.env" defaultColor="#51dcf3" @change="changeColor()"/>
									</a-tooltip><br/>
									<a-checkbox style="margin-top:5px;" value="null" disabled>
										<a-tag style="width:135px;" :color="linkTypeColor.not">no relation link</a-tag>
									</a-checkbox>
									<a-tooltip placement="bottom" title="自定义颜色">
											<colorPicker v-model="linkTypeColor.not" defaultColor="#c0cfd3" @change="changeColor()"/>
									</a-tooltip><br/>
								</a-checkbox-group><p></p>
							<a-button type="link" ref="secondarySidebarTriggerBtn" style="margin-left:-30px;" @click="collaspeSetting.graphAttribute=!collaspeSetting.graphAttribute">
								<a-icon type="caret-down" v-if="collaspeSetting.graphAttribute" style="font-size:20px;"></a-icon>
								<a-icon type="caret-up" v-else style="font-size:20px;"></a-icon>
							</a-button>
							<span style="margin-left:-10px;">图表属性</span><br/>
								<ul class="list settings-list" style="margin-left:10px;margib-bottom:10px;" v-if="collaspeSetting.graphAttribute">
									<li> 
										<h6 class="list-header text-sm text-muted">事件</h6>
									</li>
									<li style="margin-top:-15px;">
										<a-switch size="small" v-model="graphOptions.isMoveByParentNode" @change="seeksRelationGraphOptionsChange()" />
										<span>随父节点移动</span>
									</li>
									<li style="margin-top:-15px;">
										<a-switch size="small" :checked="GLOBAL.nodeClickCenter" @click="setting_nodeClickCenter"/>
										<span>节点点击居中</span>
									</li>
									<li style="margin-top:-15px;">
										<a-switch size="small" v-model="graphOptions.disableDragNode" @change="seeksRelationGraphOptionsChange()" />
										<span>禁用节点拖动</span>
									</li>
									<li>
										<h6 class="list-header text-sm text-muted m-0">属性</h6>
									</li>
									<li style="margin-top:-15px;">
										<a-switch size="small"  v-model="graphOptions.disableZoom" @change="seeksRelationGraphOptionsChange()" />
										<span>禁用滚轮缩放</span>
									</li>
									<li style="margin-top:-15px;">
										<a-switch size="small" v-model="graphOptions.allowShowMiniView" @change="seeksRelationGraphOptionsChange()" />
										<span>显示缩略图</span>
									</li>
									<li style="margin-top:-15px;margib-bottom:10px;">
										<a-switch size="small" @change="hide_notRelationNode()" />
										<span>隐藏没关系节点</span>
									</li>
								</ul><p></p>
							<a-button type="link" ref="secondarySidebarTriggerBtn" style="margin-left:-30px;" @click="collaspeSetting.nodeAttribute=!collaspeSetting.nodeAttribute">
								<a-icon type="caret-down" v-if="collaspeSetting.nodeAttribute" style="font-size:20px;"></a-icon>
								<a-icon type="caret-up" v-else style="font-size:20px;"></a-icon>
							</a-button>
							<span style="margin-left:-10px;">节点样式</span><br/>
								<a-form :label-col="labelCol" :wrapper-col="wrapperCol" labelAlign="left" style="margin-left:10px;" v-if="collaspeSetting.nodeAttribute">
									<a-form-item label="节点边框粗细" style="margin-top:-12px;">
										<a-select
											style="width:100px;height:30px;margin-left:-20px;"
											size="small"
											:options="nodeBorderThicknessOptions"
											v-model="newNode.borderWidth"
											@change="showSeeksGraph()"
										></a-select>
									</a-form-item>
									<a-form-item label="节点形状" style="margin-top:-40px;">
										<a-select
											style="width:100px;height:30px;margin-left:-20px;"
											size="small"
											:options="nodeShapeOptions"
											v-model="newNode.nodeShape"
											@change="showSeeksGraph()"
										></a-select>
									</a-form-item>
									<a-form-item label="节点透明度" style="margin-top:-40px;">
										<a-input size="small" suffix="%" v-model="newNode.opacity" @pressEnter="showSeeksGraph()" style="width:100px;height:30px;margin-left:-20px;"></a-input><br/>
									</a-form-item>
									<a-form-item label="节点高度" style="margin-top:-50px;">
										<a-input size="small" suffix="px" v-model="newNode.height" @pressEnter="showSeeksGraph()" style="width:100px;height:30px;margin-left:-20px;"></a-input><br/>
									</a-form-item>
									<a-form-item label="节点宽度" style="margin-top:-40px;margin-bottom:-40px;">
										<a-input size="small" suffix="px" v-model="newNode.width" placeholder="auto" @pressEnter="showSeeksGraph()" style="width:100px;height:30px;margin-left:-20px;"></a-input><br/>
									</a-form-item>
								</a-form>
							<p></p>
							<a-button type="link" ref="secondarySidebarTriggerBtn" style="margin-left:-30px;" @click="collaspeSetting.linkAttribute=!collaspeSetting.linkAttribute">
								<a-icon type="caret-down" v-if="collaspeSetting.linkAttribute" style="font-size:20px;"></a-icon>
								<a-icon type="caret-up" v-else style="font-size:20px;"></a-icon>
							</a-button>
							<span style="margin-left:-10px;">连线样式</span><br/>
								<ul class="list settings-list" style="margin-left:10px;margib-bottom:0px;" v-if="collaspeSetting.linkAttribute">
									<li style="margin-top:0px;">
										<a-switch size="small" v-model="graphOptions.defaultShowLineLabel" @click="seeksRelationGraphOptionsChange()"/>
										<span>显示连线文字</span>
									</li>
								</ul>
								<a-form :label-col="labelCol" :wrapper-col="wrapperCol" labelAlign="left" style="margin-left:10px;" v-if="collaspeSetting.linkAttribute">
									<a-form-item label="连线粗细" style="margin-top:-25px;">
										<a-select 
											style="width:100px;height:30px;margin-left:-20px;"
											size="small"
											:options="lineStyle_BorderThickness_Options"
											v-model="newRelation.lineWidth"
											@change="showSeeksGraph()"
										>
										<template #suffix></template>
										</a-select>											
									</a-form-item>
									<a-form-item label="默认连线箭头" style="margin-top:-40px;margin-bottom:-40px;">
										<a-select
											style="width:100px;height:30px;margin-left:-20px;"
											size="small"
										></a-select>
									</a-form-item>
								</a-form>
						</div>
					</a-card>
				</a-col>
				
				<a-col :span="col_span.panel">
					<div style="height:calc(85vh);">
						<!-- <a-divider orientation="left">节点TODO</a-divider> -->
						<SeeksRelationGraph
							ref="seeksRelationGraph" 
							:options="graphOptions"
							:on-line-click="onLineClick"
						>
						<!--分类节点-->
							<div slot="node" slot-scope="{node}" v-if="node.data.isCategoriesNode">
								<a-dropdown :trigger="['contextmenu']">
									<div
										:style="{
											textAlign:'center',
											height:'100px'
										}"
									>
										<div style="cursor: pointer;text-align: center;margin-top:3px;">
											<span style="font-size: 18px;">{{ node.text }}</span>
										</div>
									</div>
									<a-menu slot="overlay" >
										<a-menu-item @click="nodeMenuClick('edit',node)">
											<a-icon type="edit" style="font-size:20px;"></a-icon>
											编辑分类
										</a-menu-item>
										<a-menu-item @click="nodeMenuClick('addson',node)">
											<a-icon type="plus-circle" style="font-size:20px;"></a-icon>
											添加服务
										</a-menu-item>
										<a-menu-divider />
										<a-menu-item @click="nodeMenuClick('delete',node)">
											<a-icon type="delete" style="font-size:20px;" theme="twoTone" two-tone-color="red"></a-icon>
											<span style="margin-bottom:10px;">删除分类</span>
										</a-menu-item>
									</a-menu>
								</a-dropdown>
							</div>
							<!--监控节点-->
							<div slot="node" slot-scope="{node}" v-else>
								<a-dropdown :trigger="['contextmenu']">
									<div
										:style="{
											textAlign:'center',
											height:'100px'
										}"
									>
										<!--<a-icon type="check-circle" style="font-size:40px;"></a-icon>-->
										<!--<a-icon type="close-circle" style="font-size:20px;"></a-icon>-->
										<!--<a-icon type="loading" style="font-size:40px;"></a-icon>-->
										<!--<div style="height:64px;line-height: 64px;border-radius: 32px;cursor: pointer;text-align: center;">
											
										</div>-->
										<a-tooltip>
											<template #title><a :href="node.data.iteminfo.url" target="_blank">{{node.data.iteminfo.url}}</a></template>
											<div v-if="node.data.status===0">
												<a-icon type="loading" style="font-size:30px;margin-left:-3px;margin-top:-3px;"></a-icon>
												
											</div>
											
											<a-icon type="check" v-else-if="node.data.status===1" style="font-size:30px;margin-left:-3px;margin-top:-3px;"></a-icon>
											<a-icon type="close" v-else style="font-size:30px;margin-left:-3px;margin-top:-3px;"></a-icon>
										</a-tooltip>
										
										<div style="color: forestgreen;absolute;width: 200px;font-size: 22px;height:25px;line-height: 25px;margin-top:5px;margin-left:-85px;text-align: center;backgroud-color:rgba(66,187,66,0.2);">
											{{ node.text }}
										</div>
									</div>
									<a-menu slot="overlay" >
										<a-menu-item @click="nodeMenuClick('edit',node)">
											<a-icon type="edit" style="font-size:20px;"></a-icon>
											编辑服务
										</a-menu-item>
										<a-menu-item @click="nodeMenuClick('addson',node)">
											<a-icon type="plus-circle" style="font-size:20px;"></a-icon>
											添加子服务
										</a-menu-item>
										<a-menu-divider />
										<a-menu-item @click="nodeMenuClick('delete',node)">
											<a-popconfirm
												title="确定要删除该服务监控吗？"
												ok-text="Yes"
												cancel-text="Cancel"
												@confirm="deletenode(node.id)"
											>
												<a-icon type="delete" style="font-size:20px;" theme="twoTone" two-tone-color="red"></a-icon>
												<span style="margin-bottom:10px;">删除节点</span>	
											</a-popconfirm>
											
										</a-menu-item>
									</a-menu>
								</a-dropdown>
							</div>
							<div slot="bottomPanel" style="border-top:#efefef solid 1px;height:60px;line-height: 60px;text-align: center;font-size: 18px;background-color: #ffffff;">
								这里是底部插槽 slot="bottomPanel",可以自定义这里的内容
								<a-button @click="check()">Check</a-button>
							</div>
						</SeeksRelationGraph>

					</div>
				</a-col>
			</a-row>
		</template>
		
		<a-modal title="添加监控服务" v-model="addNode_visible" @ok="addNodeOk()">
			<a-form :label-col="labelCol" :wrapper-col="wrapperCol" labelAlign="left" style="margin-left:10px;">
				<a-form-item label="显示名称" style="margin-top:-12px;">
					<a-input size="small" v-model="newStateItem.name" placeholder="输入名称！" style="width:360px;height:30px;margin-left:-150px;"></a-input><br/>
				</a-form-item>
				<a-form-item label="服务链接" style="margin-top:-40px;">
					<a-input size="small" v-model="newStateItem.url" placeholder="输入链接！" style="width:360px;height:30px;margin-left:-150px;"></a-input><br/>
				</a-form-item>
				<a-form-item label="正常响应" style="margin-top:-40px;">
					<a-select
						style="width:200px;height:30px;margin-left:-150px;"
						size="small"
						:options="itemResponeTypeOptions"
						v-model="newStateItem.isUseStatusCode"
					></a-select>
				</a-form-item>
				<a-form-item style="margin-top:-40px;" v-if="newStateItem.isUseStatusCode!='0'">
					<a-textarea size="small" v-model="newStateItem.responseContent" placeholder="期望返回值(区分大小写)！" style="width:360px;height:100px;margin-left:80px;"></a-textarea><br/>
				</a-form-item>
			</a-form> 
		</a-modal>
		
		<a-modal title="编辑监控服务" v-model="editNode_visible" @ok="editNodeOk()">
			<a-form :label-col="labelCol" :wrapper-col="wrapperCol" labelAlign="left" style="margin-left:10px;">
				<a-form-item label="显示名称" style="margin-top:-12px;">
					<a-input size="small" v-model="editStateItem.name" placeholder="输入名称！" style="width:360px;height:30px;margin-left:-150px;"></a-input><br/>
				</a-form-item>
				<a-form-item label="服务链接" style="margin-top:-40px;">
					<a-input size="small" v-model="editStateItem.url" placeholder="输入链接！" style="width:360px;height:30px;margin-left:-150px;"></a-input><br/>
				</a-form-item>
				<a-form-item label="正常响应" style="margin-top:-40px;">
					<a-select
						style="width:200px;height:30px;margin-left:-150px;"
						size="small"
						:options="itemResponeTypeOptions"
						v-model="editStateItem.isUseStatusCode"
					></a-select>
				</a-form-item>
				<a-form-item style="margin-top:-40px;" v-if="editStateItem.isUseStatusCode!='0'">
					<a-textarea size="small" v-model="editStateItem.responseContent" placeholder="期望返回值(区分大小写)！" style="width:360px;height:100px;margin-left:80px;"></a-textarea><br/>
				</a-form-item>
			</a-form> 
		</a-modal>

		<!--<PluginDetailDrawer></PluginDetailDrawer>-->
	</a-card>
	<!-- / Billing Information Card -->

</template>

<script>
	import SeeksRelationGraph from 'relation-graph'
	import SdpliginService from '../service/GetSDPluginService'
	import MonitorService from '../service/GetMonitorService'
	import {EventBus} from '../../event-bus.js'
	import {FullscreenOutlined,CheckOutlined,UnorderedListOutlined} from '@ant-design/icons-vue'
//	import PluginDetailDrawer from '../Drawer/PluginDetailDrawer.vue'
	
	export default ({
		conponents:{
			SeeksRelationGraph,
			CheckOutlined,
			UnorderedListOutlined,
			FullscreenOutlined,
			//PluginDetailDrawer
		},
		
		data() {
				
			return {
				//布局
				col_span:{
					setting:4,
					panel:20
				},
				
				graph_data:{
					rootID:'6',
					nodes:[],
					links:[]
				},
				
				//关系图配置
				graphOptions:{
					allowSwitchLineShape:true,
					allowSwitchJunctionPoint:true,
					defaultJunctionPoint:'tb',	//连线接触方式 
					allowShowMiniNameFilter:false,	//
					allowShowMiniView:false,	//显示略缩图
					isMoveByParentNode:false,	//随父节点移动
					defaultLineShape:4,		//线条样式
					
					disableZoom:false,
					disableDragNode:false,
					//allowShowMiniView:false,
					
					defaultShowLineLabel:true,
					
					'layouts':[
						{
							'label':'tree',
							'layoutName':'tree',
							'min_per_width':'100',
							'max_per_width':'150',
							'min_per_height':'100',
							'max_per_height':'200',
						},
						{
							'label':'force',
							'layoutName':'force'
						},
						{
							'label':'center',
							'layoutName':'center'
						},
					]
				},
				
				//节点关系刷选 
				CheckedList_relation:['envDependencies','preDependencies','includePackages'],
				isHide_notRelationNode:true,
				
				allData:[],	//所有插件信息
				defaultCheckedList:['Product','Workload'],
				
				sdpliginService:new SdpliginService(),	//获取插件的服务
				monitorService:new MonitorService(),
				pluginServer:this.GLOBAL.pluginServer,	//选择的服务器类型：devmagic/aipuyang
				
				searchPlugin_name:'',	//事件总线中搜索的插件
				
				nodeList:{},	//节点列表
				
				activekey:['1','2'],//设置collapse默认展开项
				
				//默认节点设置
				newNode:{
					id:'',
					text:'',
					color:'#a2b0b3',
					borderWidth:1,
					nodeShape:0,
					opacity:70,
					borderWidth:3,
					isHide:false,
					
					height:50,
					width:50,
					
					data:{
						status:0,	//服务状态 0：检测中 1：健康 -1：异常
						parentNodeId:0,
						iteminfo:{}
					},
					
//					fixed:true,
//					x:500,
//					y:300,
				},
				
				//默认根节点设置
				newRootNode:{
					id:'root',
					text:'Appeon服务监控',
					html:'',
					color:'blue',
					borderWidth:1,
					nodeShape:1,
					opacity:70,
					expandHolderPosition:"top",
					
					height:50,
					width:0,
					
					data:{
						status:0,	//服务状态 0：检测中 1：健康 -1：异常
						isCategoriesNode:true,
						iteminfo:{}
					},
					
					fixed:true,
					x:500,
					y:300,
				},
				
				//默认关系连线设置
				newRelation:{
					from:'',		//起点
					to:'',			//终点
					text:'',		//
					color:'#12af12',		//颜色
					data:{
						type:''
					},
					
					lineWidth:2,
				},
				
				nodeTypeColor:{	//不同类型节点颜色
					product:'#0000ff',
					feature:'#008000',
					extension:'#ffae1a',
					environment:'#51dcf3',
					workload:'#800080'
				},
				
				linkTypeColor:{	//不同类型关系连线颜色
					env:'#51dcf3',
					pre:'#800080',
					inc:'#000000',
					not:'#c0cfd3',
				},
				
				collaspeSetting:{
					nodeSelect:true,
					linkSelect:true,
					graphAttribute:false,
					nodeAttribute:false,
					linkAttribute:false
				},
				
				//表单布局
				labelCol:{ span:12, },
				wrapperCol:{ span:10, },
				
				//节点形状
				nodeShapeOptions:[
					{ value:0, label:'圆形' },
					{ value:1, label:'矩形' },
				],
				
				//响应类型
				itemResponeTypeOptions:[
					{ value:'0', label:'状态码200 OK' },
					{ value:'1', label:'返回值' },
				],
				
				//节点边框粗细
				nodeBorderThicknessOptions:[
					{ value:0, label:'0'},
					{ value:1, label:'1'},
					{ value:2, label:'2'},
					{ value:3, label:'3'},
					{ value:4, label:'4'},
				],
				
				//连线粗细
				lineStyle_BorderThickness_Options:[
					{ value:1, label:'1'},
					{ value:2, label:'2'},
					{ value:3, label:'3'},
				],
				
				addNode_visible:false,
				editNode_visible:false,
				newStateItem:
				{
					name: '',
					url: '',
					isUseStatusCode:'0',
					responseStatusCode: 200,
					responseContent: ''
				},
				
				editStateItem:{
					id:0,
					name: '',
					url: '',
					isUseStatusCode:'0',
					responseStatusCode: 200,
					responseContent: ''	
				},
			}
		},
		
		mounted(){
			this.getAllInit()
			
			//事件总线
			EventBus.$on('searchplugin',data=>{
				this.searchPlugin_name=data
				
				//搜索的插件居中
				this.focusNode()
			})
			
			EventBus.$on('pluginServer',pluginServer=>{
				this.pluginServer=pluginServer
				this.getAllInit()
			})
			
			EventBus.$on('pluginServerApi',pluginServerApi=>{
				this.getAllInit()
			})
		},
		
		methods:{

			//显示关系图表
			showSeeksGraph(){
				//初始化节点
				//this.InitPluginNode()
				//this.updateApipsStatus()
				
				this.$refs.seeksRelationGraph.setJsonData(this.graph_data, (seeksRGGraph)=>{
					//设置图谱属性
					this.$refs.seeksRelationGraph.setOptions(this.graphOptions)
				})
			},
			
			//初始化节点信息
			InitPluginNode(){
				console.log(this.graph_data)
				this.graph_data.nodes=[]
				this.graph_data.links=[]
				
        
        //添加根节点
				const newNode_root=JSON.parse(JSON.stringify(this.newRootNode))//深拷贝 
				this.graph_data.nodes.push(newNode_root)	
				
//				const newNode_server=JSON.parse(JSON.stringify(this.newNode))//深拷贝 
//				newNode_server.id='server'
//				newNode_server.text='服务'
//				NodeList.nodes.push(newNode_server) 
				
//				const newNode_container=JSON.parse(JSON.stringify(this.newRootNode))//深拷贝 
//				newNode_container.id='container'
//				newNode_container.text='容器'
//				NodeList.nodes.push(newNode_container)
				
//				//添加节点关系
//					const newRelation1=JSON.parse(JSON.stringify(this.newRelation))//深拷贝 
//					newRelation1.from=newNode_root.id
//					newRelation1.to=newNode_server.id
//					NodeList.links.push(newRelation1)
				
				//var i=this.cardData.length - (this.cardData.length + this.cardData.length/2)
				//初始化节点和节点关系
				this.cardData.forEach(item=>{
					
					//添加节点
					const newNode=JSON.parse(JSON.stringify(this.newNode))//深拷贝 
					newNode.id=item.id.toString()
					newNode.text=item.name
					newNode.data.iteminfo=item
//					newNode.x=newNode_root.x+200
//					newNode.y=newNode_root.y+100*i
//					i++
					
					this.graph_data.nodes.push(newNode)	
          
          //添加节点关系
					const newRelation=JSON.parse(JSON.stringify(this.newRelation))//深拷贝 
					newRelation.from='root'
					newRelation.to=item.id.toString()
					//newRelation.text='inc'
					this.graph_data.links.push(newRelation)
				})	

				//this.nodeList=this._graph_data
				console.log(this.graph_data)

				//return NodeList
			},
			
			updateApipsStatus(){
				for(var i=0; i<this.graph_data.nodes.length;i++)
				{
//					try{ 
//						await this.monitorService.getapistate(this.nodelist.nodes[i].data.iteminfo.url).then(res=>{
////	            if(this.nodelist.nodes[i].iteminfo.isusestatuscode==='1'){
////								if(res.data.responsestatuscode===200){
////									this.nodelist.nodes[i].data.status=1 
////	              }else{
////									this.nodelist.nodes[i].data.status=-1
////	              }
////	            }
////							else if(res.data.responsecontent===this.nodelist.nodes[i].iteminfo.responsecontent){
////								this.nodelist.nodes[i].data.status=1
////							}
////	            else{
////								this.nodelist.nodes[i].data.status=-1
////	            }
//							this.nodelist.nodes[i].data.status=-1
//							console.log(this.nodelist.nodes[i].data.status)
//						})
//					}catch(err){
//						//this.nodelist.nodes[i].data.status = -1;
//					}
					if(this.graph_data.nodes[i].id!=this.graph_data.rootID){
						//this._graph_data.nodes[i].data.status = 1;
						this.graph_data.nodes[i].color = "green";
					}
					
					//console.log(this._graph_data.nodes[i].data.status)
				}
				
				console.log(this.graph_data)
			},
			
			//获取所有节点
			async getAllInit(){
				await this.monitorService.getCheckItem().then((res)=>{
					this.cardData=res.data
					this.InitPluginNode()
					this.showSeeksGraph()
				})
				
			},
			
			//节点点击事件处理 TODO
			onNodeClick(nodeObject, $event){
				//this.$message.info(nodeObject.id)
				//事件总线
				EventBus.$emit('pluginDetailVisible',true)
				EventBus.$emit('plugin',this.getPluginBynodeId(nodeObject.id))
				EventBus.$emit('allPlugin',this.allData)
				
				//节点居中显示
				if(this.GLOBAL.nodeClickCenter){
					this.$refs.seeksRelationGraph.focusNodeById(nodeObject.id)
				}
			},

			//根据节点ID返回节点全部信息 TODO
			getPluginBynodeId(value){
				var selectPlugin=this.allData[0]
				for (var i = 0; i < this.allData.length; i++) {
				    if(this.allData[i].id===value){
							selectPlugin=this.allData[i]
							break
						}
				}
				return selectPlugin
			},
			
			selectPlugin_checkboxgroup(CheckedValues){
				this.defaultCheckedList=CheckedValues
				
				this.getAllInit()
			},
			
			reletion_checkboxgroup(CheckedValues){
				this.CheckedList_relation=CheckedValues
				this.getAllInit()
			},

			setting_moveByParent(){
				this.graphOptions.isMoveByParentNode=this.graphOptions.isMoveByParentNode?false:true

				//this.showSeeksGraph()
				this.$refs.seeksRelationGraph.setOptions(this.graphOptions)
			},
			
			setting_nodeClickCenter(){
				this.GLOBAL.nodeClickCenter=this.GLOBAL.nodeClickCenter?false:true
			},
			
			focusNode(){
				//根据名称获取节点id
				var nodeid=this.getNodeByName(this.searchPlugin_name)
				//console.log(nodeid)
				//节点居中
				this.$refs.seeksRelationGraph.focusNodeById(nodeid)
			},
			
			getNodeByName(value){
				var node=''
				if(value===''){
					node='SnapDevelop'
				}else{
					for (var i = 0; i < this.nodeList.nodes.length; i++) {
						if(this.nodeList.nodes[i].text.toLowerCase().indexOf(value.toLowerCase())!=-1){
							console.log(this.nodeList.nodes[i])
							node=this.nodeList.nodes[i].id
							break
						} 
					}
				}
				
				return node
			},
			
			showSetting(){
				if(this.col_span.setting!=0){
					this.col_span={
						setting:0,
						panel:24
					}	
				}else{
					this.col_span={
						setting:4,
						panel:20
					}	
				}
				
			},
			
			hide_notRelationNode(){
				this.isHide_notRelationNode=!this.isHide_notRelationNode
				
				//重置图表
				this.showSeeksGraph()
			},
			
			changeColor(){
				//重置图表
				this.showSeeksGraph()
			},
			
			seeksRelationGraphOptionsChange(){
				//重置图表属性
				this.$refs.seeksRelationGraph.setOptions(this.graphOptions)
			},
			
			nodeMenuClick(type, node){
				switch(type){
					case 'detail':
						this.onNodeClick(node)
						break;
					case 'edit':
						this.editNode_visible=true
						this.editStateItem=node.data.iteminfo
						break;
					case 'addson':
						this.addNode_visible=true
						break;
					case 'delete':
						break;
				}
			},
			
			deletenode(id){
				this.monitorService.deleteCheckItemById(id).then((res)=>{
					if(res.data.responseStatusCode===200){
						this.getAllInit()
					}
				})
			},
			
			addNodeOk(){
				this.addNode_visible=false
				
				this.monitorService.addCheckItem(this.newStateItem).then(res=>{
          if(res.status===200){
						this.getAllInit()
            this.$message.success("添加成功："+this.newStateItem.name)	
          }else{
            this.$message.error("保存失败！")
          }
        })
			},
			
			editNodeOk(){
				this.editNode_visible=false
				
				this.monitorService.updateCheckItem(this.editStateItem).then(res=>{
          if(res.status===200){
						this.getAllInit()
            this.$message.success("编辑保存成功："+this.editStateItem.name)	
          }else{
            this.$message.error("编辑保存失败！")
          }
        }) 
			},
			
			check(){
				//this.updateApipsStatus()
				//console.log(this._graph_data)
				
				//this.showSeeksGraph()
				//this.$refs.seeksRelationGraph.refresh()
				//console.log(this.nodeList)
//				this.$refs.seeksRelationGraph.setJsonData(this._graph_data, (seeksRGGraph)=>{
//					//设置图谱属性
//					//this.$refs.seeksRelationGraph.setOptions(this.graphOptions)
//				})
				//this._graph_data=this.InitPluginNode()
				this.updateApipsStatus()
				this.showSeeksGraph()
			}
		}
	})

</script>