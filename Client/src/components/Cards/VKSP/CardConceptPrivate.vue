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
      <p></p>
      <div >
        <span style="margin-left:20px;">标签类型：</span>
        <a-select
          ref="select"
          v-model="Filter.type"
          style="width: 100px;margin-left:0px;"
          placeholder="卡片类型"
          @change="handleChange_Filter"
        >
  			  <a-select-option v-for="item in conceptTypeOptions" :key="item.key" :value="item.key">
            <img :src='item.imgsrc' width="50" height="50"/>
          </a-select-option>
        </a-select>
        <span style="margin-left:20px;">创建时间：</span>
        <a-select
          size="small"
          ref="select"
          v-model="Filter.selectYear"
          style="width: 100px"
          placeholder="年"
          :options="selectYearOption"
          @change="handleChange_Filter"
        ></a-select>
        <a-select
          size="small"
          ref="select"
          v-model="Filter.selectMonth"
          style="width: 100px;margin-left:10px;"
          placeholder="月"
          :options="selectMonthOption"
          @change="handleChange_Filter"
        ></a-select>
        <span style="margin-left:20px;">标题：</span>
        <a-input-search 
          placeholder="搜索"
          style="width: 350px;margin-left:0px;"
          v-model = "Filter.searchInput"
        ></a-input-search>
        <span style="margin-left:20px;">私有/公共：</span>
        <a-select
          size="small"
          ref="select"
          style="width: 100px"
          placeholder="公共"
          v-model="Filter.isPrivate"
          :options="selectIsPrivateOption"
          @change="handleChange_Filter"
        ></a-select>
        <a-button @click="handleChange_Filter()" style="margin-left:20px;">搜索</a-button>
        <a-button @click="emptyCondition()" style="margin-left:10px;">清空条件</a-button>
      </div>
      <p></p>
		<a-table 
			:columns="tableColumns_personal" 
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
			
			<!--<template slot="user" slot-scope="userInfo">
				<h6 class="m-0">
          <a-tooltip>
            <template #title>{{ userInfo.username }}</template>
            <a-avatar v-if="userInfo.avatarUrl!='' && userInfo.avatarUrl!=null" shape="circle" :src="userInfo.avatarUrl"/>
  					<a-avatar v-else style="background-color:#1890ff;">{{userInfo.username.charAt(0).toUpperCase()}}</a-avatar>  
          </a-tooltip>
				</h6>
			</template>-->

			<template slot="createDate1" slot-scope="createDate1">
        <h6>
          <span v-show="false">{{date=(createDate1 || '').split('T')}}</span>
					{{ date[0] + ' / ' + (date[1] || '').split('.')[0]}}
        </h6>
			</template>

			<template slot="slotHeader" slot-scope="slotHeader">
        <h6 class="m-0">{{ slotHeader }}</h6>
			</template>
      <template slot="isPrivate" slot-scope="isPrivate">
        <a @click="tableDetailRowClick" >Details</a>
        <a-popconfirm
          title="确定修改状态吗?"
          ok-text="Yes"
          cancel-text="No"
          @confirm="handlePrivateSwitch"
          @cancel="cancel"
        >
          <a-switch style="margin-left:10px;" checked-children="公开" un-checked-children="私密" :checked='isPrivate==0 || isPrivate==null' @change="handlePrivateSwitch"/>
        </a-popconfirm>
			</template>
      

		</a-table>
    <AddConceptDialogQuillEditor/>
    <!--<AddConceptDialogWangEditor/>-->
	</a-card>
	<!-- / Projects Table Card -->
</template>
		
<script>
import VkspService from '../../service/VkspService'
import ConceptFileService from '../../service/ConceptFileService'
import ConceptDetail from '../../DetailPage/ConceptDetail.vue'
import {EventBus} from '../../../event-bus.js'
import {notification} from 'ant-design-vue'
import PlusOutlined from '@ant-design/icons-vue'
import 'animate.css'
import axios from 'axios'
import {VkspUser} from '../../../mixin/vkspuser'
import {system_options} from '../../../mixin/options'

