using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace I200_WebApi.Controllers.Payment
{
    public class PaymentController : ApiController
    {
        // GET api/payment
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/payment/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        public ResponseModel Get(int page = 1, string method = "")
        {
            //获得优惠券列表
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            switch (method)
            {
                case "couponlist":
                    var responseInfo = fnRequestProxy.getCouponList(page.ToString(), oToken);
                    if (responseInfo.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<List<ApiModel.CouponInfoItem>>(responseInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                default:
                    break;
            }

            return responseModel;
        }

        public ResponseModel Get(string method, int userid,decimal totalMoney)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            ApiModel.UserPayment pay = new ApiModel.UserPayment();
            pay.userId = userid;
            pay.totalMoney = totalMoney;

            var payJson = Newtonsoft.Json.JsonConvert.SerializeObject(pay);

            switch (method)
            {
                case "getusrcoupon":
                    //获取会员可用优惠券
                    var responseUsrCoupon = fnRequestProxy.getUsrCoupon(payJson, oToken);
                    if (responseUsrCoupon.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<List<ApiModel.CouponModel>>(responseUsrCoupon.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                case "getusrtimecard":
                    //获取会员可用计次卡
                    var responseUsrTimeCards = fnRequestProxy.getUsrTimeCards(payJson, oToken);
                    if (responseUsrTimeCards.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<List<ApiModel.StoreCard>>(responseUsrTimeCards.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    break;
                default:
                    break;
            }

            return responseModel;
        }

        // POST api/payment
        public ResponseModel Post(ApiModel.SendCoupon sendModel)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (sendModel.couponId == 0 || sendModel.userId == 0)
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(sendModel);
            var responseInfo = fnRequestProxy.sendCoupon(requestJson, oToken);

            if (responseInfo.Status >= 0)
            {
                responseModel = new ResponseModel
                {
                    Status = 0,
                    Ver = "1.0",
                    ErrCode = responseInfo.Status,
                    ErrMsg = responseInfo.ErrDesc,
                    Data = ""
                };
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // PUT api/payment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/payment/5
        public void Delete(int id)
        {
        }
    }
}
