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
            //配置Swagger UI的终端节点（Endpoint）和对应的Swagger JSON文件。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Home/swagger.json", "SqlSugar基础法则");
                c.SwaggerEndpoint("/swagger/SQL/swagger.json", "SqlSugar对服务器操作");
                c.SwaggerEndpoint("/swagger/File/swagger.json", "文件操控");
                c.SwaggerEndpoint("/swagger/Base/swagger.json", "基础使用法");
                c.SwaggerEndpoint("/swagger/Package/swagger.json", "封装使用法");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// 配置Swagger生成器（SwaggerGen）的相关选项和文档信息
        /// </summary>
        /// <param name="services"></param>
        public void AddSwaggerGenServic(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("Home", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "SqlSugar基础法则"
                });
                options.SwaggerDoc("SQL", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "SqlSugar基础法则"
                });
                options.SwaggerDoc("File", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "文件操控"
                });
                options.SwaggerDoc("Base", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "基础使用法"
                });
                options.SwaggerDoc("Package", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "封装使用法"
                });

                //按照分组取api文档
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

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);

                //获取应用程序所在目录（绝对路径）
                var xmlPath = Path.Combine(basePath, "SqlSugarInter.xml");

                //如果需要显示控制器注释只需将第二个参数设置为true
                options.IncludeXmlComments(xmlPath, true);
            });
        }
    }
}