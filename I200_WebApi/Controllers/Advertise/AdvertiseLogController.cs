using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Advertise
{
    public class AdvertiseLogController : ApiController
    { 
        // POST api/advertiselog
        public ResponseModel Post([FromBody]T_Mobile_Advertise submitModel)
        {
            ResponseModel responseModel = new ResponseModel();
            ProxyResponseModel requestObj = new ProxyResponseModel();
            RequestProxy fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            if (submitModel.machineCode != "")
            {

                if (submitModel.logType == 0)
                {
                    submitModel.logType = 2;
                }
                submitModel.logIp = Helper.GetClientIP();


                requestObj = fnRequestProxy.SetMobileAdvertiseLog(Helper.JsonSerializeObject(submitModel), oToken);
                if (requestObj.Status == 0)
                {
                    responseModel = ReturnModel.Success(requestObj.StrObj);
                }
                else
                {
                    responseModel = ReturnModel.Error(-1, "日志记录失败");
                }
            }
            else
            {
                responseModel = ReturnModel.Error(-1, "日志记录失败");
            }
            return responseModel;
        }
         
    }
}
