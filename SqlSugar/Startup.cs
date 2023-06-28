using Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;

namespace SqlSugar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = Configuration.GetConnectionString("SqlServerConnection");
            services.AddSingleton<DbContext>(new DbContext(configuration.ToString()));

            AddSwaggerGenServic(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SqlSugar", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }

            app.UseStaticFiles();
            app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseAuthorization();
            //����Swagger UI���ն˽ڵ㣨Endpoint���Ͷ�Ӧ��Swagger JSON�ļ���
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Home/swagger.json", "SqlSugar��������");
                c.SwaggerEndpoint("/swagger/SQL/swagger.json", "SqlSugar�Է���������");
                c.SwaggerEndpoint("/swagger/File/swagger.json", "�ļ��ٿ�");
                c.SwaggerEndpoint("/swagger/Base/swagger.json", "����ʹ�÷�");
                c.SwaggerEndpoint("/swagger/Package/swagger.json", "��װʹ�÷�");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// ����Swagger��������SwaggerGen�������ѡ����ĵ���Ϣ
        /// </summary>
        /// <param name="services"></param>
        public void AddSwaggerGenServic(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("Home", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "SqlSugar��������"
                });
                options.SwaggerDoc("SQL", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "SqlSugar��������"
                });
                options.SwaggerDoc("File", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "�ļ��ٿ�"
                });
                options.SwaggerDoc("Base", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "����ʹ�÷�"
                });
                options.SwaggerDoc("Package", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "��װʹ�÷�"
                });

                //���շ���ȡapi�ĵ�
                options.DocInclusionPredicate((docName, apiDes) =>
                {
                    if (!apiDes.TryGetMethodInfo(out MethodInfo method))
                        return false;
                    var version = method.DeclaringType.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(m => m.GroupName);
                    if (docName == "v1" && !version.Any())
                        return true;
                    var actionVersion = method.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(m => m.GroupName);
                    if (actionVersion.Any())
                        return actionVersion.Any(v => v == docName);
                    return version.Any(v => v == docName);
                });

                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);

                //��ȡӦ�ó�������Ŀ¼������·����
                var xmlPath = Path.Combine(basePath, "SqlSugarInter.xml");

                //�����Ҫ��ʾ������ע��ֻ�轫�ڶ�����������Ϊtrue
                options.IncludeXmlComments(xmlPath, true);
            });
        }
    }
}