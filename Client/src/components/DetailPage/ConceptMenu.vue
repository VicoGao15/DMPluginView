<template>
  <div>
		<!--<b>文章目录</b> -->
    <a-input-search 
      size="small" 
      placeholder="文章目录" 
      v-model = "searchMenu"
      @search="onSearchMenu"
    ></a-input-search>
    <!--方案一：直接通过显示所有标题，a标签访问链接加锚点跳转-->
    <!--<p v-for="menuitem in menuTree" :key="menuitem"><a :href="getElementmaodian(menuitem)" :style="menustyle(menuitem)">{{getElementcontent(menuitem)}}</a></p>-->
    
    <!--方案二：直接通过显示所有标题，a标签，控制访问的Url不带锚点-->
    <!--<p v-for="menuitem in menuTree" :key="menuitem" style="overflow: auto;">
      <a href="javascript:void(0)" @click="goAnchor(getElementmaodian(menuitem))" :style="menustyle(menuitem)">
        <span v-html="getElementcontent(menuitem)"></span>
			</a>
    </p>-->
    
    <!--方案三：生成目录树结构json，可控制目录树节点的展开收缩-->
    <div v-if="isLoading">
			<p></p>加载中...
    </div>
    <div v-if="!isLoading && menuTree_json.length==0">
			<p></p>无分级目录
    </div>
    <ContentMenu :menu="menuTree_json" style="max-height:600px;overflow: auto;"></ContentMenu>
  </div>
</template>

<script>
import { mixins_menutree } from '../../mixin/menutree'
import ContentMenu from './ContentMenu.vue'
export default {
  mixins:[mixins_menutree],
  props:{
		content:''
  },
  components:{
		ContentMenu
  },
  watch:{
		content:{
			handler(newValue,oldValue){
        //mixins生成目录树json结构
        this.isLoading = true
        this.generateDirectoryTree_Json(newValue)
        this.isLoading = false
      }
    }
  },
  data(){
		return {
			searchMenu:'',
      isLoading:true
    }
  },
  created(){
		this.isLoading=true
  }
}
</script>

<style>

</style>