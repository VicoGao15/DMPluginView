import * as XLSX from 'xlsx'
import * as mammoth from 'mammoth'
import { EventBus } from '../event-bus'
import { notification } from 'ant-design-vue'
import htmlDocx from 'html-docx-js/dist/html-docx'
import FileSaver from 'file-saver'

export const mixins = {
  data(){
    return{
      fileContents:'',//读取的文件内容
      judgeEditOrAddFlg:0,
    }
  },
  created() {},
  mounted() {
    /*监听键盘按键*/
    this.keyDown()
  },
  computed: {},
  methods: {
    /*
    时间格式
    */
    showTimeFormat(date, showDay) {
      var timeArr = (date || '').split('T')
      var _h_m_s = (timeArr[1] || '').split(':')
      
      var d = new Date(date)
      //var week = '周一';
      //switch (d.getDay()) {
      //  case 2: week = '周二'; break;
      //  case 3: week = '周三'; break;
      //  case 4: week = '周四'; break;
      //  case 5: week = '周五'; break;
      //  case 6: week = '周六'; break;
      //  case 7: week = '周天'; break;
      //  default: week = '周一'; break;
      //}
      var week = '周' + '日一二三四五六'[d.getDay()]

      var retuenValue = showDay ? timeArr[0] + " / " + week : _h_m_s[0] + ':' + _h_m_s[1]
      return retuenValue
    },
    timeFormate(publishtime) {
      const dates = new Date(publishtime);
      
      const year = dates.getFullYear();//year
      const month = (dates.getMonth() + 1) < 10 ? '0' + (dates.getMonth() + 1) : (dates.getMonth() + 1);//month
      const day = dates.getDate() < 10 ? '0' + dates.getDate() : dates.getDate();
	
      const hour = dates.getHours() + 8;//北京时间
      const minute = dates.getMinutes() < 10 ? '0' + dates.getMinutes() : dates.getMinutes();
      const seconds = dates.getSeconds() < 10 ? '0' + dates.getSeconds() : dates.getSeconds();
			
      var week = '周' + '日一二三四五六'[dates.getDay()]

      return year + '-' + month + '-' + day + ' ' + hour + ':' + minute + ' ' + week;
    },
    
    /*
    从文件中读取内容
    */    
    importFile(file) {
      if (file) {
        if (typeof FileReader === "undefined") {
          this.$message.error('该浏览器不支持文件读取！！！')
          return
        } else if (file.file.type == 'application/json') {
          this.importJsonFile(file)
        } else if (file.file.type == 'text/plain') {
          this.importTxtFile(file)
        } else if (file.file.type == 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet') {
          this.importExcelFile(file)
        } else if (file.file.type == 'application/vnd.openxmlformats-officedocument.wordprocessingml.document') {
          this.importDocxFile(file)
        } else {
          this.$message.error('请选择Json/Text/Excel格式文件！！！')
          return
        }
      }
    },
    importFile_Todo(file, flag) {
      this.judgeEditOrAddFlg = flag
      if (file) {
        if (typeof FileReader === "undefined") {
          this.$message.error('该浏览器不支持文件读取！！！')
          return
        } else if (file.file.type == 'application/json') {
          this.importJsonFile(file)
        } else if (file.file.type == 'text/plain') {
          this.importTxtFile(file)
        } else if (file.file.type == 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet') {
          this.importExcelFile(file)
        } else if (file.file.type == 'application/vnd.openxmlformats-officedocument.wordprocessingml.document') {
          this.importDocxFile(file)
        } else {
          this.$message.error('请选择Json/Text/Excel格式文件！！！')
          return
        }
      }
    },
    //从Json文件导入的
    importJsonFile(file) {
      const readerJson = new FileReader()
        
      readerJson.onload = () => {
        const result = readerJson.result
        this.fileContents = result
        EventBus.$emit('fileloadcomplete')
      }
      readerJson.readAsText(file.file.originFileObj)
    },
      
    //从txt文件导入的
    importTxtFile(file) {
      const readerTxt = new FileReader()
  
      readerTxt.onload = () => {
        const result = readerTxt.result
        this.fileContents = result
        EventBus.$emit('fileloadcomplete')
      }
      readerTxt.readAsText(file.file.originFileObj, "GB2312") //使用UTF-8会乱码
    },
          
    //从Excel文件导入的
    importExcelFile(file) {
      // 通过FileReader对象读取文件
      const reader = new FileReader();
      // readAsArrayBuffer之后才会启动onload事件
      reader.onload = () => {
        const data = new Uint8Array(reader.result)
        const { SheetNames, Sheets } = XLSX.read(data, { type: 'array' })
        //console.log(SheetNames, Sheets, '列')
            
        SheetNames.forEach(item => {
          const workSheets = Sheets[item]
          const sheetRows = XLSX.utils.sheet_to_html(workSheets);
          this.fileContents = sheetRows
          EventBus.$emit('fileloadcomplete')
        })
            
      }
      // uploadFile是上传文件的文件流
      reader.readAsArrayBuffer(file.file.originFileObj)
    },
      
    //从Docx文件导入
    importDocxFile(file) {
      // 通过FileReader对象读取文件
      const readerDocx = new FileReader();
      // readAsArrayBuffer之后才会启动onload事件
      readerDocx.onload = () => {
        const result = readerDocx.result
        mammoth.convertToHtml({ arrayBuffer: result }).then(r => {
          this.fileContents = r.value
          EventBus.$emit('fileloadcomplete')
        })
      }
      // uploadFile是上传文件的文件流
      readerDocx.readAsArrayBuffer(file.file.originFileObj)
    },
    
    /*function 监听键盘按键*/
    keyDown() {
      document.onkeydown = (e) => {
        //事件对象兼容
        let e1 = e || event || window.event || arguments.callee.caller.arguments[0]
        //Shift + A
        if (e1.shiftKey && e1.keyCode == 65) {
          EventBus.$emit('Keydown_Shift_A')
        }
      }
    },
    
    //复制文本 插件：vue-clipboard2
    /*function 复制文本内容*/
    doCopy(value) {
      this.$copyText(value).then(() => {
        this.myNotification('success', '已复制文章链接', value)
      })
    },
    
    /*复制文章内容并将其转化为富文本内容*/
    copyContent(concept) {
      var copyHandler = this.copys(concept.contents)
      document.addEventListener('copy', copyHandler)
      document.execCommand('copy')
      removeEventListener('copy', copyHandler)
      
      this.myNotification('success', '已复制文章', concept.header)
    },
    copys(article) {
      return function(event) {
        event.clipboardData.setData('text/html', article)
        event.preventDefault()
      }
    },
    
    /*将concept保存为docx文件*/
    saveConceptToDocx(concept){
      var content = concept.contents
      
      var converted = htmlDocx.asBlob(content)
      
      FileSaver.saveAs(converted, concept.header+'.docx')
    },
    
    /*信息栏*/
    myNotification(type, message, des){
      notification.open({
        type: type,
        message: message,
        description: des,
        duration: 5,
        style: {
          width: '500px',
          marginLeft: `${335 - 500}px`,
        }
      })
    }
    
  }
}