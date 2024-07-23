<template>
  <div>
		<div v-for="item in menu" :key="item.anchor" style="overflow: auto;margin-top:10px;">

      <svg @click="item.isExpand=!item.isExpand" style="vertical-align:middle" v-if="item.child.length != 0 && !item.isExpand" t="1682586770081" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="1607" width="18" height="18"><path d="M512 444.330667l225.834667 225.834666a42.624 42.624 0 1 0 60.330666-60.330666l-256-256a42.624 42.624 0 0 0-60.330666 0l-256 256a42.624 42.624 0 1 0 60.330666 60.330666L512 444.330667z" fill="#8FBAE2" p-id="1608"></path></svg>
      <svg @click="item.isExpand=!item.isExpand" style="vertical-align:middle" v-if="item.child.length != 0 && item.isExpand" t="1682586699698" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="1393" width="18" height="18"><path d="M512 579.669333L286.165333 353.834667a42.624 42.624 0 1 0-60.330666 60.330666l256 256a42.624 42.624 0 0 0 60.330666 0l256-256a42.624 42.624 0 1 0-60.330666-60.330666L512 579.669333z" fill="#7FB5E5" p-id="1394"></path></svg>
      
      <a href="javascript:void(0)" @click="goAnchor(item.anchor)" style="margin-left:5px;margin-top:-5px;">
        <span v-html="item.title" v-if="item.child.length!=0"></span>
        <span v-html="item.title" v-else style="margin-left:16px;"></span>
			</a>
      <div v-if="item.isExpand" style="margin-left:30px;margin-top:10px;">
        <!--递归生成目录树，递归自己-->
        <contentMenu :menu="item.child"></contentMenu>
      </div>
    </div>
  </div>
</template>

<script>
import { mixins_menutree } from '../../mixin/menutree'
export default {
  name:'contentMenu', //定义name才能组件自己递归调用自己
  mixins:[mixins_menutree],
  props:{
		menu:[],
  },
  data(){
		return{
			nodeCount:0
    }
  },
  watch:{
  	menu:{
      handler(newValue,oldValue){
        this.traverseTree(newValue)
        
        //节点数量 > 30 的话，文章目录收缩
        if(this.nodeCount>20){
					newValue.forEach(item=>{
						item.isExpand = false 
          })
        }
      }
    },
    deep:true,
  },
  
  methods:{
    // 定义递归函数，计算Json树上有多少节点
		traverseTree(menu){
      this.nodeCount++
      for(var i=0;i<menu.length;i++){
        if(menu[i].child.length>0){
					for(var j=0;j<menu[i].child.length;j++){
            this.traverseTree(menu[i].child[j])
          }
        }
      }
    }
  }
}
</script>

<style>

</style>
