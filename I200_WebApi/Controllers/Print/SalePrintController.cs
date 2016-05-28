using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Print
{
    public class SalePrintController : ApiController
    {
        // GET api/saleprint
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/saleprint/5
        public ResponseModel Get(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            ProxyResponseModel requestObj = new ProxyResponseModel();
            RequestProxy fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            requestObj = fnRequestProxy.GetPrintTemplate(id.ToString(), oToken);
            if (requestObj.Status == -1)
            {
                responseModel = ReturnModel.TokenFail();
            }
            else
            {
                responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.PrintTemplate>(requestObj.StrObj));
            }
            return responseModel;
        }

        // POST api/saleprint
        public void Post([FromBody]string value)
        {
        }

        // PUT api/saleprint/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/saleprint/5
        public void Delete(int id)
        {
        }
    }
}
