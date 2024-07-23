<!-- 
	This is the sign in page, it uses the dashboard layout in: 
	"./layouts/Default.vue" .
 -->

<template>
	<div class="sign-in">
		
		<a-row type="flex" :gutter="[24,24]" justify="space-around" align="middle">

			<!-- Sign In Form Column -->
			<a-col :span="24" :md="12" :lg="{span: 12, offset: 0}" :xl="{span: 6, offset: 2}" class="col-form">
				<h1 class="mb-15">Sign In</h1>
				<h5 class="font-regular text-muted">Enter your username and password to sign in</h5>

				<!-- Sign In Form -->
				<a-form
					id="components-form-demo-normal-login"
					:form="form"
					class="login-form"
					@submit="handleSubmit"
					:hideRequiredMark="true"
				>
					<a-form-item class="mb-10" label="Username" :colon="false">
						<a-input 
						v-decorator="[
						'username',
						{ rules: [{ required: true, message: 'Please input your email!' }] },
						]" placeholder="Email" />
					</a-form-item>
					<a-form-item class="mb-5" label="Password" :colon="false">
						<a-input
						v-decorator="[
						'password',
						{ rules: [{ required: true, message: 'Please input your password!' }] },
						]" type="password" placeholder="Password" />
					</a-form-item>
					<a-form-item class="mb-10">
    					<a-switch v-model="rememberMe" /> Remember Me
					</a-form-item>
					<a-form-item>
						<a-button type="primary" block html-type="submit" class="login-form-button">
							SIGN IN
						</a-button>
					</a-form-item>
				</a-form>
				<!-- / Sign In Form -->

				<p class="font-semibold text-muted">Don't have an account? <router-link to="/sign-up" class="font-bold text-dark">Sign Up</router-link></p>
			</a-col>
			<!-- / Sign In Form Column -->

			<!-- Sign In Image Column -->
			<a-col :span="24" :md="12" :lg="12" :xl="12" class="col-img">
				<img src="images/img-signin.jpg" alt="">
			</a-col>
			<!-- Sign In Image Column -->

		</a-row>
		
	</div>
</template>

<script>
//import axios from 'axios'
import VkspService from '../components/service/VkspService'
import { sha512 } from 'js-sha512'

	export default ({
		data() {
      const vkspservice=new VkspService()
			return {
				// Binded model property for "Sign In Form" switch button for "Remember Me" .
				rememberMe: true,
        account:{
					username:'',
          password:''
        },
        
        vkspservice
			}
		},
		beforeCreate() {
			// Creates the form and adds to it component's "form" property.
			this.form = this.$form.createForm(this, { name: 'normal_login' });
		},
    mounted(){
			this.getCookie()
    },
		methods: {
			// Handles input validation after submission.
			handleSubmit(e) {
        
				e.preventDefault();
				this.form.validateFields((err,values) => {
					if ( !err ) {
            this.vkspservice.loginValidation(values.username,sha512(values.password)).then(res=>{
							if(res.status==200){
                //后端验证账号密码成功
                this.$message.success('登录成功！')
                this.$router.push('/dashboard')
                //if(this.rememberMe){
                  this.setCookie(res.data,7)
                //}
              }else{
								this.$message.error('用户名或密码不正确！')
              }
            })
					}
				});
			},

      
      setCookie(resdata,exdays){
				var exdate=new Date()
        exdate.setTime(exdate.getTime()+24*60*60*1000*exdays)
        
        //字符串拼接cookie
        window.document.cookie = "userinfo" + "=" + JSON.stringify(resdata) + ";path=/;expires=" + exdate.toGMTString()
        window.document.cookie = "isLogin" + "=true" + ";path=/;expires=" + exdate.toGMTString()
      },
      
      getCookie(){
				if(document.cookie.length>0){
          //console.log(document.cookie)
					var arr = document.cookie.split(';')
          var u_name=''
          var u_pwd=''
          //console.log(arr)
          for(var i=0;i<arr.length;i++){
						var arr2=arr[i].split('=')
            if(arr2[0]===' username'){
							u_name=arr2[1]
            }else if(arr2[0]===' userpwd'){
							u_pwd=arr2[1]
            }else if(arr2[0]===' isLogin' && arr2[1]==='true'){
							this.$router.push('/dashboard')
            }
          }
          
          this.form.setFieldsValue({
						username:u_name,
            password:u_pwd
          })
        }
      }
		},
	})

</script>

<style lang="scss">
	body {
		background-color: #ffffff;
	}
</style>