using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Reg
{
    public class RegController : ApiController
    {
        // GET api/reg
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/reg/5
        public string Get(int id)
        {
            return "";
        }

        // GET api/reg/5
        public ResponseModel GetBase(string method, string phone,string regcode="")
        {
            //获得会员基础信息
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            //var code = regcode;

            switch (method)
            {
                case "check":
                    #region 会员手机号码重复检测
                    var objPhoneNum = fnRequestProxy.AccountCheck(phone, oToken);
                    if (objPhoneNum.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objPhoneNum.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "resend":
                    #region 重发注册验证码
                    var objResend = fnRequestProxy.ReSendRegCode(phone, oToken);
                    if (objResend.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objResend.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                
                case "resendvoicePin":
                    #region 重发语音验证码
                     var objResendVoicePin = fnRequestProxy.ReSendVoicePin(phone, oToken);
                     if (objResendVoicePin.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objResendVoicePin.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                #endregion
                case "regverify":
                    #region 店铺注册激活(手机注册)
                    var accountModel = new ApiModel.RegAccount();
                    accountModel.PhoneNum = phone;
                    accountModel.RandomNum = regcode;
                    var requestJson = Helper.JsonSerializeObject(accountModel);

                    var objVerify = fnRequestProxy.RegVerify(requestJson, oToken);
                    if (objVerify.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objVerify.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "getregcode":
                    #region 获取注册验证码（验证账号是否存在）
                    var objSendRegCode = fnRequestProxy.SendRegCode(phone, oToken);
                    if (objSendRegCode.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objSendRegCode.StrObj);
                    }
                    else
                    {
                        switch (objSendRegCode.Status)
                        {
                            case 1:
                                responseModel = ReturnModel.TokenFail();
                                break;
                            case -1:                   
                            case -2:
                                 responseModel = new ResponseModel
                                {
                                    Status = -1,
                                    Ver = "1.0",
                                    ErrCode = objSendRegCode.Status,
                                    ErrMsg = objSendRegCode.ErrDesc,
                                    Data = ""
                                };
                                break;
                        }
                        
                    }
                    #endregion
                    break;
                case "verifycode":
                    #region 验证注册验证码

                    if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(regcode))
                    {
                        responseModel = ReturnModel.Error(-3, "缺少必须参数");
                        return responseModel;
                    }

                    ApiModel.VerifyCode code = new ApiModel.VerifyCode();
                    code.PhoneNum = phone;
                    code.Code = regcode;
                    var str = Helper.JsonSerializeObject(code);

                    var objVerifyRegCode = fnRequestProxy.VerifyRegCode(str, oToken);
                    if (objVerifyRegCode.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objVerifyRegCode.StrObj);
                    }
                    else
                    {
                        responseModel = new ResponseModel
                        {
                            Status = -1,
                            Ver = "1.0",
                            ErrCode = objVerifyRegCode.Status,
                            ErrMsg = objVerifyRegCode.ErrDesc,
                            Data = ""
                        };
                        
                    }
                    #endregion
                    break;
            }
            return responseModel;
        }


        /// <summary>
        /// 店铺注册(手机注册)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel Post([FromBody]ApiModel.RegAccount model)
        {
            //店铺注册(手机注册)
            var responseModel = new ResponseModel();
            var oToken = Request.GetAccId();
            var fnRequestProxy = new RequestProxy();

            if (model.RecommandCode == null)
            {
                model.RecommandCode = "";
            }

            if (string.IsNullOrEmpty(model.PhoneNum) || string.IsNullOrEmpty(model.PassWord) || string.IsNullOrEmpty(model.StoreName))
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var requestJson = Helper.JsonSerializeObject(model);
            var responseInfo = fnRequestProxy.AccountReg(requestJson, oToken);

            if (responseInfo.StrObj == "1")
            {
                responseModel = ReturnModel.Success(responseInfo.StrObj);
            }
            else
            {
                responseModel = ReturnModel.Error(-4, "未知错误");
            }

            return responseModel;
        }

        /// <summary>
        /// 新移动端注册
        /// </summary>
        /// <param name="method"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel Post(string method,[FromBody]ApiModel.RegAccount model)
        {
            //店铺注册(手机注册)
            var responseModel = new ResponseModel();
            var oToken = Request.GetAccId();
            var fnRequestProxy = new RequestProxy();

            //用户Ip
            model.UserIp = Helper.GetClientIP();

            if (model.RecommandCode == null)
            {
                model.RecommandCode = "";
            }

            if (string.IsNullOrEmpty(model.PhoneNum) || string.IsNullOrEmpty(model.PassWord) || string.IsNullOrEmpty(model.StoreName))
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var requestJson = Helper.JsonSerializeObject(model);
            var responseInfo = fnRequestProxy.AccountRegEx(requestJson, oToken);

            if (responseInfo.StrObj == "1")
            {
                responseModel = ReturnModel.Success(responseInfo.StrObj);
            }
            else
            {
                responseModel = ReturnModel.Error(-4, "未知错误");
            }

            return responseModel;
        }

        // PUT api/reg/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/reg/5
        public void Delete(int id)
        {
        }
    }
}
