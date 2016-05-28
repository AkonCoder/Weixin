using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Versions
{
    public class VersionsController : ApiController
    {
        // GET api/versions
        public ResponseModel Get()
        {
            //获取销售详细列表
            var responseModel = new ResponseModel();

            var fnRequestProxy = new Controls.MongoDBAPI();

            MongoDBAPI.VersionsLog log = fnRequestProxy.GetVersion(GetClientKey());


            if (log != null)
            {
                responseModel = Controls.ReturnModel.Success(log);
            }
            else
            {
                responseModel = Controls.ReturnModel.Error(1, "未查到版本信息");
            }


            return responseModel;
        }

        // GET api/versions/5
        public ResponseModel Get(string ver)
        {

            //获取销售详细列表
            var responseModel = new ResponseModel();

            var fnRequestProxy = new Controls.MongoDBAPI();

            MongoDBAPI.VersionsLog log = fnRequestProxy.GetVersion(GetClientKey());

            if (log != null)
            {
                if (log.Versions == ver)
                {
                    responseModel = Controls.ReturnModel.Success(0);
                }
                else
                {
                    responseModel = Controls.ReturnModel.Success(1);
                }
            }
            else
            {
                responseModel = Controls.ReturnModel.Error(1, "未查到版本信息");
            }


            return responseModel;
        }

        // POST api/versions
        public void Post([FromBody]string value)
        {
        }

        // PUT api/versions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/versions/5
        public void Delete(int id)
        {
        }


        private int GetClientKey()
        {
            string AppKey = Request.Headers.GetValues("AppKey").First();
            Dictionary<string, int> KV = new Dictionary<string, int>(){
            { "iPadMaO8VUvVH0eBss",3 }, 
            { "iPhoneHT5I0O4HDN65",2 },
            { "AndroidYnHWyROQosO",1 }
           };
            int key=0;
            if (!KV.TryGetValue(AppKey, out key))
            {
                key = 0;
            }
            return key;
        }

    }
}