import AddConceptDialogQuillEditor from '../../Dialog/AddConceptDialogQuillEditor.vue'
//import AddConceptDialogWangEditor from '../../Dialog/AddConceptDialogWangEditor.vue'

	export default ({
    mixins:[VkspUser,system_options],
		components:{
      AddConceptDialogQuillEditor,
      //AddConceptDialogWangEditor,
      ConceptDetail,
      PlusOutlined,
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
        addUserDialogVisible:false,
        
				tableData:[],
				tableData_Copy:[],
        userOptions:[],
        
        pagination:{
					total:0,
          pageSize:10,
          showSizeChanger:true,
          pageSizeOptions:['10','15','20','50'],
          showTotal:total => `共 ${total} 条数据`,
          showQuickJumper:true,//快速跳转
          showLessItems:true
        },

				selectRow,

				vkspService,
        conceptFileService,
				detailDrawerVisible,
				selectItem:{},

				searchInput:'',

        addUser:{
					username: "",
          password: ""
        },
        Filter:{
					searchInput:'',
          user:'All',
          type:'All',
          selectMonth:'0',
          selectYear:'0',
          isPrivate:false
        },
        
        selectDetailRow:0
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
        console.log(id)
				this.lookBackOrNextOne(true, id)
      })
      
      EventBus.$on('Keydown_Shift_A',()=>{
        this.addConceptControl()
      })
		},
    created(){
      //通过mixin方法返回是否登录
      this.isLogin = this.ifIsLogin()
      if(!this.isLogin){
        this.myNotification_info('请先登录！')
				return
      }else{
				this.Filter.user = this.getLoginMessage().id
      }
    },
    

		methods:{
			
			async getAllInit(){
				await this.vkspService.getAllConceptNeed().then((res)=>{
          this.tableData=res.data.reverse()
          this.tableData_Copy=res.data
          //this.GLOBAL.vkspConceptFilterDate = this.tableData_Copy
          this.handleChange_Filter()
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
						dblclick:()=>{
              this.$router.push({
								path:'/conceptdetail',
                query:{
									conceptid: record.id
                }
              })
						},
            mouseenter:()=>{
							this.selectDetailRow = record.id
            }
					}	
				}
			},
      tableDetailRowClick(){
        if(this.selectDetailRow!=0){
  				this.$router.push({
						path:'/conceptdetail',
            query:{
							conceptid: this.selectDetailRow
            }
          })	
        }
      },
      handlePrivateSwitch(){
				//todo
      },
			
			onSearch(){
				var searchTable=[]
				this.tableData_Copy.forEach(element => {
          if(element.header.toLowerCase().indexOf(this.searchInput.toLowerCase())!=-1){
						searchTable.push(element)
					}
				});

				this.tableData=searchTable
        this.GLOBAL.vkspConceptFilterDate = searchTable
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
      
      dateTimeStamp(date){
        return parseInt(Date.parse(date))
      },
      
      addConceptControl(){
        if(!this.isLogin){
          this.$message.warning("需要登录之后才能进行操作!")
          return
        }
				EventBus.$emit('addConcept')
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
      
      /*根据所选年份,月份，类型过滤文章*/
      handleChange_Filter(){
        var searchTable=[]
        var _data = this.tableData_Copy

        _data.forEach(element => {
          var publishtime = element.create_Date.split('T')[0].split('-')

          if(
               (this.Filter.user =='All' || this.Filter.user =='0' || this.Filter.user==element.userInfo.id)
            && (this.Filter.selectYear == '0' || publishtime[0]==this.Filter.selectYear)
            && (this.Filter.selectMonth == '0' || publishtime[1]==this.Filter.selectMonth)
            && (element.header.toLowerCase().indexOf(this.Filter.searchInput.toLowerCase())!=-1)
            && (this.Filter.isPrivate==element.isPrivate)
            //todo cocept类型
          )
          {
						searchTable.push(element)
					}
				});
        
  			this.tableData=searchTable
        this.GLOBAL.vkspConceptFilterDate = this.tableData
      }
      
		},
    
    computed:{ }
	})

</script>
<style scoped>

</style>