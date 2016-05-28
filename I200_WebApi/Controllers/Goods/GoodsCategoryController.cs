using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Goods
{
    public class GoodsCategoryController : ApiController
    {
        // GET api/goodscategory
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/goodscategory/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/goodscategory
        public ResponseModel Post(string method, ApiModel.CategoryModel model)
        {
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (string.IsNullOrEmpty(model.curCatID))
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var requestJson = Helper.JsonSerializeObject(model);

            if (method=="deleteMax")
            {                          
                var responseInfo = fnRequestProxy.deleteCategory(requestJson, oToken);

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
            else if (method == "deleteMin")
            {
                var responseInfo = fnRequestProxy.deleteMinCategory(requestJson, oToken);

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
            else if (method == "editCategory")
            {

                if (string.IsNullOrEmpty(model.catName))
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }

                var responseInfo = fnRequestProxy.editCategory(requestJson, oToken);

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
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
            
            return responseModel;
        }

        // PUT api/goodscategory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/goodscategory/5
        public void Delete(int id)
        {
        }
    }
}
