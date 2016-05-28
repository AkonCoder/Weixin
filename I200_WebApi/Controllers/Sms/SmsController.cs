using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonLib;
using Controls;
using Models;

namespace I200_WebApi.Controllers.Sms
{
    public class SmsController : ApiController
    {
        // GET api/sms
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/sms/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/sms
        public ResponseModel Post(ApiModel.SmsSendModel smsModel)
        {
            #region 群发短信
            //销售记账
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (smsModel.userList.Length == 0)
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            smsModel.regularTime=Convert.ToDateTime("1900-01-01 00:00:00");

            var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(smsModel);
            var responseInfo = fnRequestProxy.SendSms(requestJson, oToken);

            if (responseInfo.Status != -1)
            {
                responseModel = new ResponseModel
                {
                    Status = responseInfo.Status,
                    Ver = "1.0",
                    ErrCode = 0,
                    ErrMsg = responseInfo.ErrDesc,
                    Data = ""
                };
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
            #endregion
        }

        // PUT api/sms/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/sms/5
        public void Delete(int id)
        {
        }
    }
}
