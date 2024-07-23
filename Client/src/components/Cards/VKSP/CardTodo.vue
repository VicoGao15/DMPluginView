<template>
				
	<!-- Projects Table Card -->
	<a-card :bordered="false" :bodyStyle="{padding: 0,}">
		<template #title>
			<a-row type="flex" align="middle">
				<a-col :span="24" :md="3">
						<h5>VKSP - TodoList</h5>
				</a-col>
				<a-col :span="24" :md="21"  style="display: flex; align-items: center; justify-content: flex-end">
          <a-tooltip>
            <template #title>快捷键：Shift + A</template>
          <a-button type="dashed" @click="addConceptControl()" class="header-solid h-full animate__animated animate__bounceIn ">
              <a-icon type="plus"></a-icon>
            Add</a-button>	
          </a-tooltip>
				</a-col>
			</a-row>
		</template>
      <p></p>
      <div>
        <span style="margin-left:20px;">优先级：</span>
				<a-select
          ref="select"
          v-model:value="Filter.priority"
          style="width: 130px"
          placeholder="优先级"
        >
          <a-select-option v-for="item in priorityOptions" :key="item.value" :value="item.value">
            {{item.text}}
          </a-select-option>
    		</a-select>
        <span style="margin-left:20px;">卡片类型：</span>
        <a-select
          ref="select"
          v-model:value="Filter.type"
          style="width: 100px;margin-left:0px;"
          placeholder="卡片类型"
        >
				  <a-select-option v-for="item in cardStyleOption" :key="item.key" :value="item.key">
            <img :src='item.imgsrc' width="50" height="50"/>
          </a-select-option>
        </a-select>
        <span style="margin-left:20px;">标题：</span>
        <a-input-search 
          placeholder="搜索"
          style="width: 350px;margin-left:0px;"
          v-model = "Filter.searchInput"
        ></a-input-search>
        <a-button @click="onSearch()" style="margin-left:20px;">搜索</a-button>
        <a-button @click="emptyCondition()" style="margin-left:10px;">清空条件</a-button>
      </div>
      <p></p>
  		<TodoListCategory :todoData="tableData"/>
      <p></p>
      <p>共 {{tableData.length}} 条数据</p>	
    
    <!--新增Todo Modal-->
    <a-modal 
      :visible="addTodoDialogVisible" 
      title="新增Todo" 
      @ok="addTodoSave()"
      @cancel="addTodoDialogVisible=false"
      :closable="false"
      width="1200px"
    >
      <a-form
					id="components-form-demo-normal-login"
					class="login-form"
					:hideRequiredMark="true"
				>
					<a-form-item class="mb-10" :colon="true">
            <span >标题名称：</span>
						<a-input
            style="width:400px;"
            v-model="addTodo.Title"
						v-decorator="[
						'标题',
						{ rules: [{ required: true, message: '请输入标题' }] },
						]" placeholder="Title" />
            
            <span style="margin-left:20px;">截止时间：</span>
						<a-date-picker v-model="addTodo.Deadline"></a-date-picker>
            
            <span style="margin-left:20px;">优先级：</span>
    				<a-select
      				v-model="addTodo.Priority"
      				style="width:130px"
      				>
    					<a-select-option v-for="item in priorityOptions" :key="item.value" :value="item.value">{{item.text}}</a-select-option>
    				</a-select>
            
            <span style="margin-left:20px;">样式：</span>
            <a-select
              v-model="addTodo.Types"
      				style="width:90px"
      				>
    					<a-select-option v-for="item in cardStyleOption" :key="item.key" :value="item.key">
              <img :src='item.imgsrc' width="50" height="50"/>
              </a-select-option>
    				</a-select>
            
					</a-form-item>
          <a-form-item class="mb-5" :colon="false">
            <quill-editor
            v-if="!addForm_preview"
              ref="myQuillEditor"
              v-model="addTodo.Contents"
              :options="editorOption"
              style="height:450px;"
            />
            <a-card v-else class="header-solid h-full" style="height:600px;overflow: auto;">
              <p style="white-space: pre-line;" v-html="addTodo.contents"></p>
            </a-card>
            <p></p>
					</a-form-item>
          <div class="parent" v-if="!addForm_preview">
  					<div class="div1">
  						<a-upload style="margin-left:0px;" @change="importContentFromFile_Add" >
                <a-button type="dashed">
                  <a-icon type="upload"></a-icon>
                  从文件导入内容(json/txt/xlsx/docx)
                </a-button>
              </a-upload>
            </div>	
          </div>
          <a-button style="margin-left:0px;" @click="addTodo.contents=''">清空内容</a-button>
          <a-button style="margin-left:20px;" v-if="!addForm_preview" @click="addForm_preview=!addForm_preview">预览</a-button>
          <a-button style="margin-left:20px;" v-else @click="addForm_preview=!addForm_preview">编辑</a-button>
				</a-form>
    </a-modal>
    <!--编辑Todo Modal-->
    <a-modal 
      :visible="editTodoDialogVisible" 
      @ok="editTodoSave()"
      @cancel="editTodoDialogVisible=false"
      :closable="false"
      width="1300px"
    >
      <a-form
					id="components-form-demo-normal-login"
					class="login-form"
					:hideRequiredMark="true"
				>
					<a-form-item class="mb-10" :colon="true">
            <span >标题名称：</span>
						<a-input
            style="width:380px;"
            v-model="editTodo.title"
						v-decorator="[
						'标题',
						{ rules: [{ required: true, message: '请输入标题' }] },
						]" placeholder="Title" />
            
            <span style="margin-left:20px;">截止时间：</span>
						<a-date-picker v-model="editTodo.deadline"></a-date-picker>
            
            <span style="margin-left:20px;">优先级：</span>
    				<a-select
      				v-model="editTodo.priority"
      				style="width:130px"
      				>
    					<a-select-option v-for="item in priorityOptions" :key="item.value" :value="item.value">{{item.text}}</a-select-option>
    				</a-select>
            
            <span style="margin-left:20px;">样式：</span>
            <a-select
              v-model="editTodo.types"
      				style="width:90px"
      				>
    					<a-select-option v-for="item in cardStyleOption" :key="item.key" :value="item.key">
              <img :src='item.imgsrc' width="50" height="50"/>
              </a-select-option>
    				</a-select>
					</a-form-item>
          <a-form-item class="mb-5" :colon="false">
            <quill-editor
              v-if="!addForm_preview"
              ref="myQuillEditor"
              v-model="editTodo.contents"
              :options="editorOption"
              style="height:600px;"
            />
            <a-card v-else class="header-solid h-full" style="height:555px;overflow: auto;">
              <p style="white-space: pre-line;" v-html="editTodo.contents"></p>
            </a-card>
            <p></p>
					</a-form-item>
          <div style="margin-top:65px;">
						<a-textarea
						  v-if="!addForm_preview"
              v-model="editTodo.remark"
              placeholder="备注"
              :auto-size="{minRows:2, maxRows:5}"
            />
          </div>
          <div class="parent" v-if="!addForm_preview">
  					<div class="div1">
  						<a-upload style="margin-left:0px;" @change="importContentFromFile_Edit" >
                <a-button type="dashed">
                  <a-icon type="upload"></a-icon>
                  从文件导入内容(json/txt/xlsx/docx)
                </a-button>
              </a-upload>
            </div>	
          </div>
          <a-button style="margin-left:0px;" @click="editTodo.contents=''">清空内容</a-button>
          <a-button style="margin-left:20px;" v-if="!addForm_preview" @click="addForm_preview=!addForm_preview">预览</a-button>
          <a-button style="margin-left:20px;" v-else @click="addForm_preview=!addForm_preview">编辑</a-button>
				</a-form>
    </a-modal>
    <!--Todo详情 Modal-->
    <a-modal 
      :visible="viewTodoDetailDialogVisible" 
      @cancel="viewTodoDetailDialogVisible=false"
      :closable="false"
      width="1300px"
    >
      <a-form
					id="components-form-demo-normal-login"
					class="login-form"
					:hideRequiredMark="true"
				>
					<a-form-item class="mb-10" :colon="true">
            <div class="TodoDetaiDialogTitle_Parent">
              <div class="TodoDetaiDialogTitle_title">
                <span style="font-size:24px"><b>{{todoDetail.title}}</b></span>
              </div>
							<div class="TodoDetaiDialogTitle_other">
                <span style="margin-left:20px;">{{showTimeFormat(todoDetail.deadline,true)}}</span>
                <span style="margin-left:15px;"></span>
                <a-tag v-if="todoDetail.priority==0" color="#e52424">P0-刻不容缓</a-tag>
                <a-tag v-else-if="todoDetail.priority==1" color="#f98b24">P1-事不宜迟</a-tag>
                <a-tag v-else-if="todoDetail.priority==2" color="#f9dd24">P2-循序渐进</a-tag>
                <a-tag v-else-if="todoDetail.priority==3" color="#24cff9">P3-未雨绸缪</a-tag>
                <a-tag v-else-if="todoDetail.priority==4" color="#c0cfd3">P4-源远流长</a-tag>
                <a-tag v-else color="cyan">P5-仓库吃灰</a-tag>
                <a-button style="margin-left:20px;" @click="func_editTodo(todoDetail.id)">编辑</a-button>
                <!--<a-card :bordered="false" :style="todoCardStyle(todoDetail.types)"></a-card>-->
              </div>
            </div>
            
					</a-form-item>
          <a-form-item class="mb-5" :colon="false">
            <a-card class="header-solid h-full" style="height:600px;overflow: auto;">
              <p style="white-space: pre-line;margin-left:15px;" v-html="todoDetail.contents"></p>
            </a-card>
            <span>备注信息:</span>
            <a-card class="header-solid h-full" style="height:80px;overflow: auto;">
              <p style="white-space: pre-line;" v-html="todoDetail.remark"></p>
            </a-card>
            <div>
              创建时间：<span style="font-weight:bold;" v-html="timeFormate(todoDetail.creattime)"></span>
							<a-divider type="vertical"/>
              <span style="margin-left:0px;">编辑时间：</span><span style="margin-left:0px;font-weight:bold;">{{timeFormate(todoDetail.edittime)}}</span>
              <a-divider type="vertical"/>
              <span style="margin-left:0px;">所需工时：</span><span style="margin-left:0px;font-weight:bold;">{{todoDetail.needworkhour}}</span>
              <a-divider type="vertical"/>
              <span style="margin-left:0px;">进度：</span><span style="margin-left:0px;font-weight:bold;">{{todoDetail.schedule}}%</span>
            </div>
					</a-form-item>
				</a-form>
    </a-modal>
	</a-card>
	<!-- / Projects Table Card -->

