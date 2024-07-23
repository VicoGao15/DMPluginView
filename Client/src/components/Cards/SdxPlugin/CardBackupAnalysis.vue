<template>
				
	<!-- Projects Table Card -->
	<a-card :bordered="false" class="header-solid h-full" :bodyStyle="{padding: 0,}">
		<template #title>
			<a-row type="flex" align="middle">
				<a-col :span="12" :md="4">
          <span>版本1： </span>
					<a-select
          placeholder="下拉选择"
          show-search
          :allowClear="true"
          :filterOption="filterOption"
					v-model="selectBackupVersionId_1"
					style="width:200px"
					@change="searchBackupVersionInfo(1)"
					>
						<a-select-option v-for="item in allBackupVersion" :key="item.value" :value="item.value">{{item.text}}</a-select-option>
					</a-select>
				</a-col>
        <a-col :span="12" :md="5">
          <span>版本2： </span>
					<a-select
          placeholder="下拉选择"
          show-search
          :allowClear="true"
          :filterOption="filterOption"
					v-model="selectBackupVersionId_2"
					style="width:200px"
          @change="searchBackupVersionInfo(2)"
					>
						<a-select-option v-for="item in allBackupVersion" :key="item.value" :value="item.value">{{item.text}}</a-select-option>
					</a-select>
          <!--<a-button style="margin-left:20px;" @click="startCompare()">Start</a-button>-->
				</a-col>
        <a-col :span="12" :md="2">
          <span>展示方式：</span>
					<a-select
					v-model="selectoutputformat"
					style="width:80px"
					>
						<a-select-option v-for="item in outputformatOption" :key="item.value" :value="item.value">{{item.text}}</a-select-option>
					</a-select>
				</a-col>
        <a-col :span="12" :md="9">
          <span>比较属性：</span>
					<a-select
					v-model="selectCompareAttribute"
					style="width:580px"
          mode="multiple"
          :maxTagCount="5"
          show-search
          @change="selectAttributeChange()"
					>
						<a-select-option  v-for="item in compareAttributeOption" :key="item.value" :value="item.value">{{item.text}}</a-select-option>
					</a-select>
				</a-col>
        <a-col :span="12" :md="2">
          <a-button @click="generateCompareResult()" :disabled="selectBackupVersionId_1==''||selectBackupVersionId_2==''">对比报告</a-button>
				</a-col>
			</a-row>
		</template>
    <div style="height:900px;">
			<code-diff class="codediff" 
        v-if="(selectBackupVersionId_1!='' && selectBackupVersionId_1!=null) || (selectBackupVersionId_2!='' && selectBackupVersionId_2!=null)"
        :old-string="versionCompareStr_1" 
        :new-string="versionCompareStr_2"
        :output-format="selectoutputformat"
        :context="2"
        :isShowNoChange="true"
        :drawFileList="true"
      >
      </code-diff>
      <div v-else>
        <a-divider/>
  			<a-empty style="margin-top:200px">
  				<span slot="description">请在左上角选择比较的版本!</span>
        </a-empty>	
      </div>
      
    </div>
    <PluginCompareReport></PluginCompareReport>
	</a-card>
	<!-- / Projects Table Card -->

</template>
		
