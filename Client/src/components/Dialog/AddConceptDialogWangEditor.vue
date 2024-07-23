<template>
    <div>
		<a-modal 
      :visible="addConceptDialogVisible_wang" 
      title="新增Concept" 
      @ok="addConceptSave()"
      @cancel="addConceptDialogVisible_wang=false"
      :closable="false"
      width="1200px"
    >
      <a-form
					id="components-form-demo-normal-login"
					class="login-form"
					:hideRequiredMark="true"
				>
					<a-form-item class="mb-10" :colon="false">
            
            <span >标题名称：</span>
						<a-input
            style="width:500px;"
            v-model="addConcept.header"
            v-decorator="[
            '标题',
            { rules: [{ required: true, message: '请输入标题' }] },
            ]" placeholder="Title" />
            
            <span style="margin-left:20px;">所属模块：</span>
						<a-input 
            style="width:150px;"
            v-model="addConcept.module"
						v-decorator="[
						'所属模块',
						{ rules: [{ required: true, message: '请输入所属模块！' }] },
						]" placeholder="Module" />
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
            <div style="border: 1px solid #ccc;" v-if="!addForm_preview">
              <Toolbar
              style="border-bottom: 1px solid #ccc"
              :editor="editor"
              :defaultConfig="toolbarConfig"
              :mode="mode"
              />
              <Editor
              style="height: 500px; overflow-y: hidden;"
              v-model="addConcept.contents"
              :defaultConfig="editorConfig"
              :mode="mode"
              @onCreated="onCreated"
              />
            </div>
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
import Vue from 'vue'
import { Editor, Toolbar } from '@wangeditor/editor-for-vue'
import VkspService from '../service/VkspService'
import ConceptFileService from '../service/ConceptFileService'
import {notification} from 'ant-design-vue'
import PlusOutlined from '@ant-design/icons-vue'
import UploadOutlined from '@ant-design/icons-vue'
import 'animate.css'
import axios from 'axios'
import {EventBus} from '../../event-bus.js'
export default{
  components: { 
    Editor, Toolbar,
    PlusOutlined,UploadOutlined,
	},
  data() {
    return {
      editor: null,
      toolbarConfig: { },
      editorConfig: { placeholder: '请输入内容...' },
      mode: 'default', // or 'simple'
      
      addConceptDialogVisible_wang:false,
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
        views: 0
      },
      
      fileList:[],
    }
  },
  methods: {
    onCreated(editor) {
      this.editor = Object.seal(editor) // 一定要用 Object.seal() ，否则会报错
    },
  },
  mounted() {
    EventBus.$on('addConcept00',()=>{
			this.addConceptDialogVisible_wang = true
    })
    
    EventBus.$on('fileloadcomplete',()=>{
			//获取mixin中读取的文件内容赋值
			this.addConcept.contents = this.fileContents
    })

  },
  beforeDestroy() {
    const editor = this.editor
    if (editor == null) return
    editor.destroy() // 组件销毁时，及时销毁编辑器
  }
}
</script>

<style src="@wangeditor/editor/dist/css/style.css">

</style>