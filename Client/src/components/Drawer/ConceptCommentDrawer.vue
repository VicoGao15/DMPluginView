<template>
  <div class="conceptDetailClass">
      <a-drawer
			placement="right"
			:visible="commentDrawerVisible"
			width="600"
			@close="onClodeDrawer"
      closable
		>
      <div style="margin-left:5px;">
        <h6>全部评论 {{conceptComments.length}}</h6>
        <a-divider></a-divider>
        <a-card :bordered="false" :bodyStyle="{padding: 0,}" style="background-color:#f8f8f8;border-radius:10px;">
          <!--<a-textarea class="myinputstyle" placeholder="说点什么吧..." :auto-size="{minRows:2,maxRows:5}" v-model="newComment.commentcontent" @blur="blurEvent"></a-textarea>-->
          <!--<div ref="editor" contenteditable="true" class="textareaSty" v-on:input="getData" v-html="newComment.commentcontent" placeholder="说点什么吧..." @blur="blurEvent"></div>-->
          <div ref="editor" contenteditable="true" class="textareaSty" v-on:input="getData" placeholder="说点什么吧..." @blur="blurEvent"></div>
          
          <a-button class="mybuttonstyle" @click="addComment()"> 评论 </a-button>
          <!--<span @click="showEmojiDialog=!showEmojiDialog" @mouseover="showEmojiDialog=true" style="font-size:22px;float:right;cursor:pointer;margin:20px 20px 0 0;">😁</span>-->
        </a-card>
        <!--<VEmojiPicker 
          style="position:absolute;margin-left:50px;"
          v-show="showEmojiDialog"
          @select="insertEmojiHandle"
          @mouseleave="showEmojiDialog=!showEmojiDialog"
        />-->
        <div style="height:650px;overflow: auto;">
          <div style="margin-top:15px;" v-for="item in conceptComments" :key="item.id">
            <p></p>
            <a-avatar v-if="item.userInfo.avatarUrl!=''" style="margin-left:15px;" shape="circle" :src="item.userInfo.avatarUrl"/>
            <a-avatar v-else style="background-color:#1890ff;margin-left:15px;" >{{item.commentperson.charAt(0).toUpperCase()}}</a-avatar>
            <span style="margin-left:10px;">{{item.userInfo.username}}</span><a-tag style="margin-left:10px;" v-if="item.commentperson===selectConcept.creater" color="blue">作者</a-tag>
            <div style="margin-left:41px;margin-top:5px;">
              <p style="white-space: pre-line;" v-html="item.commentcontent"></p>
              <div style="margin-top:-10px;">
                <span><i>{{timeFormate(item.commenttime || '')}}</i></span>
                <a-tooltip title="赞">
                  <a-icon type="heart" class="exampleClass" @click="like()" style="color: #eb2f96;"></a-icon>
                </a-tooltip>
                <a-tooltip title="回复">
                  <a-icon type="message" class="exampleClass" style="margin-left:-25px;color: #48a8ff;" @click="reply(item.id)"></a-icon>
                </a-tooltip>
                
                <a-popconfirm
                  title="确定要删除该条评论吗？"
                  ok-text="OK"
                  cancel-text="Cancel"
                  @confirm="deleteComment(item.id)"
                >
									<a-tooltip title="删除评论"  v-if="loginuserinfo.id==item.userid">
                    <a-icon type="delete" class="exampleClass" style="margin-left:-25px;color: red;"></a-icon>
                  </a-tooltip>
                </a-popconfirm>
              </div>
              <a-card :bordered="false" :bodyStyle="{padding: 0,}" style="background-color:#f8f8f8;border-radius:20px;" v-if="item.id===replyComment.commentid">
                <a-textarea class="myinputstyle" placeholder="说点什么吧..." :auto-size="{minRows:2,maxRows:5}" v-model="replyComment.commentcontent"></a-textarea>
                <a-button class="mybuttonstyle"> 提交 </a-button>
              </a-card>
            </div>
          </div>	
        </div>
      </div>
		</a-drawer>
  </div>
</template>

<script> 
import axios from 'axios'
import {EventBus} from '../../event-bus.js'
import CopyOutlined from '@ant-design/icons-vue'
import LeftOutlined from '@ant-design/icons-vue'
import RightOutlined from '@ant-design/icons-vue'
import HeartOutlined from '@ant-design/icons-vue'
import MessageOutlined from '@ant-design/icons-vue'

import VkspService from '../service/VkspService'
import {VkspUser} from '../../mixin/vkspuser'

