using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.App
{
    public class MobileWebController : ApiController
    {
        // GET api/mobileweb
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/mobileweb
        public ResponseModel Get()
        {
            //获得手机橱窗Url地址
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var objUrl = fnRequestProxy.GetMobileWebUrl(oToken);
            if (objUrl.Status == 0)
            {
                responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.MobileWebSite>(objUrl.StrObj));
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // GET api/mobileweb/5
        public string Get(int id)
        {
            return "";
        }

        // POST api/mobileweb
        public void Post([FromBody]string value)
        {
        }

        // PUT api/mobileweb/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/mobileweb/5
        public void Delete(int id)
        {
        }
    }
}
