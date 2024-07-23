<template>
  <div class="conceptDetailClass">
			
      <a-drawer
			placement="left"
			closable
			:visible="detailDrawerVisible"
			:height="drawerHeight"
      width="1800"
			@close="onClodeDrawer"
		>
      <div>
				<div class="left">
  				<a-tabs v-model="activeKey">
          <a-tab-pane key="1" tab="查看数据"> 
            <div class="filearea">
              <a-descriptions :column="1" bordered size="small" style="margin:10px 10px 0 0;">
                <a-descriptions-item label="所属模块">
                  <a-tag color="blue">{{selectConcept.module}}</a-tag>
                </a-descriptions-item>
                <a-descriptions-item label="创建人员">
                  <a-tag color="green">{{selectConcept.creater}}</a-tag>
                </a-descriptions-item>
                <a-descriptions-item label="创建时间">
                  <a-tag color="cyan">{{timeFormate(selectConcept.create_Date || '')}}</a-tag>
                </a-descriptions-item>
              </a-descriptions>
              <a-descriptions :column="1" bordered size="small" style="margin:10px 10px 0 0;">
                <a-descriptions-item >
                  <template #label>附件 <a-icon type="link"></a-icon></template>
                  <div v-for="file in conceptFiles" :key="file">
                    <a-icon type="file" style="margin-right:5px;"></a-icon>
										<a :href="file.downloadUrl">{{file.fileName}} ({{file.szie}}M)</a>
                  </div>
                </a-descriptions-item>
              </a-descriptions>
              <div style="float:bottom;margin-left:15px;">
								<a-avatar src="images/face-1.jpg"></a-avatar>
                <a-avatar style="margin-left:-15px;background-color:#1890ff">V</a-avatar>
                <a-tooltip title="Vico">
									<a-avatar src="images/Vico.jpg"></a-avatar>
                </a-tooltip>
                
                <span style="margin-top:5px;"><b> Vico 等10086人觉得很赞！</b></span> 
								<a-icon style="color:#eb2f96;font-size:20px;" class="exampleClass" type="heart"></a-icon>	
                <v-number :speed="2000" v-model="someNumber"></v-number>
              </div>
            </div>
            <div class="contentarea">
              <span style="font-size:20px;margin-top:20px;"><b>{{selectConcept.header}}</b></span>
              <a-tooltip title="Copy">
                <a-icon type="copy" style="margin-left:10px;" theme="twoTone" two-tone-color="#52c41a" @click="doCopy(selectConcept.header)"></a-icon>
              </a-tooltip>
              <div style="float:right;">
							<a-tooltip title="查看上一条 ( 快捷键Ctrl + <- )">
                <a-button type="link" @click="lookBackOne()">
                  <a-icon type="left"></a-icon>
                  Back
                </a-button>
              </a-tooltip>
              <a-tooltip title="查看下一条 ( 快捷键Ctrl + -> )">
                <a-button type="link" @click="lookNextOne()">
                  Next
                  <a-icon type="right"></a-icon>
                </a-button> 
              </a-tooltip>	
              </div>
              <p></p>
                <a-card class="header-solid h-full" style="height:850px;overflow: auto;">
                  <p style="white-space: pre-line;" v-html="selectConcept.contents" v-if="!loading"></p>
                  <div v-else>
                    <!--<a-icon type="loading"></a-icon> Loading...-->
                    <a-skeleton active/>
                  </div>
                </a-card>
              
              <!--<p></p>
              <span> 所属模块： </span><a-tag color="blue">{{selectConcept.module}}</a-tag>
              <span> 创建人: </span><a-tag color="green">{{selectConcept.creater}}</a-tag>
              <span> 创建时间: </span>
              <a-tag color="cyan">{{timeFormate(selectConcept.create_Date || '')}}</a-tag>
              <div style="float:right;margin-top:-10px;">
								<a-avatar src="images/face-1.jpg"></a-avatar>
                <a-avatar style="margin-left:-15px;background-color:#1890ff">V</a-avatar>
                <a-tooltip title="Vico">
									<a-avatar src="images/Vico.jpg"></a-avatar>
                </a-tooltip>
                
                <span style="margin-top:5px;"><b> 3 人觉得很赞！</b></span> 
								<a-icon style="color:#eb2f96;font-size:20px;" class="exampleClass" type="heart"></a-icon>	
              </div>-->
								
            </div>
          </a-tab-pane>
          <a-tab-pane key="2" tab="查看完整Json数据">
            <div>
              <json-viewer 
                :value="selectConcept"
                :expand-depth="3"
                copyable
                boxed
                :expanded="true"
                style="height:850px;overflow: auto;"
                >
              </json-viewer>
            </div>
            <a-popconfirm
              title="确定要删除改条数据吗？"
              ok-text="确定"
              cancel-text="取消"
              @confirm="deleteConcept(selectConcept.id)"
            >
              <a-button typr="link">删除</a-button>
            </a-popconfirm>
          </a-tab-pane>
          <a-tab-pane key="3" tab="编辑">
            <div class="filearea">
              <a-descriptions :column="1" bordered size="small" style="margin:10px 10px 0 0;">
                <a-descriptions-item >
                  <template #label>附件 <a-icon type="link"></a-icon></template>
                  <div v-for="file in conceptFiles" :key="file">
                    <a >{{file.fileName}} ({{file.szie}}M)</a>
                    <a-popconfirm
                      title="确定要删除该附件吗？"
                      ok-text="确定"
                      cancel-text="取消"
                      @confirm="deleteConceptFile(file.id)"
                    >
                      <a-icon type="delete" style="float:right;"></a-icon>
                    </a-popconfirm>
                  </div>
                  <a-upload style="margin-left:0px;" 
                    :before-upload="beforeUpload"
                  >
                    <a-button type="dashed" size="small">
                      <a-icon type="upload"></a-icon>
                      添加附件
                    </a-button>
                  </a-upload> 
                  <a-button type="dashed" size="small" shape="round" @click="handelFileUpload()" v-if="appendfileList.length!=0">
										<a-icon type="plus"></a-icon>上传
                  </a-button>
                  
                </a-descriptions-item>
              </a-descriptions>
            </div>
            <div class="contentarea">
            <a-form
            id="components-form-demo-normal-login"
            class="login-form"
            :hideRequiredMark="true"
          >
            <a-form-item class="mb-10" :colon="false">
              <span >所属模块：</span>
              <a-input 
              style="width:200px;"
              v-model="editorselectConcept.module"
              v-decorator="[
              '所属模块',
              { rules: [{ required: true, message: '请输入所属模块！' }] },
              ]" placeholder="Module" />
              <span style="margin-left:50px;">标题名称：</span>
              <a-input
              style="width:600px;"
              v-model="editorselectConcept.header"
              v-decorator="[
              '标题',
              { rules: [{ required: true, message: '请输入标题' }] },
              ]" placeholder="Title" />
              <a-switch style="margin-left:20px;" checked-children="公开" un-checked-children="私密" :checked='editorselectConcept.isPrivate==0 || editorselectConcept.isPrivate==null' @change="onChangeIsPrivate"/>
              <a-button style="margin-left:50px;" v-if="!addForm_preview" @click="addForm_preview=!addForm_preview">预览</a-button>
              <a-button style="margin-left:50px;" v-else @click="addForm_preview=!addForm_preview">编辑</a-button>
            </a-form-item>
            <br/>
            <a-form-item class="mb-5" :colon="false">
              <div >
                <quill-editor
                style="height:690px;"
                  v-if="!addForm_preview"
                  ref="myQuillEditor"
                  v-model="editorselectConcept.contents"
                  :options="editorOption"
                  @focus="onEditorFocus($event)"
                  @ready="onEditorReady($event)"/> 	
                <a-card v-else class="header-solid h-full" style="height:750px;overflow: scroll;">
                  <p style="white-space: pre-line;" v-html="editorselectConcept.contents"></p>
                </a-card>
              </div>
            </a-form-item>
            <a-form-item style="margin-top:70px;">
            	<a-button type="primary" block @click="saveEdit()" class="login-form-button">
            		提交修改
            	</a-button>
            </a-form-item>
            </a-form>
          </div>
          </a-tab-pane>
        </a-tabs>	
        </div>
        
        <!--<div class="right">
          <div style="margin-left:15px;">
            <h6>全部评论 {{conceptComments.length}}</h6>
            <a-divider></a-divider>
            <a-card :bordered="false" :bodyStyle="{padding: 0,}" style="background-color:#f8f8f8;border-radius:20px;">
              <a-textarea class="myinputstyle" placeholder="说点什么吧..." :auto-size="{minRows:2,maxRows:5}" v-model="newComment.commentcontent"></a-textarea>
              <a-button class="mybuttonstyle" @click="addComment()"> 评论 </a-button>
            </a-card>
            <div style="height:650px;overflow: auto;">
              <div style="margin-top:15px;" v-for="item in conceptComments" :key="item">
                <p></p>
                <a-avatar style="background-color:#1890ff;margin-left:15px;" >{{item.commentperson.charAt(0).toUpperCase()}}</a-avatar>
                <span style="margin-left:10px;">{{item.commentperson}}</span><a-tag style="margin-left:10px;" v-if="item.commentperson===selectConcept.creater" color="blue">作者</a-tag>
                <div style="margin-left:41px;margin-top:5px;">
                  <p style="white-space: pre-line;"><b>{{item.commentcontent}}</b></p>
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
											<a-tooltip title="删除评论"  v-if="item.commentperson===loginUser.username">
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
        </div>-->
        
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

