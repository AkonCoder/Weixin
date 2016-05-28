using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Reset
{
    public class ResetController : ApiController
    {
        // GET api/Reset
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/Reset/5
        public string Get(int id)
        {
            return "";
        }

        // GET api/Reset/5
        public ResponseModel GetBase(string method, string phone, string regcode = "")
        {
            //获得会员基础信息
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            //var code = regcode;

            switch (method)
            {
                case "getVerifyCode":
                    #region 获取找回密码的验证码
                    var objStatus = fnRequestProxy.getVerCode(phone, oToken);
                    if (objStatus.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objStatus.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "verification":
                    #region 验证找回密码验证码
                    var verification = new ApiModel.VerifyCode();

                    verification.PhoneNum = phone;
                    verification.Code = regcode;
                    var strJson = Helper.JsonSerializeObject(verification);

                    var objCheck = fnRequestProxy.verifyCode(strJson, oToken)   ;
                    if (objCheck.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objCheck.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
            }
            return responseModel;
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel Post([FromBody]ApiModel.VerifyCode model)
        {
            var responseModel = new ResponseModel();
            var oToken = Request.GetAccId();
            var fnRequestProxy = new RequestProxy();

            if (string.IsNullOrEmpty(model.PhoneNum) || string.IsNullOrEmpty(model.Code) || string.IsNullOrEmpty(model.Pwd))
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var requestJson = Helper.JsonSerializeObject(model);
            var objPwd = fnRequestProxy.resetPwd(requestJson, oToken);

            if (objPwd.Status == 0)
            {
                responseModel = ReturnModel.Success(objPwd.StrObj);
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // PUT api/reset/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/reset/5
        public void Delete(int id)
        {
        }
    }
}
