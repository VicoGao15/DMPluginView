<template>
  <div>
		<a-modal 
      :visible="pluginCompareReportDialogVisible" 
      title="对比报告" 
      @ok="pluginCompareReportDialogVisible=false"
      @cancel="pluginCompareReportDialogVisible=false"
      :closable="false"
      width="1000px"
    >
      <a-row :gutter="16">
				<a-col :span="8">
					<a-card size="small" title="新增插件" :bordered="false" :hoverable="true" style="border-radius:5px;">
						<span slot="extra">数量：{{compareReport.addplugins.length}}</span>
            <div style="margin:5px;max-height:600px;overflow:auto;">
							<div v-for="item in compareReport.addplugins" :key="item.id">
								<p style="color:#8edc36"><i>{{item.id}}</i></p>
              </div>
            </div>
          </a-card>
        </a-col>
        <a-col :span="8">
					<a-card size="small" title="减少插件" :bordered="false" :hoverable="true" style="border-radius:5px;">
						<span slot="extra">数量：{{compareReport.deleteplugins.length}}</span>
            <div style="margin:5px;max-height:600px;overflow:auto;">
							<div v-for="item in compareReport.deleteplugins" :key="item.id">
								<p style="color:#dd245f"><i>{{item.id}}</i></p>
              </div>
            </div>
          </a-card>
        </a-col>
        <a-col :span="8">
					<a-card size="small" title="变更插件" :bordered="false" :hoverable="true" style="border-radius:5px;">
						<span slot="extra">数量：{{compareReport.modifiedplugins.length}}</span>
            <div style="margin:5px;max-height:600px;overflow:auto;">
							<div v-for="item in compareReport.modifiedplugins" :key="item">
								<span style="color:#70b7e0"><i>{{item.data1.id}}</i></span><a style="margin-left:15px;">详情</a>
              </div>
            </div>
          </a-card>
        </a-col>
      </a-row>
    </a-modal>
  </div>
</template>

<script>
import {EventBus} from '../../event-bus.js'
import {notification} from 'ant-design-vue'

export default {
  components:{
  },
  data(){
		return{
			pluginCompareReportDialogVisible:false,
      compareReport:{
				//对比新增插件
        addplugins:[],
        //对比减少插件
        deleteplugins:[],
        //对比修改插件
        modifiedplugins:[],
      },
    }
  },
  
  mounted(){
		EventBus.$on('pluginconparereport',data=>{
			this.pluginCompareReportDialogVisible = true
      this.compareReport = JSON.parse(JSON.stringify(data))//深拷贝
      console.log(this.compareReport)
    })
  },
  
  methods:{
    
  }
}
</script>

<style scoped>
.parent{
  margin-top:60px;
	display: grid;
  grid-template-columns: repeat(5,1fr);
  grid-template-rows: repeat(5,1fr);
  grid-column-gap: 10px;
  grid-row-gap: 0px;
  height: 50px;
}
.div1{
	grid-area: 1/1/2/2;
}
.div2{
	grid-area: 1/2/2/4;
}
</style>
