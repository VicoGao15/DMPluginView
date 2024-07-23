<!-- 
	This is the user profile page, it uses the dashboard layout in: 
	"./layouts/Dashboard.vue" .
 -->

<template>
	<div>

		<!-- Header Background Image -->
		<div class="profile-nav-bg" :style="backgroudstyle">
    </div>
		<!-- / Header Background Image -->

		<!-- User Profile Card -->
		<a-card :bordered="false" class="card-profile-head" :bodyStyle="{padding: 30,}">
			<span v-show="false">{{ getLoginMessage()}}</span>
			<template #title>
				<a-row type="flex" align="middle">
					<a-col :span="24" :md="12" class="col-info">
						
						<a-avatar v-if="userinfo.avatarurl==='a'" :size="74" shape="circle" style="background-color:#1890ff;">{{userinfo.username.charAt(0).toUpperCase()}}</a-avatar>
						<a-avatar v-else :size="74" shape="circle" :src="userinfo.avatarurl"/>
						
						<div class="avatar-info">
							<h4 class="font-semibold m-0">{{userinfo.username}}</h4>
							<!--<p>密码：{{userinfo.password}}</p>-->
							<p>{{userinfo.personalSignature}}</p>
						</div>
					</a-col>
				</a-row>
        <a-tabs v-model:activeKey="activeKey" style="margin-top:20px" tab-position="left">
          <a-tab-pane key="1" tab="编辑信息">
    				<a-row style="margin:20px;">
    					<a-col :span="24" :md="12">
    						<a-card :bordered="false">
    							<h6>编辑用户信息</h6>
    							<a-form
    								:hideRequiredMark="true"
                    :label-col="{ span:4 }"
                    :wrapper-col="{ span:18 }"
                    style="margin-top:20px"
    							>
    								<a-form-item label="用户名：">
    									<a-input v-model="editUsreinfo.username"/>
    								</a-form-item>
    				        <a-form-item label="头像：">
                      <!--<img v-if="tag_avatarImagePreview==true" :src="avatarImagePreview" alt="头像加载异常" width="42" height="42">-->
    									<a-upload style="margin-left:0px;" 
    			              :before-upload="handelAvatarFileUpload"
    			            >
    			              <a-button type="dashed">
    			                <a-icon type="upload"></a-icon>
    			                选择图片
    			              </a-button>
    			            </a-upload>
    								</a-form-item>
                    <!--<a-form-item label="主页背景："> 
                      <a-upload style="margin-left:0px;"
                      :before-upload="handelBackgroudFileUpload"
    			            >
    			              <a-button type="dashed">
    			                <a-icon type="upload"></a-icon>
    			                选择图片
    			              </a-button>
    			            </a-upload>
    								</a-form-item>-->
    								<a-form-item label="个性签名：">
    									<a-input v-model="editUsreinfo.signature"/>
    								</a-form-item>
                    <a-form-item :wrapper-col="{ offset: 4, span: 18 }">
      							  <a-button @click="comfirmEditUserInfo(false)">确认</a-button>
                    </a-form-item>
    							</a-form>
    						</a-card>	
    					</a-col>
            </a-row>		
          </a-tab-pane>
          <a-tab-pane key="2" tab="重置密码" force-render>
    				<a-row style="margin:20px;">
    					<a-col :span="24" :md="12">
    						<a-card :bordered="false">
    							<h6>重置密码</h6>
    							<a-form
    								:hideRequiredMark="true"
                    :label-col="{ span:4 }"
                    :wrapper-col="{ span:18 }"
                    style="margin-top:20px"
    							>
    								<a-form-item label="原密码：">
    									<a-input v-model="resetpwddata.old_pwd"/>
    								</a-form-item>
    				        <a-form-item label="新密码：">
    									<a-input-password v-model="resetpwddata.new_pwd"/>
    								</a-form-item>
    								<a-form-item label="确认密码：">
    									<a-input-password v-model="resetpwddata.new_pwd_confirm"/>
    								</a-form-item>
                    <a-form-item :wrapper-col="{ offset: 4, span: 18 }">
        							<a-button @click="resetPassword_confirm()">重置</a-button>
                    </a-form-item>
                  </a-form>
    						</a-card>	
    					</a-col>
    				</a-row>			
          </a-tab-pane>
          <a-tab-pane key="3" tab="切换主题">
						<a-dropdown>
              <template #overlay>
                <a-menu @click="handleMenuClick">
                  <a-menu-item v-for="item in backgroudOption" :key="item.key">
										<p><img :src='item.imgsrc' width="200" height="42"/></p>
                  </a-menu-item>
                </a-menu>
              </template>
              <a-button style="margin-top:10px;margin-left:10px;" size="small">
                切换主题
              </a-button>
            </a-dropdown>
          </a-tab-pane>
          <!--<a-tab-pane key="4">
            <template #tab>
              已发表 ({{myConceptData_copy.length}})
							<svg t="1689916330490" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="1658" width="20" height="20">
								<path d="M896 85.333333C299.008 85.333333 184.661333 605.013333 137.344 882.688l-2.346667 13.866667a36.138667 36.138667 0 0 0 35.626667 42.112h8.917333a42.666667 42.666667 0 0 0 41.344-32.128c1.877333-7.381333 3.584-13.525333 5.034667-18.304 34.218667-110.378667 101.12-171.776 200.746667-184.234667 170.666667-21.333333 298.666667-170.666667 341.333333-298.666667l-64-42.666666 42.666667-42.666667c42.666667-42.666667 85.504-106.666667 149.333333-234.666667z" fill="#1296db" p-id="1659">
								</path>
							</svg>
            </template>
    				<a-row>
    					<a-col :span="24" :md="24">
                <a-select
                  size="small"
                  ref="select"
                  v-model:value="myConceptFilter.selectYear"
                  style="width: 100px"
                  placeholder="年"
                  @change="handleChange_Filter"
                  :options="selectYearOption"
                ></a-select>
                <a-select
                  size="small"
                  ref="select"
                  v-model:value="myConceptFilter.selectMonth"
                  style="width: 100px;margin-left:10px;"
                  placeholder="月"
                  @change="handleChange_Filter"
                  :options="selectMonthOption"
                ></a-select>
                <a-input-search 
                  size="small" 
                  placeholder="搜索"
                  style="width: 350px;margin-top:0px;"
                  v-model = "inputSearchContent.publishedConcept"
                  @search="onSearchConcept(0)"
                ></a-input-search>
                <span style="margin-left:15px;">搜索结果: {{myConceptData.length}} 条</span>
    						<div style="margin:15px;height:640px;overflow: auto;">
						      <a-timeline v-if="myConceptData.length!=0" style="margin:10px 250px 0px 250px;" mode="alternate">
								    <a-timeline-item v-for="item in myConceptData" :key="item.id">
                      <a-icon slot="dot" type="clock-circle-o" style="font-size: 16px;" />
											<a-card hoverable @click="handleLinkToDetailPage(item.id)" class="header-solid h-full card-profile-information" :bodyStyle="{paddingTop: 20, paddingBottom: 20,paddingRight: 20 }" :headStyle="{paddingRight: 40,}">
												<p>{{timeFormate(item.create_Date)}}</p>
												{{item.header}}
											</a-card>
										</a-timeline-item>
								  </a-timeline>
									<a-empty v-else>
										<a-Button>创建</a-Button>
									</a-empty>
    						</div>		
    					</a-col>
            </a-row>
          </a-tab-pane>-->
          <a-tab-pane key="5">
            <template #tab>
              收藏夹 ({{myCollectionConceptData.length}})
							<svg style="margin:auto;" t="1690169655770" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="16194" width="24" height="24"><path d="M887.7 380.2L693 331.4c-3.6-0.9-6.7-3.2-8.7-6.3l-106.5-170c-4.6-7.3-10.8-13.5-18.1-18.1-26.8-16.8-62.2-8.7-79 18.1L374 325.2c-2 3.1-5.1 5.4-8.7 6.3l-194.7 48.8c-8.4 2.1-16.2 6.1-22.9 11.7-24.3 20.3-27.5 56.5-7.2 80.8l33.6 40.2 42.6 3.8c201.6 18 379.4 143.6 464 327.9l18.9 41.1 21 8.4c8 3.2 16.7 4.6 25.4 4 31.6-2.2 55.4-29.5 53.3-61.1l-13.8-200.2c-0.3-3.7 0.9-7.3 3.3-10.2l128.8-153.9c5.6-6.6 9.5-14.5 11.7-22.9 7.7-30.9-10.9-62-41.6-69.7z" fill="#f3e51c" p-id="16195"></path><path d="M337.6 894.1l186.2-75c1.7-0.7 3.5-1 5.4-1 1.8 0 3.6 0.3 5.4 1l107.5 43.3c-76.5-166.7-238-286.2-428.9-303.2l56.4 67.4c2.4 2.8 3.6 6.5 3.3 10.2L259 836.9c-0.6 8.6 0.8 17.3 4 25.4 9 22.3 30.5 35.9 53.2 35.9 7.2 0 14.4-1.3 21.4-4.1z" fill="#f3e51c" p-id="16196"></path></svg>
            </template>
            <a-row>
    					<a-col :span="24" :md="12">
                <a-input-search 
                  size="small" 
                  placeholder="搜索"
                  v-model = "inputSearchContent.collectionConcept"
                  @search="onSearchConcept(1)"
                ></a-input-search>
    						<div style="margin:15px;height:640px;overflow: auto;">
						      <a-timeline v-if="myCollectionConceptData.length!=0" style="margin:5px;">
								    <a-timeline-item v-for="item in myCollectionConceptData" :key="item.id" color="yellow">
											<a-card v-if="item" hoverable @click="handleLinkToDetailPage(item.id)" class="header-solid h-full card-profile-information" :bodyStyle="{paddingTop: 20, paddingBottom: 20,paddingRight: 20 }" :headStyle="{paddingRight: 40,}">
												<p>{{timeFormate(item.create_Date)}}</p>
												{{item.header}}
											</a-card>
                      <a-card v-else>
												<span style="color:red;">该文章已被删除！</span>
                      </a-card>
										</a-timeline-item>
								  </a-timeline>
									<a-empty v-else>
										<router-link :to="'/concept'" ><a-Button >浏览</a-Button></router-link>
									</a-empty>
    						</div>	
    					</a-col>
    				</a-row>						
          </a-tab-pane>
          <a-tab-pane key="6">
            <template #tab>
              3D 形象
						</template>
            <a-row>
    					<a-col :span="24" :md="18">
                <!--<Personal3DImage></Personal3DImage>-->
    					</a-col>
    				</a-row>						
          </a-tab-pane>
        </a-tabs>
			</template>
		</a-card>
		<!-- User Profile Card -->

	</div>
