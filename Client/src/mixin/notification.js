import { notification } from 'ant-design-vue'
export const myNotification = {
  data() {
    return{}
  },
  created() { },
  mounted() { },
  computed: {},
  methods: {
    
    /*成功类型提示*/
    myNotification_success(mes) {
      notification.open({
        type: 'success',
        message: mes,
        duration: 5,
        style: {
          width: '500px',
          marginLeft: `${335 - 500}px`,
        }
      })
    },
    /*成功类型提示-包含标题*/
    myNotification_success_withTitle(title,mes) {
      notification.open({
        type: 'success',
        message: title,
        description:mes,
        duration: 5,
        style: {
          width: '500px',
          marginLeft: `${335 - 500}px`,
        }
      })
    },
    /*失败类型提示*/
    myNotification_error(mes) {
      notification.open({
        type: 'error',
        message: mes,
        duration: 5,
        style: {
          width: '500px',
          marginLeft: `${335 - 500}px`,
        }
      })
    },
    /*失败类型提示-包含标题*/
    myNotification_error_withTitle(title, mes) {
      notification.open({
        type: 'error',
        message: title,
        description: mes,
        duration: 5,
        style: {
          width: '500px',
          marginLeft: `${335 - 500}px`,
        }
      })
    },
    /*警告类型提示*/
    myNotification_info(mes) {
      notification.open({
        type: 'info',
        message: mes,
        duration: 5,
        style: {
          width: '500px',
          marginLeft: `${335 - 500}px`,
        }
      })
    },
    /*警告类型提示-包含标题*/
    myNotification_info_withTitle(title, mes) {
      notification.open({
        type: 'info',
        message: title,
        description: mes,
        duration: 5,
        style: {
          width: '500px',
          marginLeft: `${335 - 500}px`,
        }
      })
    },
  }
}
