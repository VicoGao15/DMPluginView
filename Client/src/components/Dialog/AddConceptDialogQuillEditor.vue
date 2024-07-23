<template>
  <div>
		<a-modal 
      :visible="addConceptDialogVisible" 
      title="新增Concept" 
      @ok="addConceptSave()"
      @cancel="addConceptDialogVisible=false"
      :closable="false"
      width="1200px"
    >
      <a-form
					id="components-form-demo-normal-login"
					class="login-form"
					:hideRequiredMark="true"
				>
					<a-form-item class="mb-10" :colon="false">
            
            <!--<span >标题名称：</span>-->
						<a-input
            style="width:500px;border-top-width:0;border-left-width:0;border-right-width:0;"
            v-model="addConcept.header"
            v-decorator="[
            '标题',
            { rules: [{ required: true, message: '请输入标题' }] },
            ]" placeholder="标题" />
            
            
            <!--<span style="margin-left:20px;">所属模块：</span>-->
						<a-input 
            style="width:150px;margin-left:10px;border-top-width:0;border-left-width:0;border-right-width:0;"
            v-model="addConcept.module"
						v-decorator="[
						'所属模块',
						{ rules: [{ required: true, message: '请输入所属模块！' }] },
						]" placeholder="知识专栏" />
            <a-switch style="margin-left:20px;" checked-children="公开" un-checked-children="私密" default-checked @change="onChangeIsPrivate"/>
            <!--<span style="margin-left:50px;">创建人员：</span>
            <a-input
              style="width:200px;"
              v-model="addConcept.creater"
            >
            </a-input>-->
            <!--<a-button style="margin-left:50px;" @click="addUserDialogVisible=true">添加User</a-button>-->
            <a-button style="margin-left:20px;" v-if="!addForm_preview" @click="addForm_preview=!addForm_preview">预览</a-button>
            <a-button style="margin-left:20px;" v-else @click="addForm_preview=!addForm_preview">编辑</a-button>
            <a-button style="margin-left:20px;" @click="addConcept.contents=''">清空内容</a-button>
					</a-form-item>
          <a-form-item class="mb-5" :colon="false">
            <quill-editor
              v-if="!addForm_preview"
              ref="myQuillEditor"
              v-model="addConcept.contents"
              style="height:550px;"
            />
            <a-card v-else class="header-solid h-full" style="height:600px;overflow: auto;">
              <p style="white-space: pre-line;" v-html="addConcept.contents"></p>
            </a-card>
            <p></p>
					</a-form-item>
          <div class="parent" v-if="!addForm_preview">
  					<div class="div1">
  						<a-upload style="margin-left:0px;" @change="importContentFromFile" >
                <a-button type="dashed">
                  <a-icon type="upload"></a-icon>
                  从文件导入内容(json/txt/xlsx/docx)
                </a-button>
              </a-upload>
            </div>	
            <div class="div2">
							<a-upload style="margin-left:0px;" 
                :before-upload="beforeUpload"
              >
                <a-button type="dashed">
                  <a-icon type="upload"></a-icon>
                  添加附件
                </a-button>
              </a-upload>
            </div>
          </div>
				</a-form>
        
    </a-modal>
  </div>
</template>

<script>
import VkspService from '../service/VkspService'
import ConceptFileService from '../service/ConceptFileService'
import {notification} from 'ant-design-vue'
import PlusOutlined from '@ant-design/icons-vue'
import UploadOutlined from '@ant-design/icons-vue'
import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'
import { quillEditor } from 'vue-quill-editor'
import ImageResize from 'quill-image-resize-module' 
//import Quill from 'quill'
//import Table from 'quill/modules/table'
//Quill.register(Table, true)
import { container,ImageExtend,QuillWatch } from 'quill-image-extend-module'
Quill.register('modules/imageResize', ImageResize)
Quill.register('modules/ImageExtend', ImageExtend)
import 'animate.css'
import axios from 'axios'
import {EventBus} from '../../event-bus.js'
import {VkspUser} from '../../mixin/vkspuser'
import {system_options} from '../../mixin/options'

