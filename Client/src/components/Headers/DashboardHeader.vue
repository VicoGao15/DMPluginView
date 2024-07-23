<template>
	
	<!-- Main Sidebar -->
	<component :is="navbarFixed ? 'a-affix' : 'div'" :offset-top="top">

		<!-- Layout Header -->
		<a-layout-header>
			<a-row type="flex">

				<!-- Header Breadcrumbs & Title Column -->
				<a-col :span="24" :md="6">

					<!-- Header Breadcrumbs -->
					<a-breadcrumb>
						<a-breadcrumb-item><router-link to="/"> Pages</router-link></a-breadcrumb-item>
						<a-breadcrumb-item>{{ this.$route.name }}</a-breadcrumb-item>
					</a-breadcrumb>
					<!-- / Header Breadcrumbs -->

					<!-- Header Page Title -->
					<!--<div class="ant-page-header-heading">
						<span class="ant-page-header-heading-title">{{ this.$route.name }}</span>
					</div>-->
					<!-- / Header Page Title -->

				</a-col>
				<!-- / Header Breadcrumbs & Title Column -->

				<!-- Header Control Column -->
				<a-col :span="24" :md="18" class="header-control">

					<!-- Header Control Buttons -->
					<!--<a-dropdown :trigger="['click']" overlayClassName="header-notifications-dropdown" :getPopupContainer="() => wrapper">
						<a-badge count="4">
							<a class="ant-dropdown-link" @click="e => e.preventDefault()">
								<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
									<path d="M10 2C6.68632 2 4.00003 4.68629 4.00003 8V11.5858L3.29292 12.2929C3.00692 12.5789 2.92137 13.009 3.07615 13.3827C3.23093 13.7564 3.59557 14 4.00003 14H16C16.4045 14 16.7691 13.7564 16.9239 13.3827C17.0787 13.009 16.9931 12.5789 16.7071 12.2929L16 11.5858V8C16 4.68629 13.3137 2 10 2Z" fill="#111827"/>
									<path d="M10 18C8.34315 18 7 16.6569 7 15H13C13 16.6569 11.6569 18 10 18Z" fill="#111827"/>
								</svg>
							</a>
						</a-badge>
						
						<a-list item-layout="horizontal" class="header-notifications-list" :data-source="notificationsData" slot="overlay">
							<a-list-item slot="renderItem" slot-scope="item">
								<a-list-item-meta>
									<template #description>
										<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
											<path fill-rule="evenodd" clip-rule="evenodd" d="M10 18C14.4183 18 18 14.4183 18 10C18 5.58172 14.4183 2 10 2C5.58172 2 2 5.58172 2 10C2 14.4183 5.58172 18 10 18ZM11 6C11 5.44772 10.5523 5 10 5C9.44772 5 9 5.44772 9 6V10C9 10.2652 9.10536 10.5196 9.29289 10.7071L12.1213 13.5355C12.5118 13.9261 13.145 13.9261 13.5355 13.5355C13.9261 13.145 13.9261 12.5118 13.5355 12.1213L11 9.58579V6Z" fill="#111827"/>
										</svg>
										<span>{{ item.time }}</span>
									</template>
									<a slot="title" href="#">{{ item.title }}</a>
									<a-avatar v-if="item.img"
										slot="avatar"
										shape="square"
										:src="item.img"
									/>
									<a-avatar v-else
										shape="square"
										slot="avatar"  v-html="item.svg"/>
									
								</a-list-item-meta>
							</a-list-item>
						</a-list>
					</a-dropdown>-->
          
					<a-dropdown v-if="isLogin">
						<a-button type="link" ref="secondarySidebarTriggerBtn">
              <a-avatar v-if="loginUser.avatarurl!=''" style="background-color:#1890ff;margin-left:0px;" shape="circle" :src="loginUser.avatarurl"/>
							<a-avatar v-else style="background-color:#1890ff;margin-left:0px;">{{loginUser.username.charAt(0).toUpperCase()}}</a-avatar>
              
						</a-button>
						
						<template #overlay>
							<a-menu>
								<a-menu-item @click="userCenter()">用户中心</a-menu-item>
								<a-divider style="margin:4px 0"/>
								<a-menu-item @click="loginExist()">退出登录</a-menu-item>
							</a-menu>
						</template>
					</a-dropdown>
          <a-dropdown v-else>
						<a-button type="link" ref="secondarySidebarTriggerBtn">
							<a-avatar style="background-color:#8c8987;margin-left:0px;">游客</a-avatar>
						</a-button>
						
						<template #overlay>
							<a-menu>
								<a-menu-item @click="login()">登录</a-menu-item>
							</a-menu>
						</template>
					</a-dropdown>
					
					<a-tooltip placement="bottom" title="设置">
						<a-button type="link" ref="secondarySidebarTriggerBtn" @click="$emit('toggleSettingsDrawer', true)">
							<a-icon type="setting"  style="color:blue;font-size:18px;"></a-icon>
						</a-button>	
					</a-tooltip>
					<a-tooltip placement="bottom" :title="fullscreen_tooltip()">
						<a-button type="link" ref="secondarySidebarTriggerBtn"  @click="fullScreanClick()">
							<a-icon type="fullscreen" v-if="!fullscreen_show.isFull" style="color:blue;font-size:18px;"></a-icon>
							<a-icon type="fullscreen" v-else style="color:blue;font-size:18px;"></a-icon>
						</a-button>
					</a-tooltip>
					<a-tooltip placement="bottom" title="刷新路由">
						<a-button type="link" ref="secondarySidebarTriggerBtn"  @click="refreshCurrentRouter()">
              <!--<span class="icon">
                <svg t="1672734088660" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="12295" width="20" height="20"><path d="M896.00324 352c70.7 0 128-57.3 128-128 0-70.6-57.4-128-128-128-70.7 0-128 57.3-128 128 0 18.8 4.1 36.7 11.3 52.8 2.7 6 1.4 13.1-3.3 17.8l-24.2 24.2c-5.7 5.7-14.9 6.3-21.2 1.2-38.1-30.1-86.3-48-138.6-48-18.8 0-37.1 2.3-54.6 6.7-6.9 1.7-14.1-1.4-17.7-7.5l-6.6-11.4c-3.4-5.8-2.7-13.1 1.6-18.3 18.6-22.6 29.7-51.6 29.3-83.2C543.10324 89 486.30324 32.6 417.00324 32c-70.6-0.6-128.1 56.1-129 126.3-0.9 69.5 56.5 128.6 126 129.6 9.4 0.1 18.5-0.7 27.4-2.5 6.8-1.4 13.6 1.7 17.1 7.7l2.2 3.8c4 7 2.2 15.9-4.2 20.7-42.4 32.3-73 79.4-84 133.6-1.5 7.4-8.1 12.7-15.7 12.7h-94.1c-6.6 0-12.6-4-14.9-10.2-18.1-48-64.3-82.2-118.5-82.8C58.70324 370.3 0.50324 427.6 0.00324 498.1-0.49676 569.2 57.00324 627 128.00324 627c56.7 0 104.8-36.9 121.6-87.9 2.2-6.6 8.3-11.1 15.2-11.1h92c7.6 0 14.2 5.4 15.7 12.9 9.5 46.7 33.5 88 67 119.2 5.4 5 6.6 13.2 2.9 19.6l-21.7 37.6c-3.7 6.3-11.1 9.4-18.2 7.4-11.1-3.1-22.7-4.7-34.8-4.7-69.7 0.1-127 56.8-127.8 126.6-0.8 71.7 57.4 130 129.1 129.4 69.5-0.6 126.3-57.3 126.9-126.8 0.3-28-8.5-53.9-23.5-75.1-3.6-5.1-3.9-11.8-0.8-17.2l24.9-43.1c3.9-6.7 12-9.7 19.3-7 23.7 8.6 49.3 13.2 76 13.2 34.9 0 67.9-8 97.3-22.2 7.6-3.7 16.7-0.9 20.9 6.4l37 64c-26.3 23.5-43 57.7-43 95.8 0 70.9 58 128.5 128.9 128 69.7-0.5 126.2-56.7 127.1-126.3 0.9-70.1-57-129.3-127.1-129.7-6.2 0-12.3 0.4-18.3 1.2-6.5 0.9-12.8-2.2-16.1-7.8l-39.2-67.9c-3.4-5.9-2.7-13.3 1.7-18.4 34.2-39.3 54.9-90.7 54.9-147 0-38.9-9.9-75.5-27.4-107.4-3.4-6.2-2.2-13.9 2.8-18.9l28.4-28.4c4.9-4.9 12.4-6 18.7-2.9 17.4 8.6 36.9 13.5 57.6 13.5z m0-192c35.3 0 64 28.7 64 64s-28.7 64-64 64-64-28.7-64-64 28.7-64 64-64zM128.00324 563c-35.3 0-64-28.7-64-64s28.7-64 64-64 64 28.7 64 64-28.7 64-64 64z m240 349c-35.3 0-64-28.7-64-64s28.7-64 64-64 64 28.7 64 64-28.7 64-64 64z m464-112c35.3 0 64 28.7 64 64s-28.7 64-64 64-64-28.7-64-64 28.7-64 64-64zM416.00324 224c-35.3 0-64-28.7-64-64s28.7-64 64-64 64 28.7 64 64-28.7 64-64 64z m289.1 385.1C674.90324 639.4 634.70324 656 592.00324 656s-82.9-16.6-113.1-46.9C448.60324 578.9 432.00324 538.7 432.00324 496s16.6-82.9 46.9-113.1C509.10324 352.6 549.30324 336 592.00324 336s82.9 16.6 113.1 46.9C735.40324 413.1 752.00324 453.3 752.00324 496s-16.6 82.9-46.9 113.1z" p-id="12296"></path></svg>
              </span>-->
              <a-icon type="reload" style="color:blue;font-size:18px;"></a-icon>
						</a-button>
					</a-tooltip>
					<!--<a-button type="link" class="sidebar-toggler" @click="$emit('toggleSidebar', ! sidebarCollapsed) , resizeEventHandler()">
						<svg width="20" height="20" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path d="M16 132h416c8.837 0 16-7.163 16-16V76c0-8.837-7.163-16-16-16H16C7.163 60 0 67.163 0 76v40c0 8.837 7.163 16 16 16zm0 160h416c8.837 0 16-7.163 16-16v-40c0-8.837-7.163-16-16-16H16c-8.837 0-16 7.163-16 16v40c0 8.837 7.163 16 16 16zm0 160h416c8.837 0 16-7.163 16-16v-40c0-8.837-7.163-16-16-16H16c-8.837 0-16 7.163-16 16v40c0 8.837 7.163 16 16 16z"/></svg>
					</a-button>-->
					<!--<router-link to="/sign-in" class="btn-sign-in" @click="e => e.preventDefault()">
						<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
							<path fill-rule="evenodd" clip-rule="evenodd" d="M18 10C18 14.4183 14.4183 18 10 18C5.58172 18 2 14.4183 2 10C2 5.58172 5.58172 2 10 2C14.4183 2 18 5.58172 18 10ZM12 7C12 8.10457 11.1046 9 10 9C8.89543 9 8 8.10457 8 7C8 5.89543 8.89543 5 10 5C11.1046 5 12 5.89543 12 7ZM9.99993 11C7.98239 11 6.24394 12.195 5.45374 13.9157C6.55403 15.192 8.18265 16 9.99998 16C11.8173 16 13.4459 15.1921 14.5462 13.9158C13.756 12.195 12.0175 11 9.99993 11Z" fill="#111827"/>
						</svg>
						<span>Sign In</span>
					</router-link>-->
					<!-- / Header Control Buttons -->

					<!-- Header Search Input -->
					<a-input-search class="header-search" :class="searchLoading ? 'loading' : ''" placeholder="Search..." allow-clear @pressEnter="onSearch" :loading='searchLoading' v-model="searchInput" ref="inputSearch">
						<!--<svg slot="prefix" width="16" height="16" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
							<path fill-rule="evenodd" clip-rule="evenodd" d="M8 4C5.79086 4 4 5.79086 4 8C4 10.2091 5.79086 12 8 12C10.2091 12 12 10.2091 12 8C12 5.79086 10.2091 4 8 4ZM2 8C2 4.68629 4.68629 2 8 2C11.3137 2 14 4.68629 14 8C14 9.29583 13.5892 10.4957 12.8907 11.4765L17.7071 16.2929C18.0976 16.6834 18.0976 17.3166 17.7071 17.7071C17.3166 18.0976 16.6834 18.0976 16.2929 17.7071L11.4765 12.8907C10.4957 13.5892 9.29583 14 8 14C4.68629 14 2 11.3137 2 8Z" fill="#111827"/>
						</svg>-->
					</a-input-search>
					<!-- / Header Search Input -->
					
					<a-tooltip placement="bottom" title="今天是个好日子！">
						<a-button type="link" ref="secondarySidebarTriggerBtn">
							<p style="margin-top:20px;">{{nowweek}}</p>
						</a-button>
					</a-tooltip>
					
					<a-tooltip placement="bottom" title="当前时间">
						<a-button type="link" ref="secondarySidebarTriggerBtn">
							<p style="margin-top:20px;">{{nowtime}}</p>
						</a-button>
					</a-tooltip>
					

				</a-col>
				<!-- / Header Control Column -->

			</a-row>
		</a-layout-header>
		<!--  /Layout Header -->

	</component>
	<!-- / Main Sidebar -->

