import axios from 'axios'
import GLOBAL from '../../src/GLOBAL.js'
  
export default class ApipsService {
  
  constructor(){
    //获取数据接口服务
    this.dataReader = GLOBAL.dataReader
    this.monitor = GLOBAL.monitor
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
    var healthCheckItemUrl = this.dataReader +"/api/MonitoredService/delete/" + id
    return (axios.get(healthCheckItemUrl));
  }
    
  updateCheckItem(body) {
    var healthCheckItemUrl = this.dataReader +"/api/MonitoredService/update"
    return (axios.post(healthCheckItemUrl, body));
  }
      
}