</template>
		
<script>
import * as XLSX from 'xlsx'
import * as mammoth from 'mammoth'
import PluginDetailDrawer from '../../Drawer/PluginDetailDrawer.vue'
import VkspService from '../../service/VkspService'
import ConceptFileService from '../../service/ConceptFileService'
import {EventBus} from '../../../event-bus.js'
import {notification} from 'ant-design-vue'
import PlusOutlined from '@ant-design/icons-vue'
import UploadOutlined from '@ant-design/icons-vue'
import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'
import { quillEditor } from 'vue-quill-editor'
import ImageResize from 'quill-image-resize-module' 
import { container,ImageExtend,QuillWatch } from 'quill-image-extend-module'
Quill.register('modules/imageResize', ImageResize)
Quill.register('modules/ImageExtend', ImageExtend)
import 'animate.css'
import TodoListCategory from './TodoListCategory.vue'

import {VkspUser} from '../../../mixin/vkspuser'
import {system_options} from '../../../mixin/options'
import {Todo_Mixin} from '../../../mixin/todo'
import { myNotification } from '../../../mixin/notification'

	export default ({
    mixins:[VkspUser,system_options,Todo_Mixin,myNotification],
		components:{
			PluginDetailDrawer,quillEditor,TodoListCategory,
      PlusOutlined,UploadOutlined,
      VNodes:(_,{ attrs })=>{
				return attrs.vnodes
      }
		},
		data() {

			const vkspService=new VkspService();
      const conceptFileService = new ConceptFileService();
      
			return {
        isLogin:false,
        addTodoDialogVisible:false,
        viewTodoDetailDialogVisible:false,
        editTodoDialogVisible:false,
        addForm_preview:false,
        
				tableData:[],
				tableData_Copy:[],
        todoDetail:{},

				vkspService,
        conceptFileService,

				searchInput:'',
        
        addTodo:{
          Title: "",
          Contents: "",
          Deadline: "",
          Status: 2,
          Types: 0,
          Remark: "",
          Priority: 0,
          Author: "Vico",
          Userid:0
        },
        editTodo:{
          id:0,
          title: "",
          contents: "",
          deadline: "",
          status: 0,
          types: 0,
          remark: "",
          priority: 0,
          author: ""
        },
        Filter:{
					searchInput:'',
          priority:'All',
          type:'All'
        }
			}
		},
		mounted(){
      /*初始化*/
			this.getAllInit()
			
      /*事件总线*/
      EventBus.$on('searchplugin',data=>{
				this.searchInput=data
				this.onGlobalSearch()
			})
      
      EventBus.$on('refreshConcept',()=>{
				this.getAllInit()
      })
      
      EventBus.$on("ViewTodoDetail", todoid=>{
				this.viewTodoDetailDialogVisible = true
        this.getTodoDetail(todoid,false)
      })
      
      EventBus.$on('fileloadcomplete',()=>{
				//获取mixin中读取的文件内容赋值
        if(this.judgeEditOrAddFlg==0){
					this.addTodo.Contents = this.fileContents
        }
        else{
					this.editTodo.contents = this.fileContents
        }
      })
      
      EventBus.$on("EditTodoItem", todoid=>{
        this.getTodoDetail(todoid,true)
      })
      
      EventBus.$on("EditTodoItemStatus", (item)=>{
        this.editTodoStatusSave(item)
      })
      
      EventBus.$on('Keydown_Shift_A',()=>{
        this.addConceptControl()
      })
		},
    created(){
      this.isLogin = this.ifIsLogin()
      if(this.isLogin){
				this.addTodo.Userid=this.getLoginMessage().id
      }
    },
    
		methods:{
			
			async getAllInit(){
        if(!this.isLogin){
          this.myNotification_info('请先登录！')
					return
        }
				await this.vkspService.getAllUserTodo(this.addTodo.Userid).then((res)=>{
          this.tableData=res.data.reverse()
          this.tableData_Copy=res.data
				}).catch(err=>{
					console.log(err.message)
				})
			},
      
      async getTodoDetail(todoid, foredit){
				await this.vkspService.getTodoDetail(todoid).then((res)=>{
          if(foredit){
						this.editTodo = res.data
            this.editTodoDialogVisible = true
          }else{
						this.todoDetail = res.data
          }
				}).catch(err=>{
					console.log(err.message)
				})
      },
      
      importContentFromFile_Add(file){
        //mixin中混入的方法
        this.importFile_Todo(file, 0)
      },
      importContentFromFile_Edit(file){
        //mixin中混入的方法
        this.importFile_Todo(file, 1)
      },
      
      onGlobalSearch(){
				var searchTable=[]
				this.tableData_Copy.forEach(element => {
          if(element.header.toLowerCase().indexOf(this.searchInput.toLowerCase())!=-1){
						searchTable.push(element)
					}
				});

				this.tableData=searchTable
			},

			getPluginByID(value){
				var plugin
				for (let index = 0; index < this.allData.length; index++) {
					const element = this.allData[index];
					if(value===this.allData[index].id){
						plugin=this.allData[index]
						break;
					}
				}

				return plugin
			},
      
      deleteTodo(id){
				console.log(id)
      },
      
      addTodoSave(){
        //判断title最大长度
        if(this.addTodo.Title.length>100){
          this.$message.info("标题已超过最大长度100个字符！")
					return
        }
        
        //this.addTodo.contents = this.addTodo.contents.replaceAll('&lt;img','<img')
        //this.addTodo.contents = this.addTodo.contents.replaceAll('/&gt;','>')
        this.addTodo.Contents = this.addTodo.Contents.replaceAll('<img','<img preview=\'' +new Date().getTime()+ '\'')
        
        this.vkspService.addTodo(this.addTodo).then(res=>{
					if(res.data){
            this.$message.success("添加成功："+this.addTodo.Title)
            this.getAllInit()
						this.addTodoDialogVisible=false
          }else{
						this.$message.error("添加失败！")
          }
        })
      },
      
      /*function 更改状态*/
      editTodoStatusSave(item){
        if(item.opera === 'delete'){
					this.deleteTodo(item.id)
          return 
        }
        this.vkspService.getTodoDetail(item.id).then((res)=>{
          var todoitem = res.data
          switch(item.opera){
            case 'doing': todoitem.status = 4; break
            case 'complete': todoitem.status = 1; break
            case 'redo': todoitem.status = 2; break
            case 'revocation': todoitem.status = 3; break
          }
          
          this.vkspService.updateTodo(todoitem).then((res)=>{
						this.$message.success("修改成功！")
            this.getAllInit()
          })
				}).catch(err=>{
					console.log(err.message)
				})
      },
      
      /*function 删除Todo*/
      deleteTodo(id){
        console.log('delete',id)
				this.vkspService.deleteTodo(id).then(res=>{
					this.$message.success("删除成功！")
          this.getAllInit()
        })
      },
      
      /*function 保存编辑*/
      editTodoSave(){
				//判断title最大长度
        if(this.editTodo.title.length>100){
          this.$message.info("标题已超过最大长度100个字符！")
					return
        }
        this.editTodo.contents = this.editTodo.contents.replaceAll('<img','<img preview=\'' +new Date().getTime()+ '\'')
        
        this.vkspService.updateTodo(this.editTodo).then(res=>{
					if(res.data){
            this.$message.success("编辑成功："+this.editTodo.title)
						this.editTodoDialogVisible=false
            this.getAllInit()
          }else{
						this.$message.error("添加失败！")
          }
        })
      },
      
      /*function 由detial切换编辑*/
      func_editTodo(todoid){
        this.viewTodoDetailDialogVisible = false
				this.getTodoDetail(todoid,true)
      },
      
      dateTimeStamp(date){
        return parseInt(Date.parse(date))
      },
      
      addConceptControl(){
        if(!this.isLogin){
          this.$message.warning("需要登录之后才能进行操作!")
          return
        }
				this.addTodoDialogVisible = true
      },
      
      onSearch(){
				var searchTable=[]
				this.tableData_Copy.forEach(element => {
          if(
              (this.Filter.priority=='All' || element.priority==this.Filter.priority) &&
              (this.Filter.type=='All' || element.types==parseInt(this.Filter.type)) &&
              (element.title.toLowerCase().indexOf(this.Filter.searchInput.toLowerCase())!=-1)
            )
          {
						searchTable.push(element)
          }
				})	

				this.tableData=searchTable
			},
      emptyCondition(){
				this.Filter={
					search:'',
          priority:'All',
          type:'All'
        }
        this.getAllInit()
      }
      
		},
    
    computed:{
    }
	})

</script>
<style scoped>
.parent{
  margin-top:10px;
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

.TodoDetaiDialogTitle_Parent{
	display: grid;
  grid-template-columns: repeat(2,1fr);
  grid-template-rows: 1fr;
  grid-column-gap: 0px;
  grid-row-gap: 0px;
}
.TodoDetaiDialogTitle_title{
	grid-area: 1/1/2/2;
}
.TodoDetaiDialogTitle_other{
  grid-area: 1/2/2/3;
}
</style>