</template>

<script>
import {EventBus} from '../../event-bus.js'
import screenfull from 'screenfull'  
import {FullscreenOutlined,FullscreenExitOutlined,SettingOutlined,ReloadOutlined} from '@ant-design/icons-vue'

	const notificationsData = [
		{
			title: 'New message from Sophie',
			img: 'images/face-1.jpg',
			time: '13 minutes ago',
		},
		{
			title: 'New album by Travis Scott',
			svg: `<svg width="20" height="20" viewBox="0 0 107 107" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
					<title>logo-spotify</title>
					<g id="Page-1" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
						<g id="logo-spotify" fill="#2EBD59" fill-rule="nonzero">
							<path d="M53.5,0 C23.9517912,0 0,23.9517912 0,53.5 C0,83.0482088 23.9517912,107 53.5,107 C83.0482088,107 107,83.0482088 107,53.5 C107,23.9554418 83.0482088,0.00365063118 53.5,0 Z M78.0358922,77.1597407 C77.0757762,78.7368134 75.0204708,79.2296486 73.4506994,78.2695326 C60.8888775,70.5922552 45.0743432,68.8582054 26.4524736,73.1111907 C24.656363,73.523712 22.8675537,72.3993176 22.458683,70.6032071 C22.0461617,68.8070966 23.1669055,67.0182873 24.9666667,66.6094166 C45.3444899,61.9548618 62.8273627,63.9590583 76.9297509,72.5745479 C78.4995223,73.5419652 78.9996588,75.5899693 78.0358922,77.1597407 L78.0358922,77.1597407 Z M84.5814739,62.5973729 C83.373115,64.5614125 80.8030706,65.1747185 78.8426817,63.9700102 C64.4664961,55.1318321 42.5408052,52.5727397 25.5325145,57.7347322 C23.3275333,58.4027977 20.9984306,57.1579324 20.3267144,54.9566018 C19.6622996,52.7516206 20.9071648,50.4261685 23.1084954,49.7544524 C42.5371546,43.858683 66.6933811,46.7134766 83.2051859,56.8622313 C85.1692255,58.0705902 85.7898328,60.636984 84.5814739,62.5973729 Z M85.1436711,47.4253497 C67.8980894,37.1853292 39.4523712,36.2434664 22.9880246,41.2375299 C20.3449676,42.0406687 17.5485841,40.5475606 16.7490959,37.9045036 C15.9496076,35.2614466 17.4390652,32.4650631 20.0857728,31.6619243 C38.9850904,25.9267827 70.3987718,27.0329239 90.2509041,38.8171614 C92.627465,40.2299556 93.4087001,43.3001365 91.9995565,45.6730467 C90.5940635,48.0532583 87.5165814,48.838144 85.1436711,47.4253497 Z" id="Shape"></path>
						</g>
					</g>
				</svg>`,
			time: '1 day ago',
		},
		{
			title: 'Payment completed',
			svg: `<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
					<path fill="#1890FF" d="M4 4C2.89543 4 2 4.89543 2 6V7H18V6C18 4.89543 17.1046 4 16 4H4Z"/>
					<path fill="#1890FF" fill-rule="evenodd" clip-rule="evenodd" d="M18 9H2V14C2 15.1046 2.89543 16 4 16H16C17.1046 16 18 15.1046 18 14V9ZM4 13C4 12.4477 4.44772 12 5 12H6C6.55228 12 7 12.4477 7 13C7 13.5523 6.55228 14 6 14H5C4.44772 14 4 13.5523 4 13ZM9 12C8.44772 12 8 12.4477 8 13C8 13.5523 8.44772 14 9 14H10C10.5523 14 11 13.5523 11 13C11 12.4477 10.5523 12 10 12H9Z"/>
				</svg>`,
			time: '2 days ago',
		},
	] ;

	export default ({
		props: {
			// Header fixed status.
			navbarFixed: {
				type: Boolean,
				default: false, 
			},

			// Sidebar collapsed status.
			sidebarCollapsed: {
				type: Boolean,
				default: false,
			},

			// Header notifications data.
			notificationsData: {
				type: Array,
				default: () => notificationsData,
			},
		},
		components:{
			FullscreenOutlined,
			FullscreenExitOutlined,
			SettingOutlined,
			ReloadOutlined
		},
		data() {
			return {
				// Fixed header/sidebar-footer ( Affix component ) top offset.
				top: 0,

				// Search input loading status.
				searchLoading: false,

				// The wrapper element to attach dropdowns to.
				wrapper: document.body,
				
				//动态查询
				searchInput:'',
				
				//时间
				nowtime:'',
				
				//周几
				nowweek:'',
				
				fullscreen_show:{
					title:"全屏",
					isFull:false
				},
				
				loginUser:{
					username:'',
					password:'',
          avatarurl:''
				},
        
        //是否登录
        isLogin:false,
			}
		},
		methods: {
			resizeEventHandler(){
				this.top = this.top ? 0 : -0.01 ;
				// To refresh the header if the window size changes.
				// Reason for the negative value is that it doesn't activate the affix unless
				// scroller is anywhere but the top of the page.
			},
			onSearch(value){
				EventBus.$emit('searchplugin',this.searchInput)
			},
			
			setTime(){
				const dates = new Date();
				
				const year = dates.getFullYear();//year
				const month = (dates.getMonth() + 1) < 10 ? '0' + (dates.getMonth() + 1) : (dates.getMonth() + 1);//month
				const day = dates.getDate() < 10 ? '0' + dates.getDate() : dates.getDate();
		
				const hour = dates.getHours();//北京时间
				const minute = dates.getMinutes() < 10 ? '0' + dates.getMinutes() : dates.getMinutes();
				const seconds = dates.getSeconds()<10 ? '0' + dates.getSeconds() : dates.getSeconds();
				
				var week = '星期一';
				switch(dates.getDay()){
					case 2:
						week='星期二';
						break;
					case 3:
						week='星期三';
						break;
					case 4:
						week='星期四';
						break;
					case 5:
						week='星期五';
						break;
					case 6:
						week='星期六';
						break;
					case 7:
						week='星期天';
						break;
					default:
						week='星期一';
						break;
				}
				
				this.nowtime = year + '/' + month + '/' + day + ' ' + hour + ':' + minute + ':' + seconds;
				this.nowweek = week
			},
			fullScreanClick(){
				if(screenfull.isEnabled){
					if(screenfull.isFullscreen){
						screenfull.exit()
						this.fullscreen_show.title="全屏"
						this.fullscreen_show.isFull=false
					}else{
						screenfull.toggle()
						this.fullscreen_show.title="退出全屏"
						this.fullscreen_show.isFull=true
					}
				}
			},
			refreshCurrentRouter(){
				EventBus.$emit('refreshRouter')
			},
			fullscreen_tooltip(){
				return this.fullscreen_show.title
			},
			
			loginExist(){
				var exdate=new Date()
        exdate.setTime(exdate.getTime()+24*60*60*1000*7)
				window.document.cookie = "isLogin" + "=false" + ";path=/;expires=" + exdate.toGMTString()
				this.$router.push('/sign-in')
			},
      
      userCenter(){
				this.$router.push('/usercenter')
      },
      
      login(){
				this.$router.push('/sign-in')
      }
			
		},
		mounted: function(){
			// Set the wrapper to the proper element, layout wrapper.
			this.wrapper = document.getElementById('layout-dashboard') ;
			this.setTime()
		},
		created() {
			//获取登录信息
			//获取cookie是否登录信息
			//console.log(document.cookie)
			var arr = document.cookie.split(';')
			for (var i = 0; i < arr.length; i++) {
				var arr2 = arr[i].split('=')
				if (arr2[0] === ' userinfo' || arr2[0] === 'userinfo') {
          
          var editUsreinfo = JSON.parse(arr2[1])
						
						this.loginUser.username = editUsreinfo.username
						this.loginUser.avatarurl = editUsreinfo.avatarUrl
				}
        if(arr2[0] === ' isLogin' || arr2[0] === 'isLogin'){
            this.isLogin = arr2[1]=='true'?true:false
    		}
			}
			
			// Registering window resize event listener to fix affix elements size
			// error while resizing.
			window.addEventListener("resize", this.resizeEventHandler);
			
			//计时器
			this.timer = setInterval(this.setTime, 1000)
		},
		destroyed() {
			// Removing window resize event listener.
			window.removeEventListener("resize", this.resizeEventHandler);
			EventBus.$emit('searchplugin',this.searchInput)
		},
		beforeUnmounte(){
			//销毁定时器
			clearInterval(this.timer);
			this.timer = null
		},
		

	})

</script>

<style>
.headerIcon{
	font-size: 40px;
}
</style>
