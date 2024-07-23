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
    this.todoHost = "http://172.25.13.72/api.pluginViewer/api/todo"
    //this.todoHost = "https://localhost:7483/api/Todo"
    
    this.collectionConceptHost = "http://172.25.13.72/api.pluginViewer/api/CollectionConcept"
  }
  
  getAllConcept(url){
    var checkUrl = this.vkspConcept +"/Retrieve"
    return (axios.get(checkUrl));
  }
  editConcept(concept) {
    var checkUrl = this.vkspConcept +'/Update'
    return (axios.post(checkUrl, concept));
  }
  addConcept(newconcept){
    var url = this.conceptHost
    //var url = 'https://localhost:7483/api/Concept'
    return (axios.post(url, newconcept)); 
  }
  updateConcept(concept){
    var url = this.conceptHost
    //var checkUrl = 'https://localhost:7483/api/Concept'
    return (axios.put(checkUrl, concept));
  }
    
  getAllDynamic(url) {
    
    var checkUrl = this.vkspDynamic + "Retrieve"
    return (axios.get(checkUrl));
  }
  
  /*
    获取Concept部分属性 EFCore
  */
  getAllConceptNeed(){
    var url = this.conceptHost + "/allneed"
    //var url = 'https://localhost:7483/api/Concept/allneed'
    return (axios.get(url));
  }
    
  getConceptDetail(id){
    var url = this.conceptHost + '/' + id
    return (axios.get(url));
  }
  
  getConceptWithoutContent(id) {
    var url = this.conceptHost + '/withoutcontent/' + id
    return (axios.get(url));
  }
    
  /*
    评论
  */
  getConceptComments(conceptid){
    var url = this.commentHost + "/concepts/" + conceptid
    return (axios.get(url));
  }
  
  addComment(commentItem) {
    var url = this.commentHost
    return (axios.post(url, commentItem));
  }
  
  updateConcept(concept){
    var url = this.conceptHost
    return (axios.put(url, concept));
  }
  
  deleteConcept(id){
    var url = this.conceptHost + "?id="+id
    return (axios.delete(url));
  }
  
  /*
    User
  */
  getAllUser(){
    var url=this.userHost
    return (axios.get(url));
  }
  
  getUserInfoById(id){
    var url = this.userHost + '/' + id
    return (axios.get(url))
  }
  
  addUser(UserItem){
    var url = this.userHost
    return (axios.post(url, UserItem));
  }
  
  loginValidation(username,pwd){
    var url = this.userHost + "/login?username=" + username + "&pwd=" + pwd
    return (axios.post(url));
  }
  
  updateUser(userinfo) {
    var url = this.userHost
    return (axios.put(url,userinfo));
  }
  
  getConceptsByUserid(userid) {
    var url = this.conceptHost + "/userconcept?userid=" + userid
    return (axios.get(url));
  }
    
  //获取用户所有收藏的文章
  getCollectionConceptByUserId(userid){
    var url = this.collectionConceptHost + "/" + userid
    return (axios.get(url));
  }
  
  //检查某文章是否已经收藏某文章
  checkCenceptIfCollection(userid,conceptid){
    var url = this.collectionConceptHost + "/checkifcollection?" + "userid=" + userid + "&conceptid=" + conceptid
    return (axios.get(url));
  }
  
  //用户添加收藏文章
  addCollectionConcept(user, concept){
    var data = {
      userid: user,
      conceptid: concept
    }
      
    var url = this.collectionConceptHost
    return (axios.post(url, data));
  }
  
  //用户移除收藏文章
  deleteCollectionConcept(userid, conceptid){
    
    var url = this.collectionConceptHost + "/deleteofcollection?" + "userid=" + userid + "&conceptid=" + conceptid
    return (axios.delete(url));
  }
  
  
  /*
    评论comment
  */
  getAllComment(url) {
    var checkUrl = this.vkspComment + "Retrieve"
    
    return (axios.get(checkUrl));
  }
  
  deleteComment(id){
    var url = this.commentHost+"?id="+id
    return (axios.delete(url));
  }
  
  /*
    Todo
  */  
  getAllTodo(url) {
    var checkUrl = this.todoHost + "/alltodoneed"
    return (axios.get(checkUrl));
  }
  
  getAllUserTodo(userid){
    var checkUrl = this.todoHost + "/user/" + userid
    return (axios.get(checkUrl));
  }
  
  addTodo(todoItem){
    var url = this.todoHost
    return (axios.post(url, todoItem));
  }
    
  updateTodo(todoItem){
    var url = this.todoHost
    return (axios.put(url, todoItem));
  }
  
  getTodoDetail(id){
    var checkUrl = this.todoHost + "/" + id
    return (axios.get(checkUrl));
  }
  
  deleteTodo(id){
    var checkUrl = this.todoHost + "?id=" + id
    return (axios.delete(checkUrl));
  }
}
      