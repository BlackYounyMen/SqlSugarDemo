using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Model;

namespace SqlSugarInter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "File")]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// 格式化文件大小
        /// </summary>
        /// <param name="filesize">文件传入大小</param>
        /// <returns></returns>
        private static string GetFileSize(long filesize)
        {
            try
            {
                if (filesize < 0)
                {
                    return "0";
                }
                else if (filesize >= 1024 * 1024 * 1024)  //文件大小大于或等于1024MB
                {
                    return string.Format("{0:0.00} GB", (double)filesize / (1024 * 1024 * 1024));
                }
                else if (filesize >= 1024 * 1024) //文件大小大于或等于1024KB
                {
                    return string.Format("{0:0.00} MB", (double)filesize / (1024 * 1024));
                }
                else if (filesize >= 1024) //文件大小大于等于1024bytes
                {
                    return string.Format("{0:0.00} KB", (double)filesize / 1024);
                }
                else
                {
                    return string.Format("{0:0.00} bytes", filesize);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="jpg"></param>
        /// <returns></returns>
        [HttpPost("FileLoad")]
        public FileBackItem FileLoad(IFormFile jpg)
        {
            var postfile = HttpContext.Request.Form.Files[0];
            var saveUrl = Directory.GetCurrentDirectory() + @"\wwwroot\File\" + postfile.FileName;
            using (FileStream fs = new FileStream(saveUrl, FileMode.Create))
            {
                postfile.CopyTo(fs);
                fs.Flush();
            }
            FileBackItem d = new FileBackItem();
            d.FileName = postfile.FileName.Substring(0, postfile.FileName.IndexOf('.'));
            d.UploadTime = DateTime.Now;
            d.FileSize = GetFileSize(postfile.Length);
            d.FileType = postfile.FileName.Substring(postfile.FileName.IndexOf('.') + 1);

            // 获取当前请求的主机名和端口号
            var request = HttpContext.Request;
            var host = request.Host.Host;
            var port = request.Host.Port;

            // 构建URL时使用当前的主机名和端口号
            d.Url = $"http://{host}:{port}/File/{postfile.FileName}";

            //d.Url = "https://localhost:5001/File/" + postfile.FileName; http://localhost:26360/swagger/index.html?urls.primaryName=%E6%96%87%E4%BB%B6%E6%93%8D%E6%8E%A7

            return d;
        }
    }
}