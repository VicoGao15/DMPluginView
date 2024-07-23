import axios from 'axios'
import GLOBAL from '../../Global.vue'

export default class SdpliginService {
  constructor() {
    //api.sdpluginviewer EFCore
    this.sdxbackupHost = "http://172.25.13.72/api.pluginViewer/api/SdxBackup"
    //this.sdxbackupHost = "https://localhost:7483/api/SdxBackup"
  }
  
  getAllBackupVersion(){
    var url = this.sdxbackupHost
    return (axios.get(url));
  }
    
  getBackupVersionJsonDataById(id){
    var url = this.sdxbackupHost + '/' + id
    
    return (axios.get(url))
  }
  
  addBackupVersionJsonData(newJsonData) {
    var Url = this.sdxbackupHost
    return (axios.post(Url, newJsonData)); 
  }
  
  editBackupVersionJsonData(editJsonData){
    var Url = this.sdxbackupHost
    return (axios.put(Url, editJsonData)); 
  }
  
  deleteBackupVersionJsonData(id){
    var Url = this.sdxbackupHost + "?id=" + id
    return (axios.delete(Url))
  }
}
      