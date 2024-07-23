<script>

var apiAddress='http://172.25.13.72/api.pluginViewer/api'
//var apiAddress='http://localhost:5483/api'
//var apiAddress='https://172.25.13.2:32002/Api.pluginViewer/api'
var pluginServer='aipuyang'
var pluginServer_sdx='sdplugin'
var detailDrawerVisible=false
var currentRouter='/relation'
var nullRouter='/'
var nodeClickCenter=false
var pluginVersion='23.0' 

var checkNormalPlugin_time=20000
var checkTodoList_time=3000000

var vkspConceptFilterDate = []

export default{
  pluginServer,pluginServer_sdx,pluginVersion,
  detailDrawerVisible,
  apiAddress,
  currentRouter,
  nullRouter,
  nodeClickCenter,
  
  checkNormalPlugin_time,
  checkTodoList_time,
  
  vkspConceptFilterDate
}

</script>