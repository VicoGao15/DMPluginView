<template>
  <div>
		<!--<model-gltf
      :lights="lights"
      @load="onLoad"
      src="models/rocket/scene.gltf"
    />-->
    <model-fbx :lights="lights" src="models/mazda-rx-7/source/rx7.fbx"/>
  </div>
</template>

<script>
//import {ModelGltf} from 'vue-3d-model'
import {ModelFbx} from 'vue-3d-model'

export default {
  components: {
    //ModelGltf
    ModelFbx
	},
  data(){
		return{
			rotation:{
				x:0,
        y:0,
        z:0
      },
      lights: [//3d模型灯光
          {
              type: 'HemisphereLight',
              position: { x: 0, y: 1, z: 0 },
              skyColor: 0xaaaaff,
              groundColor: 0x806060, //此代码为灯光后颜色
              intensity: 0.2,
          },
          {
              type: 'DirectionalLight',
              position: {x: 1, y: 1, z: 1},
              color: 0xffffff,
              intensity: 0.8,
          }
      ],
    }
  },
  methods:{
		onLoad(){
			rotate()
    },
    rotate(){
			requestAnimationFrame(rotate);
      this.rotation.z += 0.01;
    }
  }
}
</script>

<style>

</style>