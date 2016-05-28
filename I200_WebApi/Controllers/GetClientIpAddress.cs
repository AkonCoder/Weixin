using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers
{
    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage =
            "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            // Web-hosting. Needs reference to System.Web.dll
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting. Needs reference to System.ServiceModel.dll. 
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
            if (request.Properties.ContainsKey(OwinContext))
            {
                dynamic owinContext = request.Properties[OwinContext];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取缓存的AccId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static OpenRequestModel GetAccId(this HttpRequestMessage request)
        {
            var oResult = new OpenRequestModel();
            if (request.Properties.ContainsKey(HttpContext))
            {
                var strUserId = request.Headers.SingleOrDefault(x => x.Key == "UserId");
                var strToken = request.Headers.SingleOrDefault(x => x.Key == "Token");
                var strAppkey = request.Headers.SingleOrDefault(x => x.Key == "AppKey");

                var fnRequestPorxy = new RequestProxy();
                oResult.UserId = (strUserId.Key == null ? 0 : int.Parse(strUserId.Value.FirstOrDefault()));
                oResult.Token = (strToken.Key == null ? "" : strToken.Value.FirstOrDefault());
                oResult.AppKey = (strAppkey.Key == null ? "" : strAppkey.Value.FirstOrDefault());
                oResult.AccId = fnRequestPorxy.LoginUserInfoSearch(oResult.UserId, oResult.Token, oResult.AppKey);
            }
            else
            {
                oResult.AccId = 0;
                oResult.UserId = 0;
                oResult.Token = "";
                oResult.AppKey = "";
            }

            return oResult;
        }
    }
}