export default {
  mixins:[VkspUser],
  props:{
  },
  components:{
		CopyOutlined,
    LeftOutlined,
    RightOutlined,
    HeartOutlined,
    MessageOutlined,
  },
  data(){
    const vkspservice=new VkspService()
    return{
      commentDrawerVisible:false,
      vkspservice,
      isLogin:false,
      isAuthor:false,
      selectConcept:{},
      conceptComments:[],
      newComment:{
				commentperson:'Vico',
        commentcontent:'',
        commenttime:'',
        conceptid:0,
        userid:21
      },
      loginuserinfo:{},

      replyComment:{
				commentid:0,
        commentperson:'',
        commentcontent:'',
        commenttime:'',
        conceptid:''
      },
      
      showEmojiDialog:false,//是否展开表情选择dialog
      blurIndex:0, //光标位置，emoji插入位置
    }
  },
  
  mounted(){
		//事件总线
		EventBus.$on('ConceptCommentVisible',visible=>{
			this.commentDrawerVisible=visible
		})
    EventBus.$on('ConceptComments', conceptid=>{
      this.newComment.conceptid = conceptid
      this.getConceptComments(conceptid)
		})  
    
    this.keyDown()
    
    this.getLoginInfo()
  },
  
  methods:{
    
    //获取登录信息
    getLoginInfo(){
      this.isLogin = this.ifIsLogin()
      this.loginuserinfo = this.getLoginMessage()
      this.newComment.userid = this.loginuserinfo.id
    },
    
		onClodeDrawer(){
			this.commentDrawerVisible=false
		},
    
    getConceptComments(conceptid){
			this.vkspservice.getConceptComments(conceptid).then(res=>{
        this.conceptComments=res.data
        console.log(this.conceptComments)
        //this.getCommentsUserDetail()
      })
    },
    
    addComment(){
      if(!this.isLogin){
        this.$message.warning("需要登录之后才能进行评论!")
				return
      }
      if(this.newComment.commentcontent!=''){
        this.newComment.commenttime=new Date()
        this.newComment.userid = this.loginuserinfo.id
        this.vkspservice.addComment(this.newComment).then(res=>{
          if(res.data){
            this.newComment.commentcontent=''
            this.getConceptComments(this.newComment.conceptid)
          }
        })	
      }else{
				this.$message.info("请先输入评论内容！")
      }
			
    },
    
    lookBackOne(){ 
			EventBus.$emit('LookBackOne',this.selectConcept.id)
    },
    lookNextOne(){
			EventBus.$emit('LookNextOne',this.selectConcept.id)
    },
    
    // 监听键盘
    keyDown() {
      document.onkeydown = (e) => {
        //事件对象兼容
        let e1 = e || event || window.event || arguments.callee.caller.arguments[0]
        //键盘按键判断:左箭头-37;上箭头-38；右箭头-39;下箭头-40
        if (e1.ctrlKey && e1.keyCode == 37 ) {
          // 按 ctrl + <- 箭头
          this.lookBackOne()
        } else if (e1.ctrlKey && e1.keyCode == 39) {
          // 按 ctrl + -> 箭头
          this.lookNextOne()
        }
      }
    },
    
    //删除评论
    deleteComment(commentid){
			this.vkspservice.deleteComment(commentid).then(res=>{
				if(res.data){
					this.getConceptComments(this.newComment.conceptid)
        }
      })
    },
    
    //commentid:要回复的评论
    reply(commentid){
      if(this.replyComment.commentid==commentid){
				this.replyComment.commentid=-1
      }else{
				this.replyComment.commentid=commentid
      }
    },
    
    async getCommentsUserDetail(){
			await this.conceptComments.forEach(item=>{
        this.vkspservice.getUserInfoById(item.userid).then(res=>{
					item.userinfo = res.data
          console.log(item)
        })
				
      })
    },
    
    //评论人员
    getUserInfoByUserid(userid){
			this.vkspservice.getUserInfoById(userid).then(res=>{
				console.log(res.data)
      })
    },
    
    /*插入emoji*/
    insertEmojiHandle(emoji){
			var index = this.blurIndex
      var str = this.newComment.commentcontent
      //在光标位置插入emoji
      
      this.newComment.commentcontent = str.slice(0,index) + emoji.data + str.slice(index)
      //console.log(this.newComment.commentcontent)
      //表情每增加一个，光标位置+2
      this.blurIndex+=2
    },
    
    /*获取光标所在位置index*/
    blurEvent(e){
      console.log("blur")
			this.blurIndex = e.srcElement.selectionStart
    },
    
    getData(){
      //console.log(this.$refs.editor.innerHTML)
			this.newComment.commentcontent = this.$refs.editor.innerHTML
    },
    
  }
}
</script>

<style scope>
.drawer_des{
  /*color:blue;*/
	font-weight: bold; 
	font-size: 15px;
}

.editor-wrap {
width: 900px;
}
.editor-wrap > div {
width: 100%;
}
.fr-wrapper > div[style*='z-index:9999;'],
.fr-wrapper > div[style*='z-index: 9999;'] {
height: 0;
overflow: hidden;
}
.fr-box .second-toolbar #logo {
width: 0 !important;
height: 0 !important;
overflow: hidden;
}
.fr-box .fr-toolbar {
border-radius: 4px 4px 0 0;
border-color: #dcdfe6;
}
.fr-box .second-toolbar {
border-radius: 0 0 4px 4px;
border-color: #dcdfe6;
}
.fr-box .fr-wrapper {
border-color: #dcdfe6 !important;
}

.myinputstyle{
  margin:10px 0 0 0;
	background-color:#f8f8f8;
  border:none;
  outline:medium;
  border-radius:20px;
}
.mybuttonstyle{
  float:right;
  margin:12px;
  border-radious:24px;
}


.exampleClass {
cursor: pointer;
padding: 12px 24px;
backgroundColor: white;
color: black;
borderRadius: 7px;
transition: transform 250ms cubic-bezier(.2,.8,.4,1);
}
.exampleClass:hover {
transform: scale(1.20);
}
.exampleClass:active {
transform: scale(0.9);
}

/*.ant-avatar {
  margin-left:-15px;
	box-shadow: $shadow-3;
	border-radius: 25px;
  cursor: pointer;
  transition: transform 250ms cubic-bezier(.2,.8,.4,1);
}*/
.ant-avatar:hover {
    transform: scale(1.20);
}
.ant-avatar:active {
    transform: scale(0.9);
}

conceptDetailClass{
	height: 100%;
}

.textareaSty{
  margin:10px;
  width:100% - 10px;
	height:100px;
  resize:vertical;
  border:0px solid #d2d2d2;
  border-radius: 8px;
  overflow:auto;
  outline:none;
  font-weight:600;
  white-space: pre-line;
}
.textareaSty:empty:before{
	content:attr(placeholder);
  color:#c1c1c1;
  font-size:15px;
}
.textareaSty:focus:before{
	content:none;
}

</style>
