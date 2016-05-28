using CommonLib;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Controls
{
    public class MessageProxy
    {
        #region 接口信息
        /// <summary>
        /// API接口Key
        /// </summary>
        public static string AuthKey = "api_ar2fis5on7iopdybo0oa";
        /// <summary>
        /// API接口Key
        /// </summary>
        public static string AuthSecret = "k6jw52urrertnzwx579g85u9ip4yx4g6";

        /// <summary>
        /// 接口地址
        /// </summary>
        public static string ProxyUrl = string.Format("http://{0}/mobile", ConfigurationManager.AppSettings["MessageHost"].ToString());
        #endregion

        #region CreateAuth 生成验证信息
        /// <summary>
        /// 生成验证信息
        /// </summary>
        /// <returns></returns>
        public MessageModel.Auth CreateAuth()
        {
            var model = new MessageModel.Auth();
            model.appkey = AuthKey;
            model.timestamp = Helper.GetTimeStamp();
            model.nonce = Helper.GetRandomNum();

            var strCode = new StringBuilder();
            strCode.Append(model.appkey);
            strCode.Append(model.timestamp);
            strCode.Append(model.nonce);
            strCode.Append(AuthSecret);

            model.signature = Helper.Md5Hash(strCode.ToString());

            return model;
        }
        #endregion

        #region SendRequest 发送代理请求信息
        /// <summary>
        /// 发送代理请求信息
        /// </summary>
        /// <param name="router"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string SendRequest(OpenRequestModel oToken, string router, Dictionary<string, string> parameters)
        {
            //验证AccId
            var accId = oToken.AccId;
            if (accId == 0)
            {
                var fnProxy = new RequestProxy();
                var oInfo = fnProxy.GetSmsBalance(oToken);
                if (oInfo.AccId > 0)
                {
                    accId = oInfo.AccId;
                }
            }

            //发送Message请求
            string strResult = "";
            if (accId > 0)
            {
                var auth = CreateAuth();
                var header = new Dictionary<string, string>();
                header.Add("appkey", auth.appkey);
                header.Add("timestamp", auth.timestamp);
                header.Add("nonce", auth.nonce);
                header.Add("signature", auth.signature);

                var strUrl = ProxyUrl + router;

                //添加店铺ID参数
                if (parameters == null)
                {
                    parameters = new Dictionary<string, string>();
                }
                parameters.Add("accid", accId.ToString());

                strResult = Helper.RestGet(strUrl, "", parameters, header);
            }
            return strResult;
        }
        #endregion
    }
}
