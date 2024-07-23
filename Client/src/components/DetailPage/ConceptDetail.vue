<template>
  <fullscreen v-model="fullscreen" style="height:calc(100% - 800px)">
  <a-card :bordered="false" style="background:rgba(255,00,255,0);box-shadow:none;margin-top:-15px;">
		<template #title>
			<a-row type="flex" align="middle">
				<a-col :span="24" :md="20">
          <div style="float:left;width:20%">
            <a-button type="dashed" @click="backToConcept()" class="header-solid h-full animate__animated animate__bounceIn ">
              <a-icon type="rollback"></a-icon>
            返回</a-button>
          </div>
          <div>
						<h4 style="margin-left:0px;">{{selectConcept.header}}</h4>
          </div>
				</a-col>
				<a-col :span="24" :md="4"  style="display: flex; align-items: center; justify-content: flex-end;backgroud-color:green;">
          <svg t="1681970485774" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="1903" width="24" height="24"><path d="M367.2 226h474.824a21.84 21.84 0 0 1 21.616 24.924l-23.544 164.816a21.848 21.848 0 0 0 1.696 12.032l71.588 159.448a21.828 21.828 0 0 1-19.92 30.78h-324.092a38.256 38.256 0 0 1-33.188-19.236L327.2 234l40-8z" fill="#009CFF" p-id="1904"></path><path d="M536.18 598.764l-78.24-136.568L743.2 502l-186.364 113.888a38.236 38.236 0 0 1-20.656-17.124z" fill="#007CE9" p-id="1905"></path><path d="M147.2 118h496.88c20.268 0 37.564 14.652 40.896 34.644L743.2 502h-532l-64-384z" fill="#00ACFF" p-id="1906"></path><path d="M107.88 111.372c-2.964-20.48 11.256-39.52 31.74-42.484 20.484-2.968 39.52 11.256 42.488 31.74l117.572 812c2.968 20.48-11.256 39.52-31.74 42.484-20.48 2.968-39.52-11.256-42.484-31.74l-117.576-812z" fill="#C5CEEB" p-id="1907"></path>
          </svg> 
          <p style="margin-left:5px;">{{selectConcept.views}} 阅读</p>
				</a-col>
			</a-row>
		</template>
      <!--<div style="background:#f2f3f5;">-->
      <div>
        <div class="leftleft" v-if="ifShowPreview" v-show="false">
					<div style="position: absolute;top:43%;">
            <a-tooltip :title="previewAndNextConcept.preview.header" placement="top">
              <a-card :bordered="false" style="position:absolute;left:30px;height:80px;" @click="lookBackOne()" >
								<svg t="1681971775760" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="1900" width="32" height="32"><path d="M514 114.3c219.9 0 398.8 178.9 398.8 398.9C912.8 733.1 733.9 912 514 912S115.2 733.1 115.2 513.2 294.1 114.3 514 114.3z m0 701.6c166.9 0 302.7-135.8 302.7-302.7S680.9 210.5 514 210.5 211.3 346.3 211.3 513.2 347.1 815.9 514 815.9z" fill="#BDD2EF" p-id="1901"></path><path d="M281.9 554.7L446 718.8c22.9 22.9 60.2 22.9 83.1 0 22.9-22.9 22.9-60.2 0-83.1L465.3 572H694c32.5 0 58.8-26.3 58.8-58.8s-26.3-58.8-58.8-58.8H465.3l63.8-63.8c11.5-11.5 17.2-26.5 17.2-41.5s-5.7-30.1-17.2-41.5c-22.9-22.9-60.2-22.9-83.1 0l-164.1 164c-22.9 23-22.9 60.2 0 83.1z" fill="#2867CE" p-id="1902"></path>
                </svg>
              </a-card>
            </a-tooltip>
          </div>
        </div>
				<div class="left">
          <div class="filearea" style="height:400px;display:flex;width:15%;">
            <a-card :bordered="false" style="margin:10px 10px 0 20px;position: absolute;top:8.8%;width:15%;">
              <a-descriptions :column="1" size="small" style="margin:5px 5px 0 5px;">
                <a-descriptions-item label="创建人员">
                  <!--<a-tag color="green">{{selectConcept.creater}}</a-tag>-->
                  <div v-if="selectConcept.userInfo">
  									<a-avatar style="margin-left:15px" v-if="selectConcept.userInfo.avatarUrl!='' && selectConcept.userInfo.avatarUrl!=null" shape="circle" :src="selectConcept.userInfo.avatarUrl"/>
          					<a-avatar v-else style="background-color:#1890ff;margin-left:15px">{{selectConcept.userInfo.username.charAt(0).toUpperCase()}}</a-avatar>
                    {{ selectConcept.userInfo.username }}	
                  </div>
                  
                </a-descriptions-item>
                <a-descriptions-item label="创建时间">
                  <a-tag color="cyan">{{timeFormate(selectConcept.create_Date || '')}}</a-tag>
                </a-descriptions-item>
                <a-descriptions-item label="知识专栏">
                  <a-tag color="blue" @click="gotoTagCollectionConcept()">{{selectConcept.module}}</a-tag>
                </a-descriptions-item>
              </a-descriptions>
            </a-card>
            <a-card :bordered="false" style="margin:10px 10px 0 20px;position: absolute;top:24%;width:15%;" v-if="ifShowPreview||ifShowNext">
              <!--上一篇，下一篇-->
              <p></p>
              <p v-if="ifShowPreview"><< 上一篇：
  							<a-tooltip >
                  <template #title>{{previewAndNextConcept.preview.creater}} 发布于 {{timeFormate(previewAndNextConcept.preview.create_Date || '')}}</template>
									<a @click="lookBackOne()">{{previewAndNextConcept.preview.header}}</a>
                </a-tooltip>
              </p>
              <p></p>
              <p v-if="ifShowNext">>> 下一篇：
								<a-tooltip>
									<template #title>{{previewAndNextConcept.next.creater}} 发布于 {{timeFormate(previewAndNextConcept.next.create_Date || '')}}</template>
                  <a @click="lookNextOne()">{{previewAndNextConcept.next.header}}</a>
                </a-tooltip>
              </p>
              <p></p>
            </a-card>
            <a-card :bordered="false" style="margin:10px 10px 0 20px;position: absolute;top:40%;width:15%;">
              <!--文章目录-->
              <ConceptMenu :content="selectConcept.contents"/>
            </a-card>
          </div>
          <div class="contentarea">
              <a-card class="header-solid h-full" :style="contentcardStyle" :bordered="false" v-highlight>
                <p style="white-space: pre-line;" v-html="selectConcept.contents" v-if="!loading"></p>
                <div v-else>
                  在使劲加载中...
                  <a-skeleton active/>
                </div>
              </a-card>
          </div>
        </div>
        <div class="right">
          <div style="position: absolute;top:16%;" v-if="loginUser.id==selectConcept.createrId">
            <a-tooltip title="编辑" placement="left">
            <a-button shape="circle" style="position:absolute;left:20px;border:none;" @click="openEditDialog()">
							<svg style="margin:auto;" t="1681370883620" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="2701" width="24" height="24"><path d="M252.3 743.3l235.8-42.4-147.8-179.1zM365.2 501.4l148.2 178.8L868.3 389 720.2 210.2zM958 259.7l-92.6-111.9c-15.1-18.4-43.7-20.3-63.7-4.2l-53.9 44 148.1 179.1 53.9-44c19.6-16.1 23.3-44.6 8.2-63z" fill="#2867CE" p-id="2702"></path><path d="M770.1 893.7H259.6c-93.1 0-168.5-75.5-168.5-168.5V345.4c0-93.1 75.5-168.5 168.5-168.5h49.6c26.6 0 48.1 21.5 48.1 48.1s-21.5 48.1-48.1 48.1h-49.6c-40 0-72.4 32.4-72.4 72.4v379.8c0 40 32.4 72.4 72.4 72.4h510.5c40 0 72.4-32.4 72.4-72.4v-132c0-26.6 21.5-48.1 48.1-48.1s48.1 21.5 48.1 48.1v132c-0.1 93-75.5 168.4-168.6 168.4z" fill="#BDD2EF" p-id="2703"></path>
              </svg>
						</a-button>
            </a-tooltip>
          </div>
          <div style="position: absolute;top:22%;">
            <a-tooltip title="附件" placement="left">
            <a-button shape="circle" style="position:absolute;left:20px;border:none;" @click="openAttachments()">
              <a-badge style="margin:auto;" :count="conceptFiles.length">
							<svg style="margin:auto;margin-top:3px;" t="1681371010045" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="3312" width="24" height="24"><path d="M439.9 359.2l-99.7-156.3h-81.4c-66.5 0-120.5 53.9-120.5 120.5v294.9h751.4V359.2H439.9z" fill="#2867CE" p-id="3313"></path><path d="M769.3 871.6H258.7c-92.9 0-168.5-75.6-168.5-168.5V323.3c0-92.9 75.6-168.5 168.5-168.5h81.4c16.4 0 31.7 8.4 40.5 22.2l85.5 134.1h423.5c26.6 0 48.1 21.5 48.1 48.1v343.9c0.1 92.9-75.5 168.5-168.4 168.5zM258.7 250.9c-39.9 0-72.4 32.5-72.4 72.4v379.8c0 39.9 32.5 72.4 72.4 72.4h510.5c39.9 0 72.4-32.5 72.4-72.4V407.3H439.9c-16.4 0-31.7-8.4-40.5-22.2L313.9 251h-55.2z" fill="#BDD2EF" p-id="3314"></path><path d="M840.2 272.7h-314l-69.6-109.2h383.6z" fill="#2867CE" p-id="3315"></path>
              </svg>
              </a-badge>
						</a-button>
            </a-tooltip>
          </div>
					<div style="position: absolute;top:28%;">
            <a-tooltip title="评论" placement="left">
            <a-button shape="circle" style="position:absolute;left:20px;border:none;" @click="openComments()">
              <a-badge style="margin:auto;" :count="conceptComments.length">
              <svg style="margin:auto;margin-top:3px;" t="1681370939725" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="3107" width="24" height="24"><path d="M762.4 117.3H265.6c-85.1 0-154.3 69.2-154.3 154.3v334.8c0 85.1 69.2 154.3 154.3 154.3h123.7l76.1 121.5c4.6 7.3 10.8 13.5 18.1 18.1 26.8 16.8 62.2 8.7 79-18.1l76.1-121.5h123.7c85.1 0 154.3-69.2 154.3-154.3V271.6c0-85.1-69.1-154.3-154.2-154.3z m58.1 489.1c0 32-26.1 58.1-58.1 58.1H265.6c-32 0-58.1-26.1-58.1-58.1V271.6c0-32 26.1-58.1 58.1-58.1h496.8c32 0 58.1 26.1 58.1 58.1v334.8z" fill="#BDD2EF" p-id="3108"></path><path d="M324.4 450.3m-65.7 0a65.7 65.7 0 1 0 131.4 0 65.7 65.7 0 1 0-131.4 0Z" fill="#2867CE" p-id="3109"></path><path d="M514.2 450.3m-65.7 0a65.7 65.7 0 1 0 131.4 0 65.7 65.7 0 1 0-131.4 0Z" fill="#2867CE" p-id="3110"></path><path d="M704 450.3m-65.7 0a65.7 65.7 0 1 0 131.4 0 65.7 65.7 0 1 0-131.4 0Z" fill="#2867CE" p-id="3111"></path>
              </svg>
              </a-badge>
						</a-button>
            </a-tooltip>
          </div>
          <div style="position: absolute;top:34%;">
            <a-tooltip title="收藏" placement="left" v-if="!ifCollection">
              <a-button shape="circle" style="position:absolute;left:20px;border:none;" @click="AddToCollection()">
  							<svg style="margin:auto;" t="1690163991267" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="5434" width="24" height="24"><path d="M887.7 380.2L693 331.4c-3.6-0.9-6.7-3.2-8.7-6.3l-106.5-170c-4.6-7.3-10.8-13.5-18.1-18.1-26.8-16.8-62.2-8.7-79 18.1L374 325.2c-2 3.1-5.1 5.4-8.7 6.3l-194.7 48.8c-8.4 2.1-16.2 6.1-22.9 11.7-24.3 20.3-27.5 56.5-7.2 80.8l33.6 40.2 42.6 3.8c201.6 18 379.4 143.6 464 327.9l18.9 41.1 21 8.4c8 3.2 16.7 4.6 25.4 4 31.6-2.2 55.4-29.5 53.3-61.1l-13.8-200.2c-0.3-3.7 0.9-7.3 3.3-10.2l128.8-153.9c5.6-6.6 9.5-14.5 11.7-22.9 7.7-30.9-10.9-62-41.6-69.7z" fill="#dbdbdb" p-id="5435"></path><path d="M337.6 894.1l186.2-75c1.7-0.7 3.5-1 5.4-1 1.8 0 3.6 0.3 5.4 1l107.5 43.3c-76.5-166.7-238-286.2-428.9-303.2l56.4 67.4c2.4 2.8 3.6 6.5 3.3 10.2L259 836.9c-0.6 8.6 0.8 17.3 4 25.4 9 22.3 30.5 35.9 53.2 35.9 7.2 0 14.4-1.3 21.4-4.1z" fill="#dbdbdb" p-id="5436"></path>
                </svg>
  						</a-button>
            </a-tooltip>
            <a-tooltip title="移除收藏" placement="left" v-else>
              <a-button shape="circle" style="position:absolute;left:20px;border:none;" @click="DeleteOfCollection()">
  							<svg style="margin:auto;" t="1690169655770" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="16194" width="24" height="24"><path d="M887.7 380.2L693 331.4c-3.6-0.9-6.7-3.2-8.7-6.3l-106.5-170c-4.6-7.3-10.8-13.5-18.1-18.1-26.8-16.8-62.2-8.7-79 18.1L374 325.2c-2 3.1-5.1 5.4-8.7 6.3l-194.7 48.8c-8.4 2.1-16.2 6.1-22.9 11.7-24.3 20.3-27.5 56.5-7.2 80.8l33.6 40.2 42.6 3.8c201.6 18 379.4 143.6 464 327.9l18.9 41.1 21 8.4c8 3.2 16.7 4.6 25.4 4 31.6-2.2 55.4-29.5 53.3-61.1l-13.8-200.2c-0.3-3.7 0.9-7.3 3.3-10.2l128.8-153.9c5.6-6.6 9.5-14.5 11.7-22.9 7.7-30.9-10.9-62-41.6-69.7z" fill="#f3e51c" p-id="16195"></path><path d="M337.6 894.1l186.2-75c1.7-0.7 3.5-1 5.4-1 1.8 0 3.6 0.3 5.4 1l107.5 43.3c-76.5-166.7-238-286.2-428.9-303.2l56.4 67.4c2.4 2.8 3.6 6.5 3.3 10.2L259 836.9c-0.6 8.6 0.8 17.3 4 25.4 9 22.3 30.5 35.9 53.2 35.9 7.2 0 14.4-1.3 21.4-4.1z" fill="#f3e51c" p-id="16196"></path>
                </svg>
  						</a-button>
            </a-tooltip>
          </div>
          <div style="position: absolute;top:40%;">
            <a-tooltip title="分享" placement="left">
						<a-button shape="circle" style="position:absolute;left:20px;border:none;" @click="doCopy('http://172.25.13.72/dmpluginview/conceptdetail?conceptid=' + selectConcept.id)">
							<svg style="margin:auto;" t="1680503724406" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="7784" width="24" height="24"><path d="M902.4 291.2L692.2 128.7c-44-34-107.9-2.6-107.9 53v45c-188.9 1.3-341.9 159.8-341.9 356 0 42.7 7.1 80.7 19 118.7C308.8 563.7 434.7 464 584.2 464v42.7c0 55.6 63.9 87 107.9 53l210.3-162.5c34.7-26.8 34.7-79.2 0-106z" fill="#2867CE" p-id="7785"></path><path d="M768.3 901.9H257.7c-93.1 0-168.5-75.5-168.5-168.5V353.6c0-93.1 75.5-168.5 168.5-168.5h49.6c26.6 0 48.1 21.5 48.1 48.1s-21.5 48.1-48.1 48.1h-49.6c-40 0-72.4 32.4-72.4 72.4v379.8c0 40 32.4 72.4 72.4 72.4h510.5c40 0 72.4-32.4 72.4-72.4v-132c0-26.6 21.5-48.1 48.1-48.1s48.1 21.5 48.1 48.1v132c0 93-75.5 168.4-168.5 168.4z" fill="#BDD2EF" p-id="7786"></path>
              </svg>
            </a-button>
            </a-tooltip>
          </div>
           <div style="position: absolute;top:46%;">
            <a-tooltip title="复制文章" placement="left">
            <a-button shape="circle" style="position:absolute;left:20px;border:none;box-shadow: 0 2px 4px 0 rgba(50,50,50,.04);" @click="copyContent(selectConcept)">
							<svg style="margin:auto;" t="1682062200773" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="2210" width="24" height="24"><path d="M602.6 764.5h-321C188.7 764.5 113 688.9 113 596V286.7c0-92.9 75.6-168.5 168.5-168.5h321c92.9 0 168.5 75.6 168.5 168.5V596c0.1 92.9-75.5 168.5-168.4 168.5z m-321-550.2c-39.9 0-72.4 32.5-72.4 72.4V596c0 39.9 32.5 72.4 72.4 72.4h321c39.9 0 72.4-32.5 72.4-72.4V286.7c0-39.9-32.5-72.4-72.4-72.4h-321z" fill="#BDD2EF" p-id="2211"></path><path d="M573 592.5H305.9c-32.5 0-58.8-26.3-58.8-58.8s26.3-58.8 58.8-58.8H573c32.5 0 58.8 26.3 58.8 58.8-0.1 32.5-26.4 58.8-58.8 58.8zM573 408.2H305.9c-32.5 0-58.8-26.3-58.8-58.8s26.3-58.8 58.8-58.8H573c32.5 0 58.8 26.3 58.8 58.8s-26.4 58.8-58.8 58.8z" fill="#2867CE" p-id="2212"></path><path d="M818.8 278.2V739.7c0 39.9-32.5 72.4-72.4 72.4H273.3c27.1 56.8 85.1 96.2 152.2 96.2h321c92.9 0 168.5-75.6 168.5-168.5V430.4c0-67.1-39.4-125.1-96.2-152.2z" fill="#BDD2EF" p-id="2213"></path>
              </svg>
						</a-button>
            </a-tooltip>
          </div>
          <div style="position: absolute;top:52%;">
            <a-tooltip title="另存为文档" placement="left">
            <a-button shape="circle" style="position:absolute;left:20px;border:none;" @click="saveConceptToDocx(selectConcept)">
							<svg style="margin:auto;" t="1682064653922" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="1914" width="24" height="24"><path d="M786.6 156.7v305.7H241.4V116.9h-17.9c-43.1 0-78 34.9-78 78v636.5c0 43.1 34.9 78.1 78.1 78.1h16.9v-351h547.2v351h16.9c43.1 0 78.1-34.9 78.1-78.1V236.1l-96.1-79.4z" fill="#2867CE" p-id="1915"></path><path d="M304.5 622.6v286.9h419V622.6h-419z m388.3 210.1H334.9V786h357.9v46.7z m0-89.5H334.9v-46.7h357.9v46.7zM305.5 116.9v281.3h417V116.9h-417z m344.1 179.4h-64.1v-93.5h64.1v93.5z" fill="#BDD2EF" p-id="1916"></path></svg>
						</a-button>
            </a-tooltip>
          </div>
          <div style="position: absolute;top:58%;">
            <a-tooltip title="沉浸式阅读" placement="left">
						<a-button shape="circle" style="position:absolute;left:20px;border:none;" @click="fullScreanClick()">
							<svg style="margin:auto;" t="1680505492259" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="16811" width="24" height="24"><path d="M716.5 853.4H316.1c-77.6 0-140.7-63.1-140.7-140.7V312.3c0-77.6 63.1-140.7 140.7-140.7h400.4c77.6 0 140.7 63.1 140.7 140.7v400.4c0.1 77.6-63.1 140.7-140.7 140.7zM316.1 267.7c-24.6 0-44.6 20-44.6 44.6v400.4c0 24.6 20 44.6 44.6 44.6h400.4c24.6 0 44.6-20 44.6-44.6V312.3c0-24.6-20-44.6-44.6-44.6H316.1z" fill="#BDD2EF" p-id="16812"></path><path d="M192.1 895.3h232.1c32.5 0 58.8-26.3 58.8-58.8 0-32.4-26.3-58.8-58.8-58.8H334L463.7 648c22.9-22.9 22.9-60.2 0-83.1-22.9-22.9-60.2-22.9-83.1 0L250.9 694.7v-90.2c0-16.2-6.6-30.9-17.2-41.5-10.6-10.6-25.3-17.2-41.5-17.2-32.5 0-58.8 26.3-58.8 58.8v232.1c-0.1 32.3 26.3 58.6 58.7 58.6zM835.9 131.1H603.8c-32.5 0-58.8 26.3-58.8 58.8 0 32.4 26.3 58.8 58.8 58.8H694L564.3 378.3c-22.9 22.9-22.9 60.2 0 83.1 22.9 22.9 60.2 22.9 83.1 0l129.7-129.7v90.2c0 16.2 6.6 30.9 17.2 41.5 10.6 10.6 25.3 17.2 41.5 17.2 32.5 0 58.8-26.3 58.8-58.8V189.9c0.1-32.5-26.3-58.8-58.7-58.8z" fill="#2867CE" p-id="16813"></path>
              </svg>
            </a-button>	
            </a-tooltip>
          </div>
          <!--<a-popover placement="LB" trigger="click">
            <template #content>
              111
            </template>
            <div style="position: absolute;top:58%;">
                <a-button shape="circle" style="position:absolute;left:20px;border:none;">
                  <a-tooltip title="目录" placement="right">
                  <svg style="margin:auto;" t="1683862917055" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="1917" width="30" height="30"><path d="M514 912c-219.9 0-398.8-178.9-398.8-398.8S294.1 114.3 514 114.3s398.8 178.9 398.8 398.9C912.8 733.1 733.9 912 514 912z m0-701.5c-166.9 0-302.7 135.8-302.7 302.7S347.1 815.9 514 815.9s302.7-135.8 302.7-302.7S680.9 210.5 514 210.5z" fill="#BDD2EF" p-id="1918"></path><path d="M658.2 421.6H511.7c-32.5 0-58.8-26.3-58.8-58.8s26.3-58.8 58.8-58.8h146.5c32.5 0 58.8 26.3 58.8 58.8-0.1 32.5-26.4 58.8-58.8 58.8zM658.2 572.8H511.7c-32.5 0-58.8-26.3-58.8-58.8s26.3-58.8 58.8-58.8h146.5c32.5 0 58.8 26.3 58.8 58.8-0.1 32.5-26.4 58.8-58.8 58.8zM658.2 724.1H511.7c-32.5 0-58.8-26.3-58.8-58.8s26.3-58.8 58.8-58.8h146.5c32.5 0 58.8 26.3 58.8 58.8-0.1 32.5-26.4 58.8-58.8 58.8zM369.3 421.6h-4.5c-32.5 0-58.8-26.3-58.8-58.8s26.3-58.8 58.8-58.8h4.5c32.5 0 58.8 26.3 58.8 58.8s-26.4 58.8-58.8 58.8zM369.3 572.8h-4.5c-32.5 0-58.8-26.3-58.8-58.8s26.3-58.8 58.8-58.8h4.5c32.5 0 58.8 26.3 58.8 58.8s-26.4 58.8-58.8 58.8zM369.3 724.1h-4.5c-32.5 0-58.8-26.3-58.8-58.8s26.3-58.8 58.8-58.8h4.5c32.5 0 58.8 26.3 58.8 58.8-0.1 32.5-26.4 58.8-58.8 58.8z" fill="#2867CE" p-id="1919"></path></svg>
                  </a-tooltip>
                </a-button>	
            </div>
          </a-popover>-->
          <div>
      </div>
        </div>
        <div class="rightright" v-if="ifShowNext"  v-show="false">
					<div style="position: absolute;top:43%;">
            <a-tooltip :title="previewAndNextConcept.next.header" placement="top">
              <a-card :bordered="false" style="position:absolute;left:5px;height:80px;" @click="lookNextOne()">
								<svg t="1681972070724" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="2055" width="32" height="32"><path d="M514 912c-219.9 0-398.8-178.9-398.8-398.9 0-219.9 178.9-398.8 398.8-398.8s398.8 178.9 398.8 398.8c0 220-178.9 398.9-398.8 398.9z m0-701.5c-166.9 0-302.7 135.8-302.7 302.7S347.1 815.9 514 815.9s302.7-135.8 302.7-302.7S680.9 210.5 514 210.5z" fill="#BDD2EF" p-id="2056"></path><path d="M746.1 471.6L582 307.5c-22.9-22.9-60.2-22.9-83.1 0-22.9 22.9-22.9 60.2 0 83.1l63.8 63.8H334c-32.5 0-58.8 26.3-58.8 58.8S301.5 572 334 572h228.7l-63.8 63.8c-11.5 11.5-17.2 26.5-17.2 41.5s5.7 30.1 17.2 41.5c22.9 22.9 60.2 22.9 83.1 0l164.1-164.1c22.9-22.9 22.9-60.1 0-83.1z" fill="#2867CE" p-id="2057"></path>
                </svg>
              </a-card>
            </a-tooltip>
          </div>
        </div>
      </div>
      <ConceptCommentDrawer></ConceptCommentDrawer>
      <ConceptFileDrawer></ConceptFileDrawer>
      <ConceptDetailDrawer></ConceptDetailDrawer>
  </a-card>
  <Chat />
  </fullscreen>
