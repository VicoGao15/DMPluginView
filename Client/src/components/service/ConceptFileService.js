import axios from 'axios'

export default class ConceptFileService {

  constructor(){

    //api.sdpluginviewer EFCore
    this.fileHost ="http://172.25.13.72/api.pluginViewer/api/Dbfile"
  }
  
  getFileByConceptId(id){
    var checkUrl = this.fileHost + "/concepts/" + id
    return (axios.get(checkUrl));
  }
  addconceptfile(newconceptfile){
    var Url = this.fileHost
    return (axios.post(Url, newconceptfile)); 
  }
  deleteConceptFile(fileid){
    var Url = this.fileHost + "?id=" + fileid
    return (axios.delete(Url)); 
  }
    
}
      