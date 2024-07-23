using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : Controller
{
    private readonly HttpClient _httpClient;
    
    public FileController(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
    }
    
    
    /// <summary>
    /// 上传文件到服务器
    /// </summary>
    /// <param name="files">文件</param>
    /// <returns></returns>
    [HttpPost]
    public JsonResult Upload(IFormCollection files)
    {
        try
        {
            // 服务器将要存储文件的路径
            var Folder = "D:/HttpFileServer/VKSP/file";
            if (Directory.Exists(Folder) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(Folder);
            }

            string[] filelist = { };
            
            FormFileCollection fileCollection = (FormFileCollection)files.Files;
            foreach (IFormFile file in fileCollection)
            {
                StreamReader reader = new StreamReader(file.OpenReadStream());
                String content = reader.ReadToEnd();
                String name = file.FileName; // 获取文件名
                string pathName = Path.GetExtension(name); // 获取后缀名
                filelist.Append(name);
                if (pathName != ".xlsx")
                {
                    return Json(new
                    {
                        Message = "不支持该格式",
                        Success = false
                    });
                }

                String filename = Folder + Guid.NewGuid().ToString().Replace("-", "") + pathName; // 生成新的文件名，唯一不重复
                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    // 复制文件
                    file.CopyTo(fs);
                    // 清空缓冲区数据
                    fs.Flush();
                }
            }

            return Json(new
            {
                Message = "保存成功："+ filelist,
                Success = true
            });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                Message = ex.Message,
                Success = false
            });
        }
    }

    /// <summary>
    /// 上传文件到服务器
    /// </summary>
    /// <param name="files">文件</param>
    /// <returns></returns>
    [HttpPost("upload")]
    [Consumes("application/json","multipart/form-data")]
    public async Task<JsonResult> PostFormData([FromForm(Name="vkspfiles")] List<IFormFile> files) 
    {
        var formFile = Request.Form.Files.FirstOrDefault();
        if (formFile == null) 
        {
            throw new Exception("文件不能为空！");
        }
        
        List<string> fileNameList = new List<string>();

        var UploadPath = "D:/HttpFileServer/VKSP/file";

        try
        {
            for (int i = 0; i < files.Count; i++)
            {
                using (var fileStream = new FileStream(Path.Combine(UploadPath, files[i].FileName), FileMode.Create))
                {
                    await files[i].CopyToAsync(fileStream);
                    fileNameList.Add(files[i].FileName);
                }
            }

            return Json(new
            {
                FilePath = UploadPath,
                FileNameList = fileNameList,
                Success = true
            });
        }
        catch(Exception ex)
        {
            return Json(new
            {
                Message = ex.Message,
                Success = false
            });
        }
        
    }

    /// <summary>
    /// 上传Avatar文件到服务器
    /// </summary>
    /// <param name="files">文件</param>
    /// <returns></returns>
    [HttpPost("uploadavatar")]
    [Consumes("application/json", "multipart/form-data")]
    public async Task<JsonResult> PostFormDataAvatar([FromForm(Name = "vkspfiles")] List<IFormFile> files)
    {
        var formFile = Request.Form.Files.FirstOrDefault();
        if (formFile == null)
        {
            throw new Exception("文件不能为空！");
        }

        List<string> fileNameList = new List<string>();

        var UploadPath = "D:/HttpFileServer/VKSP/avatar";

        try
        {
            for (int i = 0; i < files.Count; i++)
            {
                using (var fileStream = new FileStream(Path.Combine(UploadPath, files[i].FileName), FileMode.Create))
                {
                    await files[i].CopyToAsync(fileStream);
                    fileNameList.Add(files[i].FileName);
                }
            }

            return Json(new
            {
                FilePath = UploadPath,
                FileNameList = fileNameList,
                Success = true
            });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                Message = ex.Message,
                Success = false
            });
        }

    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="files">文件</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public JsonResult Download(int? id)
    {
        return Json(new
        {
            Message = "Todo",
            Success = true
        });
    }
}