using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Message
{
    public class MessageController : ApiController
    {
        public ResponseModel GetMessage(string method)
        {
            var responseModel = new ResponseModel();
            var msgProxy = new MessageProxy();
            var oToken = Request.GetAccId();

            var objReturn = new ResponseModel();
            switch (method)
            {
                case "unread":
                    //获取未读消息数
                    var strResult = msgProxy.SendRequest(oToken, "/unread", null);
                    objReturn = ReturnModel.Success(Convert.ToInt32(strResult));
                    break;
                case "list":
                    //获取最近10条消息
                    var strMsg = msgProxy.SendRequest(oToken, "/list", null);
                    objReturn = ReturnModel.Success(Helper.JsonDeserializeObject<List<MessageModel.Message>>(strMsg));
                    break;
            }

            return objReturn;
        }

        public ResponseModel GetMessage(string method, string msgid)
        {
            var responseModel = new ResponseModel();
            var msgProxy = new MessageProxy();
            var oToken = Request.GetAccId();

            var msgDict = new Dictionary<string, string>();
            msgDict.Add("msgid", msgid);

            var objReturn = new ResponseModel();
            switch (method)
            {
                case "detail":
                    //获取消息详情
                    var strDetail = msgProxy.SendRequest(oToken, "/detail", msgDict);
                    objReturn = ReturnModel.Success(Helper.JsonDeserializeObject<MessageModel.Message>(strDetail));
                    break;
                case "setread":
                    //重置消息为已读状态
                    var strRead = msgProxy.SendRequest(oToken, "/setread", msgDict);
                    if (strRead != "")
                    {
                        objReturn = ReturnModel.Success(Helper.JsonDeserializeObject<MessageModel.Message>(strRead));
                    }
                    else
                    {
                        objReturn = ReturnModel.Error(-1, "未找到该信息");
                    }
                    break;
                case "globaldetail":
                    //获取公告消息
                    var strGlobal = msgProxy.SendRequest(oToken, "/globaldetail", msgDict);
                    objReturn = ReturnModel.Success(Helper.JsonDeserializeObject<MessageModel.Message>(strGlobal));
                    break;
            }

            return objReturn;
        }

        // GET api/message/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/message
        public void Post([FromBody]string value)
        {
        }

        // PUT api/message/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/message/5
        public void Delete(int id)
        {
        }
    }
}
