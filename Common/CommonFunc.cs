using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Serilog.Sinks.File;
using Serilog.Formatting.Json;

namespace WebT.Common
{
    public class CommonFunc
    {
       public void LogJson(string type,string jsonString)
        {
            ConfigureLogger();
            // 获取当前时间
            //string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // 解析 JSON 字符串为动态对象
            //dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

            // 使用 Serilog 记录时间和 JSON 对象
            Log.Information("{@type} : {@jsonString}",type, jsonString);
        }
        static void ConfigureLogger()
        {

            // 配置 Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(
                    path: "C:/weblogs/.log",
                    rollingInterval: RollingInterval.Day, // 按天滚动日志
                    retainedFileCountLimit: null, // 不限制保留文件数量
                    rollOnFileSizeLimit: false, // 禁用基于文件大小的滚动
                    shared: true
                )
                .CreateLogger();
        }

    }
}