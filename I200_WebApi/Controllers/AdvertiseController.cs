using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace I200_WebApi.Controllers
{
    public class AdvertiseController : Controller
    {
        //
        // GET: /Advertise/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 有钱 请求地址
        /// </summary>
        /// <param name="c">标示</param>
        /// <param name="d">机器码</param>
        public int YouQian(string c, string d)
        {
            //c=iyouqian&d=155F9B4A-06AC-43B5-9A4C-2354B6E19A11  

            T_Mobile_Advertise submitModel = new T_Mobile_Advertise();
            submitModel.logType = 1;
            submitModel.machineCode = d;
            submitModel.logIp = CommonLib.Helper.GetClientIP();
            submitModel.logmark = "有钱请求";
            submitModel.remark = c;
            var oResult = new OpenRequestModel();
            oResult.AccId = 0;
            oResult.UserId = 0;
            oResult.Token = "";
            oResult.AppKey = "YouQianWXQ9VYIUT584AAX";

            RequestProxy fnRequestProxy = new RequestProxy();
            ProxyResponseModel requestObj = fnRequestProxy.SetAdvertiseLog(CommonLib.Helper.JsonSerializeObject(submitModel), oResult);
            if (requestObj.Status == 0)
            {
                return 200;
            }
            else
            {
                return -1;
            }
        }

    }
}
