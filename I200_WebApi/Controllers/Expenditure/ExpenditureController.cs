using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonLib;
using Controls;
using Models;
using Newtonsoft.Json;

namespace I200_WebApi.Controllers.Expenditure
{
    public class ExpenditureController : ApiController
    {
        // GET api/expenditure
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/expenditure/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        /// <summary>
        /// 获取支出分类列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel Get(string id)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var responseInfo = fnRequestProxy.GetPayClass(id.ToString(), oToken);
            if (responseInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<List<ApiModel.PayClass>>(responseInfo.StrObj));
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        public ResponseModel Get(int page,int year,int month,int maxId,int minId)
        {
            var queryModel = new ApiModel.PayQueryModel();
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            queryModel.Page = page;
            queryModel.Year = year;
            queryModel.Month = month;
            queryModel.MaxId = maxId;
            queryModel.MinId = minId;

            string requestJson = Helper.JsonSerializeObject(queryModel);

            var responseInfo = fnRequestProxy.GetPayInfoList(requestJson, oToken);
            if (responseInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.PayListModel>(responseInfo.StrObj));
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        public ResponseModel Get(string report, int year, int month)
        {
            var queryModel = new ApiModel.PayReportQuery();
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            queryModel.Year = year;
            queryModel.Month = month;

            string requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(queryModel);

            var responseInfo = fnRequestProxy.GetPayReport(requestJson, oToken);
            if (responseInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApiModel.PayReportMax>>(responseInfo.StrObj));
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // POST api/expenditure
        /// <summary>
        /// 新增/修改支出
        /// </summary>
        /// <param name="method"></param>
        /// <param name="payRecord"></param>
        /// <returns></returns>
        public ResponseModel PostPayInfo(string method, ApiModel.t_PayRecord payRecord)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            //if (payRecord.PaySum == 0)
            //{
            //    //缺少必须参数
            //    responseModel = ReturnModel.Error(-3, "缺少必须参数");
            //    return responseModel;
            //}

            var requestJson = Helper.JsonSerializeObject(payRecord);
            ProxyResponseModel responseInfo = new ProxyResponseModel();
            switch (method)
            {
                case "addnew":
                    responseInfo = fnRequestProxy.AddExpenditure(requestJson, oToken);
                    break;
                case "edit":
                    responseInfo = fnRequestProxy.EditPayInfo(requestJson, oToken);
                    break;
                case "delete":
                    responseInfo = fnRequestProxy.DeletePayInfo(payRecord.ID.ToString(), oToken);

                    break;
            }
            if (responseInfo.Status >= 0)
            {
                responseModel = new ResponseModel
                {
                    Status = 0,
                    Ver = "1.0",
                    ErrCode = responseInfo.Status,
                    ErrMsg = responseInfo.ErrDesc,
                    Data = responseInfo.StrObj
                };
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        /// <summary>
        /// 修改/删除/新增分类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        public ResponseModel Post(string type, ApiModel.UpdateModel updateModel)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var requestJson = Helper.JsonSerializeObject(updateModel);

            switch (type)
            {
                case "modify":
                    var responseInfoModify = fnRequestProxy.ModifyPayClass(requestJson, oToken);

                    if (responseInfoModify.Status >= 0)
                    {
                        responseModel = new ResponseModel
                        {
                            Status = 0,
                            Ver = "1.0",
                            ErrCode = responseInfoModify.Status,
                            ErrMsg = responseInfoModify.ErrDesc,
                            Data = ""
                        };
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                case "delete":
                    var responseInfoDelete = fnRequestProxy.DeletePayClass(requestJson, oToken);

                    if (responseInfoDelete.Status >= 0)
                    {
                        responseModel = new ResponseModel
                        {
                            Status = 0,
                            Ver = "1.0",
                            ErrCode = responseInfoDelete.Status,
                            ErrMsg = responseInfoDelete.ErrDesc,
                            Data = ""
                        };
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                case "add":
                    var responseInfoAdd = fnRequestProxy.AddPayClass(requestJson, oToken);

                    if (responseInfoAdd.Status >= 0)
                    {
                        responseModel = new ResponseModel
                        {
                            Status = 0,
                            Ver = "1.0",
                            ErrCode = responseInfoAdd.Status,
                            ErrMsg = responseInfoAdd.ErrDesc,
                            Data = responseInfoAdd.StrObj
                        };
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
            }
            

            return responseModel;
        }




        // PUT api/expenditure/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/expenditure/5
        public void Delete(int id)
        {
        }
    }
}
