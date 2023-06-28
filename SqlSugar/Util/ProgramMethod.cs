using System.Net.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace SqlSugarInter
{
    public class ProgramMethod
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task ExecuteApiAsync()
        {
            try
            {
                // 构造请求的URL
                string apiUrl = "http://localhost:5000/api/Values/GetString";

                // 发送GET请求并等待响应
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                // 读取响应内容
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"API调用成功，响应内容：{responseBody}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API调用出错：{ex.Message}");
            }
        }
    }
}