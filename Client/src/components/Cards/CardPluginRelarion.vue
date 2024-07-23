<template>

	<!-- Billing Information Card -->
	<a-card :bordered="false" class="header-solid h-full" :bodyStyle="{paddingTop: 0, paddingBottom: '16px' }">
		<template #title>
			<div>
			<span class="font-semibold m-0">插件依赖关系图</span>
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
							:on-node-click="onNodeClick" 
							:on-line-click="onLineClick"
						>
							<!--<div slot="node" slot-scope="{node}" @mouseover="showNodeDetail(node)">
								
							</div>-->
						</SeeksRelationGraph>
							
							
					</div>
				</a-col>
			</a-row>
			<!--<div style="height:calc(85vh);">
				<SeeksRelationGraph ref="seeksRelationGraph" :options="graphOptions" :on-node-click="onNodeClick" :on-line-click="onLineClick"/>
			</div>-->
		</template>

		<!--<PluginDetailDrawer></PluginDetailDrawer>-->
	</a-card>
	<!-- / Billing Information Card -->

</template>

<script>
	import SeeksRelationGraph from 'relation-graph'
	import SdpliginService from '../service/GetSDPluginService'
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
				_graph_data:{
					rootID:'',
					nodes:[],
					links:[]
				},
				
				//关系图配置
				graphOptions:{
					allowSwitchLineShape:true,
					allowSwitchJunctionPoint:true,
					defaultJunctionPoint:'border',
					allowShowMiniNameFilter:true,
					allowShowMiniView:true,
					isMoveByParentNode:true,
					defaultLineShape:5,
					
					disableZoom:false,
					disableDragNode:false,
					allowShowMiniView:false,
					
					defaultShowLineLabel:true,
					
					'layouts':[
						{
							'label':'force',
							'layoutName':'force'
						},
						{
							'label':'center',
							'layoutName':'center'
						},
						{
							'label':'tree',
							'layoutName':'tree'
						},
					]
				},
				
				//节点关系刷选 
				CheckedList_relation:['envDependencies','preDependencies','includePackages'],
				isHide_notRelationNode:false,
				
				allData:[],	//所有插件信息
				defaultCheckedList:['Product','Featrue','Extention'],
				
				sdpliginService:new SdpliginService(),	//获取插件的服务
				pluginServer:{
					pluginserver:this.GLOBAL.pluginServer,
          pluginserversdx:this.GLOBAL.pluginServer_sdx,
          pluginversion:this.GLOBAL.pluginVersion,
        },	//选择的服务器类型：devmagic/aipuyang
				
				searchPlugin_name:'',	//事件总线中搜索的插件
				
				nodeList:{},	//节点列表
				
				activekey:['1','2'],//设置collapse默认展开项
				
				//默认节点设置
				newNode:{
					id:'',
					text:'',
					//html:'<a-icon type="check" style="color:#52c41a"></a-icon><span>{{showName}}</span>',
					color:'blue',
					borderWidth:1,
					nodeShape:1,
					opacity:90,
					//borderWidth:1,
					
					height:35,
				},
				
				//默认关系连线设置
				newRelation:{
					from:'',		//起点
					to:'',			//终点
					text:'init',		//
					color:'blue',		//颜色
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
				
				//节点边框粗细
				nodeBorderThicknessOptions:[
					{ value:0, label:'0'},
					{ value:1, label:'1'},
					{ value:2, label:'2'},
					{ value:3, label:'3'},
					{ value:4, label:'4'},
				],
				
				//连线箭头
				
				//连线粗细
				lineStyle_BorderThickness_Options:[
					{ value:1, label:'1'},
					{ value:2, label:'2'},
					{ value:3, label:'3'},
				]
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
      
			EventBus.$on('pluginVersion', pluginVersion=>{
				this.pluginServer.pluginversion = pluginVersion
				this.getAllInit()
      })
      
			
			EventBus.$on('pluginServerApi',pluginServerApi=>{
				this.getAllInit()
				//this.$message.info(pluginServerApi)
			})
		},
		
		methods:{

			//显示关系图表
			showSeeksGraph(){
				//初始化插件节点
				var _graph_data=this.InitPluginNode()

				//this.$message.info(this.graphOptions.isMoveByParentNode)
				
				
				this.$refs.seeksRelationGraph.setJsonData(_graph_data, (seeksRGGraph)=>{
					//设置图谱属性
					this.$refs.seeksRelationGraph.setOptions(this.graphOptions)
				})
			},
			
			//初始化节点信息
			InitPluginNode(){
				
				//没有作为出发点关系的节点
				var allHavenotLinkStartNodes=[]
				//有作为结束点关系的节点
				var allHaveLinkEndNodes=[]
				//工作负载节点
				var workloadNodes=[]
				
				var NodeList={
					rootID:'',
					nodes:[],
					links:[]
				}
				
				//初始化节点和节点关系
				this.allData.forEach(item=>{
					
					//设置根节点
					if(item.id==='SnapDevelop'){
						NodeList.rootID=item.id
					}
					var showName=''
					if(item.packageType==='SDSDK-CORE'){
						showName=item.id		//Feature包显示id
					}else{
						showName=item.title	//其他包显示title
					}
					
					//添加节点
					const newNode=JSON.parse(JSON.stringify(this.newNode))//深拷贝 
					newNode.id=item.id
					newNode.text=showName
					newNode.color=this.setNodeStyle(item.packageType).color
					
					NodeList.nodes.push(newNode)

					//没有依赖关系的节点
					if(item.includePackages.length+item.envDependencies.length+item.preDependencies.length===0){
						allHavenotLinkStartNodes.push(item.id)
					}
					//工作负载节点
					if(item.packageType==='SDSDK-WORKLOAD'){
						workloadNodes.push(item.id)
					}
					
					if(this.CheckedList_relation.indexOf("includePackages")!=-1)//['envDependencies','preDependencies','includePackages']
					{
						//添加包含节点关系
						item.includePackages.forEach(element=>{
							const newRelation=JSON.parse(JSON.stringify(this.newRelation))//深拷贝 
							newRelation.from=item.id
							newRelation.to=element.id
							newRelation.text='inc'
							newRelation.color=this.linkTypeColor.inc
							newRelation.data.type='include'
							
							NodeList.links.push(newRelation)
							allHaveLinkEndNodes.push(element.id)
						})	
					}
					if(this.CheckedList_relation.indexOf("envDependencies")!=-1){
						//添加前置环境依赖节点关系
						item.envDependencies.forEach(element=>{
							const newRelation=JSON.parse(JSON.stringify(this.newRelation))//深拷贝 
							newRelation.from=item.id
							newRelation.to=element.id
							newRelation.text='env'
							newRelation.color=this.linkTypeColor.env
							newRelation.data.type='envDependencies'

							NodeList.links.push(newRelation)
							allHaveLinkEndNodes.push(element.id)
						})	
					}
					if(this.CheckedList_relation.indexOf("preDependencies")!=-1){
						//添加前置环境依赖节点关系
						item.preDependencies.forEach(element=>{
							const newRelation=JSON.parse(JSON.stringify(this.newRelation))//深拷贝 
							newRelation.from=item.id
							newRelation.to=element.id
							newRelation.text='pre'
							newRelation.color=this.linkTypeColor.pre
							newRelation.data.type='preDependencies'
							
							NodeList.links.push(newRelation)
							allHaveLinkEndNodes.push(element.id)
						})
					}
					
				})	
				
				//为工作负载添加虚拟关系,如果没有核心包
				if(this.defaultCheckedList.indexOf("Featrue")===-1){
					NodeList=this.addWorkloadLink(NodeList,workloadNodes)	
				} 
				
				//处理无关系节点
				NodeList=this.operateNotLinkNode(NodeList, allHavenotLinkStartNodes, allHaveLinkEndNodes)
				
				this.nodeList=NodeList
				return NodeList
			},
			
			changeRoot(){
				NodeList.rootID=item.id
			},
			
			//不同节点设置不同样式
			setNodeStyle(value){
				var style={
					color:'purple',
				}

				switch(value){
					case 'SDSDK':
						style.color=this.nodeTypeColor.product;
						break;
					case 'SDSDK-CORE':
						style.color=this.nodeTypeColor.feature;
						break;
					case 'SDSDK-EXTENSION':
						style.color=this.nodeTypeColor.extension;
						break;
					case 'SDSDK-ENVIRONMENT':
						style.color=this.nodeTypeColor.environment;
						break;
					case 'SDSDK-WORKLOAD':
						style.color=this.nodeTypeColor.workload;
						break;
					defaullt:
						style.color=this.nodeTypeColor.workload;
						break;
				}
				return style
			},

			operateNotLinkNode(nodeList, allHavenotLinkStartNodes, allHaveLinkEndNodes){

				var notRelationNodes=[]
				var haveRelationNodesId=[]
				var nodesIdList=[]
				
				//console.log(this.allData)
				//console.log(nodeList.links)
				nodeList.nodes.forEach(item=>{
					nodesIdList.push(item.id)
				})
				nodeList.links.forEach(item=>{
					if(nodesIdList.indexOf(item.from)!=-1 && nodesIdList.indexOf(item.to)!=-1){
						haveRelationNodesId.push(item.from)
						haveRelationNodesId.push(item.to)	
					}
				})
				
				
				console.log(haveRelationNodesId)
				nodeList.nodes.forEach(item=>{
					if(haveRelationNodesId.indexOf(item.id)===-1){
						notRelationNodes.push(this.getPluginBynodeId(item.id))
					}
				})
				
				//如果隐藏无关系节点
				if(this.isHide_notRelationNode){
					//console.log(notRelationNodes)

					for (let index = 0; index < notRelationNodes.length; index++){
						for(let i=0;i<nodeList.nodes.length;i++){
							if(nodeList.nodes[i].id===notRelationNodes[index].id){
								//console.log(nodeList.nodes[i].id)
								nodeList.nodes[i].isHide=true
								break
							}
						}
					}
					
				}
				else{
					//添加无关系'根'节点
					const newNode=JSON.parse(JSON.stringify(this.newNode))//深拷贝 
					newNode.id='notlinkRootNode'
					newNode.text='无关系的插件'
					newNode.color='red'
					
					const newRelation=JSON.parse(JSON.stringify(this.newRelation))//深拷贝 
					newRelation.from=nodeList.rootID
					newRelation.to='notlinkRootNode'
					newRelation.text=''
					newRelation.color=this.linkTypeColor.not
					newRelation.data.type='preDependencies'

					nodeList.nodes.push(newNode)
					nodeList.links.push(newRelation)
					
					notRelationNodes.forEach(element => {
						const newRelation=JSON.parse(JSON.stringify(this.newRelation))//深拷贝 
						newRelation.from='notlinkRootNode'
						newRelation.to=element.id
						newRelation.text=''
						newRelation.color=this.linkTypeColor.not
						newRelation.data.type='preDependencies'
	
						nodeList.links.push(newRelation)
					});
				}

				return nodeList
			},
			
			//获取节点集合中无关系的节点
			getHaveNoRelationNodes(nodeList){
				
			},
			
			//为工作负载添加虚拟关系
			addWorkloadLink(nodeList,workloadNodes){
				
				workloadNodes.forEach(element=>{
					const newRelation=JSON.parse(JSON.stringify(this.newRelation))//深拷贝 
						newRelation.from=nodeList.rootID
						newRelation.to=element
						newRelation.text='virtual'
						newRelation.color=this.linkTypeColor.not
						newRelation.data.type='preDependencies'
					
					nodeList.links.push(newRelation)
				})
				return nodeList
			},
			
			//获取所有节点
			async getAllInit(){
				var uriStr='/api/v3/product/getpackages?packageType='
				this.defaultCheckedList.forEach(item=>{
					switch(item){
						case 'Product':
							uriStr=uriStr+'SDSDK;'
							break;
						case 'Featrue':
							uriStr=uriStr+'SDSDK-CORE;'
							break;
						case 'Extention':
							uriStr=uriStr+'SDSDK-EXTENSION;'
							break;
						case 'Environment':
							uriStr=uriStr+'SDSDK-ENVIRONMENT;'
							break;
						case 'Workload':
							uriStr=uriStr+'SDSDK-WORKLOAD;'
							break;
					}
				})
				uriStr=uriStr+'&'
				//this.$message.info(uriStr)
				
				await this.sdpliginService.getPlugin(uriStr, this.pluginServer).then((res)=>{
					this.allData=res.data
					//console.log(this.allData.length)
				})
				this.showSeeksGraph()
			},

			//节点过滤 TODO
			async nodeFilter(value, iSelect){
				//value=选择的节点/类型
				//isSelect=勾选/反勾选
			},
			
			//节点点击事件处理 TODO
			onNodeClick(nodeObject, $event){
				//this.$message.info(nodeObject.id)
				//事件总线
				EventBus.$emit('pluginDetailVisible',true)
				EventBus.$emit('plugin',this.getPluginBynodeId(nodeObject.id))
				EventBus.$emit('allPlugin',this.allData)
				
				//changeRoot(nodeObject.id)
				
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

			//关系线条点击事件
			onLineClick(lineObject, $event){
				//console.log(lineObject)
				var lineText=''
				var Text=lineObject.fromNode.text + ' -> ' + lineObject.toNode.text
				switch(lineObject.relations[0].data.type){
					case 'include':
						lineText='包含：' + Text
						break;
					case 'envDependencies':
						lineText='环境依赖：' + Text
						break;
					case 'preDependencies':
						lineText='前置依赖：' + Text
						break;
					default:
						lineTex='未知'
						break;
				}
				this.$message.info(lineText)
			},
			
			selectPlugin_checkboxgroup(CheckedValues){
				this.defaultCheckedList=CheckedValues
				
				this.getAllInit()
				//this.$message.info(this.defaultCheckedList)
			},
			
			reletion_checkboxgroup(CheckedValues){
				this.CheckedList_relation=CheckedValues
				this.getAllInit()
				//this.$message.info(this.CheckedList_relation)
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
				//console.log(this.nodeList.nodes)
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
			
			showNodeDetail(node){
				this.$message.info(node.id)
			}
		}
	})

</script>