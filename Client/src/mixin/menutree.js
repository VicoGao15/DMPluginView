export const mixins_menutree = {
  data() {
    return {
      menuTree_json : [],
      menu_init:[],
      nodeNum : 0
    }
  },
  created() { },
  mounted() { },
  computed: {},
  methods: {
    
    /*生成树状结构的文章目录树json
    content:文章内容
    */
    generateDirectoryTree_Json(content){
      this.menuTree_json = []
      const regex = /<h[1-6](.*?)>(.*?)<\/h[1-6]>/g;

      var match
      while ((match = regex.exec(content)) !== null) {
        var item = {
          level: this.menuItemLevel(match[0]),      //层级：H1-1/H2-2/H3-3/H4-4/H5-5/H6-6
          anchor: this.getElementmaodian(match[0]), //锚点
          title: this.getElementcontent(match[0]),  //内容
          child: [],                                //子节点
          isExpand:true                             //默认该树节点展开
        }
          
        this.menu_init.push(item)
        this.addMenuitem(this.menuTree_json,item) //从树根节点开始添加子节点
        
        //节点数量
        this.nodeNum++
      }
    },
    
    /*递归插入*/
    addMenuitem(menu, item){
      if (menu.length === 0) {
        menu.push(item)
      }
      else {
        if (item.level > menu[menu.length - 1].level) {
          //递归添加子节点
          this.addMenuitem(menu[menu.length - 1].child, item)
        }
        else if (menu.length - 1 === 0) {
          menu.push(item)
        }
        else {
          menu.push(item)
        }
      }
    },
    
    /*搜索匹配插入，不递归*/
    
    /*onSearchMenu 
    搜索目录，重新生成目录
    */
    onSearchMenu() {
      this.menuTree_json = []
      
      this.menu_init.forEach(item => {
        if (item.title.toLowerCase().indexOf(this.searchMenu.toLowerCase()) != -1) {
          item.child=[]
          this.addMenuitem(this.menuTree_json, item)
        }
      })
    },
    

    /*文章结构标题缩进*/
    menustyle(menuiten) {
      var type = menuiten.slice(1, 3)
      var menutyle = ''
      switch (type) {
        case 'h1': menutyle = 'margin-left:10px;'; break
        case 'h2': menutyle = 'margin-left:20px;'; break
        case 'h3': menutyle = 'margin-left:30px;'; break
        case 'h4': menutyle = 'margin-left:40px;'; break
        case 'h5': menutyle = 'margin-left:50px;'; break
        case 'h6': menutyle = 'margin-left:60px;'; break
        default: break
      }
      
      return menutyle
    },
    
    /*标题级别*/
    menuItemLevel(menuiten) {
      var type = menuiten.slice(1, 3)
      var level = 0
      switch (type) {
        case 'h1': level = 1; break
        case 'h2': level = 2; break
        case 'h3': level = 3; break
        case 'h4': level = 4; break
        case 'h5': level = 5; break
        case 'h6': level = 6; break
        default: break
      }
      
      return level
    },
    
    /*获取标签锚点*/
    getElementmaodian(item) {
      var placeholder = document.createElement('div')
      placeholder.innerHTML = item
      
      //返回id锚点
      return '#' + placeholder.firstElementChild.id
    },
    
    /*锚点跳转*/
    goAnchor(selector) {
      document.querySelector(selector).scrollIntoView({
        behavior: 'smooth'
      })
    },
    
    /*获取标签内容*/
    getElementcontent(item) {
      var placeholder = document.createElement('div')
      placeholder.innerHTML = item
      //返回dom显示内容
      return placeholder.firstElementChild.innerHTML
    },
  }
}
