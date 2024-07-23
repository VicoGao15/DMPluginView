<template>
  <div>
		<a-modal 
      :visible="editbackupPluginInfoDialogVisible" 
      title="编辑SDX版本数据备份" 
      @ok="saveToDB()"
      @cancel="editbackupPluginInfoDialogVisible=false"
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
            v-model="editBackupInfo.build"
            placeholder="build" />
					</a-form-item>
          <a-form-item class="mb-10" :colon="false">
            <span >大版本号：</span>
						<a-input
            style="width:400px;"
            v-model="editBackupInfo.version"
            placeholder="version" />
					</a-form-item>
          <a-form-item class="mb-10" :colon="false">
            <span >关键版本：</span>
						<a-switch @change="changeisKey()" checked-children='Y' un-checked-children='N' :checked="editBackupInfo.isKeyVersion"></a-switch>
            <a-input
              v-show="editBackupInfo.isKeyVersion"
              style="width:346px;margin-left:10px;"
              v-model="editBackupInfo.keyVersion"
              placeholder="keyversion" />
					</a-form-item>
          <a-form-item class="mb-10" :colon="false">
            <span >备注信息：</span>
						<a-input
            style="width:400px;"
            v-model="editBackupInfo.remark"
            placeholder="remark" />
					</a-form-item>
          <a-form-item class="mb-10" :colon="false">
            <span >离线安装：</span>
						<a-input
            style="width:400px;"
            v-model="editBackupInfo.offlineinstaller"
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
			editbackupPluginInfoDialogVisible:false,
      addForm_preview:false,
      
      sdxBackupService : new SdxBackupService(),
      
      editBackupInfo:{
        build: "",
        version: "",
        isKeyVersion: false,
        keyversion: "",
        remark: "",
        offlineinstaller:"",
        jsondata: "",
      },
      
    }
  },
  
  mounted(){
		EventBus.$on('editbackupPluginInfo',data=>{
      this.editBackupInfo = JSON.parse(JSON.stringify(data))
      //console.log(this.editBackupInfo)
      //console.log(JSON.stringify(this.editBackupInfo.jsondata))
			this.editbackupPluginInfoDialogVisible = true
    })
  },
  
  methods:{
    changeisKey(){
			this.editBackupInfo.isKeyVersion = !this.editBackupInfo.isKeyVersion
      console.log(this.editBackupInfo.isKeyVersion)
    },
    
    saveToDB(){
      this.editBackupInfo.jsondata = JSON.stringify(this.editBackupInfo.jsondata)
      //console.log(this.editBackupInfo)
			this.sdxBackupService.editBackupVersionJsonData(this.editBackupInfo).then(res=>{
        
				if(res.data){
					this.editbackupPluginInfoDialogVisible = false
          
          EventBus.$emit('reloadBackupVersionInfo')
          notification.open({
						type:'success',
						message:'编辑成功！',
						duration:5,
						style:{
							width:'500px',
							marginLeft:`${335-500}px`,
						}
					})
        }
        else{
					notification.open({
						type:'warning',
						message:'编辑失败！',
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
