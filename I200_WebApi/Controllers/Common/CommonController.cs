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

namespace I200_WebApi.Controllers.Common
{
    public class CommonController : ApiController
    {
        // GET api/common
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/common/5
        //public string Get(int id)
        //{
        //    return "value";
        //}
        public ResponseModel Get(string type)
        {
            var responseModel = new ResponseModel();
            if (type == "getactivity")
            {
                responseModel = ReturnModel.Success("0");
            }
            return responseModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">sign</param>
        /// <returns></returns>
        public ResponseModel Get(int id, string type)
        {
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            //每日签到
            var responseInfo = fnRequestProxy.getSigned(id.ToString(), oToken);
            if (responseInfo.Status >= 0)
            {
                if (responseInfo.ErrDesc != "" && type == "sign")
                {
                    responseModel = new ResponseModel
                    {
                        Status = 0,
                        Ver = "1.0",
                        ErrCode = 0,
                        ErrMsg = responseInfo.ErrDesc,
                        Data = Helper.JsonDeserializeObject<ApiModel.SignInModel>(responseInfo.StrObj)
                    };
                }
                else
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SignInModel>(responseInfo.StrObj));
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }
        //Android (1.75) iOS (1.8.2) 之后舍弃
        public ResponseModel Get(int id)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            //每日签到
            var responseInfo = fnRequestProxy.getSigned(id.ToString(), oToken);
            if (responseInfo.Status >= 0)
            {
                if (responseInfo.ErrDesc != "")
                {
                    responseModel = new ResponseModel
                    {
                        Status = 1,
                        Ver = "1.0",
                        ErrCode = 0,
                        ErrMsg = responseInfo.ErrDesc,
                        Data = Helper.JsonDeserializeObject<ApiModel.SignInModel>(responseInfo.StrObj)
                    };
                }
                else
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SignInModel>(responseInfo.StrObj));
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        public ResponseModel Get(string method = "", string code = "",string gName="")
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            switch (method)
            {
                case "ads":
                    var responseInfo = fnRequestProxy.getAds(method, oToken);
                    if (responseInfo.Status == 0)
                    {
                        responseModel = ReturnModel.Success(JsonConvert.DeserializeObject<ApiModel.Sys_AdModel>(responseInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                case "barcode":
                    //条码查询接口
                    var resBarcode = fnRequestProxy.SearchBarcode(code, oToken);
                    if (resBarcode.Status == 0)
                    {
                        responseModel = ReturnModel.Success(JsonConvert.DeserializeObject<ApiModel.Barcode>(resBarcode.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                case "mobilemap":
                    var resMap = fnRequestProxy.GetMapData(gName, oToken);
                    if (resMap.Status == 0)
                    {
                        responseModel = ReturnModel.Success(JsonConvert.DeserializeObject<List<ApiModel.MobileMapModel>>(resMap.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                default:
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    break;
            }


            return responseModel;
        }

        // POST api/common
        public void Post([FromBody]string value)
        {
        }

        // PUT api/common/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/common/5
        public void Delete(int id)
        {
        }
    }
}