</template>

<script>
import axios from 'axios'
import VkspService from '../components/service/VkspService'
import { sha512 } from 'js-sha512'
import { mixins } from '../mixin/index'
import { system_options } from '../mixin/options'
import { VkspUser } from '../mixin/vkspuser'
//import Personal3DImage from '../components/3DModel/Personal3DImage.vue'
import { myNotification } from '../mixin/notification'

	export default ({
		mixins:[mixins,VkspUser,system_options,myNotification],
		components: {
      //Personal3DImage
		},
		data() {
			const vkspservice=new VkspService()
			var userinfo = {
					username:'',
					password:'',
					avatarurl:'',
					personalSignature:'',
				}
			var editUsreinfo = {}
			var resetpwddata = {
				old_pwd:'',
				new_pwd:'',
				new_pwd_confirm:''
			}
			var myConceptData = []
      var myConceptData_copy = []
      var myConceptData_structure = []
			var myCollectionConceptData = []
      var myCollectionConceptData_copy = []
      
      var tag_avatarImagePreview = false
      
      var myConceptFilter={
				selectYear:'0',
        selectMonth:'0',
        selectType:''
      }
      var backgroudstyle = "background-image: url('images/background/earth.jpg')"
			return {
				userinfo,
				resetpwddata,
				myConceptData,myConceptData_copy,
				myCollectionConceptData,myCollectionConceptData_copy,
				
				vkspservice,
        
        backgroudImagePreview:'',
        tag_avatarImagePreview,
        avatarImagePreview:'',
        
        myConceptFilter,
        backgroudstyle,
        
        activeKey: '4',
        activeKey_myconcept:'month',
        
        inputSearchContent:{
					publishedConcept:'',
          collectionConcept:''
        },
			}
		},
    
    created(){
			this.isLogin = this.ifIsLogin()
      if(!this.isLogin){
        this.myNotification_info('请先登录！')
				return
      }
    },
		
		methods:{
			getLoginMessage(){
				//获取cookie是否登录信息
				var arr = document.cookie.split(';')
				for (var i = 0; i < arr.length; i++) {
					var arr2 = arr[i].split('=')
					if(arr2[0] === ' userinfo' || arr2[0] === 'userinfo'){
						this.editUsreinfo = JSON.parse(arr2[1])
						
						this.userinfo.username = this.editUsreinfo.username
						this.userinfo.avatarurl = this.editUsreinfo.avatarUrl
						this.userinfo.personalSignature = this.editUsreinfo.signature
						
					}
				}
			},
			
			//文件上传
	    handelAvatarFileUpload(file){
	        const formDate=new FormData();
	        formDate.append('vkspfiles',file)

	        var fileUploadUrl = "http://172.25.13.72/api.pluginViewer/api/file/uploadavatar"
					//var fileUploadUrl = "https://localhost:7483/api/file/uploadavatar"
	        axios.post(fileUploadUrl,formDate)
	        .then(res=>{
	          if(res.data.success){
	            //this.$message.success("头像上传成功！")
	            this.editUsreinfo.avatarUrl = "http://172.25.13.72:8880/VKSP/avatar/" + file.name
	          }else{
	            this.$message.error("头像文件失败！",err)
	          }
	        })
	    },
      handelBackgroudFileUpload(file){
  			//this.getBase64(file)
      },
    	// 文件对象转base64
      getBase64 (obj) {
        const image = obj; //获取文件域中选中的图片
        let reader = new FileReader(); //实例化文件读取对象
        reader.readAsDataURL(image); //将文件读取为 DataURL,也就是base64编码
        reader.onload = function (ev) { //文件读取成功完成时触发
          let dataURL = ev.target.result; //获得文件读取成功后的DataURL,也就是base64编码
          
//          this.avatarImagePreview = dataURL
//          this.tag_avatarImagePreview = true
//          console.log(this.avatarImagePreview)
        }
      },
      handleMenuClick(e){
				for(var i=0;i<this.backgroudOption.length;i++){
					if(this.backgroudOption[i].key==e.key){
            this.backgroudstyle = "background-image: url('" + this.backgroudOption[i].imgsrc + "')"
            break
          }
        }
      },
      
			comfirmEditUserInfo(isresetpwd){
				this.vkspservice.updateUser(this.editUsreinfo).then(res=>{
					if(res.status==200){
	          this.$message.success('编辑成功！')
						if(isresetpwd){
							var exdate=new Date()
		  				exdate.setTime(exdate.getTime()+24*60*60*1000*7)
							window.document.cookie = "isLogin" + "=false" + ";path=/;expires=" + exdate.toGMTString()
		          this.$router.push('/sign-in')	
						}else{
							var exdate=new Date()
        			exdate.setTime(exdate.getTime()+24*60*60*1000*7)
        
        			//字符串拼接cookie
        			window.document.cookie = "userinfo" + "=" + JSON.stringify(this.editUsreinfo) + ";path=/;expires=" + exdate.toGMTString()
							this.userinfo.username = this.editUsreinfo.username
							this.userinfo.avatarurl = this.editUsreinfo.avatarUrl
							this.userinfo.personalSignature = this.editUsreinfo.signature
						}
	        }else{
						this.$message.error('编辑失败！')
	        }
	      })
			},
			
			resetPassword_confirm(){
				if(sha512(this.resetpwddata.old_pwd) != this.editUsreinfo.password){
					this.$message.error("旧密码不正确！！！")
					return
				}
				
				if(this.resetpwddata.new_pwd != this.resetpwddata.new_pwd_confirm){
					this.$message.error("两次输入密码不匹配！")
					return
				}
				
				this.editUsreinfo.password = sha512(this.resetpwddata.new_pwd)
				
				this.comfirmEditUserInfo(true)
			},
			
			getAllMyConcept(){
				this.vkspservice.getConceptsByUserid(this.editUsreinfo.id).then(res=>{
					this.myConceptData = res.data
          this.myConceptData_copy = JSON.parse(JSON.stringify(this.myConceptData))//深拷贝
          
          this.myConceptData_structure = this.handleCollectionConceptStructure(this.myConceptData_copy)
          //console.log(this.myConceptData_structure)
				})
			},
			
			getAllCollectionConcept(){
				this.vkspservice.getCollectionConceptByUserId(this.editUsreinfo.id).then(res=>{
					res.data.forEach(item=>{
							this.myCollectionConceptData.push(item.likeConcept)
					})
          this.myCollectionConceptData_copy = JSON.parse(JSON.stringify(this.myCollectionConceptData))//深拷贝
				})
			},
			
			handleLinkToDetailPage(id){
				//window.location.href = 'http://172.25.13.72/dmpluginview/conceptdetail?conceptid=' + id
				window.open('http://172.25.13.72/dmpluginview/conceptdetail?conceptid=' + id, '_blank')
			},
      
      /*搜索文章，0为已发布，1为已收藏*/
      onSearchConcept(type){
        var searchTable=[]
        var _data = type==0?this.myConceptData_copy:this.myCollectionConceptData_copy
        var searchInput = type==0?this.inputSearchContent.publishedConcept:this.inputSearchContent.collectionConcept

        _data.forEach(element => {
          if((element.header ||'').toLowerCase().indexOf((searchInput||'').toLowerCase())!=-1){
						searchTable.push(element)
					}
				});

				if(type==0){
  				this.myConceptData=searchTable
        }else{
					this.myCollectionConceptData=searchTable
        }
      },
      
      /*根据所选年份,月份，类型过滤文章*/
      handleChange_Filter(){
        var searchTable=[]
        var _data = this.myConceptData_copy

        _data.forEach(element => {
          
          var pulishtime = element.create_Date.split('T')[0].split('-')
          console.log(pulishtime)
          if(
            (this.myConceptFilter.selectYear == '0' || pulishtime[0]==this.myConceptFilter.selectYear)
            && (this.myConceptFilter.selectMonth == '0' || pulishtime[1]==this.myConceptFilter.selectMonth)
          )
          {
						searchTable.push(element)
					}
				});
        
  			this.myConceptData=searchTable
      }
		},
		
		mounted(){
			if(this.editUsreinfo.id){
				this.getAllMyConcept()
				this.getAllCollectionConcept()	
			}
			
		}
		
	})

</script>

<style lang="scss" scoped>
.ql-editor{
	height:500px;
}
</style>