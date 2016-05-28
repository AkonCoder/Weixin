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
    public class UpdatePasswordController : ApiController
    { 
        // POST api/updatepassword
        public ResponseModel Post(Models.OpenRequest.UpdatePassword updatePassword)
        {
            //修改店铺密码
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var jsonStr = Helper.JsonSerializeObject(updatePassword);
            var responseInfo = fnRequestProxy.UpdatePassword(jsonStr, oToken);

            if (responseInfo.Status == 0)
            {
                if (responseInfo.StrObj == "1")
                {
                    //更新
                    responseModel = ReturnModel.Success(responseInfo.StrObj);
                }
                else if (responseInfo.StrObj == "0")
                {
                    responseModel = ReturnModel.Error(-1, "原密码错误");
                }
                else
                {
                    responseModel = ReturnModel.Error(-2, "密码修改失败");
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }
         
    }
}
