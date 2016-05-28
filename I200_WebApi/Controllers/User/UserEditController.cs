using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.User
{
    public class UserEditController : ApiController
    {
        //// GET api/useredit
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/useredit/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        public ResponseModel Post(int usrId, [FromBody] ApiModel.UserSubmitModel model)
        {

            var responseModel = new ResponseModel();
            if (model == null || string.IsNullOrEmpty(model.UserNo) || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.UserPhone))
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            model.UsrId = usrId;

            var jsonStr = Helper.JsonSerializeObject(model);
            var responseInfo = fnRequestProxy.UserEditEx(jsonStr, oToken);

            responseModel = new ResponseModel
            {
                Status = responseInfo.Status,
                Ver = "1.0",
                ErrCode = responseInfo.Status,
                ErrMsg = responseInfo.ErrDesc,
                Data = ""
            };

            return responseModel;
        
        }

        // POST api/user
        public ResponseModel Post(int id,[FromBody]OpenRequest.UserInfo model)
        {
            //会员编辑更新
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();


            model.UserId = id;

            var jsonStr = Helper.JsonSerializeObject(model);
            var responseInfo = fnRequestProxy.UserEdit(jsonStr, oToken);

            if (responseInfo.Status == 0)
            {
                if (responseInfo.StrObj == "1")
                {
                    //更新
                    responseModel = ReturnModel.Success(responseInfo.StrObj);
                }
                else
                {
                    responseModel = ReturnModel.Error(-2, "修改失败");
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }
            
            return responseModel;
        }

        public ResponseModel Post(int id, int birthid, [FromBody]ApiModel.EditBirthModel model)
        {
            //会员生日更新
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            model.UserId = id;
            model.BirthId = birthid;

            var jsonStr = Helper.JsonSerializeObject(model);
            var responseInfo = fnRequestProxy.UserEditBirth(jsonStr, oToken);

            if (responseInfo.Status == 0)
            {
                if (responseInfo.StrObj == "1")
                {
                    //更新
                    responseModel = ReturnModel.Success(responseInfo.StrObj);
                }
                else
                {
                    responseModel = ReturnModel.Error(-2, "修改失败");
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // PUT api/useredit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/useredit/5
        public void Delete(int id)
        {
        }
    }
}
