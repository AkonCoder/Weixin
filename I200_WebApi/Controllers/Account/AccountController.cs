using CommonLib;
using Controls;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Account
{
    public class AccountController : ApiController
    {
        // GET api/account
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/account/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        public ResponseModel Get(int id)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var responseInfo = fnRequestProxy.accountInfo(id.ToString(), oToken);
            if (responseInfo.Status == 0)
            {
                if (responseInfo.StrObj == "none")
                {
                    responseModel = ReturnModel.SuccessNone();
                }
                else
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.AccountInfo>(responseInfo.StrObj));
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }
        public ResponseModel Get(string content="")
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var strIp = "";
            try
            {
                strIp = CommonLib.Helper.GetClientIP();
            }
            catch (Exception ex)
            {
                Logger.Error("获取IP地址错误", ex);
            }

            switch (content)
            {
                case "app":
                    var responseInfo = fnRequestProxy.accountAppInfo(content, oToken);
                    if (responseInfo.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.AppHomePower>(responseInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                case "sales":
                    var responseSaleInfo = fnRequestProxy.accountSaleInfo(content, oToken);
                    if (responseSaleInfo.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.SaleNumCompare>(responseSaleInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                case "logintoken":
                    //创建登录Token信息
                    var responseLoginToken = fnRequestProxy.GetLoginToken(strIp, oToken);
                    if (responseLoginToken.Status == 0)
                    {
                        responseModel = ReturnModel.Success(responseLoginToken.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                default:
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
            }
            return responseModel;
        }

        // POST api/account
        public ResponseModel Post(string method, [FromBody]ApiModel.AccountModify model)
        {
            //新增会员
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (method != "modify" || (model.field != "accName" && model.field != "accShortName"
                && model.field != "UserName" && model.field != "UserTel" && model.field != "ShopType" && model.field != "accAdress"))
            {
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var requestJson = Helper.JsonSerializeObject(model);

            var responseInfo = fnRequestProxy.modifyAcc(requestJson, oToken);

            if (responseInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(responseInfo.StrObj);
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }
        /// <summary>
        /// 获取每日店铺运营数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel POST([FromBody]ApiModel.DateTimeRange model)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var responseInfo = fnRequestProxy.GetDailyOperation(JsonConvert.SerializeObject(model), oToken);
            if (responseInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(JsonConvert.DeserializeObject<DailyOperation>(responseInfo.StrObj));
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }
        // PUT api/account/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/account/5
        public void Delete(int id)
        {
        }
    }
}
