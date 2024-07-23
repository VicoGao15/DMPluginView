export const VkspUser = {
  data() {
    return{}
  },
  created() { },
  mounted() { },
  computed: {},
  methods: {
    
    //获取cookie是否登录信息
    getLoginMessage() {
      
      var userinfo = {}
      var arr = document.cookie.split(';')
      
      for (var i = 0; i < arr.length; i++) {
        var arr2 = arr[i].split('=')
        if (arr2[0] === ' userinfo' || arr2[0] === 'userinfo') {
          userinfo = JSON.parse(arr2[1])
					
          break
        }
      }
      
      return userinfo
    },
    
    //判断是否登录
    ifIsLogin(){
      var islogin = false
      var arr = document.cookie.split(';')
      
      for (var i = 0; i < arr.length; i++) {
        var arr2 = arr[i].split('=')
        if (arr2[0] === ' isLogin' || arr2[0] === 'isLogin') {
          islogin = arr2[1]=='true'?true:false
					
          break
        }
      }
      
      return islogin
    },
    
    //测试方法
    testvkspuserfunction(){
      console.log('testvkspuserfunction')
    },
        
    //返回已发布文章的时间顺序结构
    handleCollectionConceptStructure(data){
      var structure = []
      
      data.forEach(item=>{
        var _y_m_d = (item.create_Date || '').split('T')[0].split('-')
        var _y_m = _y_m_d[0] + '年' + _y_m_d[1] + '月'
        
        if (structure.indexOf(_y_m)==-1){
          structure.push(_y_m)
        }
      })
      return structure
    }
      
  }
}
