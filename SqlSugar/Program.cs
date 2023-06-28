using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SqlSugarInter;

namespace SqlSugar
{
    public class Program
    {
        private static Timer _timer;

        public static void Main(string[] args)
        {
            #region 拥有程序启动方法，启动可每秒轮询像别人发送数据，或者执行程序方法

            //// 创建定时器，每隔5秒执行一次接口  剩下的你应该能看的懂了把  ，已经能拿到我接口传过来的数据，剩下的就是发送个另一个服务器
            //_timer = new Timer(async (_) => await ProgramMethod.ExecuteApiAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            //CreateHostBuilder(args).Build().Run();

            //// 按任意键退出程序
            //Console.ReadKey();

            //// 停止定时器并释放资源
            //_timer.Dispose();
            //_httpClient.Dispose();

            #endregion 拥有程序启动方法，启动可每秒轮询像别人发送数据，或者执行程序方法

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}