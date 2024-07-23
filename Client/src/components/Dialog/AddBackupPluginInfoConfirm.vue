<template>
  <div>
		<a-modal 
      :visible="addbackupPluginInfoDialogVisible" 
      title="新增SDX版本数据备份" 
      @ok="saveToDB()"
      @cancel="addbackupPluginInfoDialogVisible=false"
      :closable="false"
      width="600px"
    >
      <a-form
					id="components-form-demo-normal-login"
					class="login-form"
					:hideRequiredMark="true"
				>
					<a-form-item class="mb-10" :colon="false">
            
            <span >Build版本：</span>
						<a-input
            :disabled="true"
            style="width:400px;"
            v-model="addBackupInfo.build"
            placeholder="build" />
					</a-form-item>
          <a-form-item class="mb-10" :colon="false">
            <span >大版本号：</span>
						<a-input
            style="width:400px;"
            v-model="addBackupInfo.version"
            placeholder="version" />
					</a-form-item>
          <a-form-item class="mb-10" :colon="false">
            <span >关键版本：</span>
						<a-switch @change="changeisKey()" checked-children='Y' un-checked-children='N' :checked="addBackupInfo.isKeyversion"></a-switch>
            <a-input
              v-show="addBackupInfo.isKeyversion"
              style="width:346px;margin-left:10px;"
              v-model="addBackupInfo.keyversion"
              placeholder="keyversion" />
					</a-form-item>
          <a-form-item class="mb-10" :colon="false">
            <span >备注信息：</span>
						<a-input
            style="width:400px;"
            v-model="addBackupInfo.remark"
            placeholder="remark" />
					</a-form-item>
          <a-form-item class="mb-10" :colon="false">
            <span >离线安装：</span>
						<a-input
            style="width:400px;"
            v-model="addBackupInfo.offlineinstaller"
            placeholder="Download Url" />
					</a-form-item>
				</a-form>
    </a-modal>
  </div>
</template>

<script>
import SdxBackupService from '../service/GetSdxBackupService';
import axios from 'axios'
import {EventBus} from '../../event-bus.js'
import {notification} from 'ant-design-vue'

export default {
  components:{
		SdxBackupService
  },
  data(){
		return{
			addbackupPluginInfoDialogVisible:false,
      addForm_preview:false,
      
      sdxBackupService : new SdxBackupService(),
      
      addBackupInfo:{
        build: "",
        version: "",
        isKeyversion: true,
        keyversion: "",
        remark: "",
        offlineInstaller:"",
        jsondata: "",
      },
      
    }
  },
  
  mounted(){
		EventBus.$on('addbackupPluginInfo',data=>{
      this.addBackupInfo = JSON.parse(JSON.stringify(data))
			this.addbackupPluginInfoDialogVisible = true
    })
  },
  
  methods:{
    changeisKey(){
			this.addBackupInfo.isKeyversion = !this.addBackupInfo.isKeyversion
      //console.log(this.addBackupInfo)
    },
    
    saveToDB(){
      this.addBackupInfo.jsondata = JSON.stringify(this.addBackupInfo.jsondata)
			this.sdxBackupService.addBackupVersionJsonData(this.addBackupInfo).then(res=>{
        
				if(res.data){
					this.addbackupPluginInfoDialogVisible = false
          
          notification.open({
						type:'success',
						message:'保存成功！',
						description:'可前往历史版本中查看详细信息',
						duration:5,
            onClick:()=>{
							this.$router.push('/backupversion')
						}, 
						style:{
							width:'500px',
							marginLeft:`${335-500}px`,
						}
					})
        }
        else{
					notification.open({
						type:'warning',
						message:'保存失败！',
						description:'检查相关流程',
						duration:5,
						style:{
							width:'500px',
							marginLeft:`${335-500}px`,
						}
					})
        }
      })
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
