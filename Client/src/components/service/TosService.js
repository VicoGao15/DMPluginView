import axios from 'axios'

export default class TosService {

  constructor(){

    this.apiHost = "http://localhost:3100/api"
    this.token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2NzMzNDQxNDAsImlzcyI6Imp3dHNlcnZlciIsImF1ZCI6Imp3dHNlcnZlci9yZXNvdXJjZXMifQ.kagcKaYfN3ozki55Kp1t8fs8uDg2inwIamIl7AfHoRM"
  }
  
  /* 会议室*/
  getAllMettingRoom(){
    var url = this.apiHost + "/meetting-room/page?pageNum=1&pageSize=10"
    return (axios({
      method:'get',
      url:url,
      headers:{'Authorization': this.token}
    }));
  }
  addMettingRoom(body) {
    var url = this.apiHost + "/meetting-room"
    return (axios({
      method: 'post',
      data:body,
      url: url,
      headers: { 'Authorization': this.token }
    }));
  }
  deleteMettingRoom(id){
    var url = this.apiHost + "/meetting-room/" + id
    return (axios({
      method: 'delete',
      url: url,
      headers: { 'Authorization': this.token }
    }));
  }
  updateMettingRoom(item){
    var url = this.apiHost + "/meetting-room/" + item.id
    var body = {
      name:item.name,
      maxNumber:item.maxNumber
    }
    return (axios({
      method: 'put',
      data: body,
      url: url,
      headers: { 'Authorization': this.token }
    }));
  }
    
}
      