export default {
  mixins:[VkspUser,system_options],
  components:{
		quillEditor,
    PlusOutlined,UploadOutlined,
  },
  data(){
		return{
			addConceptDialogVisible:false,
      addForm_preview:false,
      
      vkspService : new VkspService(),
      conceptFileService : new ConceptFileService(),
      
      addConcept:{
        module: "Vue",
        creater: "Vico",
        create_Date: "2022-12-2T00:00:00",
        header: "",
        contents: "",
        likes: 0,
        views: 0,
        createrId:0,
        isprivate:0
      },
      
      fileList:[],
      //userinfo:{}
    }
  },
  
  mounted(){
		EventBus.$on('addConcept',()=>{
			this.addConceptDialogVisible = true
      //获取登录用户id作为作者
      this.addConcept.createrId = this.getLoginMessage().id
    })
    
    EventBus.$on('fileloadcomplete',()=>{
			//获取mixin中读取的文件内容赋值
			this.addConcept.contents = this.fileContents
    })
  },
  
  methods:{
		addConceptSave(){
      this.addConcept.create_Date = new Date();
      
      //判断title最大长度
      if(this.addConcept.header.length>100){
        this.$message.info("标题已超过最大长度100个字符！")
				return
      }
      
      const str_content = this.addConcept.contents
      const regex = /<h[1-6]>(.*?)<\/h[1-6]>/g;
      
      var match
      var menuTree = []
      while((match = regex.exec(str_content)) !== null){
        
        menuTree.push(match[0])
      }
      
      menuTree.forEach(item=>{
        var placeholder = document.createElement('div')
        placeholder.innerHTML = item
        
        //标题字符串处理，保证锚点正确
        var str64 = window.btoa(window.encodeURIComponent(placeholder.firstElementChild.innerHTML))
        //移除特殊字符
        str64 = str64.replaceAll('=', '')
        //生成随机数转成36进制，再截取后8位
        var str_random = Math.random().toString(36).slice(-8)

        str64 = str64 + str_random
        
        var rep = item.replaceAll('>'+placeholder.firstElementChild.innerHTML+'<', ' id=\'' + str64 + '\'>'+placeholder.firstElementChild.innerHTML+'<')
				
        this.addConcept.contents = this.addConcept.contents.replace(item, rep)
      })

      this.addConcept.contents = this.addConcept.contents.replaceAll('<img','<img preview=\'' +this.addConcept.create_Date.getTime()+ '\'')
      
      this.vkspService.addConcept(this.addConcept).then(res=>{
				if(res.data){
          this.$message.success("添加成功："+this.addConcept.header)
          //上传附件
          this.handelFileUpload(res.data)

					this.addConceptDialogVisible=false
          
          EventBus.$emit('refreshConcept')
        }else{
					this.$message.error("添加失败！")
        }
      })
    },
    
    beforeUpload(file){
      this.fileList.push(file)
			return false
    },
    //文件上传
    handelFileUpload(conceptid){
      if(this.fileList.length===0){
				//this.$message.info('请先选择上传文件！')
        return 
      }else{
        const formDate=new FormData();
        
        this.fileList.forEach(file=>{
          formDate.append('vkspfiles',file)
        })
        
        var fileUploadUrl = "http://172.25.13.72/api.pluginViewer/api/file/upload"
        axios.post(fileUploadUrl,formDate)
        .then(res=>{
          if(res.data.success){
            this.$message.success("附件上传成功！")
            this.addDbFileLog(conceptid,res.data)
          }else{
            this.$message.error("上传文件失败！",err)
          }
        })
        .catch(err=>{
          this.$message.error("上传失败！",err)
        })
        
        this.fileList=[]
      }
    },
    
    addDbFileLog(conceptid, fileResult){
			//console.log(conceptid,fileResult)
      fileResult.fileNameList.forEach(file=>{
				var newConceptFile={
          fileName: file, 
          suddix: "",
          conceptid: conceptid,
          szie: 0,
          downloadUrl: fileResult.filePath.replace("D:/HttpFileServer","http://172.25.13.72:8880") + "/" + file
        }
        
        this.conceptFileService.addconceptfile(newConceptFile).then(res=>{
					//console.log(res.data)
        })
      })
    },
    
    importContentFromFile(file){
      //mixin中混入的方法
      this.importFile(file)
    },
    
    onChangeIsPrivate(checked){
      this.addConcept.isprivate = checked?0:1
    }
    
  }
}
</script>

<style scoped>
.parent{
  margin-top:60px;
	display: grid;
  grid-template-columns: repeat(5,1fr);
  grid-template-rows: repeat(5,1fr);
  grid-column-gap: 10px;
  grid-row-gap: 0px;
  height: 50px;
}
.div1{
	grid-area: 1/1/2/2;
}
.div2{
	grid-area: 1/2/2/4;
}
</style>