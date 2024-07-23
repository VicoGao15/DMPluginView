<template>
  <div>
    <a-collapse :bordered="false" :activeKey="activekey">
      <template #expandIcon="props">
        <a-icon type="caret-right" :rotate="props.isActive ? 90 : 0" />
      </template>
      <a-collapse-panel v-for="itemcategory in category" :key="itemcategory.key">
        <template #header>
          <b>{{itemcategory.text}} ( {{itemcategory.data.length}} )</b>
        </template>
				<a-row :gutter="[24, 24]" style="margin-top:0px;">
          <draggable 
            :group="itemcategory.key"
            v-model="itemcategory.data"
            animation="400"
            style="display:flex;flex-flow:wrap;"
            @start="onStartDrag()"
            @end="onEndDrag()"
            >
            <transition-group>
							<div v-for="item in itemcategory.data" :key="item.id">
                <a-col :span="4">
                  <a-card :bordered="false" :style="todoCardStyle(item.types)" @click="viewTodoDetail(item.id)" hoverable class="header-solid h-full card-profile-information" :bodyStyle="{paddingTop: 0, paddingBottom: '16px' }" :headStyle="{paddingRight: 0,}">
                    <template #title >
                      <h6 style="margin-left:25px;margin-top:30px;" >{{item.title}}</h6>
                    </template>
                    
                      <a-button type="link" slot="extra" style="margin-right:35px;">
                        <a-dropdown :trigger="['hover']">
                        <svg @click.prevent t="1673427750207" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="46986" width="18" height="18">
                          <path d="M64 512c0-31.44 10.92-58.07 32.75-79.91 21.83-21.83 48.47-32.75 79.9-32.75 31.44 0 58.08 10.92 79.91 32.75s32.75 48.47 32.75 79.91-10.92 58.07-32.75 79.91c-21.83 21.83-48.47 32.75-79.91 32.75-31.44 0-58.07-10.91-79.9-32.75C74.92 570.07 64 543.44 64 512z m335.35 0c0-31.44 10.91-58.07 32.75-79.91 21.83-21.83 48.47-32.75 79.91-32.75 31.44 0 58.07 10.92 79.91 32.75 21.83 21.83 32.75 48.47 32.75 79.91s-10.92 58.07-32.75 79.91c-21.83 21.83-48.47 32.75-79.91 32.75-31.44 0-58.07-10.91-79.91-32.75-21.84-21.84-32.75-48.47-32.75-79.91z m335.34 0c0-31.44 10.92-58.07 32.75-79.91 21.83-21.83 48.47-32.75 79.91-32.75s58.07 10.92 79.91 32.75C949.09 453.93 960 480.56 960 512s-10.91 58.07-32.75 79.91-48.47 32.75-79.91 32.75-58.07-10.91-79.91-32.75c-21.82-21.84-32.74-48.47-32.74-79.91z" p-id="46987" fill="#515151">
                          </path>
                        </svg>
                        <template #overlay>
                          <a-menu>
                            <a-menu-item key="0" @click="viewTodoDetail(item.id)" class="menuitem">
                              <svg t="1679644841438" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="9404" width="16" height="16">
                                <path d="M512 1011.2a496.384 496.384 0 0 0 448.3072-279.296 38.4 38.4 0 0 0-68.9664-33.8944A420.0448 420.0448 0 0 1 512 934.4c-232.9088 0-422.4-189.4912-422.4-422.4S279.0912 89.6 512 89.6s422.4 189.4912 422.4 422.4a38.4 38.4 0 0 0 76.8 0c0-275.2512-223.9488-499.2-499.2-499.2S12.8 236.7488 12.8 512s223.9488 499.2 499.2 499.2z" fill="#438CFF" p-id="9405"></path>
                                <path d="M760.4224 537.6a38.4 38.4 0 0 0-38.4-38.4h-460.8a38.4 38.4 0 0 0 0 76.8h460.8a38.4 38.4 0 0 0 38.4-38.4zM261.2224 378.8288h307.2a38.4 38.4 0 0 0 0-76.8h-307.2a38.4 38.4 0 0 0 0 76.8zM261.2224 696.3712a38.4 38.4 0 0 0 0 76.8h204.8a38.4 38.4 0 0 0 0-76.8h-204.8z" fill="#438CFF" p-id="9406"></path><path d="M711.5776 340.4288m-51.2 0a51.2 51.2 0 1 0 102.4 0 51.2 51.2 0 1 0-102.4 0Z" fill="#438CFF" p-id="9407"></path>
                              </svg>
                              <span style="margin:-3px 0 0 10px;">详情</span>
                            </a-menu-item>
                            <a-menu-item key="1"  @click="editTodo(item.id)" class="menuitem">
                              <svg t="1679883108482" class="icon" viewBox="0 0 1126 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="23294" width="16" height="16">
                              <path d="M593.92 331.093333c-5.12-5.12-13.653333-5.12-18.773333 0L296.96 607.573333c-13.653333 13.653333-20.48 30.72-20.48 49.493334l1.706667 128c0 6.826667 6.826667 13.653333 13.653333 13.653333h153.6l298.666667-296.96c3.413333-3.413333 3.413333-6.826667 3.413333-10.24 0-3.413333-1.706667-6.826667-3.413333-10.24l-150.186667-150.186667zM851.626667 373.76l-150.186667-150.186667c-5.12-5.12-13.653333-5.12-18.773333 0l-51.2 51.2c-3.413333 3.413333-3.413333 6.826667-3.413334 10.24 0 3.413333 1.706667 6.826667 3.413334 10.24l150.186666 150.186667c3.413333 3.413333 6.826667 3.413333 10.24 3.413333 3.413333 0 6.826667-1.706667 10.24-3.413333l51.2-51.2c3.413333-3.413333 3.413333-6.826667 3.413334-10.24-1.706667-5.12-1.706667-8.533333-5.12-10.24z" fill="#1296db" p-id="23295"></path><path d="M742.4 0h-358.4C199.68 0 51.2 148.48 51.2 332.8v358.4C51.2 875.52 199.68 1024 384 1024h358.4C926.72 1024 1075.2 875.52 1075.2 691.2v-358.4C1075.2 148.48 926.72 0 742.4 0z m-307.2 855.04H290.133333c-37.546667 0-68.266667-30.72-68.266666-68.266667v-145.066666c0-18.773333 6.826667-35.84 20.48-47.786667l402.773333-402.773333c27.306667-27.306667 69.973333-27.306667 97.28 0l145.066667 145.066666c27.306667 27.306667 27.306667 69.973333 0 97.28L482.986667 834.56c-11.946667 13.653333-30.72 20.48-47.786667 20.48z m436.906667 0H622.933333c-13.653333 0-25.6-11.946667-25.6-25.6 0-6.826667 3.413333-13.653333 6.826667-18.773333 5.12-5.12 11.946667-6.826667 18.773333-6.826667h249.173334c18.773333 0 34.133333 5.12 34.133333 23.893333 0 18.773333-15.36 27.306667-34.133333 27.306667z" fill="#1296db" p-id="23296"></path>
                              </svg>
                              <span style="margin:-3px 0 0 10px;">编辑</span>
                            </a-menu-item>
                            <a-menu-divider v-if="itemcategory.key!='revocation'"/>
                            <a-menu-item key="2" @click="editTodoStatus(item.id, 'doing')" class="menuitem" v-if="itemcategory.key!='doing' && itemcategory.key!='revocation'">
                              <svg t="1679882817448" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="18005" width="16" height="16">
                              <path d="M766.208 958.336H269.696c-112.768 0-204.032-91.392-204.032-204.032V257.792c0-112.64 91.264-204.032 204.032-204.032h496.512c112.64 0 204.032 91.392 204.032 204.032v496.512c0 112.768-91.392 204.032-204.032 204.032z" fill="#FF9429" p-id="18006"></path><path d="M705.408 743.04c-1.664-10.624-3.84-20.992-6.272-30.976-1.152-4.736-2.688-9.216-4.096-13.824-1.792-6.016-3.456-12.032-5.632-17.792-1.92-5.248-3.968-10.112-6.144-15.232-2.048-4.736-3.968-9.6-6.144-14.208-2.432-5.12-5.12-10.112-7.808-14.976-2.176-3.968-4.48-7.936-6.784-11.776-2.944-4.864-6.144-9.472-9.344-14.08-2.432-3.456-4.864-6.656-7.424-9.856-3.456-4.352-7.04-8.704-10.752-12.8-2.688-2.816-5.376-5.504-8.064-8.192-3.84-3.84-7.808-7.552-11.904-11.008-2.944-2.432-6.016-4.48-8.96-6.656-4.224-3.072-8.32-6.272-12.672-8.832-0.256-0.128-0.512-0.384-0.768-0.512-13.312-7.936-21.376-22.016-21.376-37.504v-17.536c0-15.488 8.192-29.568 21.376-37.504 0.256-0.128 0.512-0.384 0.768-0.512 4.352-2.688 8.448-5.76 12.672-8.832 3.072-2.176 6.016-4.224 8.96-6.656 4.096-3.456 7.936-7.168 11.904-11.008 2.688-2.688 5.504-5.376 8.192-8.192 3.712-4.096 7.296-8.32 10.752-12.672 2.56-3.2 4.992-6.528 7.424-9.856 3.2-4.48 6.4-9.216 9.344-14.08 2.432-3.84 4.608-7.808 6.784-11.776 2.688-4.864 5.376-9.856 7.808-14.976 2.176-4.608 4.224-9.344 6.144-14.208 2.048-4.992 4.224-9.984 6.144-15.232 2.048-5.76 3.84-11.776 5.632-17.792 1.408-4.608 2.944-9.088 4.096-13.824 2.56-10.112 4.608-20.48 6.272-30.976 0.128-0.896 0.384-1.664 0.512-2.56 4.096-26.752-16.384-51.072-43.52-51.072H373.376c-27.136 0-47.616 24.32-43.52 51.072 0.128 0.896 0.384 1.664 0.512 2.56 1.664 10.624 3.84 20.992 6.272 31.104 1.152 4.736 2.688 9.216 4.096 13.824 1.792 6.016 3.456 12.032 5.632 17.792 1.92 5.248 3.968 10.112 6.144 15.232 2.048 4.736 3.968 9.6 6.144 14.208 2.432 5.12 5.12 10.112 7.808 14.976 2.176 3.968 4.48 7.936 6.784 11.776 2.944 4.864 6.144 9.472 9.344 14.08 2.432 3.456 4.864 6.656 7.424 9.856 3.456 4.352 7.04 8.704 10.752 12.672 2.688 2.816 5.376 5.504 8.192 8.192 3.84 3.84 7.808 7.552 11.904 11.008 2.944 2.432 6.016 4.48 8.96 6.656 4.224 3.072 8.32 6.272 12.672 8.832 0.256 0.128 0.512 0.384 0.768 0.512 13.312 7.936 21.376 22.016 21.376 37.504v17.536c0 15.488-8.192 29.568-21.376 37.504-0.256 0.128-0.512 0.384-0.768 0.512-4.352 2.688-8.448 5.76-12.672 8.832-2.944 2.176-6.016 4.224-8.96 6.656-4.096 3.456-7.936 7.168-11.904 11.008-2.688 2.688-5.504 5.248-8.064 8.192-3.712 4.096-7.296 8.32-10.752 12.8-2.56 3.2-4.992 6.528-7.424 9.856-3.2 4.608-6.4 9.216-9.344 14.08-2.304 3.84-4.608 7.808-6.784 11.776-2.688 4.864-5.376 9.856-7.808 14.976-2.176 4.608-4.224 9.344-6.144 14.208-2.048 4.992-4.224 9.984-6.144 15.232-2.048 5.76-3.84 11.776-5.632 17.792-1.408 4.608-2.944 9.088-4.096 13.824-2.56 10.112-4.608 20.48-6.272 31.104-0.128 0.896-0.384 1.664-0.512 2.56-4.096 26.752 16.384 51.072 43.52 51.072h289.024c27.136 0 47.616-24.32 43.52-51.072-0.256-1.024-0.512-1.92-0.64-2.816z" fill="#FFFFFF" p-id="18007"></path><path d="M415.872 755.712h204.16c7.68 0 11.264-9.728 5.248-14.592l-93.952-73.216c-1.92-1.536-2.944-3.84-2.944-6.4V425.856c0-2.304 1.024-4.48 2.688-6.016l55.936-45.952c5.504-5.12 1.92-14.336-5.632-14.336H454.4c-7.552 0-11.136 9.216-5.632 14.336l55.296 45.696c1.664 1.536 2.688 3.712 2.688 6.016v235.648c0 2.432-1.152 4.736-2.944 6.4L410.496 741.12c-5.888 4.992-2.432 14.592 5.376 14.592z" fill="#FF9429" p-id="18008"></path>
                              </svg>
                              <span style="margin:-3px 0 0 10px;">进行中</span>
                            </a-menu-item>
                            <a-menu-item key="3" @click="editTodoStatus(item.id, 'complete')" class="menuitem" v-if="itemcategory.key!='revocation' && itemcategory.key!='complete'">
                              <svg t="1679880905905" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="10584" width="16" height="16"><path d="M186.56 128h114.048c15.728 0 27.76 14.048 26.24 29.84l-0.944 6.24c-0.32 2.512-0.48 4.928-0.496 7.2 0 28.32 19.28 53.52 37.648 53.52h291.632c16.72 0 35.824-25.472 35.808-53.52 0-2.32-0.16-4.72-0.48-7.216l-0.928-6.224C687.568 142.048 699.6 128 715.328 128h112.896C911.424 128 960 182.544 960 266.32V790.88c0 92.608-46.656 137.12-137.6 137.12H192.368C105.728 928 64 879.744 64 790.848V266.32C64 177.2 100.176 128 186.56 128z" fill="#FFBD27" p-id="10585"></path>
                              <path d="M404.88 96C379.936 96 360 117.44 360 143.936c0 26.56 19.984 48.064 44.88 48.064h209.056c23.68 0 43.664-21.504 43.664-48.064C657.6 117.44 637.664 96 612.672 96H404.88z" fill="#333333" p-id="10586"></path><path d="M492.32 695.44a34.336 34.336 0 0 1-24.368-10.128l-137.872-138.416a34.752 34.752 0 0 1 0-48.96 34.368 34.368 0 0 1 48.736 0l113.504 113.968 216.864-217.744a34.304 34.304 0 0 1 48.736 0c13.44 13.504 13.44 35.392 0 48.912l-241.232 242.24a34.4 34.4 0 0 1-24.368 10.128z" fill="#FFFFFF" p-id="10587"></path>
                              </svg>
                              <span style="margin:-3px 0 0 10px;">已完成</span>
                            </a-menu-item>
                            <a-menu-divider/>
                            <a-menu-item key="4"  @click="editTodoStatus(item.id, 'redo')" class="menuitem" v-if="itemcategory.key==='revocation'">
                              <svg t="1679882451393" class="icon" viewBox="0 0 1078 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="14399" width="16" height="16">
                              <path d="M269.877331 956.631579C277.994961 956.631579 286.071044 954.684987 293.576898 950.825095L489.966291 850.068286C520.647662 834.287535 557.177015 834.287535 587.855888 850.019145L784.300048 950.81597C801.669578 959.719203 821.957136 958.291251 837.657789 947.15551 852.922729 936.325546 860.10292 919.109729 857.005105 901.486387L819.477736 687.941939C813.454702 653.551853 825.090587 618.54609 850.453089 594.407338L1009.388824 443.178451C1022.744037 430.483298 1027.269465 412.585986 1021.62169 395.597728 1015.832926 378.233612 1000.737916 365.81618 981.511615 363.096701L761.83511 331.927417C727.477318 327.028232 697.6072 305.856384 681.976056 274.9816L583.735997 80.666294C575.412714 64.168129 558.480874 53.894737 538.947368 53.894737 519.413863 53.894737 502.482023 64.168129 494.137969 80.707427L395.905019 275.0086C380.287538 305.856384 350.417419 327.028232 316.022704 331.932669L96.360018 363.099974C77.156821 365.81618 62.061808 378.233612 56.287176 395.555293 50.625272 412.585986 55.150699 430.483298 68.525044 443.196647L227.446562 594.412016C252.804149 618.54609 264.440033 653.551853 258.411606 687.972681L220.889237 901.488624C217.791815 919.109729 224.97201 936.325546 240.236949 947.15551 249.013109 953.374623 259.313065 956.631579 269.877331 956.631579ZM167.808317 892.158108 205.330293 678.644402C208.230418 662.085379 202.635486 645.200686 190.290705 633.451547L31.374101 482.240854C2.911809 455.185264-7.123138 415.453628 5.144649 378.552808 17.4381 341.677039 49.493468 315.297838 88.811982 309.736411L308.451565 278.572379C325.493009 276.142386 340.198955 265.720974 347.821408 250.665038L446.040698 56.390866C463.595439 21.59437 499.218217 0 538.947368 0 578.67652 0 614.299298 21.59437 631.854037 56.390866L730.073331 250.665038C737.695782 265.720974 752.401726 276.142386 769.443172 278.572379L989.082753 309.736411C1028.401271 315.297838 1060.456637 341.677039 1072.750091 378.552808 1085.017875 415.453628 1074.982928 455.185264 1046.520638 482.240854L887.60403 633.451547C875.259252 645.200686 869.664321 662.085379 872.564445 678.644402L910.086419 892.158108C916.810606 930.411703 900.975406 968.314584 868.843046 991.111427 836.736345 1013.883214 794.954008 1016.839287 759.716201 998.777177L563.25196 897.970047C548.058381 890.17904 529.86202 890.153989 514.617113 897.995097L318.178534 998.777177C302.882297 1006.64334 286.328484 1010.526316 269.877331 1010.526316 248.472866 1010.526316 227.222389 1003.987882 209.051693 991.111427 176.91933 968.314584 161.084133 930.411703 167.808317 892.158108Z" fill="#1296db" p-id="14400"></path>
                              </svg>
                              <span style="margin:-3px 0 0 10px;">激活</span>
                            </a-menu-item>
                            <a-menu-item key="5" class="menuitem" v-if="itemcategory.key==='revocation'">
                              <a-popconfirm
                              title="确定要删除该待办事件吗？"
                              class="menuitem"
                              @confirm="editTodoStatus(item.id, 'delete')"
                              >
                                <svg t="1679909421346" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="8760" width="16" height="16">
                                <path d="M128 320a32 32 0 0 1 0-64h768a32 32 0 0 1 0 64H128z" fill="#2B82D8" p-id="8761"></path><path d="M320 384h512v441.6a102.4 102.4 0 0 1-102.4 102.4h-307.2a102.4 102.4 0 0 1-102.4-102.4V384z" fill="#CAE705" p-id="8762"></path><path d="M352 256h320V224a64 64 0 0 0-64-64h-192a64 64 0 0 0-64 64v32z m64-160h192a128 128 0 0 1 128 128v96H288V224a128 128 0 0 1 128-128zM368 464a32 32 0 0 1 64 0v256a32 32 0 0 1-64 0v-256zM584 464a32 32 0 0 1 64 0v256a32 32 0 0 1-64 0v-256z" fill="#2B82D8" p-id="8763"></path><path d="M256 320v480a64 64 0 0 0 64 64h384a64 64 0 0 0 64-64V320H256zM192 256h640v544a128 128 0 0 1-128 128H320a128 128 0 0 1-128-128V256z" fill="#2B82D8" p-id="8764"></path>
                                </svg>
                                <span style="margin:-3px 0 0 10px;">删除</span>
                              </a-popconfirm>
                            </a-menu-item>
                            <a-menu-item key="6" v-else>
                              <a-popconfirm
                              title="是否撤销该申请？"
                              class="menuitem"
                              @confirm="editTodoStatus(item.id, 'revocation')"
                              >
                                <svg t="1679883550041" class="icon" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="36933" width="16" height="16">
                                <path d="M760.2176 176.384h-75.8272c-3.1744 43.6736-39.5264 78.1312-84.0192 78.1312h-176.128c-44.4928 0-80.8448-34.4576-84.0192-78.1312H264.3968c-65.4336 0-118.4768 53.0432-118.4768 118.4768v503.04c0 65.4336 53.0432 118.4768 118.4768 118.4768h495.8208c65.4336 0 118.4768-53.0432 118.4768-118.4768V294.8608c0-65.4336-53.0432-118.4768-118.4768-118.4768z" fill="#FF5F5F" p-id="36934"></path><path d="M763.2384 161.024h-60.6208c-4.6592-50.6368-47.36-90.4704-99.1744-90.4704H427.2128c-51.8656 0-94.5152 39.8336-99.1744 90.4704H267.4176c-73.7792 0-133.8368 60.0576-133.8368 133.8368v503.04c0 73.7792 60.0576 133.8368 133.8368 133.8368h495.8208c73.7792 0 133.8368-60.0576 133.8368-133.8368V294.8608c0-73.8304-60.0576-133.8368-133.8368-133.8368zM427.2128 101.2736h176.1792c37.9904 0 68.9664 30.9248 68.9664 68.9664 0 1.6384-0.1536 3.2768-0.256 4.9152v0.1024c-1.9456 26.8288-19.3536 49.3056-43.1104 58.9312l-0.4608 0.1536c-1.792 0.7168-3.6352 1.3824-5.5296 1.9456-0.4608 0.1536-0.8704 0.2048-1.3312 0.3584-1.6384 0.4608-3.2768 0.8704-4.9152 1.2288-0.9216 0.1536-1.8432 0.256-2.7648 0.4096-1.28 0.2048-2.5088 0.4096-3.7888 0.5632-2.2528 0.2048-4.5056 0.3584-6.8096 0.3584H427.2128c-2.304 0-4.5568-0.1024-6.8096-0.3584-1.28-0.1024-2.5088-0.3584-3.7888-0.5632-0.9216-0.1536-1.8432-0.256-2.7648-0.4096-1.6896-0.3584-3.328-0.768-4.9152-1.2288-0.4608-0.1024-0.8704-0.2048-1.3312-0.3584-1.8944-0.5632-3.7376-1.2288-5.5296-1.9456l-0.4608-0.1536c-23.7056-9.5744-41.1648-32.0512-43.1104-58.9312v-0.1024c-0.1024-1.6384-0.256-3.2768-0.256-4.9152 0.0512-38.0416 30.976-68.9664 68.9664-68.9664z m439.1424 696.6272c0 56.8832-46.2336 103.1168-103.1168 103.1168H267.4176c-56.8832 0-103.1168-46.2336-103.1168-103.1168V294.8608c0-56.8832 46.2336-103.1168 103.1168-103.1168h62.5664c6.7584 30.464 27.4432 55.6544 55.04 68.6592 0.3584 0.2048 0.768 0.3584 1.1264 0.5632 1.9456 0.8704 3.9424 1.6896 5.9904 2.4576 0.8704 0.3584 1.792 0.6656 2.6624 0.9728 1.7408 0.6144 3.5328 1.1776 5.3248 1.6896 1.28 0.3584 2.56 0.6656 3.8912 1.024 1.5872 0.3584 3.1232 0.768 4.7104 1.024 1.7408 0.3584 3.5328 0.5632 5.3248 0.8192 1.2288 0.1536 2.4576 0.3584 3.7376 0.512 3.1232 0.3072 6.2464 0.4608 9.4208 0.4608h176.1792c3.1744 0 6.3488-0.2048 9.4208-0.4608 1.28-0.1024 2.4576-0.3072 3.7376-0.512 1.792-0.256 3.584-0.4608 5.3248-0.8192 1.5872-0.3072 3.1232-0.6656 4.7104-1.024 1.28-0.3072 2.6112-0.6144 3.8912-1.024 1.792-0.512 3.584-1.0752 5.3248-1.6896 0.9216-0.3072 1.792-0.6656 2.6624-0.9728 1.9968-0.768 3.9936-1.5872 5.9904-2.4576 0.4096-0.1536 0.768-0.3584 1.1264-0.5632 27.5968-12.9536 48.3328-38.1952 55.04-68.6592h62.5664c56.8832 0 103.1168 46.2336 103.1168 103.1168v503.04z" fill="#424242" p-id="36935"></path><path d="M553.7792 557.056l125.2352-125.2352a15.36 15.36 0 0 0 0-21.7088 15.36 15.36 0 0 0-21.7088 0l-125.2352 125.2352-125.2352-125.184a15.36 15.36 0 0 0-21.7088 0 15.36 15.36 0 0 0 0 21.7088l125.2352 125.2352-125.2352 125.2352a15.36 15.36 0 0 0 10.8544 26.2144c3.9424 0 7.8848-1.4848 10.8544-4.5056l125.2352-125.2352 125.2352 125.2352c3.0208 3.0208 6.912 4.5056 10.8544 4.5056s7.8848-1.4848 10.8544-4.5056a15.36 15.36 0 0 0 0-21.7088l-125.2352-125.2864z" fill="#FFFFFF" p-id="36936"></path>
                                </svg>
                                <span  style="margin:-3px 0 0 10px;">任务撤销</span>
                              </a-popconfirm>
                            </a-menu-item>
                          </a-menu>
                        </template>
                        </a-dropdown>  
                      </a-button>
                    <!--<hr/>-->
                    <div style="margin-left:35px;">
										  <a-tag v-if="item.priority==0" color="#e52424">P0-刻不容缓</a-tag>
                      <a-tag v-else-if="item.priority==1" color="#f98b24">P1-事不宜迟</a-tag>
                      <a-tag v-else-if="item.priority==2" color="#f9dd24">P2-循序渐进</a-tag>
                      <a-tag v-else-if="item.priority==3" color="#24cff9">P3-未雨绸缪</a-tag>
                      <a-tag v-else-if="item.priority==4" color="#c0cfd3">P4-源远流长</a-tag>
                      <a-tag v-else color="cyan">P5-仓库吃灰</a-tag>
                    </div>
                    <a-descriptions :column="1" style="margin-left:35px;">
                      <a-descriptions-item>
                        {{showTimeFormat(item.deadline,true)}}
                        <span style="margin-left:15px;margin-top:0px;"></span>
                      </a-descriptions-item>
                    </a-descriptions>
                  </a-card>	
                </a-col>
              </div>
            </transition-group>
          </draggable>
        </a-row>
      </a-collapse-panel>
    </a-collapse>
    
    <!--实验-->
    <!--<draggable v-model="category.complete.data" animation="400"> 
      <transition-group> 
			<div class="item" v-for="item in category.complete.data" :key="item.id">
        <a-col :span="6">
          <div>
						{{item.title}}
          </div>
        </a-col>
				</div> 
      </transition-group> 
    </draggable>-->
    <!--实验-->
  </div>
