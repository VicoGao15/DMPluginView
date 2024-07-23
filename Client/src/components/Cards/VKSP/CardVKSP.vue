<template>
				
	<!-- Projects Table Card -->
	<a-card :bordered="false" :bodyStyle="{padding: 0,}">
		<template #title>
			<a-row type="flex" align="middle">
				<a-col :span="24" :md="3">
						<h5>VKSP - Concept</h5>
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
		<a-table 
			:columns="tableColumns" 
			:data-source="tableData" 
      :pagination="pagination"
      :scroll="{y:740}"
			row-key="id" 
			:customRow="tableRowClick"
			:showHeader="true" 
			style="margin-right:20px;overflow: auto;"
			>
			
			<template slot="module" slot-scope="module">
				<span class="font-bold text-muted text-sm">{{ module }}</span>
			</template>
			
			<template slot="company" slot-scope="company">
				<h6 class="m-0">
					{{ company }}
				</h6>
			</template>

			<template slot="createDate1" slot-scope="createDate1">
        <h6>
          <span v-show="false">{{date=(createDate1 || '').split('T')}}</span>
					{{ date[0] + ' / ' + (date[1] || '').split('.')[0]}}
        </h6>
				
			</template>

			<template slot="slotHeader" slot-scope="slotHeader">
        <h6 class="m-0">{{ slotHeader }}</h6>
			</template>

		</a-table>
		<PluginDetailDrawer></PluginDetailDrawer>
    <a-modal 
		  :visible="addUserDialogVisible" 
      title="新增User" 
      @ok="addUserSave()"
      @cancel="addUserDialogVisible=false"
      :closable="false"
      width="300px"
    >
      <a-form
        id="components-form-demo-normal-login"
        class="login-form"
        :hideRequiredMark="true"
      >
        <a-form-item class="mb-10" :colon="true">
          <span >名称：</span>
					<a-input
          v-model="addUser.username"
          style="width:200px;"
					v-decorator="[
					'name',
					{ rules: [{ required: true, message: '请输入name' }] },
					]" placeholder="User" />
				</a-form-item>
      </a-form> 
    </a-modal>
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
            <quill-editor
              v-if="!addForm_preview"
              ref="myQuillEditor"
              v-model="addConcept.contents"
              :options="editorOption"
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

	</a-card>
	<!-- / Projects Table Card -->
    
  

</template>
		
