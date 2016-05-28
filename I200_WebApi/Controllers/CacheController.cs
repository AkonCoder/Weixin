using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonLib;
using Controls;
using I200_WebApi.Controllers;
using Models;

namespace I200_WebApi
{
    public class CacheController : ApiController
    {
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        public ResponseModel GetCacheList(int id)
        {
            ApiModel.CacheList modelList = new ApiModel.CacheList();
            ApiModel.CacheListBase model1 = new ApiModel.CacheListBase
            {
                Cachenamespace = "helpCenter",
                Key = "style.css",
                Hash = "52582FEE9A93F2120B0D64B8CA81968A",
                Large = false
            };
            ApiModel.CacheListBase model2 = new ApiModel.CacheListBase
            {
                Cachenamespace = "global",
                Key = "angular.js",
                Hash = "52582FEE9A93F2120B0D64B8CA81968A",
                Large = true
            };
            modelList.cachelist.Add(model1);
            modelList.cachelist.Add(model2);
            var responseModel = ReturnModel.Success(modelList);
            return responseModel;
        }

        public ResponseModel GetCacheModelList(string cachenamespace, string key)
        {
            //ApiModel.CacheModelList modelList = new ApiModel.CacheModelList();
            ApiModel.CacheModelBase model1 = new ApiModel.CacheModelBase
            {
                Cachenamespace = "helpCenter",
                Key = "angular.js",
                Hash = "52582FEE9A93F2120B0D64B8CA81968A",
                Type = "URL",
                Value = "http://i200.cn/public/cache/help.html"
            };
            //ApiModel.CacheModelBase model2 = new ApiModel.CacheModelBase
            //{
            //    Cachenamespace = "helpCenter",
            //    Key = "angular.js",
            //    Hash = "52582FEE9A93F2120B0D64B8CA81968A",
            //    Type = "URL",
            //    Value = "http://i200.cn/public/cache/help.html"
            //};
            //modelList.cachemodellist.Add(model1);
            //modelList.cachemodellist.Add(model2);
            var responseModel = ReturnModel.Success(model1);
            return responseModel;
        }

       

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}