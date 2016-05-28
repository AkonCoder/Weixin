using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;
using Newtonsoft.Json;

namespace I200_WebApi.Controllers.Order
{
    public class OrderController : ApiController
    { 

        // GET api/order/5
        public ResponseModel Get(string keyword, int nowPage=1)
        {
            ResponseModel responseModel = new ResponseModel();
            ProxyResponseModel requestObj = new ProxyResponseModel();
            RequestProxy fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            switch (keyword)
            {
                case "project":
                    #region 得到产品列表
                    requestObj = fnRequestProxy.GetOrderProjectList("", oToken);
                    if (requestObj.Status == -1)
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    else
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<List<OrderInfo.T_OrderMobileProjectBasis>>(requestObj.StrObj));
                    }
                    #endregion
                    break;
				case "getorderlist":
					#region 得到产品列表
					requestObj = fnRequestProxy.Getorderlist(nowPage.ToString(), oToken);
					if (requestObj.Status == -1)
					{
						responseModel = ReturnModel.TokenFail();
					}
					else
					{
						responseModel =ReturnModel.Success(JsonConvert.DeserializeObject(requestObj.StrObj));
					}
					#endregion
					break;
                case "getordercouponlist":
                    #region
                    requestObj = fnRequestProxy.GetOrderCouponList(nowPage.ToString(), oToken);
                    responseModel = requestObj.Status == -1 ? ReturnModel.TokenFail() : ReturnModel.Success(JsonConvert.DeserializeObject<OrderInfo.OrderCouponModel>(requestObj.StrObj));
                    #endregion

                    break;
            }
            return responseModel;
        }
       /// <summary>
       /// 得到价格
       /// </summary>
       /// <param name="mpid"></param>
       /// <param name="quantity"></param>
       /// <param name="couponcode"></param>
       /// <param name="remark"></param>
       /// <param name="commuteIntegral"></param>
       /// <returns></returns>
        public ResponseModel Get(int mpid, int quantity, string couponcode = "", string remark = "", int commuteIntegral = 0)
        {
            ResponseModel responseModel = new ResponseModel();
            ProxyResponseModel requestObj = new ProxyResponseModel();
            RequestProxy fnRequestProxy = new RequestProxy();
            OpenRequest.OrderSubmitModel submitModel = new OpenRequest.OrderSubmitModel();
            submitModel.MobileBusId = mpid;
            submitModel.GoodsQuantity = quantity;
            submitModel.Remark = remark;
            submitModel.CommuteIntegral = commuteIntegral;
            submitModel.CouponCode = couponcode;
            var oToken = Request.GetAccId();
            if (submitModel.MobileBusId > 0)
            {
                requestObj = fnRequestProxy.GetOrderInfoPrice(Helper.JsonSerializeObject(submitModel), oToken);
                if (requestObj.Status == 0)
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<OrderInfo.OrderShowModel>(requestObj.StrObj));
                }
                else if (requestObj.Status == -1)
                {
                    responseModel = ReturnModel.TokenFail();
                }
                else
                {
                    responseModel = ReturnModel.Error(requestObj.Status, requestObj.ErrDesc);
                }
            }
            else
            {
                responseModel = ReturnModel.Error(-1, "请选择产品ID");
            }
            return responseModel;
        }

        // POST api/order
        public ResponseModel Post(OpenRequest.OrderSubmitModel submitModel)
        {
            ResponseModel responseModel = new ResponseModel();
            ProxyResponseModel requestObj = new ProxyResponseModel();
            RequestProxy fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            if (submitModel.MobileBusId > 0)
            {
                submitModel.OperatorIp = Helper.GetClientIP();
                requestObj = fnRequestProxy.AddOrderInfo(Helper.JsonSerializeObject(submitModel), oToken);
                if (requestObj.Status == 0)
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<OrderInfo.OrderCreateStatus>(requestObj.StrObj));
                }
                else if (requestObj.Status == -1)
                {
                    responseModel = ReturnModel.TokenFail();
                }
                else
                {
                    responseModel = ReturnModel.Error(requestObj.Status, requestObj.ErrDesc);
                }
            }
            else
            {
                responseModel = ReturnModel.Error(-1, "请选择产品ID");
            }
            return responseModel;
        }
        public ResponseModel Post(string receipt,[FromBody]OpenRequest.OrderTransactionReceipt submitModel)
        {
            ResponseModel responseModel = new ResponseModel();
            ProxyResponseModel requestObj = new ProxyResponseModel();
            RequestProxy fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            if (submitModel.orderNo != "")
            {
                string AppKey = Request.Headers.GetValues("AppKey").First();
                if (AppKey == "AndroidYnHWyROQosO")
                {
                    requestObj = fnRequestProxy.SetOrderTransactionReceiptByAndroid(Helper.JsonSerializeObject(submitModel), oToken);
                }
                else if (AppKey == "iPadMaO8VUvVH0eBss" || AppKey == "iPhoneHT5I0O4HDN65")
                {
                    requestObj = fnRequestProxy.SetOrderTransactionReceiptByIOS(Helper.JsonSerializeObject(submitModel), oToken);
                }
                else
                {
                    requestObj.Status = -10;
                    requestObj.StrObj = "非信任设备";
                }

                if (requestObj.Status == 0)
                {
                    responseModel = ReturnModel.Success(requestObj.StrObj);
                }
                else if (requestObj.Status == -1)
                {
                    responseModel = ReturnModel.TokenFail();
                }
                else
                {
                    responseModel = ReturnModel.Error(requestObj.Status, requestObj.ErrDesc);
                }
            }
            else
            {
                responseModel = ReturnModel.Error(-1, "请选择产品ID");
            }
            return responseModel;
        }
         
    }
}
