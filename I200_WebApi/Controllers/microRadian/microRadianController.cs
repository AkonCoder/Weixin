using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonLib;
using Controls;
using Models;

namespace I200_WebApi.Controllers.microRadian
{
    public class microRadianController : ApiController
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
        public ResponseModel GetBase(string method, string id = "")
        {
            //加料信息
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            switch (method)
            {
                case "getsingleauthorizer":

                    #region 获取是否绑定

                    var responseitemInfo = fnRequestProxy.GetSingleAuthorizerByAccId(id, oToken);
                    responseModel = responseitemInfo.Status == 0 ? ReturnModel.Success(responseitemInfo.StrObj) : ReturnModel.TokenFail();
                    break;

                    #endregion
            }
            return responseModel;
        }
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}