</template>

<script> 
import ConceptCommentDrawer from '../Drawer/ConceptCommentDrawer.vue'
import ConceptFileDrawer from '../Drawer/ConceptFileDrawer.vue'
import ConceptDetailDrawer from '../Drawer/ConceptDetailDrawer.vue'
import axios from 'axios'
import {EventBus} from '../../event-bus.js'
import VueFullscreen from 'vue-fullscreen'
import Vue from 'vue'
import CopyOutlined from '@ant-design/icons-vue'
import LeftOutlined from '@ant-design/icons-vue'
import RightOutlined from '@ant-design/icons-vue'
import HeartOutlined from '@ant-design/icons-vue'
import MessageOutlined from '@ant-design/icons-vue'

import JsonViewer from 'vue-json-viewer'
import { VNumber } from "@maxflex/v-number"
import VkspService from '../service/VkspService'
import ConceptFileService from '../service/ConceptFileService'

import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'
import { quillEditor } from 'vue-quill-editor'
import ImageResize from 'quill-image-resize-module' 
import {ImageDrop} from 'quill-image-drop-module' 
Quill.register('modules/imageResize', ImageResize)
Quill.register('modules/imageDrop', ImageDrop)
import Chat from '../Drawer/Chat.vue'

Vue.use(VueFullscreen)