import JsonViewer from 'vue-json-viewer'
import { VNumber } from "@maxflex/v-number"
import VkspService from '../service/VkspService'
import ConceptFileService from '../service/ConceptFileService'


import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'
//import * as quillEmoji from 'quill-emoji'
//import 'quill-emoji/dist/quill-emoji.css'
import { quillEditor, Quill } from 'vue-quill-editor'
import ImageResize from 'quill-image-resize-module' 
import {ImageDrop} from 'quill-image-drop-module' 
Quill.register('modules/imageResize', ImageResize)
Quill.register('modules/imageDrop', ImageDrop)
//Quill.register('modules/quillEmoji',quillEmoji)

import { myNotification } from '../../mixin/notification'
import {VkspUser} from '../../mixin/vkspuser'

export default {
  mixins:[myNotification,VkspUser],
  props:{
  },
  components:{
		CopyOutlined,
    LeftOutlined,
    RightOutlined,
    HeartOutlined,
    MessageOutlined,
    JsonViewer,
    quillEditor,VNumber
  },
  data(){
    const vkspservice=new VkspService()
    const conceptFileService = new ConceptFileService()
    return{
      isLogin:false,
      isAuthor:false,
      vkspservice,
      conceptFileService,
      selectConcept:{},
      loading:false,
      conceptComments:[],
      conceptFiles:[],
      newComment:{
				commentperson:'Vico',
        commentcontent:'',
        commenttime:'',
        conceptid:0
      },
      editorselectConcept:{},
      detailDrawerVisible:false,
      addForm_preview:false,
      
      activeKey:'3',
      someNumber:10086,
       
      editorOption:{
        placeholder:'编辑Concept',
				modules:{
					toolbar:[
            ['bold', 'italic', 'underline', 'strike'], // 字体
            ['blockquote', 'code-block'], 
            [{ 'header': 1 }, { 'header': 2 }], // 样式标题
            [{ 'list': 'ordered'}, { 'list': 'bullet' }],
            [{ 'script': 'sub'}, { 'script': 'super' }], // 下标、上标
            [{ 'indent': '-1'}, { 'indent': '+1' }], // 缩进
            [{ 'direction': 'rtl' }],
            //[{ 'size': ['small', false, 'large', 'huge'] }], // 字体
            [{ 'header': [1, 2, 3, 4, 5, 6, true] }],
            [{ 'color': [] }, { 'background': [] }],
            [{ 'font': [] }],
            [{ 'align': [] }],
            ['clean'], // 格式清除 
            ["link", "image", "video"], // 链接、图片、视频
            //['emoji']
          ],
          imageResize:{
						displayStyles:{
							backgroundColor:'black',
              border:'none',
              color:'white'
            },
            modules:['Resize', 'DisplaySize', 'Toolbar']
          },
          imageDrop:true, 
          //'emoji-toolbar':true,
          //'emoji-shortname':true,
        }
      },

      replyComment:{
				commentid:0,
        commentperson:'',
        commentcontent:'',
        commenttime:'',
        conceptid:''
      },
      
      appendfileList:[],//追加上传附件
    }
  },
  
  computed:{
    //动态设置高度
    drawerHeight: function(){
			return window.innerHeight
    },
    //动态设置高度
    contentcardStyle: function(){
			return 'height:' + (window.innerHeight - 200) + 'px' + ';overflow: auto;'
    },
  },
  
  mounted(){
		//事件总线
		EventBus.$on('ConceptDetailVisible',visible=>{
			this.detailDrawerVisible=visible
		})
    EventBus.$on('Concept',data=>{
      this.loading=true
      this.activeKey="3"
      this.selectConcept=data
      this.getConceptDetail(data.id)
      this.getConceptComments(data.id)
      this.getConceptFile(data.id)
		})  
    
    this.keyDown()
  },
  
  created(){},
  
  methods:{

    getConceptDetail(id){
			this.vkspservice.getConceptDetail(id).then(res=>{
        this.selectConcept=res.data
        this.editorselectConcept=JSON.parse(JSON.stringify(res.data))//深拷贝
        this.loading=false
      })
    },
    
    getConceptComments(conceptid){
			this.vkspservice.getConceptComments(conceptid).then(res=>{
        this.conceptComments=res.data
      })
    },
    
    getConceptFile(conceptid){
      this.conceptFiles=[]
			this.conceptFileService.getFileByConceptId(conceptid).then(res=>{
				
        res.data.forEach(filelog=>{
					this.conceptFiles.push(filelog)
        })
      })
    },
    
		onClodeDrawer(){
			this.detailDrawerVisible=false
		},
    
    //复制文本 插件：vue-clipboard2
    doCopy(value){
			this.$copyText(value).then(()=>{
				this.$message.success("复制成功:"+value)
      })
    },
    
    timeFormate(publishtime){
      const dates = new Date(publishtime);
      
      const year = dates.getFullYear();//year
			const month = (dates.getMonth() + 1) < 10 ? '0' + (dates.getMonth() + 1) : (dates.getMonth() + 1);//month
			const day = dates.getDate() < 10 ? '0' + dates.getDate() : dates.getDate();
	
			const hour = dates.getHours()+8;//北京时间
			const minute = dates.getMinutes() < 10 ? '0' + dates.getMinutes() : dates.getMinutes();
			const seconds = dates.getSeconds()<10 ? '0' + dates.getSeconds() : dates.getSeconds();
			
			var week = '周一';
			switch(dates.getDay()){
				case 2:week='周二';break;
				case 3:week='周三';break;
				case 4:week='周四';break;
				case 5:week='周五';break;
				case 6:week='周六';break;
				case 7:week='周天';break;
				default:week='周一';break;
			}

      return year + '-' + month + '-' + day + ' ' + hour + ':' + minute + ' / ' + week;
    },
    
    saveEdit(){
      
      console.log('log:',this.editorselectConcept.contents)
      
      const str_content = this.editorselectConcept.contents
      const regex = /<h[1-6]>(.*?)<\/h[1-6]>/g;
      
      var match
      var menuTree = []
      while((match = regex.exec(str_content)) !== null){
        menuTree.push(match[0])
      }
      
      menuTree.forEach(item=>{
        var placeholder = document.createElement('div')
        placeholder.innerHTML = item
        
        if(!placeholder.firstElementChild.id){//已存在锚点的不添加 
          //TODO：相同锚点值的情况
          //标题字符串处理，保证锚点正确
          var str64 = window.btoa(window.encodeURIComponent(placeholder.firstElementChild.innerHTML))
          //移除特殊字符
          str64 = str64.replaceAll('=', '')
          //生成随机数转成36进制，再截取后8位
          var str_random = Math.random().toString(36).slice(-8)

          str64 = str64 + str_random
          
          var rep = item.replaceAll('>'+placeholder.firstElementChild.innerHTML+'<', ' id=\'' + str64 + '\'>'+placeholder.firstElementChild.innerHTML+'<')
          this.editorselectConcept.contents = this.editorselectConcept.contents.replace(item, rep)	
        }
      })
      console.log(this.editorselectConcept)
        
      this.editorselectConcept.contents = this.editorselectConcept.contents.replaceAll('<img src=','<img preview=\'' +this.editorselectConcept.create_Date+ '\' src=')
      this.vkspservice.updateConcept(this.editorselectConcept).then(res=>{
				if(res.data){
					//this.$message.success("更新成功!")
          this.myNotification_success('更新成功!')
          this.selectConcept=JSON.parse(JSON.stringify(this.editorselectConcept))//深拷贝
          EventBus.$emit('refreshConcept')
        }else{
          this.myNotification_error("更新失败!")
        }
      }).catch(err=>{
        this.myNotification_info("更新失败:"+err.message)
      })
    },
    
    addComment(){
      if(!this.isLogin){
        this.$message.warning("需要登录之后才能进行评论!")
				return
      }
      if(this.newComment.commentcontent!=''){
        this.newComment.commenttime=new Date()
        this.newComment.conceptid=this.selectConcept.id
        console.log(this.newComment)
        this.vkspservice.addComment(this.newComment).then(res=>{
          if(res.data){
            this.newComment.commentcontent=''
            this.getConceptComments(this.selectConcept.id)
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
//        //左
//        if (e1 && e1.keyCode == 37) {
//          // 按下左箭头
//          switch(this.activeKey){
//						case '1': this.activeKey='3'; break
//            case '2': this.activeKey='1'; break
//            case '3': this.activeKey='2'; break
//          }
//        } else if (e1 && e1.keyCode == 39) {
//          // 按下右箭头
//          switch(this.activeKey){
//						case '1': this.activeKey='2'; break
//            case '2': this.activeKey='3'; break
//            case '3': this.activeKey='1'; break
//          }
//        } 
        if (e1.ctrlKey && e1.keyCode == 37 ) {
          // 按 ctrl + <- 箭头
          this.lookBackOne()
        } else if (e1.ctrlKey && e1.keyCode == 39) {
          // 按 ctrl + -> 箭头
          this.lookNextOne()
        }
      }
    },
    
    deleteConcept(id){
			this.vkspservice.deleteConcept(id).then(res=>{
				if(res.data){
					this.$message.success("删除成功!")
          this.detailDrawerVisible=false
          //EventBus.$emit('refreshConcept')
          this.$router.push('/concept')
        }else{
					this.$message.error("删除失败!")
        }
      })
    },
    
    deleteComment(commentid){
			this.vkspservice.deleteComment(commentid).then(res=>{
				if(res.data){
					this.getConceptComments(this.selectConcept.id)
        }
      })
    },
    
    deleteConceptFile(fileid){
			this.conceptFileService.deleteConceptFile(fileid).then(res=>{
				if(res.data){
					this.$message.info("成功移除附件！")
          this.getConceptFile(this.selectConcept.id)
        }
      })
    },
    
    beforeUpload(file){
      this.appendfileList.push(file)
			return false
    },
    
    //文件上传
      handelFileUpload(){
        if(this.appendfileList.length===0){
					this.$message.info('请先选择上传文件！')
          return 
        }else{
          const formDate=new FormData();
          
          this.appendfileList.forEach(file=>{
            formDate.append('vkspfiles',file)
          })
          
          var fileUploadUrl = "http://172.25.13.72/api.pluginViewer/api/file/upload"
          axios.post(fileUploadUrl,formDate)
          .then(res=>{
            if(res.data.success){
              this.$message.success("附件上传成功！")
              this.addDbFileLog(this.selectConcept.id,res.data)
              this.appendfileList=[]
            }else{
              this.$message.error("上传文件失败！",err)
            }
          })
          .catch(err=>{
            this.$message.error("上传失败！",err)
          })
        }
      },
      
      addDbFileLog(conceptid, fileResult){
				console.log(conceptid,fileResult)
        fileResult.fileNameList.forEach(file=>{
					var newConceptFile={
            fileName: file, 
            suddix: "",
            conceptid: conceptid,
            szie: 0,
            downloadUrl: fileResult.filePath.replace("D:/HttpFileServer","http://172.25.13.72:8880") + "/" + file
          }
          
          this.conceptFileService.addconceptfile(newConceptFile).then(res=>{
            this.getConceptFile(this.selectConcept.id)
          })
        })
      },
    
    like(){
			
    },
    
    onChangeIsPrivate(checked){
      this.editorselectConcept.isPrivate = checked?0:1
    },
    
    //commentid:要回复的评论
    reply(commentid){
      if(this.replyComment.commentid==commentid){
				this.replyComment.commentid=-1
      }else{
				this.replyComment.commentid=commentid
      }
    }
    
  }
}
</script>

<style>
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

/*.ql-editor{
	height:600px;
}*/

.left{
	float:left;
  width:75%;
}
.left .filearea{
	float:left;
  width:20%;
}
.left .contentarea{
	float:right;
  width:80%;
}
.right{
	float:left;
  width:25%;
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
  margin:15px 15px 15px 0;
	background-color:#c5c5c5;
  border:none;
  outline:medium;
  width:90px;
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
}
.ant-avatar:hover {
    transform: scale(1.20);
}
.ant-avatar:active {
    transform: scale(0.9);
}

conceptDetailClass{
	height: 100%;
}*/

</style>
