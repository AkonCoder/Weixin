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
    public class BaseController : ApiController
    {
        //// GET api/base
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public ApiModel.UserStoreMoneyInModel Get()
        {
            var model = new ApiModel.UserStoreMoneyInModel();
            return model;
        }


        public ResponseModel GetBase(string method, string keyword = "")
        {
            //获得商品基础信息
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            switch (method)
            {
                case "cashier":
                    #region 收银员列表
                    var responseInfo = fnRequestProxy.GetCashierList(oToken);
                    if (responseInfo.Status == 0)
                    {
                        if (responseInfo.StrObj == "none")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SalesManList>(responseInfo.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "smsbalance":
                    #region 短信余额
                    var smsBalanceInfo = fnRequestProxy.GetSmsBalance(oToken);
                    if (smsBalanceInfo.Status == 0)
                    {
                        responseModel = ReturnModel.Success(smsBalanceInfo.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "appinfo":
                    #region App应用信息
                    var appInfoObj = fnRequestProxy.GetAppInfo(oToken);
                    if (appInfoObj.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.AppHomeModel>(appInfoObj.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "basicconfig":
                    #region 店铺其他信息
                    var basicConfig = fnRequestProxy.GetAccountBasicConfig(oToken);
                    if (basicConfig.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.AccountBasicConfig>(basicConfig.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }

                    #endregion
                    break;
                case "bbslogin":
                    #region 论坛登录信息
                    var oBbsLoginCode = fnRequestProxy.GetBbsLoginCode(oToken);
                    if (oBbsLoginCode.StrObj!= "")
                    {
                        responseModel = ReturnModel.Success(oBbsLoginCode.StrObj);
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

        // GET api/base/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/base
        public void Post([FromBody]string value)
        {
        }

        // PUT api/base/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/base/5
        public void Delete(int id)
        {
        }
    }
}