import { mixins_menutree } from '../../mixin/menutree'
import { myNotification } from '../../mixin/notification'
import { VkspUser } from '../../mixin/vkspuser'
import ContentMenu from './ContentMenu.vue'
import ConceptMenu from './ConceptMenu.vue'

export default {
  mixins:[mixins_menutree,myNotification,VkspUser],
  props:{
  },
  components:{
    ConceptCommentDrawer,
    ConceptFileDrawer,
    ConceptDetailDrawer,
    ContentMenu,ConceptMenu,
    
		CopyOutlined,
    LeftOutlined,
    RightOutlined,
    HeartOutlined,
    MessageOutlined,
    
    JsonViewer,
    quillEditor,
    VNumber,
    Chat
  },
  data(){
    const vkspservice=new VkspService()
    const conceptFileService = new ConceptFileService()
    return{
      isLogin:false,
      isAuthor:false,
      vkspservice,
      conceptFileService,
      selectConcept:{},
      loading:false,
      conceptComments:[],
      conceptFiles:[],
      newComment:{
				commentperson:'Vico',
        commentcontent:'',
        commenttime:'',
        conceptid:0
      },
      editorselectConcept:{},
      addForm_preview:false,
      
      activeKey:'1',
      someNumber:10086,
       
      editorOption:{
				modules:{
          imageResize:{
						displayStyles:{
							backgroundColor:'black',
              border:'none',
              color:'white'
            },
            modules:['Resize', 'DisplaySize', 'Toolbar']
          },
          imageDrop:true, 
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
      
      //用户登录信息
      loginUser:{},
      
      replyComment:{
				commentid:0,
        commentperson:'',
        commentcontent:'',
        commenttime:'',
        conceptid:''
      },
      
      appendfileList:[],//追加上传附件
      
      affixposition:{
				top:100,
        bottom:100
      },
      
      isdatafromRouter:true, //conceptid是否直接从路由获取

      searchMenu:'',
      
      //沉浸式阅读
      fullscreen:false,
      //是否已经收藏
      ifCollection:false,
      //是否显示上一篇、下一篇按钮
      ifShowPreview:true,
      ifShowNext:true,
      //上一篇、下一篇文章内容
      previewAndNextConcept:{
				preview:{},
        next:{}
      }
    }
  },
  
  mounted(){
    this.keyDown()
  },
  
  computed:{
    //是否全屏展示，动态设置高度
    contentcardStyle: function(){
      if(!this.fullscreen){
				return 'height:' + (window.innerHeight - 230) + 'px' + ';overflow: auto;margin-top:5px;'
      }
			return 'height:' + (window.innerHeight + 90) + 'px' + ';overflow: auto;margin-top:5px;'
    },
    lookPreviewTitle: function(){
			return '上一篇'+this.previewAndNextConcept.preview.header
    },
  },
  
  created(){
    //获取路由中的concept id
    var conceptid = this.$route.query.conceptid
    this.allInit(conceptid)
  },
  
  methods:{
    
    allInit(conceptid){
      this.loading=true
      this.getConceptDetail(conceptid)
      this.getConceptComments(conceptid)
      this.getConceptFile(conceptid)
  		//获取cookie是否登录信息
      this.loginUser = this.getLoginMessage()
      //检查是否收藏文章
      this.checkIfCollection()
      //获取上一篇、下一篇
      this.getPreviouAndNext()
    },
    
    getConceptDetail(id){
			this.vkspservice.getConceptDetail(id).then(res=>{
        this.selectConcept=res.data
        this.editorselectConcept=JSON.parse(JSON.stringify(res.data))//深拷贝
        this.loading=false
      })
    },
    
    getConceptComments(conceptid){
			this.vkspservice.getConceptComments(conceptid).then(res=>{
        this.conceptComments=res.data
      })
    },
    
    getConceptFile(conceptid){
      this.conceptFiles=[]
			this.conceptFileService.getFileByConceptId(conceptid).then(res=>{
				
        res.data.forEach(filelog=>{
					this.conceptFiles.push(filelog)
        })
      })
    },

    lookBackOne(){ 
      this.$router.push("/conceptdetail?conceptid="+this.previewAndNextConcept.preview.id)
      this.selectConcept.header = this.previewAndNextConcept.preview.header
      this.allInit(this.previewAndNextConcept.preview.id)
    },
    lookNextOne(){
      this.$router.push("/conceptdetail?conceptid="+this.previewAndNextConcept.next.id)
      this.selectConcept.header = this.previewAndNextConcept.next.header
      this.allInit(this.previewAndNextConcept.next.id)
    },
    
    // 监听键盘
    keyDown() {
      document.onkeydown = (e) => {
        //事件对象兼容
        let e1 = e || event || window.event || arguments.callee.caller.arguments[0]
        if (e1.ctrlKey && e1.keyCode == 37 ) {
          // 按 ctrl + <- 箭头
          this.lookBackOne()
        } else if (e1.ctrlKey && e1.keyCode == 39) {
          // 按 ctrl + -> 箭头
          this.lookNextOne()
        }
      }
    },
    
    deleteConcept(id){
			this.vkspservice.deleteConcept(id).then(res=>{
				if(res.data){
					this.$message.success("删除成功!")
          EventBus.$emit('refreshConcept')
        }else{
					this.$message.error("删除失败!")
        }
      })
    },
    
    deleteConceptFile(fileid){
			this.conceptFileService.deleteConceptFile(fileid).then(res=>{
				if(res.data){
					this.$message.info("成功移除附件！")
          this.getConceptFile(this.selectConcept.id)
        }
      })
    },
    
    beforeUpload(file){
      this.appendfileList.push(file)
			return false
    },
    
    //文件上传
    handelFileUpload(){
      if(this.appendfileList.length===0){
				this.$message.info('请先选择上传文件！')
        return 
      }else{
        const formDate=new FormData();
        
        this.appendfileList.forEach(file=>{
          formDate.append('vkspfiles',file)
        })
        
        var fileUploadUrl = "http://172.25.13.72/api.pluginViewer/api/file/upload"
        axios.post(fileUploadUrl,formDate)
        .then(res=>{
          if(res.data.success){
            this.$message.success("附件上传成功！")
            this.addDbFileLog(this.selectConcept.id,res.data)
            this.appendfileList=[]
          }else{
            this.$message.error("上传文件失败！",err)
          }
        })
        .catch(err=>{
          this.$message.error("上传失败！",err)
        })
      }
    },
    
    addDbFileLog(conceptid, fileResult){
      fileResult.fileNameList.forEach(file=>{
				var newConceptFile={
          fileName: file, 
          suddix: "",
          conceptid: conceptid,
          szie: 0,
          downloadUrl: fileResult.filePath.replace("D:/HttpFileServer","http://172.25.13.72:8880") + "/" + file
        }
        
        this.conceptFileService.addconceptfile(newConceptFile).then(res=>{
          this.getConceptFile(this.selectConcept.id)
        })
      })
    },
    
    /*添加收藏*/
    AddToCollection(){
      if(this.loginUser.id==0){
				this.myNotification_info('请先登录！')
        return
      }
      this.vkspservice.addCollectionConcept(this.loginUser.id, this.$route.query.conceptid).then(res=>{
        if(res.data){
					this.ifCollection = true
          this.myNotification_success('收藏成功！')
        }
        else{
					this.myNotification_error('收藏失败！')
        }
      })
    },
    
    /*移除收藏*/
    DeleteOfCollection(){
			this.vkspservice.deleteCollectionConcept(this.loginUser.id, this.$route.query.conceptid).then(res=>{
        if(res.data){
					this.ifCollection = false
          this.myNotification_success('移除收藏成功！')
        }
        else{
					this.myNotification_error('移除收藏失败！')
        }
      })
    },
    
    /*检查是否收藏*/
    checkIfCollection(){
      if(this.loginUser && this.loginUser.id){
  			this.vkspservice.checkCenceptIfCollection(this.loginUser.id, this.$route.query.conceptid).then(res=>{
  				this.ifCollection = res.data
        })	
      }
    },
    
    /*获取上一篇、下一篇文章*/
    getPreviouAndNext(){
      this.ifShowPreview = true
      this.ifShowNext = true
      //length=0 说明不是通过文章列表进入，无法记录上下文，上一篇、下一篇按钮隐藏;
      //length=1 说明只有一篇文章，上一篇、下一篇按钮隐藏;
      if(this.GLOBAL.vkspConceptFilterDate.length == 0 || this.GLOBAL.vkspConceptFilterDate.length == 1){
				this.ifShowPreview = false
        this.ifShowNext = false
      }
      else{
        var _data = this.GLOBAL.vkspConceptFilterDate
        for(var i=0;i<_data.length;i++){
					if(_data[i].id==this.$route.query.conceptid){
						if(i==0){
							this.ifShowPreview = false
              this.previewAndNextConcept.next = _data[1]
            }
            else if(i==_data.length-1){
							this.ifShowNext = false
              this.previewAndNextConcept.preview = _data[i-1]
            }else{
              this.previewAndNextConcept.preview = _data[i-1]
							this.previewAndNextConcept.next = _data[i+1]
            }
            break
          }
        }
      }
    },
    
    openComments(){
			EventBus.$emit('ConceptCommentVisible',true)
      EventBus.$emit('ConceptComments',this.selectConcept.id)
    },
    
    /*
    编辑concept
    */
    openEditDialog(){
			//传递事件总线参数
			EventBus.$emit('ConceptDetailVisible',true)
			EventBus.$emit('Concept',this.selectConcept)
    },
    
    /**/
    openAttachments(){
			EventBus.$emit('ConceptFileVisible',true)
      EventBus.$emit('ConceptFile',this.conceptFiles)
    },
    
    //commentid:要回复的评论
    reply(commentid){
      if(this.replyComment.commentid==commentid){
				this.replyComment.commentid=-1
      }else{
				this.replyComment.commentid=commentid
      }
    },
    
    /*function 返回concept主页面*/
    backToConcept(){
			//this.$router.push('/concept')
      this.$router.back()
      //this.$router.go(-1)
    },
    
    /*全屏，沉浸式阅读*/
    fullScreanClick(){
      
			this.fullscreen = !this.fullscreen
		},
    
    /*调整专栏*/
    gotoTagCollectionConcept(){
			
    }
  }
}
</script>

<style>
.drawer_des{
  /*color:blue;*/
	font-weight: bold; 
	font-size: 15px;
}

.editor-wrap {
width: 900px;
}
.editor-wrap > div {
width: 100%;
}
.fr-wrapper > div[style*='z-index:9999;'],
.fr-wrapper > div[style*='z-index: 9999;'] {
height: 0;
overflow: hidden;
}
.fr-box .second-toolbar #logo {
width: 0 !important;
height: 0 !important;
overflow: hidden;
}
.fr-box .fr-toolbar {
border-radius: 4px 4px 0 0;
border-color: #dcdfe6;
}
.fr-box .second-toolbar {
border-radius: 0 0 4px 4px;
border-color: #dcdfe6;
}
.fr-box .fr-wrapper {
border-color: #dcdfe6 !important;
}

/*.ql-editor{
	height:600px;
}*/
.leftleft{
  float:left;
  height:900px;
  flex-direction: column;
  width:5%;
}
.left{
  width:85%;
}
.left .filearea{
	float:left;
  width:18%;
}
.left .contentarea{
	float:right;
  width:80%;
}
.right{
  height:900px;
  width:5%;
  display: flex;
  justify-content: left;
  flex-direction: column;
}

.rightright{
	height:900px;
  width:5%;
  display: flex;
  justify-content: left;
  flex-direction: column;
}

.myinputstyle{
  margin:10px 0 0 0;
	background-color:#f8f8f8;
  border:none;
  outline:medium;
  border-radius:20px;
}
.mybuttonstyle{
  float:right;
  margin:15px 15px 15px 0;
	background-color:#c5c5c5;
  border:none;
  outline:medium;
  width:90px;
}


.exampleClass {
  cursor: pointer;
  padding: 12px 24px;
  background-color: white;
  color: black;
  border-radius: 7px;
  transition: transform 250ms cubic-bezier(.2,.8,.4,1);
}
.exampleClass:hover {
  transform: scale(1.20);
}
.exampleClass:active {
  transform: scale(0.9);
}

/*.ant-avatar {
  margin-left:-15px;
	box-shadow: $shadow-3;
	border-radius: 25px;
  cursor: pointer;
  transition: transform 250ms cubic-bezier(.2,.8,.4,1);
}
.ant-avatar:hover {
    transform: scale(1.20);
}
.ant-avatar:active {
    transform: scale(0.9);
}*/

conceptDetailClass{
	height: 100%;
}

</style>