<script>
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
import * as quillEmoji from 'quill-emoji'
import 'quill-emoji/dist/quill-emoji.css'
import { quillEditor } from 'vue-quill-editor'
import ImageResize from 'quill-image-resize-module' 
import { container,ImageExtend,QuillWatch } from 'quill-image-extend-module'
Quill.register('modules/imageResize', ImageResize)
Quill.register('modules/ImageExtend', ImageExtend)
Quill.register('modules/quillEmoji',quillEmoji)
import 'animate.css'
import axios from 'axios'

	export default ({
		components:{
			PluginDetailDrawer,quillEditor,
      PlusOutlined,UploadOutlined,
      VNodes:(_,{ attrs })=>{
				return attrs.vnodes
      }
		},
		data() {
			
			var selectRow

			var pluginicon= 'images/logos/snapdevelop.png';
			const vkspService=new VkspService();
      const conceptFileService = new ConceptFileService();
			var detailDrawerVisible=false;
			var userFilterOptions=[]
      
			return {
        isLogin:false,
        addConceptDialogVisible:false,
        addUserDialogVisible:false,
        addForm_preview:false,
        
				tableData:[],
				tableData_Copy:[],
        userOptions:[],
        pagination:{
					total:0,
          pageSize:10,
          showSizeChanger:true,
          pageSizeOptions:['10','15','20','50'],
          showTotal:total => `共 ${total} 条数据`
          
        },
				tableColumns:[
					{
						title: '所属模块',
						dataIndex: 'module',
						scopedSlots: { customRender: 'type' },
						width: 200,
            sortDirections:["descend","ascend"],
            sorter:(a,b)=>a.module.length-b.module.length
					},
					{
						title: '创建者',
						dataIndex: 'creater',
						scopedSlots: { customRender: 'company' },
						width: 300,
            filters:[
							{text:'Vico',value:'Vico'},
              {text:'Admin',value:'Admin'},
              {text:'Song',value:'Song'},
              {text:'Other',value:'Sherry'}
            ], 
            //filters:userFilterOptions,
            filterMultiple:true,
            onFilter:(value,record)=>record.creater.indexOf(value)===0
					},
					{
						title: '创建日期',
						dataIndex: 'create_Date',
            scopedSlots: { customRender: 'createDate1' },
						width: 250,
            sortDirections:["descend","ascend"],
            sorter:(a,b)=>this.dateTimeStamp(a.create_Date)-this.dateTimeStamp(b.create_Date)
					},
          {
						title: '标题',
						dataIndex: 'header',
            scopedSlots: { customRender: 'slotHeader' },
						class: 'font-bold text-muted text-sm',
					}
				],
				selectRow,

				vkspService,
        conceptFileService,
				detailDrawerVisible,
				selectItem:{},

				searchInput:'',
        
        addConcept:{
          module: "Vue",
          creater: "Vico",
          create_Date: "2022-12-2T00:00:00",
          header: "",
          contents: "",
          likes: 0,
          views: 0
        },
        addUser:{
					username: "",
          password: ""
        },
        
        editorOption:{
          modules:{
//            ImageExtend:{
//							loading: true,
//              name: 'img',
//              action: 'http://172.25.13.72:8880/VKSP/image/',//上传的服务器地址
//              response: res => {
//                return res.data
//              }
//            },
            imageResize:{
              displayStyles:{
                backgroundColor:'black',
                border:'none',
                color:'white'
              },
              modules:['Resize', 'DisplaySize', 'Toolbar']
            },
            'emoji-toolbar':true,
            'emoji-shortname':true,
            toolbar:[
              ['bold', 'italic', 'underline', 'strike'], // 字体
              ['blockquote', 'code-block'],
              [{ 'header': 1 }, { 'header': 2 }], // 样式标题
              [{ 'list': 'ordered'}, { 'list': 'bullet' }],
              [{ 'script': 'sub'}, { 'script': 'super' }], // 下标、上标
              [{ 'indent': '-1'}, { 'indent': '+1' }], // 缩进
              [{ 'direction': 'rtl' }],
              [{ 'size': ['small', false, 'large', 'huge'] }], // 字体
              [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
              [{ 'color': [] }, { 'background': [] }],
              [{ 'font': [] }],
              [{ 'align': [] }],
              ['clean'], // 格式清除 
              ["link", "image", "video"], // 链接、图片、视频
              ['emoji']
            ],
          },
          placeholder:'输入内容...'
        },
        
        fileList:[],
			}
		},
		mounted(){
      /*初始化*/
			this.getAllInit()
			
      /*事件总线*/
      EventBus.$on('searchplugin',data=>{
				this.searchInput=data
				this.onSearch()
			})
      
      EventBus.$on('refreshConcept',()=>{
				this.getAllInit()
      })
      
      EventBus.$on('LookBackOne',id=>{
				this.lookBackOrNextOne(false, id)
      })
      
      EventBus.$on('LookNextOne',id=>{
				this.lookBackOrNextOne(true, id)
      })
      
      EventBus.$on('fileloadcomplete',()=>{
				//获取mixin中读取的文件内容赋值
				this.addConcept.contents = this.fileContents
      })
      
      EventBus.$on('Keydown_Shift_A',()=>{
        this.addConceptControl()
      })
		},
    created(){
      //获取cookie是否登录信息
        //console.log(document.cookie)
        var arr = document.cookie.split(';')
        for (var i = 0; i < arr.length; i++) {
          var arr2 = arr[i].split('=')
          if (arr2[0] === ' username' || arr2[0] === 'username') {
            this.isLogin = true
            this.addConcept.creater=arr2[1]
          }
        }
    },
    

		methods:{
			
			async getAllInit(){
				await this.vkspService.getAllConceptNeed().then((res)=>{
          this.tableData=res.data.reverse()
          this.tableData_Copy=res.data
				}).catch(err=>{
					console.log(err.message)
				})
			},

			viewMore(raw){
				this.detailDrawerVisible=true
				this.selectRow=this.tableData[0]
			},
			
			tableRowClick(record,index){
				return{
					on:{
						click:()=>{
							//传递事件总线参数
							EventBus.$emit('ConceptDetailVisible',true)
							EventBus.$emit('Concept',record)
						}
					}	
				}
				
			},
			
			onSearch(){
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

			calculateSize(value){
				//var num=value/(1024*1024)
				return (value/(1024*1024)).toFixed(2)
			},
			
			JudgePackageisNormal(){
				
				var notNormalList=[]
				var message="存在 "+notNormalList.length+" 个不正常的包！"
				var des="点击前往查看"
				
				if(notNormalList.length===0){
					
					notification.open({
						type:'warning',
						message:message,
						description:des,
						duration:5,
						onClick:()=>{
							this.$router.push('/')
						}, 
						style:{
							width:'500px',
							marginLeft:`${335-500}px`,
						}
					})
				}
			},

      deleteConcept(id){
				console.log(id)
      },
      
      addConceptSave(){
        
        this.addConcept.create_Date = new Date();
        
        //判断title最大长度
        if(this.addConcept.header.length>100){
          this.$message.info("标题已超过最大长度100个字符！")
					return
        }
        
        //this.addConcept.contents = this.addConcept.contents.replaceAll('&lt;img','<img')
        //this.addConcept.contents = this.addConcept.contents.replaceAll('/&gt;','>')
        this.addConcept.contents = this.addConcept.contents.replaceAll('<img','<img preview=\'' +this.addConcept.create_Date.getTime()+ '\'')
        
        this.vkspService.addConcept(this.addConcept).then(res=>{
					if(res.data){
            this.$message.success("添加成功："+this.addConcept.header)
            //上传附件
            this.handelFileUpload(res.data)
            this.getAllInit()
						this.addConceptDialogVisible=false
          }else{
						this.$message.error("添加失败！")
          }
        })
      },
      
      lookBackOrNextOne(isNext, id){
        var currentIndex=0
				for (var i = 0; i < this.tableData.length; i++) {
          if(id===this.tableData[i].id){
            currentIndex=i
						break
          }
        }
        //找到back或者next的index
        var Index = isNext? ((currentIndex+1+ this.tableData.length) % this.tableData.length) : ((currentIndex-1+ this.tableData.length) % this.tableData.length)
        
        EventBus.$emit('Concept',this.tableData[Index])
      },
      
      importContentFromFile(file){
        //mixin中混入的方法
        this.importFile(file)
      },
      
      dateTimeStamp(date){
        return parseInt(Date.parse(date))
      },
      
      addConceptControl(){
        if(!this.isLogin){
          this.$message.warning("需要登录之后才能进行操作!")
          return
        }
				this.addConceptDialogVisible=true
        //this.getAllUser()
      },
      
      addUserSave(){
				console.log(this.addUser)
        this.vkspService.addUser(this.addUser).then(res=>{
					if(res.data){
						this.$message.success("添加成功!")
            this.addUserDialogVisible=false
            this.getAllUser()
          }else{
						this.$message.error("添加失败！！！")
          }
        })
      },
      
      getAllUser(){
				this.vkspService.getAllUser().then(res=>{
					if(res.data){
            this.userOptions=[]
						res.data.forEach(item=>{
              var newUserOption={
								label:item.username,
                value:item.username
              }
							this.userOptions.push(newUserOption)
            })
          }else{
						this.$message.error("getUser Error!!!")
          }
        })
      },
      
      filterUserOption(input, option){
				return (option.name || '').toLowerCase().indexOf((input || '').toLowerCase()) >= 0; 
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
      
		},
    
    computed:{
			timeFormate(timeStr){
        if(timeStr){
  				var day=timeStr.split('T')
          
          return day[0]+' '+day[1]		
        }
        return ''
      }, 
    }
	})

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