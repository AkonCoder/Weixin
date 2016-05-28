using I200_WebApi.Fiters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace I200_WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // 移除XML格式输出
            var json = config.Formatters.JsonFormatter;
            // 解决json序列化时的循环引用问题
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 去除XML序列化器
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            // 禁用XML序列化器
            config.Formatters.XmlFormatter.UseXmlSerializer = false;


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //增加过滤器
            config.Filters.Add(new WebResponFilter());
            config.Filters.Add(new WebApiAuthAttribute());

            //日期格式
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new MyDateTimeConvertor());
        }

    }

    /// <summary>
    /// 自定义时间格式
    /// </summary>
    public class MyDateTimeConvertor : IsoDateTimeConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
