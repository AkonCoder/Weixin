using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Base
{
    public class BaseEditController : ApiController
    {
        // GET api/baseedit
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/baseedit/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/baseedit
        public ResponseModel Post([FromBody]Models.OpenRequest.BaseEdit editModel)
        {
            //会员编辑更新
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var jsonStr = Helper.JsonSerializeObject(editModel);
            var responseInfo = fnRequestProxy.BaseEdit(jsonStr, oToken);

            if (responseInfo.Status == 0)
            {
                if (responseInfo.StrObj == "1")
                {
                    //更新
                    responseModel = ReturnModel.Success(responseInfo.StrObj);
                }
                else
                {
                    responseModel = ReturnModel.Error(-2, "修改失败");
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // PUT api/baseedit/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/baseedit/5
        //public void Delete(int id)
        //{
        //}
    }
}
