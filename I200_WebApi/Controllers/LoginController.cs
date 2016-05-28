using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace I200_WebApi.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/login
        public string Get()
        {
            return "error";
        }

        // GET api/login/5
        public string Get(int id)
        {
            return "error";
        }

        public ResponseModel Get(string logout)
        {
            //退出登录
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            //去除缓存登录信息
            fnRequestProxy.LoginUserInfoRemove(oToken.UserId, oToken.AccId, oToken.Token, oToken.AppKey);
            
            var responseInfo = fnRequestProxy.GetLogout(oToken);
            if (responseInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(responseInfo.StrObj);
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }
        
        // POST api/login
        public ResponseModel Post(LoginModel loginModel)
        {
            //登录处理
            var oRequest=Request.GetAccId();
            var responseModel = new ResponseModel();
            responseModel.Ver = "1.0";

            loginModel.IpAddress = Request.GetClientIpAddress();
            loginModel.AppKey = oRequest.AppKey;

            var fnRequestProxy = new RequestProxy();


            //------------ 测试演示账号处理 Begin ------------
            if (loginModel.UserName == "demo" && loginModel.UserPwd == "demo")
            {
                //指定演示账号密码
                loginModel.UserName = ConfigurationManager.AppSettings["Demo-UserName"].ToString();
                loginModel.UserPwd = ConfigurationManager.AppSettings["Demo-PassWord"].ToString();

                var jsonStr = Helper.JsonSerializeObject(loginModel);
                var responseInfo = fnRequestProxy.GetLoginIn(jsonStr);
                if (responseInfo.Status == 1)
                {
                    //登录成功
                    responseModel.Status = 0;
                    ApiModel.AccountModel accountModel = Helper.JsonDeserializeObject<ApiModel.AccountModel>(responseInfo.StrObj);

                    //处理DemoToken
                    accountModel.Token = "DemoToken";

                    responseModel.Data = accountModel;

                    //登录缓存处理
                    fnRequestProxy.LoginUserInfoAdd(accountModel.LgUserId, accountModel, oRequest.AppKey);
                }
                else
                {
                    responseModel.Status = -1;
                    responseModel.ErrCode = responseInfo.Status;
                    responseModel.ErrMsg = "未知错误";
                }

                return responseModel;
            }
            //------------ 测试演示账号处理 End ------------


            //检测是否需要验证码
            var chkResult = fnRequestProxy.CheckVerifyCode(loginModel.UserName, loginModel.VerifyCode);
            if (chkResult.Key)
            {
                //特殊符号处理
                loginModel.UserPwd = HttpUtility.UrlDecode(loginModel.UserPwd);

                var jsonStr = Helper.JsonSerializeObject(loginModel);
                var responseInfo = fnRequestProxy.GetLoginIn(jsonStr);
                if (responseInfo.Status == 1)
                {
                    //登录成功
                    responseModel.Status = 0;
                    ApiModel.AccountModel accountModel = Helper.JsonDeserializeObject<ApiModel.AccountModel>(responseInfo.StrObj);
                    responseModel.Data = accountModel;

                    //登录缓存处理
                    fnRequestProxy.LoginUserInfoAdd(accountModel.LgUserId, accountModel, oRequest.AppKey);

                    //重置错误登录次数
                    fnRequestProxy.ErrRecordRest(loginModel.UserName);
                }
                else
                {
                    //登录失败
                    responseModel.Status = -1;
                    responseModel.ErrCode = responseInfo.Status;
                    if (responseInfo.Status == -1)
                    {
                        responseModel.ErrMsg = "账号不存在";
                    }
                    else if (responseInfo.Status == -3)
                    {
                        responseModel.ErrMsg = "账号未激活";
                    }
                    else if (responseInfo.Status == -4)
                    {
                        responseModel.ErrMsg = "登录权限被关闭，请联系老板";
                    }
                    else if (responseInfo.Status == -2)
                    {
                        responseModel.ErrMsg = "密码错误";

                        //增加错误次数
                        var errInfo = fnRequestProxy.ErrRecord(loginModel.UserName);
                        if (errInfo.Key >= 10)
                        {
                            //验证码
                            responseModel.ErrCode = -12;
                            responseModel.ErrMsg = errInfo.Value;
                        }
                    }
                    else
                    {
                        responseModel.ErrMsg = "未知错误";
                    }
                }
            }
            else
            {
                //验证码错误
                responseModel.Status = -1;
                responseModel.ErrCode = -11;
                responseModel.ErrMsg = chkResult.Value;

            }

            return responseModel;
        }

		public ResponseModel Post([FromBody]testModel durationList,int type)
		{
			#region 添加在线时长纪录

			ResponseModel responseModel = null;

			var durationListStr = durationList.durationList;
			var fnRequestProxy = new RequestProxy();
			var oToken = Request.GetAccId();
			if (string.IsNullOrEmpty(durationListStr))
			{
				//缺少必须参数
				responseModel = ReturnModel.Error(-3, "缺少必须参数");
			}
			else
			{
				var responseInfo = fnRequestProxy.AddOnlineDuration(durationListStr, oToken);
				if (responseInfo.Status == 0)
				{
					if (responseInfo.ErrDesc == "")
					{
						responseModel = ReturnModel.Success(responseInfo.StrObj);
					}
					else
					{
						if (responseInfo.ErrDesc == "-1")
						{
							responseModel = ReturnModel.Error(-1, "移动端添加在线时长错误");
						}
					}
				}
				else
				{
					responseModel = ReturnModel.TokenFail();
				}
			}
			return responseModel;

			#endregion
		}
		
	    // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/login/5
        public void Delete(int id)
        {

        }
    }
}
