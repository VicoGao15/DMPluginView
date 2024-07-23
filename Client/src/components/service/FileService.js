import axios from 'axios'

export default class VkspService {

  constructor(){
    //获取数据接口服务 snapobject
    this.vkspConcept = "http://172.25.13.72/api.vksp/api/Concept"
    this.vkspComment = "http://172.25.13.72/api.vksp/api/Comment"
    this.vkspDynamic = "http://172.25.13.72/api.vksp/api/dynamic"
    this.vkspUser = "http://172.25.13.72/api.vksp/api/user"
    this.vkspTodo = "http://172.25.13.72/api.vksp/api/Todo"
    
    //api.sdpluginviewer EFCore
    this.conceptHost ="http://172.25.13.72/api.pluginViewer/api/concept"
    this.userHost ="http://172.25.13.72/api.pluginViewer/api/user"    
    this.commentHost = "http://172.25.13.72/api.pluginViewer/api/comment"    
  }
  
  uploadFile(url){
    var checkUrl = this.vkspConcept +"/Retrieve"
    return (axios.get(checkUrl));
  }

}
      