

import Vue from 'vue'
import Antd from 'ant-design-vue';
import 'ant-design-vue/dist/antd.css';
import App from './App.vue'
import DefaultLayout from './layouts/Default.vue'
import DashboardLayout from './layouts/Dashboard.vue'
import DashboardRTLLayout from './layouts/DashboardRTL.vue'
import router from './router'
import SeeksRelationGraph from 'relation-graph'

import VueClipboard from 'vue-clipboard2'
import vcolorpicker from 'vcolorpicker'//基于vue的颜色选择器
import {ColorPicker, ColorPanel} from  'one-colorpicker'

import preview from 'vue-photo-preview'
import 'vue-photo-preview/dist/skin.css'
//import '../assets/css/all.css'
//ant design icons
import { FullscreenOutlined, FullscreenExitOutlined, SettingFilled } from '@ant-design/icons-vue'
//import PluginDetailDrawer from './components/Drawer/PluginDetailDrawer.vue'
// import './plugins/click-away'

import './scss/app.scss';
import global_ from './Global'
import  { mixins } from './mixin/index'
import draggable from 'vuedraggable'
import h1 from 'highlight.js'
import 'highlight.js/styles/github-dark-dimmed.css' //代码高亮的样式
import VueParticles from 'vue-particles'
import VEmojiPicker from 'v-emoji-picker'
import Chat from 'vue-beautiful-chat'
  
//自定义代码高亮指令
Vue.directive('highlight', function(e1){
  const blocks = e1.querySelectorAll('pre')
  blocks.forEach((block)=>{
    h1.highlightBlock(block)
  })
})
Vue.prototype.$h1js = h1;

Vue.mixin(mixins)
Vue.use(Antd);
Vue.use(VueClipboard);
Vue.use(vcolorpicker);
Vue.use(ColorPanel)
Vue.use(ColorPicker)
Vue.use(preview)
Vue.use(VueParticles)
Vue.use(VEmojiPicker)
Vue.use(Chat)
    
Vue.config.productionTip = false 

Vue.prototype.GLOBAL = global_

// Adding template layouts to the vue components.
Vue.component("layout-default", DefaultLayout);
Vue.component("layout-dashboard", DashboardLayout);
Vue.component("layout-dashboard-rtl", DashboardRTLLayout);
Vue.component("SeeksRelationGraph", SeeksRelationGraph);
//Vue.component("PluginDetailDrawer",PluginDetailDrawer);
Vue.component('draggable', draggable);

Vue.component(FullscreenOutlined.name, FullscreenOutlined);
Vue.component(FullscreenExitOutlined.name, FullscreenExitOutlined);
Vue.component(SettingFilled.name, SettingFilled);


new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
  
document.title="DM插件&VKSP记录"

//屏蔽警告信息
//app.config.warnHandler = () => null
//app.config.errorHandler=()=>null
//apply.config.silent = true