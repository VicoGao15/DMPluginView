<!-- 
	This is the user profile page, it uses the dashboard layout in: 
	"./layouts/Dashboard.vue" .
 -->

<template>
	<div>

		<!-- Header Background Image -->
		<div class="profile-nav-bg" style="background-image: url('images/info-card-3.jpg')"></div>
		<!-- / Header Background Image -->

		<!-- User Profile Card -->
		<a-card :bordered="false" class="card-profile-head" :bodyStyle="{padding: 0,}">
			<template #title>
				<a-row type="flex" align="middle">
					<a-col :span="24" :md="12" class="col-info">
						<a-avatar :size="74" shape="square" src="images/Vico.jpg" />
						<div class="avatar-info">
							<h4 class="font-semibold m-0">Vico Gao</h4>
							<p>Devmagic Tester / Appeon</p>
						</div>
					</a-col>
					<a-col :span="24" :md="12" style="display: flex; align-items: center; justify-content: flex-end">
						<a-radio-group v-model="profileHeaderBtns" size="small">
							<a-radio-button value="overview">OVERVIEW</a-radio-button>
							<a-radio-button value="teams">TEAMS</a-radio-button>
							<a-radio-button value="projects">PROJECTS</a-radio-button>
						</a-radio-group>
					</a-col>
				</a-row>
			</template>
		</a-card>
		<!-- User Profile Card -->

		<a-row type="flex" :gutter="24">

			<!-- Platform Settings Column -->
			<a-col :span="24" :md="8" class="mb-24">

				<!-- Platform Settings Card -->
				<CardPlatformSettings></CardPlatformSettings>
				<!-- / Platform Settings Card -->

			</a-col>
			<!-- / Platform Settings Column -->

			<!-- Profile Information Column -->
			<a-col :span="24" :md="8" class="mb-24">

				<!-- Profile Information Card -->
				<CardProfileInformation></CardProfileInformation>
				<!-- / Profile Information Card -->

			</a-col>
			<!-- / Profile Information Column -->
			
			<!-- Conversations Column -->
			<a-col :span="24" :md="8" class="mb-24">

			</a-col>
			<!-- / Conversations Column -->

		</a-row>
		
		<!-- Projects Card -->
		<a-card :bordered="false" class="header-solid h-full mb-24" :bodyStyle="{paddingTop: '14px'}">
			<template #title>
				<h6 class="font-semibold">已存在版本列表</h6>			
				<p>Architects design houses</p>	
			</template>

			<a-row type="flex" :gutter="[24,24]" align="stretch">

				<!-- Project Column -->
				<a-col :span="24" :md="12" :xl="6" v-for="(project, index) in projects" :key="index">
					<CardProject
						:id="project.id"
						:title="project.title"
						:content="project.content"
						:cover="project.cover"
						:team="project.team"
					></CardProject>
				</a-col>
				<!-- / Project Column -->

				<!-- Project Column -->
				<a-col :span="24" :md="12" :xl="6">

					<!-- Project Upload Component -->
					<a-upload
						name="avatar"
						:file-list="fileList"
						list-type="picture-card"
						class="projects-uploader"
						:show-upload-list="true"
						:before-upload="uploadFile"
						action=""
							
					>
						<img v-if="false" src="" alt="avatar" />
						<div v-else>
							<a-icon v-if="false" type="loading" />
							<svg v-else width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
								<path fill-rule="evenodd" clip-rule="evenodd" d="M3 17C3 16.4477 3.44772 16 4 16H16C16.5523 16 17 16.4477 17 17C17 17.5523 16.5523 18 16 18H4C3.44772 18 3 17.5523 3 17ZM6.29289 6.70711C5.90237 6.31658 5.90237 5.68342 6.29289 5.29289L9.29289 2.29289C9.48043 2.10536 9.73478 2 10 2C10.2652 2 10.5196 2.10536 10.7071 2.29289L13.7071 5.29289C14.0976 5.68342 14.0976 6.31658 13.7071 6.70711C13.3166 7.09763 12.6834 7.09763 12.2929 6.70711L11 5.41421L11 13C11 13.5523 10.5523 14 10 14C9.44771 14 9 13.5523 9 13L9 5.41421L7.70711 6.70711C7.31658 7.09763 6.68342 7.09763 6.29289 6.70711Z" fill="#111827"/>
							</svg>

							<div class="ant-upload-text font-semibold text-dark">
								上传文件
							</div>
						</div>
					</a-upload>
					<!-- / Project Upload Component -->

				</a-col>
				<!-- / Project Column -->

			</a-row>
		</a-card>
		<!-- / Projects Card -->

	</div>
</template>

<script>
import ref from 'vue'
import jsftp from 'jsftp'
import marked from 'marked'

	export default ({
		components: {
			marked,jsftp
		},
		data() {
			return {
				// Active button for the "User Profile" card's radio button group.
				profileHeaderBtns: 'overview',

				// Associating Conversation's list data with its corresponding property.
				conversationsData,

				// Project cards data
				projects,
				
				fileList:ref([]),
			}
		},
		
		methods:{
			uploadFile(file){
				var jsftp = require("jsftp")
				const ftp=new jsftp({
					host:"172.25.13.72",
					port:"21",
					user:"appeon",
					pass:"Gcf10080353"
				})
				
				ftp.put(buffer,file,err=>{
					if(!err){
						this.$message.success("upload success!")
					}
				})
				return false
			}
		}
	})

</script>

<style lang="scss" scoped>
.ql-editor{
	height:500px;
}
</style>