import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

let routes = [
	{
		// will match everything
		path: '*',
		component: () => import('../views/404.vue'),
	},
	{
		path: '/',
		name: 'Home',
		redirect: '/dashboard',
	},
	{
		path: '/dashboard',
		name: '组件总览',
		layout: "dashboard",
		// route level code-splitting
		// this generates a separate chunk (about.[hash].js) for this route
		// which is lazy-loaded when the route is visited.
		component: () => import(/* webpackChunkName: "dashboard" */ '../views/Dashboard.vue'),
	},
	{
		path: '/layout',
		name: 'Layout',
		layout: "dashboard",
		component: () => import('../views/Layout.vue'),
	},
	{
		path: '/group',
		name: '插件分类',
		layout: "dashboard",
		component: () => import('../views/Tables.vue'),
  },//WorkloadAndComponent
  {
    path: '/groupnew',
    name: '插件分类New',
    layout: "dashboard",
    component: () => import('../views/WorkloadAndComponent.vue'),
  },
	{
    path: '/relation',
		name: '依赖关系',
		layout: "dashboard",
		component: () => import('../views/Billing.vue'),
  },
  {
    path: '/backupversion',
    name: '历史版本',
    layout: "dashboard",
    component: () => import('../views/backup/DashboardBackup.vue'),//DashboardCompare
  },
  {
    path: '/versioncompare',
    name: '版本比较',
    layout: "dashboard",
    component: () => import('../views/backup/DashboardCompare.vue'),
  },
  {
    path: '/pluginstatistics',
    name: '插件统计',
    layout: "dashboard",
    component: () => import('../views/backup/Analysis.vue'),
  },
  {
    path: '/monitor',
    name: '监控',
    layout: "dashboard",
    component: () => import('../views/Billing_monitor.vue'),
  },
  {
    path: '/dllcompare',
    name: 'DllCompare',
    layout: "dashboard",
    component: () => import('../views/DllCheck.vue'),
  },
  {
    path: '/usercenter',
    name: '用户中心',
    layout: "dashboard",
    component: () => import('../views/UserCenter.vue'),
  },
  {
    path: '/vksp',
    name: 'vksp',
    layout: "dashboard",
    component: () => import('../views/Vksp.vue'),
  },
  {
    path: '/concept',
    name: '公共知识库',
    layout: "dashboard",
    component: () => import('../views/Concept.vue'),
    meta:{noCache:false, keepAlive:true}
  },
  {
    path: '/privateconcept',
    name: '个人知识库',
    layout: "dashboard",
    component: () => import('../views/ConceptPrivate.vue'),
    meta: { noCache: false, keepAlive: true }
  },
  {
    path: '/conceptdetail',
    name: '文章详情',
    layout: "dashboard",
    component: () => import('../components/DetailPage/ConceptDetail.vue'),
  },
  {
    path: '/todolist',
    name: '待办事件',
    layout: "dashboard",
    component: () => import('../views/TodoList.vue'),
  },
	{
		path: '/sign-in',
		name: 'Sign-In',
		component: () => import('../views/Sign-In.vue'),
	},
	{
		path: '/sign-up',
		name: 'Sign-Up',
		meta: {
			layoutClass: 'layout-sign-up',
		},
		component: () => import('../views/Sign-Up.vue'),
	},
]

// Adding layout property from each route to the meta
// object so it can be accessed later.
function addLayoutToRoute( route, parentLayout = "default" )
{
	route.meta = route.meta || {} ;
	route.meta.layout = route.layout || parentLayout ;
	
	if( route.children )
	{
		route.children = route.children.map( ( childRoute ) => addLayoutToRoute( childRoute, route.meta.layout ) ) ;
	}
	return route ;
}

routes = routes.map( ( route ) => addLayoutToRoute( route ) ) ;

const router = new VueRouter({
	mode: 'history',
	base: process.env.BASE_URL,
	routes,
	scrollBehavior (to, from, savedPosition) {
		if ( to.hash ) {
			return {
				selector: to.hash,
				behavior: 'smooth',
			}
		}
		return {
			x: 0,
			y: 0,
			behavior: 'smooth',
		}
	}
})

export default router