<script>
  import SdxBackupService from '../../service/GetSdxBackupService';
  import PluginCompareReport from '../../Dialog/PluginCompareReport.vue'
	import {EventBus} from '../../../event-bus.js'
	import {notification} from 'ant-design-vue'
  import {CodeDiff} from 'v-code-diff'
	//import {CloseCircleOutlined,ExclamationCircleOutlined} from '@ant-design/icons-vue'

	export default ({
		components:{
      CodeDiff,
      PluginCompareReport
		},
		data() {
			var selectRow
      const sdxbackupService = new SdxBackupService()
			
			return {
        sdxbackupService,
        
        //****backup***
        //数据库备份版本
				selectBackupVersionId_1:'',
        selectBackupData_1:'',
        selectBackupData_1_json:[],
        selectBackupVersionId_2:'',
        selectBackupData_2:'',
        selectBackupData_2_json:[],
        allBackupVersion:[],
        
        compareReport:{
					//对比新增插件
          addplugins:[],
          //对比减少插件
          deleteplugins:[],
          //对比修改插件
          modifiedplugins:[],
        },
        
        versionCompareStr_1:'',
        versionCompareStr_2:'',
        
        //****vue-code-diff风格
        outputformatOption:[
          {value:'side-by-side',text:'左右'},
          {value:'line-by-line',text:'上下'}
				],
        selectoutputformat:'side-by-side',
        
        //****要进行比较的属性
        selectCompareAttribute:['id','title','description'],
        compareAttributeOption:[
					{value:'id',text:'id'},
          {value:'title',text:'title'},
          {value:'version',text:'version'},
          {value:'packageType',text:'packageType'},
          {value:'description',text:'description'},
          {value:'releaseNotes',text:'releaseNotes'},
          {value:'isRequireLicenseAcceptance',text:'isRequireLicenseAcceptance'},
          {value:'hasLicense',text:'hasLicense'},
          {value:'license',text:'license'},
          {value:'licenseUrl',text:'licenseUrl'},
          {value:'md5',text:'md5'},
          {value:'installBytes',text:'installBytes'},
          {value:'zipBytes',text:'zipBytes'},
          {value:'published',text:'published'},
          {value:'kind',text:'kind'},
          {value:'tags',text:'tags'},
          {value:'authors',text:'authors'},
          {value:'minClientVersion',text:'minClientVersion'},
          {value:'compatibleVersion',text:'compatibleVersion'},
          {value:'summary',text:'summary'},
          {value:'downloads',text:'downloads'},
          {value:'isPrerelease',text:'isPrerelease'},
          {value:'hasEmbeddedIcon',text:'hasEmbeddedIcon'},
          {value:'hasReadme',text:'hasReadme'},
          {value:'envDependencies',text:'envDependencies'},
          {value:'preDependencies',text:'preDependencies'},
          {value:'includePackages',text:'includePackages'},
        ]
			}
		},
		mounted(){
      this.dataFromBackupInit()
		},
		beforeUnmounte(){},
		created(){},
		methods:{
      
      /*获取备份版本Option*/
      dataFromBackupInit(){
				this.sdxbackupService.getAllBackupVersion().then(res=>{
          //版本排序
          res.data = res.data.sort(function(a,b){
          	return b.build.localeCompare(a.build)
          })
					res.data.forEach(element=>{
						var allBackupVersionOption={
							value:element.id,
							text:element.isKeyVersion?element.build + ' - ' + element.keyVersion:element.build
						}
						this.allBackupVersion.push(allBackupVersionOption)
					})
				})
			},
      
      /*选中版本查找具体数据*/
      searchBackupVersionInfo(num){
        var id = num===1?this.selectBackupVersionId_1:this.selectBackupVersionId_2
        
        //如果是对select执行clear
        if(id==null){
          if(this.selectBackupVersionId_1==null) this.selectBackupData_1 = ''
          if(this.selectBackupVersionId_2==null) this.selectBackupData_2 = ''
          
          this.startCompare()
					return
        }
         
				this.sdxbackupService.getBackupVersionJsonDataById(id).then(res=>{
					if(res.data){
						var backupdata = eval("(" + JSON.parse(JSON.stringify(res.data.jsonData)) + ")") 
            
            //属性过滤
            backupdata = this.changeSelectCompareAttribute(backupdata)
            
            //Json排序
            backupdata = backupdata.sort(function(a,b){
              var sort_Attribute_a = a.id?a.id:a.title
              var sort_Attribute_b = b.id?b.id:b.title
            	return sort_Attribute_a.localeCompare(sort_Attribute_b)
            })

            if(num===1){
              this.selectBackupData_1_json = backupdata
							this.selectBackupData_1 = JSON.stringify(backupdata,null,4)
            }else{
              this.selectBackupData_2_json = backupdata
							this.selectBackupData_2 = JSON.stringify(backupdata,null,4)
            }
            
            this.startCompare()
					}
				})
			},
      
      startCompare(){
				this.versionCompareStr_1 = this.selectBackupData_1
        this.versionCompareStr_2 = this.selectBackupData_2
      },
      
      /*移除不选择检查的Json属性*/
      changeSelectCompareAttribute(backupdata){
        
        backupdata.forEach(item=>{
          if(this.selectCompareAttribute.indexOf('id')==-1)                           { delete item.id }
          if(this.selectCompareAttribute.indexOf('description')==-1)                  { delete item.description }
          if(this.selectCompareAttribute.indexOf('downloads')==-1)                    { delete item.downloads }
          if(this.selectCompareAttribute.indexOf('published')==-1)                    { delete item.published }
          if(this.selectCompareAttribute.indexOf('version')==-1)                      { delete item.version }
          if(this.selectCompareAttribute.indexOf('includePackages')==-1)              { delete item.includePackages }
          else{ //对includePackages排序 1.id 2.title
						item.includePackages = item.includePackages.sort(function(a,b){
              var sort_Attribute_a = a.id?a.id:a.title
              var sort_Attribute_b = b.id?b.id:b.title
            	return sort_Attribute_a.localeCompare(sort_Attribute_b)
            })
          }
          if(this.selectCompareAttribute.indexOf('envDependencies')==-1)              { delete item.envDependencies }
          else{ //对envDependencies排序
						item.envDependencies = item.envDependencies.sort(function(a,b){
            	var sort_Attribute_a = a.id?a.id:a.title
              var sort_Attribute_b = b.id?b.id:b.title
            	return sort_Attribute_a.localeCompare(sort_Attribute_b)
            })
          }
          if(this.selectCompareAttribute.indexOf('preDependencies')==-1)              { delete item.preDependencies }
          else{ //对preDependencies排序
						item.preDependencies = item.preDependencies.sort(function(a,b){
            	var sort_Attribute_a = a.id?a.id:a.title
              var sort_Attribute_b = b.id?b.id:b.title
            	return sort_Attribute_a.localeCompare(sort_Attribute_b)
            })
          }
          if(this.selectCompareAttribute.indexOf('installBytes')==-1)                 { delete item.installBytes }
          if(this.selectCompareAttribute.indexOf('zipBytes')==-1)                     { delete item.zipBytes }
          if(this.selectCompareAttribute.indexOf('hasEmbeddedIcon')==-1)              { delete item.hasEmbeddedIcon }
          if(this.selectCompareAttribute.indexOf('hasLicense')==-1)                   { delete item.hasLicense }
          if(this.selectCompareAttribute.indexOf('hasReadme')==-1)                    { delete item.hasReadme }
          if(this.selectCompareAttribute.indexOf('isPrerelease')==-1)                 { delete item.isPrerelease }
          if(this.selectCompareAttribute.indexOf('isRequireLicenseAcceptance')==-1)   { delete item.isRequireLicenseAcceptance }
          if(this.selectCompareAttribute.indexOf('license')==-1)                      { delete item.license }
          if(this.selectCompareAttribute.indexOf('licenseUrl')==-1)                   { delete item.licenseUrl }
          if(this.selectCompareAttribute.indexOf('minClientVersion')==-1)             { delete item.minClientVersion }
          if(this.selectCompareAttribute.indexOf('releaseNotes')==-1)                 { delete item.releaseNotes }
          if(this.selectCompareAttribute.indexOf('summary')==-1)                      { delete item.summary }
          if(this.selectCompareAttribute.indexOf('authors')==-1)                      { delete item.authors }
          if(this.selectCompareAttribute.indexOf('compatibleVersion')==-1)            { delete item.compatibleVersion }
          if(this.selectCompareAttribute.indexOf('tags')==-1)                         { delete item.tags }
          if(this.selectCompareAttribute.indexOf('kind')==-1)                         { delete item.kind }
          if(this.selectCompareAttribute.indexOf('packageType')==-1)                  { delete item.packageType }
          if(this.selectCompareAttribute.indexOf('md5')==-1)                          { delete item.md5 }
        })
        
        return backupdata
      },
      
      selectAttributeChange(){
				if(this.selectCompareAttribute.indexOf('title')==-1){
					this.$message.info("title为必选比较项")
          this.selectCompareAttribute.push('title')
        }else{
					this.searchBackupVersionInfo(1)
          this.searchBackupVersionInfo(2)
          
          this.startCompare()
        }
      },
      
      filterOption(input,option){
				return (
          option.componentOptions.children[0].text.toLowerCase().indexOf(input.toLowerCase()) >= 0
        )
      },
      
      generateCompareResult(){
        //数据初始化
        this.compareReport = {
          addplugins:[],
          deleteplugins:[],
          modifiedplugins:[],
        }
        for(var i=0;i<this.selectBackupData_1_json.length;i++){
          //每个元素
          var item_1 = this.selectBackupData_1_json[i]
          //记录是否存在
          var isExist = false
					for(var j=0;j<this.selectBackupData_2_json.length;j++){
            
            var item_2 = this.selectBackupData_2_json[j]
            //执行比较
						if(item_1.id==item_2.id){
							isExist = true
              
              if(JSON.stringify(item_1,null,4)!=JSON.stringify(item_2,null,4)){
								//插件修改
                var diff_Component = {
                  data1: item_1,
                  data2: item_2
                }
                this.compareReport.modifiedplugins.push(diff_Component)
              }
              break
            }
          }
          
          if(!isExist){
            //插件减少
						this.compareReport.deleteplugins.push(item_1)
          }
        }
        
        for(var i=0;i<this.selectBackupData_2_json.length;i++){
          var item_2 = this.selectBackupData_2_json[i]
          var isExist = false
					for(var j=0;j<this.selectBackupData_1_json.length;j++){
            var item_1 = this.selectBackupData_1_json[j]
						if(item_2.id==item_1.id){
							isExist = true
              break
            }
          }
          
          if(!isExist){
            //插件新增
						this.compareReport.addplugins.push(item_2)
          }
        }
        
        //通知打开dialog
        EventBus.$emit('pluginconparereport',this.compareReport)
      }
      
		}
	})

</script>
<style>
.codediff{
	max-height:900px;
  overflow-y:auto;
}
.codediff>>>.d2h-code-side-line{
	height:25px;
}
</style>