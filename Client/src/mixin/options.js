import VkspService from '../components/service/VkspService'
export const system_options = {
  data() {
    const vkspService=new VkspService();
    return {
      vkspService,
      ///
      /*Todo相关*/
      ///
      priorityOptions :[//优先级
				{ value: 0, text: 'P0-刻不容缓'},
        { value: 1, text: 'P1-事不宜迟'},
        { value: 2, text: 'P2-循序渐进'},
        { value: 3, text: 'P3-未雨绸缪'},
        { value: 4, text: 'P4-源远流长'},
      ],
      cardStyleOption: [//card样式
        { key: '0', imgsrc: 'images/background/yellow.png' },
        { key: '1', imgsrc: 'images/background/blue.png' },
        { key: '2', imgsrc: 'images/background/white.png' },
        { key: '3', imgsrc: 'images/background/bianqian.png' },
        { key: '4', imgsrc: 'images/background/huaban.png' },
        { key: '5', imgsrc: 'images/background/zhiban.png' }
      ],
      
      ///
      /*知识库相关*/
      ///
      userOptions:[],//用户
      conceptTypeOptions:[],
      pagination:{
					total:0,
          pageSize:10,
          showSizeChanger:true,
          pageSizeOptions:['10','15','20','50'],
          showTotal:total => `共 ${total} 条数据`
        },
      tableColumns:[
					{
						title: '所属模块',
						dataIndex: 'module',
						scopedSlots: { customRender: 'type' },
						width: 200,
            sortDirections:["descend","ascend"],
            sorter:(a,b)=>a.module.length-b.module.length
					},
					{
						title: '创建者',
						dataIndex: 'userInfo',
						scopedSlots: { customRender: 'user' },
						width: 100,
            filters:[
							{text:'Vico',value:'Vico'},
              {text:'Admin',value:'Admin'},
              {text:'Song',value:'Song'},
              {text:'Other',value:'Sherry'}
            ], 
            //filters:userFilterOptions,
            filterMultiple:true,
            onFilter:(value,record)=>record.creater.indexOf(value)===0
					},
					{
						title: '创建日期',
						dataIndex: 'create_Date',
            scopedSlots: { customRender: 'createDate1' },
						width: 250,
            sortDirections:["descend","ascend"],
            sorter:(a,b)=>this.dateTimeStamp(a.create_Date)-this.dateTimeStamp(b.create_Date)
					},
          {
						title: '标题',
						dataIndex: 'header',
            scopedSlots: { customRender: 'slotHeader' },
            width: 400,
						class: 'font-bold text-muted text-sm',
					},
          {
            title: '简易内容',
            dataIndex: 'simpleContent',
            scopedSlots: { customRender: 'slotsimpleContent' },
            class: 'font-bold text-muted text-sm',
          }
				],
      tableColumns_personal: [
        {
          title: '所属模块',
          dataIndex: 'module',
          scopedSlots: { customRender: 'type' },
          width: 150,
        },
        //{
        //  title: '创建者',
        //  dataIndex: 'userInfo',
        //  scopedSlots: { customRender: 'user' },
        //  width: 100,
        //},
        {
          title: '创建日期',
          dataIndex: 'create_Date',
          scopedSlots: { customRender: 'createDate1' },
          width: 250,
        },
        {
          title: '标题',
          dataIndex: 'header',
          scopedSlots: { customRender: 'slotHeader' },
          width: 400,
          class: 'font-bold text-muted text-sm',
        },
        {
          title: '简易内容',
          dataIndex: 'simpleContent',
          scopedSlots: { customRender: 'slotsimpleContent' },
          class: 'font-bold text-muted text-sm',
        },
        {
          title: '操作',
          dataIndex: 'isPrivate',
          scopedSlots: { customRender: 'isPrivate' },
          width: 180,
          class: 'font-bold text-muted text-sm',
        }
      ],
      
      
      ///
      /*用户中心相关*/
      ///
      backgroudOption:[//背景图
				{key:'1',imgsrc:'images/bg-signup.jpg'},
        {key:'2',imgsrc:'images/bg-profile.jpg'},
        {key:'3',imgsrc:'images/info-card-2.jpg'},
        {key:'4',imgsrc:'images/info-card-3.jpg'},
        {key:'8',imgsrc:'images/dark-gradient.png'},
        {key:'5',imgsrc:'images/home-decor-1.jpeg'},
        {key:'6',imgsrc:'images/home-decor-2.jpeg'},
        {key:'7',imgsrc:'images/home-decor-3.jpeg'}
      ],
      selectMonthOption:[
				{value:'0',label:'月份不限'},
        {value:'01',label:'1 月'},
        {value:'02',label:'2 月'},
        {value:'03',label:'3 月'},
        {value:'04',label:'4 月'},
        {value:'05',label:'5 月'},
        {value:'06',label:'6 月'},
        {value:'07',label:'7 月'},
        {value:'08',label:'8 月'},
        {value:'09',label:'9 月'},
        {value:'10',label:'10 月'},
        {value:'11',label:'11 月'},
        {value:'12',label:'12 月'}
      ],
      selectYearOption:[
				{value:'0',label:'年份不限'},
        { value: '2024', label: '2024' },
        {value:'2023',label:'2023'},
        {value:'2022',label:'2022'},
        {value:'2021',label:'2021'},
      ],
      selectIsPrivateOption:[
        { value: true, label: '私密' },
        { value: false, label: '公共' }
      ],
      
      ///
      /*公共*/
      ///
      //quill富文本编辑器配置
      editorOption:{
        modules:{
          ImageExtend:{
						loading: true,
            name: 'img',
            action: 'http://172.25.13.72:8880/VKSP/image/',//上传的服务器地址
            response: res => {
              return res.data
            }
          },
          imageResize:{
            displayStyles:{
              backgroundColor:'black',
              border:'none',
              color:'white'
            },
            modules:['Resize', 'DisplaySize', 'Toolbar']
          },
          toolbar:[
            ['bold', 'italic', 'underline', 'strike'], // 字体
            ['blockquote', 'code-block'],
            [{ 'header': 1 }, { 'header': 2 }], // 样式标题
            [{ 'list': 'ordered'}, { 'list': 'bullet' }],
            [{ 'script': 'sub'}, { 'script': 'super' }], // 下标、上标
            [{ 'indent': '-1'}, { 'indent': '+1' }], // 缩进
            [{ 'direction': 'rtl' }],
            [{ 'size': ['small', false, 'large', 'huge'] }], // 字体
            [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
            [{ 'color': [] }, { 'background': [] }],
            [{ 'font': [] }],
            [{ 'align': [] }],
            ['clean'], // 格式清除 
            ["link", "image", "video"] // 链接、图片、视频
          ]
        }
      },
      
    }
  },
  created() {
    this.getUserOptions()
  },
  mounted() { },
  computed: { },
  methods: {
    //获取所有用户形成下拉option选项
    getUserOptions(){
      var itemoption_all = {
        value: 0, text: 'All'
      }
      this.userOptions.push(itemoption_all)
      this.vkspService.getAllUser().then(res=>{
        res.data.forEach(item=>{
          var itemoption = {
            value: item.id, text: item.username, avatar: item.avatarUrl
          }
          this.userOptions.push(itemoption)
        })
      })
    }
  }
}