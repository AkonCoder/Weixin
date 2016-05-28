using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.SalesCart
{
    public class SaleCartController : ApiController
    {

        public ResponseModel Get()
        {
            #region 得到列表
            var responseModel = new ResponseModel();
            responseModel.Ver = "1.0";
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            var objSaleInfo = fnRequestProxy.GetSaleCartList("", oToken);
            if (objSaleInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.SaleCartList>(objSaleInfo.StrObj));
            }
            else
            {
                responseModel = ReturnModel.Error(1, objSaleInfo.ErrDesc);
            }
            return responseModel;

            #endregion
        }

        // GET api/salecart/5
        public ResponseModel Get(int id)
        {
            #region  获得挂单信息
            var responseModel = new ResponseModel();
            responseModel.Ver = "1.0";
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();


            var objSaleInfo = fnRequestProxy.GetCartInfo(id.ToString(), oToken);
            if (objSaleInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.T_Sale_Cart>(objSaleInfo.StrObj));
            }
            else
            {
                responseModel = ReturnModel.Error(1, objSaleInfo.ErrDesc);
            }
            return responseModel;
            #endregion
        }

        public ResponseModel Get(string method, int id)
        {
            #region  删除挂单信息
            var responseModel = new ResponseModel();
            responseModel.Ver = "1.0";
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (method=="del")
            {
                var objSaleInfo = fnRequestProxy.DeleteSaleCart(id.ToString(), oToken);
                if (objSaleInfo.Status == 0)
                {
                    responseModel = ReturnModel.Success(objSaleInfo.StrObj);
                }
                else
                {
                    responseModel = ReturnModel.Error(1, objSaleInfo.ErrDesc);
                }    
            }
            
            return responseModel;
            #endregion
        }

        // POST api/salecart
        public ResponseModel Post([FromBody]ApiModel.T_Sale_Cart value)
        {
            #region  保存挂单功能
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            var responseInfo = fnRequestProxy.SaveSaleCart(requestJson, oToken);

            if (responseInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(responseInfo.StrObj));
            }
            else
            {
                responseModel = ReturnModel.Error(1, responseInfo.ErrDesc);
                responseModel.Data = responseInfo.StrObj;
            }
            return responseModel;
            #endregion
        }

    }
}
