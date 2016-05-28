using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Sales
{
    public class SalesController : ApiController
    {


        // GET api/sales
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public ResponseModel GetBase(string method)
        {
            //获得销售基础信息
            var responseModel = new ResponseModel();
            responseModel.Ver = "1.0";

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            switch (method)
            {
                case "getSaleInfo":
                    #region 获取汇总的销售信息
                    var objSaleInfo = fnRequestProxy.getSaleInfo(method, oToken);
                    if (objSaleInfo.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,ApiModel.SalesView>>(objSaleInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                default:
                    break;
            }

            return responseModel;
        }

        //if VER==Latest xxxxxx
        // GET api/sales
        public ResponseModel Get(string type, int page, int year,int month,int? userid = null, string datetype = "on", int? datediff = null, DateTime? datebg = null, DateTime? dateed = null, string keyword = "", int? gid = null, string saletype = "all", int? paytype = null, int? maxid = null, int? minid = null)
        {
            //获取销售记录
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var salesQuery = new ApiModel.SaleQueryOrderStrEx();
            salesQuery.iPage = page;
            salesQuery.userID = userid;
            salesQuery.dateType = datetype;
            salesQuery.dateDiff = datediff;
            salesQuery.dateBg = datebg;
            salesQuery.dateEd = dateed;
            salesQuery.keyword = keyword;
            salesQuery.goodsID = gid;
            salesQuery.saleType = saletype;
            salesQuery.payType = paytype;
            salesQuery.maxID = maxid;
            salesQuery.minID = minid;
            salesQuery.Year = year;
            salesQuery.Month = month;

            var salesQueryObj = Helper.JsonSerializeObject(salesQuery);

            var responseInfo = fnRequestProxy.SalesListEx(salesQueryObj, oToken);
            if (responseInfo.Status == 0)
            {
                if (responseInfo.ErrDesc != "none")
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SalesSummaryForApp>(responseInfo.StrObj));
                }
                else
                {
                    responseModel = ReturnModel.SuccessNone();
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        //else xxxxxx
        // GET api/sales
        public ResponseModel Get(int page, int? userid = null, string datetype = "on", int? datediff = null, DateTime? datebg = null, DateTime? dateed = null, string keyword = "", int? gid = null, string saletype = "all", int? paytype = null, int? maxid = null, int? minid = null)
        {
            //获取销售记录
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var salesQuery = new ApiModel.SaleQueryOrderStr();
            salesQuery.iPage = page;
            salesQuery.userID = userid;
            salesQuery.dateType = datetype;
            salesQuery.dateDiff = datediff;
            salesQuery.dateBg = datebg;
            salesQuery.dateEd = dateed;
            salesQuery.keyword = keyword;
            salesQuery.goodsID = gid;
            salesQuery.saleType = saletype;
            salesQuery.payType = paytype;
            salesQuery.maxID = maxid;
            salesQuery.minID = minid;

            var salesQueryObj = Helper.JsonSerializeObject(salesQuery);

            var responseInfo = fnRequestProxy.SalesList(salesQueryObj, oToken);
            if (responseInfo.Status == 0)
            {
                if (responseInfo.ErrDesc != "none")
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SalesSummary>(responseInfo.StrObj));
                }
                else
                {
                    responseModel = ReturnModel.SuccessNone();
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }


        // GET api/sales/5
        public ResponseModel Get(int id)
        {
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();


            //获取销售详细列表
            var responseInfo = fnRequestProxy.SalesDetail(id.ToString(), oToken);
            if (responseInfo.Status == 0)
            {
                if (responseInfo.ErrDesc != "none")
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<List<ApiModel.SalesDetail>>(responseInfo.StrObj));
                }
                else
                {
                    responseModel = ReturnModel.SuccessNone();
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // GET api/sales/5
        public ResponseModel Get(string method,int id)
        {
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();


            if (method=="returnpaylog")
            {
                //获取销售详细列表
                var responseInfo = fnRequestProxy.GetReturnPayLog(id.ToString(), oToken);
                if (responseInfo.Status == 0)
                {
                    if (responseInfo.StrObj != "none")
                    {
                        responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.ReturnLogList>(responseInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.SuccessNone();
                    }
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
            }
            else if (method== "getuserinfoforandroidsale")
            {
                //获取销售详细列表
                var responseInfo = fnRequestProxy.GetUserInfoForAndroidSale(id.ToString(), oToken);
                if (responseInfo.Status == 0)
                {
                    if (responseInfo.StrObj != "none" && responseInfo.StrObj != "")
                    {
                        responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.UserInfoForAndroidSale>(responseInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.SuccessNone();
                    }
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
            }
            else if (method == "getpaytypesort")
            {
                //获取销售排序详细列表
                var responseInfo = fnRequestProxy.GetPayTypeSort(id.ToString(), oToken);
                if (responseInfo.Status == 0)
                {
                    if (responseInfo.StrObj != "none" && responseInfo.StrObj != "")
                    {
                        responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.T_Sale_PayTypeSort>(responseInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.SuccessNone();
                    }
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
            }
            else
            {
                //获取销售详细列表
                var responseInfo = fnRequestProxy.SalesDetailEx(id.ToString(), oToken);
                if (responseInfo.Status == 0)
                {
                    if (responseInfo.ErrDesc != "none")
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SaleDetailWithStat>(responseInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.SuccessNone();
                    }
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
            }
            
            return responseModel;
        }

        public ResponseModel Get(string diff, string type, int maxid = 0, int top = 10,string startTime="",string endTime="")
        {
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (diff == null)
            {
                diff = "";
            }

            var queryStr = new ApiModel.AnalyseQueryStr();
            queryStr.diff = diff;
            queryStr.type = type;
            queryStr.maxid = maxid;
            queryStr.top = top;

            if ( diff == "" || diff == "other")
            {
                DateTime nowTime = DateTime.Now.Date;
                if (DateTime.TryParse(startTime, out nowTime))
                {
                    queryStr.startTime = nowTime;
                }
                else
                {
                    queryStr.startTime = DateTime.Now.Date;
                }
                if (DateTime.TryParse(endTime, out nowTime))
                {
                    queryStr.endTime = nowTime;
                }
                else
                {
                    queryStr.endTime = DateTime.Now.Date;
                }
            }

            var queryJson = Helper.JsonSerializeObject(queryStr);

            switch (type)
            {
                case "sale":
                    //获取销售分析数据
                    var saleInfo = fnRequestProxy.SalesGraphData(queryJson, oToken);
                    if (saleInfo.Status == 0)
                    {
                        if (saleInfo.ErrDesc != "none")
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SalesViews>(saleInfo.StrObj));
                        }
                        else
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                case "saleGoodsType":
                    //获取销售类型分析
                    var saleGoodsTypeInfo = fnRequestProxy.SalesTypeData(queryJson, oToken);
                    if (saleGoodsTypeInfo.Status == 0)
                    {
                        if (saleGoodsTypeInfo.ErrDesc != "none")
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SalesClass>(saleGoodsTypeInfo.StrObj));
                        }
                        else
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                case "goodsAnalyze":
                    //获取商品销售分析
                    var goodsAnalyzeInfo = fnRequestProxy.GoodsAnalyze(queryJson, oToken);
                    if (goodsAnalyzeInfo.Status == 0)
                    {
                        if (goodsAnalyzeInfo.ErrDesc != "none")
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.GoodsView>(goodsAnalyzeInfo.StrObj));
                        }
                        else
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                case "memberview":
                    //销售概要分析
                    var oMemberView = fnRequestProxy.GetMemberView(queryJson, oToken);
                    if (oMemberView.Status == 0)
                    {
                        if (oMemberView.ErrDesc != "none")
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.MemberView>(oMemberView.StrObj));
                        }
                        else
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
            }

            return responseModel;
        }

       public ResponseModel Get(string method, string orderNo,string tradeNo)
        {
            //支付宝收款状态查询

            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();


            //获取销售详细列表
            var model = new ApiModel.QueryRequest();
            model.AccId = oToken.AccId;
            model.OrderNo = orderNo;
            model.TradeNo = tradeNo;
            var strObj = Helper.JsonSerializeObject(model);

            var responseInfo = fnRequestProxy.AliapyQuery(strObj, oToken);
            if (responseInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.AlipayAsyncStatus>(responseInfo.StrObj));
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

       public ResponseModel Get(int uid, string salePwd, int iBranch)
       {
           //销售记账
           var responseModel = new ResponseModel();

           var fnRequestProxy = new RequestProxy();
           var oToken = Request.GetAccId();

           if (string.IsNullOrEmpty(salePwd))
           {
               //缺少必须参数
               responseModel = ReturnModel.Error(-3, "缺少必须参数");
               return responseModel;
           }

           ApiModel.UserPwdCheck usrCheck = new ApiModel.UserPwdCheck();
           usrCheck.Uid = uid;
           usrCheck.Pwd = salePwd;
           usrCheck.iBranch = iBranch;

           var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(usrCheck);
           var responseInfo = fnRequestProxy.CheckSalePwd(requestJson, oToken);

           if (responseInfo.Status == 0)
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

        // POST api/sales
        public ResponseModel Post([FromBody]ApiModel.T_Sales_Model model)
        {
            #region 销售记账
            //销售记账
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (string.IsNullOrEmpty(model.gRealMoney) || string.IsNullOrEmpty(model.gPayType))
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            var responseInfo = fnRequestProxy.SalesAdd(requestJson, oToken);

            if (responseInfo.Status == 0)
            {
                var oResult = Helper.JsonDeserializeObject<ApiModel.SaleResult>(responseInfo.StrObj);
                if (oResult.okVal == 1)
                {
                    responseModel = ReturnModel.Success(oResult);
                }
                else if (oResult.okVal == 4)
                {
                    //会员储值余额不足
                    responseModel = ReturnModel.Error(-4, "会员储值余额不足");
                }
                else if (oResult.okVal == 5)
                {
                    //会员剩余次数不足
                    responseModel = ReturnModel.Error(-5, "会员剩余次数不足");
                }
                else if (oResult.okVal == 10)
                {
                    //会员消费密码错误
                    responseModel = ReturnModel.Error(-10, "会员消费密码错误");
                }
                else
                {
                    //未知错误
                    responseModel = ReturnModel.Error(-99, "未知错误");
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
            #endregion
        }

        public ResponseModel Post(string behave, [FromBody]ApiModel.DeleteSaleList model)
        {
            #region 删除销售记录
            var responseModel = new ResponseModel();
            
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            var responseInfo = new ProxyResponseModel();

            if (model.Ver == 2)
            {
                if (model.saleListId <1 || behave != "delete")
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }
                var requestJson = Helper.JsonSerializeObject(model);
                responseInfo = fnRequestProxy.SalesDel(requestJson, oToken);
            }
            else
            {

                if (string.IsNullOrWhiteSpace(model.val) || behave != "delete")
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }
                responseInfo = fnRequestProxy.SalesDelOld(model.val, oToken);
            }

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
            #endregion
        }

        public ResponseModel Post(string returnBehave, [FromBody]ApiModel.DeleteSaleLists model)
        {
            #region 批量退货

            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            var responseInfo = new ProxyResponseModel();
            if (model.DeleteSales.Count <= 0)
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }
            foreach (var item in model.DeleteSales)
            {
                if (item.Ver == 2)
                {
                    if (item.saleListId < 1 || returnBehave != "delete")
                    {
                        //缺少必须参数
                        responseModel = ReturnModel.Error(-3, "缺少必须参数");
                        return responseModel;
                    }
                    var requestJson = Helper.JsonSerializeObject(item);
                    responseInfo = fnRequestProxy.SalesDel(requestJson, oToken);
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
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(item.val) || returnBehave != "delete")
                    {
                        //缺少必须参数
                        responseModel = ReturnModel.Error(-3, "缺少必须参数");
                        return responseModel;
                    }
                    responseInfo = fnRequestProxy.SalesDelOld(item.val, oToken);
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
                }
            }
            return responseModel;
            #endregion
        }


        public ResponseModel Post(string id, ApiModel.NoGoodsSaleQueryModel model)
        {
            var responseModel = new ResponseModel();
            if (id == "nogoods")
            {
                #region 无商品 销售记账
                //销售记账

                var fnRequestProxy = new RequestProxy();
                var oToken = Request.GetAccId();

                if (string.IsNullOrEmpty(model.gRealMoney) || string.IsNullOrEmpty(model.gPayType))
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }
                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.NoGoodsSalesAdd(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    var oResult = Helper.JsonDeserializeObject<ApiModel.SaleResult>(responseInfo.StrObj);
                    if (oResult.okVal == 1)
                    {
                        responseModel = ReturnModel.Success(oResult);
                    }
                    else if (oResult.okVal == 4)
                    {
                        //会员储值余额不足
                        responseModel = ReturnModel.Error(-4, "会员储值余额不足");
                    }
                    else if (oResult.okVal == 5)
                    {
                        //会员剩余次数不足
                        responseModel = ReturnModel.Error(-5, "会员剩余次数不足");
                    }
                    else if (oResult.okVal == 10)
                    {
                        //会员消费密码错误
                        responseModel = ReturnModel.Error(-10, "会员消费密码错误");
                    }
                    else
                    {
                        //未知错误
                        responseModel = ReturnModel.Error(-99, "未知错误");
                    }
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }

                return responseModel;
                #endregion
            }
            else
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }
        }

        public ResponseModel Post(string alipay, ApiModel.AlipayRequestLite model)
        {
            var responseModel = new ResponseModel();
            if (alipay == "create")
            {
                #region 创建支付宝交易

                var fnRequestProxy = new RequestProxy();
                var oToken = Request.GetAccId();

                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.AliapyCreate(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    var oResult = Helper.JsonDeserializeObject<ApiModel.AlipayStatus>(responseInfo.StrObj);
                    responseModel = ReturnModel.Success(oResult);
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }

                return responseModel;
                #endregion
            }
            else
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }
        }

        public ResponseModel Post(string returnpay, ApiModel.ReturnPayModel model)
        {
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            //获取销售详细列表
            var strObj = Helper.JsonSerializeObject(model);

            var responseInfo = fnRequestProxy.AddReturnInfo(strObj, oToken);
            if (responseInfo.Status == 0)
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

        public ResponseModel Post(string sValue)
        {
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            var responseInfo = fnRequestProxy.AddSalePayTypeSort(sValue, oToken);
            if (responseInfo.Status == 0)
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

        // PUT api/sales/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/sales/5
        public void Delete(int id)
        {
        }
    }
}
