介绍如何在IIS环境全新搭建网站。
动态网站的搭建需要分别搭建前后端，前端资源文件以及后端接口，另外需要接口访问的数据库还原。
本文档记录测试自搭建网站DMPluginView（http://172.25.13.72/DMPluginView）

**1    IIS安装**  
![image](https://github.com/user-attachments/assets/d2844491-e9a8-40f2-abe5-dfe21b4276bd)
![image](https://github.com/user-attachments/assets/3545767e-6a28-406c-bbfc-5d5074782053)
![image](https://github.com/user-attachments/assets/b5fdf05c-9438-402e-bb04-e3c22a5876a1)

**2    前端Vue**  
2.1  部署站点  
1）部署到iis的站点名称为“DMPluginView”，在源码目录public-index.html中添加<base href="/dmpluginview/">  
![image](https://github.com/user-attachments/assets/c3aa0fa4-c60a-47f9-b5f8-57c0ce836a51)

2）新增public-web.config文件，内容：  
![image](https://github.com/user-attachments/assets/a1b73d18-93b8-4b81-ae55-42e12cee9506)

3）执行npm build，将build后的文件夹拷贝到iis子目录，转为站点(可转可不转)  
![image](https://github.com/user-attachments/assets/014240cd-63cc-4b6e-bdfa-904194a71685)

2.2  安装url rewrite工具  
![image](https://github.com/user-attachments/assets/10f2da57-523f-40c5-9641-363a93874cd5)

1）安装好之后能看到有Url rewrite模块：  
![image](https://github.com/user-attachments/assets/1d45ae83-af22-4ccb-9674-761d5297acdb)
![image](https://github.com/user-attachments/assets/1f1184ee-ff32-48b8-84f2-c4fcd28d96f8)

2）设置url重写规则
双击URL Rewrite-add rule, 按下图设置  
![image](https://github.com/user-attachments/assets/ce34b4ad-9db1-4b77-bff4-4e5e422689d3)
![image](https://github.com/user-attachments/assets/04b60f53-aebd-4608-9af7-2b46c9e2b2db)

设置好之后：  
![image](https://github.com/user-attachments/assets/e8e386a6-9545-49dc-8c01-d4b8295a2869)

此时前端页面在浏览器通过路由即可访问。

**3    后端Asp Net Api**  
3.1   部署站点  
1）项目直接发布到文件夹：  
![image](https://github.com/user-attachments/assets/e08ccb2b-f2ce-45b0-a455-eac2b60b2187)

2）将文件夹拷贝到iis子目录下，并转为站点。注意站点要选择新建的应用池(应用池好像有数量限制，尽量不使用iis默认站点应用池)  
![image](https://github.com/user-attachments/assets/acf7a637-9b75-45e5-b5a4-f835be200d09)

3）此时直接访问站点，会报错500。这是因为IIS模块缺少了【AspNetCoreModuleV2】  
![image](https://github.com/user-attachments/assets/3a6e6cf9-59f0-41c7-adf4-4e2dbd56686b)

4）在微软的Net下载处下载Hosting Bundle安装即可

3.2 安装Hosting Bundle  
![image](https://github.com/user-attachments/assets/f02b730e-f7d6-4e93-b44c-b9d54a5d7909)


**4    数据库备份与恢复**  
4.1    数据库备份  
![image](https://github.com/user-attachments/assets/60bb6951-36a9-4b86-bdad-97c006acf956)

4.2    数据库恢复  
![image](https://github.com/user-attachments/assets/db1201eb-b538-47a9-bc98-f579c842a7ad)

创建后端api中数据库连接用户：Vico02/Vico202109
4.3    数据库用户密码过期
把强制密码过期项反勾选掉，否则一段时间后密码过期。一时间摸不着头脑哪里的问题。  
![image](https://github.com/user-attachments/assets/f36af971-9e5a-46c6-8af9-5bc39f69fc44)

