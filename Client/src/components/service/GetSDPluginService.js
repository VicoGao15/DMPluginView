import axios from 'axios'
import GLOBAL from '../../Global.vue'

export default class SdpliginService {
  
        
  getPlugin(sdpluginUrl, pluginServer, selectVersion) {
    const apiUrl = GLOBAL.apiAddress
    //const apiUrl = 'http://localhost/api.pluginViewer/api'
    //const apiUrl ='https://172.25.13.2:32002/Api'
    //const apiUrl = 'https://localhost:7483/api'
      
    var pluginUri=''
    if (pluginServer.pluginserver === "devmagic"){
      pluginUri = "http://" + pluginServer.pluginserversdx + "." + pluginServer.pluginserver + ".com" + sdpluginUrl
    }else{
      pluginUri = "http://" + pluginServer.pluginserversdx + "." + pluginServer.pluginserver + ".com" + sdpluginUrl
    }
    
    pluginUri = selectVersion === '' ? pluginUri : pluginUri + 'TargetVersionIncludeOnly=True&version=' + selectVersion
      
    return (axios.get(apiUrl, {
      headers: { 'uri': pluginUri }
    })) 
    
  }
  
  getInstaller(pluginServer){
    const apiUrl = GLOBAL.apiAddress
    
    var installerUri = "http://" + pluginServer.pluginserversdx + "." + pluginServer.pluginserver + ".com/api/v3/product/getpackages?PackageType=SDSDK-INSTALLER"
      
    return (axios.get(apiUrl, {
      headers: { 'uri': installerUri }
    }))
  }
    
  getallExtentiomVersionById(pluginServer, id){
    const apiUrl = GLOBAL.apiAddress
      
    var devmagicpluginUri = "http://" + pluginServer.pluginserversdx + "." + pluginServer.pluginserver +".com/api/v3/product/getlogs?packageId=" + id + "&version=0"
    console.log(devmagicpluginUri)
    
    return (axios.get(apiUrl,{
      headers: {'uri':devmagicpluginUri}
    }))
  }
  
  getPluginById_Version(pluginServer, id, version){
    const apiUrl = GLOBAL.apiAddress
      
    var devmagicpluginUri = "http://" + pluginServer.pluginserversdx + "." + pluginServer.pluginserver + ".com/api/v3/product/getpackages?PackageIds=" + id + "&Version=" + version
    
    console.log(devmagicpluginUri)
    return (axios.get(apiUrl, {
      headers: { 'uri': devmagicpluginUri }
    }))
  }
  
  getPackageById(id){
    const apiUrl = GLOBAL.apiAddress
    
    const uri ="https://sdplugin.devmagic.com/api/v3/product/getpackages?PackageIds="+id
    
    return (axios.get(apiUrl,{
      headers:{'uri': uri}
    }))
  }
  
  getAll(){
    const apiUrl = GLOBAL.apiAddress
    
    const uri ="http://sdplugin.aipuyang.com/api/v3/product/getpackages?packageType=SDSDK;SDSDK-WORKLOAD;SDSDK-CORE;SDSDK-EXTENSION;SDSDK-ENVIRONMENT&version=25"
      
    return (axios.get(apiUrl, {
      headers: { 'uri': uri }
    }))
  }
  
  getProducts(server){
    const apiUrl = GLOBAL.apiAddress
    
    const uri = "https://" + server.pluginserversdx + "." + server.pluginserver + ".com/api/v3/product/getproducts"
    
    return (axios.get(apiUrl, {
      headers: { 'uri': uri }
    }))
  }
}
      