<!-- 
	This is the main page of the application, the layout component is used here,
	and the router-view is passed to it.
	Layout component is dynamically declared based on the layout for each route,
	specified in routes list router/index.js .
 -->

<template>
	<div id="app">
		<vue-particles 
			color="#dedede"
			:particleOpacity="0.7"
			:particlesNumber="120"
			shapeType="circle"
			:particleSize="4"
			linesColor="#dedede"
			:linesWidth="1"
			:lineLinked="true"
			:lineOpacity="0.4"
			:linesDistance="150"
			:moveSpeed="2"
			:hoverEffect="true"
			hoverMode="repulse"
			:clickEffect="true"
			clickMode="push"
		></vue-particles>
			<component :is="layout" v-if="isShow">
				<keep-alive>
					<router-view v-if="$route.meta.keepAlive"></router-view>
				</keep-alive>
				<router-view v-if="!$route.meta.keepAlive"/>
			</component>
	</div>
</template>

<script>
import {EventBus} from './event-bus.js'
//import {getCookie} from './cookie.js'

	export default ({
		data(){
			return {
				isShow:true,
				isLogin:false
			}
		},
		computed: {
			// Sets components name based on current route's specified layout, defaults to
			// <layout-default></layout-default> component.
			layout() {
				return "layout-" + ( this.$route.meta.layout || "default" ).toLowerCase() ;
			}
		},
		created(){
			
			//获取cookie是否登录信息
			var arr = document.cookie.split(';')
			for (var i = 0; i < arr.length; i++) {
				var arr2 = arr[i].split('=')
				if (arr2[0] === ' isLogin' && arr2[0] === 'true') {
					this.isLogin=true
				}
			}

		},
		mounted(){
			EventBus.$on('refreshRouter',()=>{
				this.reload()
			})
			
			if(!this.isLogin){
				//this.$router.push('/sign-in')
			}
		},
		
		methods:{
			reload(){
				this.isShow=false
				this.$nextTick(()=>{
					this.isShow=true
				})
			},
		}
		
	})
	
</script>

<style lang="scss">
::-webkit-scrollbar-track { 
	background: rgba(0, 0, 0, 0.1); 
	border-radius: 0; 
} 
::-webkit-scrollbar { 
	-webkit-appearance: none; 
	width: 6px; height: 6px; 
} 
::-webkit-scrollbar-thumb { 
	cursor: pointer; 
	border-radius: 5px; 
	background: rgba(0, 0, 0, 0.15); 
	transition: color 0.2s ease; 
}

#particles-js{
	width:100%;
	height: calc(98%);
	position: absolute;
}
</style>