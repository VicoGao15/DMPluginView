import axios from 'axios'
//import GLOBAL from '../../Global.vue'

export default class MonitorService {

  constructor(){
    //获取数据接口服务
    this.dataReader = "https://monitor.aipuyang.com/DataReader"
    this.monitor = "https://monitor.aipuyang.com/Monitor"
  }
  
  getapistate(url){
    var checkUrl = this.monitor +"/api/MonitoredService?url="+url
    return (axios.get(checkUrl));
  }
  
  getCheckItem(){
    var healthCheckItemUrl = this.dataReader +"/api/MonitoredService"
    return (axios.get(healthCheckItemUrl));
  }
    
  addCheckItem(body) {
    var healthCheckItemUrl = this.dataReader +"/api/MonitoredService/add"
    return (axios.post(healthCheckItemUrl,body));
  }
    
  deleteCheckItemById(id){
    //console.log(id)
    var healthCheckItemUrl = this.dataReader +"/api/MonitoredService/delete/" + id
    return (axios.get(healthCheckItemUrl));
  }
    
  updateCheckItem(body) {
    var healthCheckItemUrl = this.dataReader +"/api/MonitoredService/update"
    return (axios.post(healthCheckItemUrl, body));
  }
}
      