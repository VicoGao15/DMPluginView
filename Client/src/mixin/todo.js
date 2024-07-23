// JavaScript source code
export const Todo_Mixin={
  data() {
    return {}
  },
  created() { },
  mounted() { },
  computed: {},
  methods: {
    /*
    Function card样式
    */
    todoCardStyle(type) {
      var style = "height:40px;width:40px;background-size:100% 100%;box-shadow:none;"
      switch (type) {
        case 0:
          style += "background-image: url('images/background/yellow.png');"
          break
        case 1:
          style += "background-image: url('images/background/blue.png');"
          break
        case 2:
          style += "background-image: url('images/background/white.png');"
          break
        case 3:
          style += "background-image: url('images/background/bianqian.png');"
          break
        case 4:
          style += "background-image: url('images/background/huaban.png');"
          break
        default:
          style += "background-image: url('images/background/zhiban.png');"
          break
      }
      
      return style
    },
  }
}