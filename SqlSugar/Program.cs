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
            #region ӵ�г�������������������ÿ����ѯ����˷������ݣ�����ִ�г��򷽷�

            //// ������ʱ����ÿ��5��ִ��һ�νӿ�  ʣ�µ���Ӧ���ܿ��Ķ��˰�  ���Ѿ����õ��ҽӿڴ����������ݣ�ʣ�µľ��Ƿ��͸���һ��������
            //_timer = new Timer(async (_) => await ProgramMethod.ExecuteApiAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            //CreateHostBuilder(args).Build().Run();

            //// ��������˳�����
            //Console.ReadKey();

            //// ֹͣ��ʱ�����ͷ���Դ
            //_timer.Dispose();
            //_httpClient.Dispose();

            #endregion ӵ�г�������������������ÿ����ѯ����˷������ݣ�����ִ�г��򷽷�

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