</template>

<script>
import {notification} from 'ant-design-vue'
import {EventBus} from '../../../event-bus.js'
import draggable from 'vuedraggable'
export default {
  props:{
		todoData:[]//父组件传值
  },
  components:{
		draggable
  },
  mounted(){
		EventBus.$on('searchplugin',data=>{
			this.searchInput=data
			this.onSearch()
		})
  },
  created(){
		//计时器,判断是否存在非正常包
		this.timer = setInterval(this.showMessageForTodo, this.checkTodoList_time)
	},
  watch:{
		todoData:{
      handler(newValue,oldValue){
        this.todoData_copy = JSON.parse(JSON.stringify(newValue)) 
        this.todoData_copy = this.todoData_copy.sort(function(a,b){
					return a.priority - b.priority
        })
				this.categoryTodoList()
      }
    },
    deep:true,
  },
  data(){
    return{
      drag:false,
      searchInput:'',
      todoData_copy:[],
			activekey:[],
      category:{
				todo:{text:'未开始', key:'todo', data:[]},
        doing:{text:'进行中', key:'doing', data:[]},
        complete:{text:'已完成', key:'complete', data:[]},
        expire:{text:'已过期', key:'expire', data:[]},
        revocation:{text:'已取消', key:'revocation', data:[]},
      },
      
      checkTodoList_time:this.GLOBAL.checkTodoList_time,
      
      //imgSrc: require('images/backgroud/bianqian.jpg')
    }
  },
  methods:{
    /*
    Function TODO分类
    */
		categoryTodoList(){
      this.activekey=[]
      this.category.todo.data = []
      this.category.doing.data = []
      this.category.complete.data = []
      this.category.expire.data = []
      this.category.revocation.data = []
      this.todoData_copy.forEach(item=>{
        if( Date.parse(item.deadline) < new Date() && item.status===0){
          this.category.expire.data.push(item)
        }
        else{
          switch(item.status){
            case 1:
              this.category.complete.data.push(item)
              break
            case 2:
              this.category.todo.data.push(item)
              break
            case 3:
              this.category.revocation.data.push(item)
              break
            default:
              this.category.doing.data.push(item)
              break
          }
        }
      })
      if(this.category.todo.data.length>0 && this.category.todo.data.length<=6){ this.activekey.push('todo')}
      if(this.category.doing.data.length>0 && this.category.doing.data.length<=6){ this.activekey.push('doing')}
      if(this.category.complete.data.length>0 && this.category.complete.data.length<=6){ this.activekey.push('complete')}
      if(this.category.expire.data.length>0 && this.category.expire.data.length<=6){ this.activekey.push('expire')}
      if(this.category.revocation.data.length>0 && this.category.revocation.data.length<=6){ this.activekey.push('revocation')}
    },
    
    /*
    Function showMessageForTodo
    */
    showMessageForTodo(){
			var message=""
			var des=""
			
			if(this.category.todo.data.length!=0){
				message="您有 "+this.category.todo.data.length+" 个待办事件！"
        des="点击前往查看"
        
				notification.open({
					type:'warning',
					message:message,
					description:des,
					duration:5,
					onClick:()=>{
						this.$router.push('/todolist')
					}, 
					style:{
						width:'500px',
						marginLeft:`${335-500}px`,
					}
				})
			}
    },
    
    /*
    Function viewTodoDetail
    */
    viewTodoDetail(todoid){
      EventBus.$emit("ViewTodoDetail", todoid)
    },
    editTodo(todoid){
			EventBus.$emit("EditTodoItem", todoid)
    },
    
    /*
    Function 修改Todo状态
    */
    editTodoStatus(itemid, operate){
      EventBus.$emit("EditTodoItemStatus", {id:itemid, opera:operate})
    },
    
    /*
    Function card样式
    */
    todoCardStyle(type){
      var style = "height:180px;background-color:rgb(250, 250, 250);background-size:100% 100%;box-shadow:none;"
      switch(type){
        case 0:
          style += "background-image: url('images/background/yellow.png');"
          break
				case 1:
          style += "background-image: url('images/background/blue.png');"
          break
        case 2:
          style += "background-image: url('images/background/white.png');"
          break
        case 3:
          style += "background-image: url('images/background/bianqian.png');"
          break
        case 4:
          style += "background-image: url('images/background/huaban.png');"
          break
        default:
          style += "background-image: url('images/background/zhiban.png');"
          break
      }
      
			return style
    },
    
    /*
    Function 搜索框过滤
    */
    onSearch(){
			var searchTable=[]
			this.todoData.forEach(element => {
        if(element.title.toLowerCase().indexOf(this.searchInput.toLowerCase())!=-1){
					searchTable.push(element)
				}
			});
			this.todoData_copy = JSON.parse(JSON.stringify(searchTable)) 
      this.categoryTodoList()
      this.expand_Category_CollapsePanel()
		},
    
    /*
    Function 展开下拉分类
    */
    expand_Category_CollapsePanel(){
      this.activekey = [] //complete expire revocation doing todo
			if(this.category.complete.data.length!=0){
				this.activekey.push('complete')
      }
      if(this.category.todo.data.length!=0){
				this.activekey.push('todo')
      }
      if(this.category.expire.data.length!=0){
				this.activekey.push('expire')
      }
      if(this.category.doing.data.length!=0){
				this.activekey.push('doing')
      }
      if(this.category.revocation.data.length!=0){
				this.activekey.push('revocation')
      }
    },
    
    /*function 拖曳方法*/
    onStartDrag(){
      console.log(this.category)
			this.drag = true
    },
    onEndDrag(){
			this.drag = false
    }
    
  }
}
</script>

<style>
.menuitem{
	display:flex;
}
</style>
