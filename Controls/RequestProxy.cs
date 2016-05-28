using CommonLib;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Controls
{
    public class RequestProxy
    {
        #region 接口信息
        /// <summary>
        /// API接口密钥
        /// </summary>
        public static string AuthCode = "MQ_v1RJwOBpT6i4Uokrl";

        /// <summary>
        /// 主站接口地址
        /// </summary>
        public static string ProxyUrl = string.Format("http://{0}/API/open.ashx", ConfigurationManager.AppSettings["ApiMainService"].ToString());

        /// <summary>
        /// 后台接口地址
        /// </summary>
        public static string ProxyUrlAds = string.Format("http://{0}/API/AdsApi.ashx", ConfigurationManager.AppSettings["ApiBackStage"].ToString());

        public static string ProxyUrlMap = string.Format("http://{0}/API/MobileMap.ashx", ConfigurationManager.AppSettings["ApiBackStage"].ToString());

        /// <summary>
        /// 登录缓存信息
        /// </summary>
        public static Hashtable HtLoginUserInfo = new Hashtable();

        /// <summary>
        /// 登录失败用户列表
        /// </summary>
        public static Hashtable HtErrLogin = new Hashtable();
        #endregion


        #region CreateAuthCode 生成验证信息
        /// <summary>
        /// 生成验证信息
        /// </summary>
        /// <returns></returns>
        public ProxyRequestModel CreateAuthCode()
        {
            var requestMd = new ProxyRequestModel { Timestamp = Helper.GetTimeStamp(), Nonce = Helper.GetRandomNum() };

            var strSign = new StringBuilder();
            strSign.Append(AuthCode);
            strSign.Append(requestMd.Timestamp);
            strSign.Append(requestMd.Nonce);
            requestMd.Signature = Helper.SHA1_Encrypt(strSign.ToString());

            return requestMd;
        }
        #endregion

        #region SendRequest 发送代理请求(Post)
        /// <summary>
        /// 发送代理请求(Post)
        /// </summary>
        /// <param name="accInfo">请求参数</param>
        /// <param name="requestName">请求名称</param>
        /// <param name="requestJson">请求参数(Json)</param>
        /// <returns></returns>
        public ProxyResponseModel SendRequest(OpenRequestModel accInfo, string requestName, string requestJson = "")
        {

            var requestMd = CreateAuthCode();
            requestMd.AccId = accInfo.AccId;
            requestMd.UserId = accInfo.UserId;
            requestMd.Token = accInfo.Token;
            requestMd.RequestName = requestName;
            requestMd.RequestJson = requestJson;

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("signature", requestMd.Signature);
            parameters.Add("timestamp", requestMd.Timestamp);
            parameters.Add("nonce", requestMd.Nonce);
            parameters.Add("accid", requestMd.AccId.ToString());
            parameters.Add("userid", requestMd.UserId.ToString());
            parameters.Add("token", requestMd.Token);
            parameters.Add("appkey", accInfo.AppKey);
            parameters.Add("requestname", requestMd.RequestName);
            parameters.Add("requestjson", requestMd.RequestJson);

            string strResult = string.Empty;

            if (requestName != "getAds" && requestName != "getmapdata")
            {
                strResult = Helper.RestPost(ProxyUrl, "", parameters);
            }
            else if (requestName == "getmapdata")
            {
                strResult = Helper.RestPost(ProxyUrlMap, "", parameters);
            }
            else
            {
                strResult = Helper.RestPost(ProxyUrlAds, "", parameters);
            }

            //调试日志
            //Logger.Info("请求参数：" + Helper.JsonSerializeObject(parameters) + " \r\n\r\n\r\n " + "返回参数：" + strResult);

            var objResponse = Helper.JsonDeserializeObject<ProxyResponseModel>(strResult);

            return objResponse;
        }
        #endregion

        #region GetLoginIn 登录处理
        /// <summary>
        /// 登录处理
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetLoginIn(string strObj)
        {
            var accInfo = new OpenRequestModel();
            accInfo.AccId = 0;
            accInfo.UserId = 0;
            accInfo.Token = "";

            var response = SendRequest(accInfo, "login", strObj);

            return response;
        }
        #endregion

        #region GetLogout 退出处理
        /// <summary>
        /// 退出处理
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetLogout(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "logout", "");

            return response;
        }
        #endregion

        #region 添加店铺在线时长统计

        /// <summary>
        /// 添加店铺在线时长统计
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel AddOnlineDuration(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addonlineduration", strObj);
            return response;
        }
        #endregion

        #region LoginUserInfoAdd 更新登录缓存信息
        /// <summary>
        /// 更新登录缓存信息
        /// </summary>
        /// <param name="iUserId">登录用户Id</param>
        /// <param name="accModel">登录信息Model</param>
        /// <param name="appKey">登录AppKey</param>
        public void LoginUserInfoAdd(int iUserId, ApiModel.AccountModel accModel, string appKey)
        {

            var oNewInfo = new ApiModel.LoginUserInfo();
            oNewInfo.AccId = accModel.AccId;
            oNewInfo.Token = accModel.Token;
            oNewInfo.ExpireTime = DateTime.Now.AddDays(7);
            oNewInfo.LoginAccountModel = accModel;
            var dictObj = new Dictionary<string, ApiModel.LoginUserInfo>() { { appKey, oNewInfo } };

            if (HtLoginUserInfo != null)
            {
                if (HtLoginUserInfo.ContainsKey(iUserId))
                {
                    var oList = (Dictionary<string, ApiModel.LoginUserInfo>)HtLoginUserInfo[iUserId];
                    if (oList.ContainsKey(appKey))
                    {
                        lock (HtLoginUserInfo)
                        {
                            oList[appKey] = oNewInfo;
                            HtLoginUserInfo[iUserId] = oList;
                        }
                    }
                    else
                    {
                        lock (HtLoginUserInfo)
                        {
                            oList.Add(appKey, oNewInfo);
                            HtLoginUserInfo[iUserId] = oList;
                        }
                    }
                }
                else
                {
                    lock (HtLoginUserInfo)
                    {
                        HtLoginUserInfo.Add(iUserId, dictObj);
                    }
                }
            }
            else
            {
                HtLoginUserInfo = new Hashtable { { iUserId, dictObj } };
            }
        }
        #endregion

        #region LoginUserInfoRemove 移除登录缓存信息
        /// <summary>
        /// 移除登录缓存信息
        /// </summary>
        /// <param name="iUserId">登录用户Id</param>
        /// <param name="accId"></param>
        /// <param name="token"></param>
        /// <param name="appKey"></param>
        public void LoginUserInfoRemove(int iUserId, int accId, string token, string appKey)
        {
            if (HtLoginUserInfo != null)
            {
                if (HtLoginUserInfo.ContainsKey(iUserId))
                {
                    var oList = (Dictionary<string, ApiModel.LoginUserInfo>)HtLoginUserInfo[iUserId];
                    if (oList.ContainsKey(appKey))
                    {
                        lock (HtLoginUserInfo)
                        {
                            oList.Remove(appKey);
                            HtLoginUserInfo[iUserId] = oList;
                        }
                    }
                }
            }
        }

        
        #endregion

        #region LoginUserInfoSearch 查询登录缓存信息
        /// <summary>
        /// 查询登录缓存信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public int LoginUserInfoSearch(int userId, string token, string appKey)
        {
            int result = 0;
            if (HtLoginUserInfo != null)
            {
                if (HtLoginUserInfo.ContainsKey(userId))
                {
                    var oList = (Dictionary<string, ApiModel.LoginUserInfo>)HtLoginUserInfo[userId];
                    if (oList.ContainsKey(appKey))
                    {
                        var oItem = (ApiModel.LoginUserInfo)oList[appKey];
                        TimeSpan ts = oItem.ExpireTime - DateTime.Now;
                        if (token.ToUpper() == oItem.Token.ToUpper() && ts.Days > 0)
                        {
                            result = oItem.AccId;
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        #region CheckVerifyCode 检测登录验证码
        /// <summary>
        /// 检测登录验证码
        /// </summary>
        /// <param name="userName">用户登录账号</param>
        /// <param name="verifyCode">登录验证码</param>
        /// <param name="loginType">登录方式</param>
        /// <returns>Fales-验证码错误</returns>
        public KeyValuePair<bool, string> CheckVerifyCode(string userName, string verifyCode)
        {
            bool bResult = true;
            string strValidateCode = "";

            if (HtErrLogin != null)
            {
                if (HtErrLogin.ContainsKey(userName))
                {
                    var oErrModel = (ErrUserModel)HtErrLogin[userName];
                    if (oErrModel.ErrTimes > 10)
                    {
                        //错误登录>10
                        TimeSpan ts = DateTime.Now - oErrModel.CreateTime;
                        if (oErrModel.ValidateCode != "" && ts.TotalMinutes <= 10)
                        {
                            var strVerifyCode = (verifyCode == null ? "" : verifyCode);
                            if (oErrModel.ValidateCode.Trim().ToUpper() != strVerifyCode.Trim().ToUpper())
                            {
                                bResult = false;
                            }
                        }
                        else
                        {
                            bResult = false;
                        }

                        //重置验证码
                        strValidateCode = CommonLib.ValidateCode.CreateValidateCode();
                        oErrModel.ValidateCode = strValidateCode;
                        oErrModel.CreateTime = DateTime.Now;
                        lock (HtErrLogin)
                        {
                            HtErrLogin[userName] = oErrModel;
                        }
                    }
                }
            }

            var codeUrl = ValidateCode.CreateValidateUrl(ValidateCode.EnCodeVal(strValidateCode));
            var oResult = new KeyValuePair<bool, string>(bResult, codeUrl);
            return oResult;
        }
        #endregion

        #region ErrRecord 记录错误登录次数
        /// <summary>
        /// 记录错误登录次数
        /// </summary>
        /// <param name="userName">登录用户名</param>
        /// <returns>错误次数</returns>
        public KeyValuePair<int, string> ErrRecord(string userName)
        {
            int iResult = 1;
            string strValidate = "";
            if (HtErrLogin != null)
            {
                if (HtErrLogin.ContainsKey(userName))
                {
                    var oErrModel = (ErrUserModel)HtErrLogin[userName];
                    oErrModel.ErrTimes = oErrModel.ErrTimes + 1;
                    iResult = oErrModel.ErrTimes;

                    //大于10次增加验证码
                    if (oErrModel.ErrTimes >= 10)
                    {
                        strValidate = CommonLib.ValidateCode.CreateValidateCode();
                        oErrModel.ValidateCode = strValidate;
                        oErrModel.CreateTime = DateTime.Now;
                    }

                    lock (HtErrLogin)
                    {
                        HtErrLogin[userName] = oErrModel;
                    }
                }
                else
                {
                    var oErrModel = new ErrUserModel();
                    oErrModel.ErrTimes = 1;
                    lock (HtErrLogin)
                    {
                        HtErrLogin.Add(userName, oErrModel);
                    }
                }
            }
            else
            {
                var oErrModel = new ErrUserModel();
                oErrModel.ErrTimes = 1;
                HtErrLogin = new Hashtable { { userName, oErrModel } };
            }

            var codeUrl = ValidateCode.CreateValidateUrl(ValidateCode.EnCodeVal(strValidate));
            var oResult = new KeyValuePair<int, string>(iResult, codeUrl);
            return oResult;
        }
        #endregion

        #region ErrRecordRest 错误登录次数重置
        /// <summary>
        /// 错误登录次数重置
        /// </summary>
        /// <param name="userName">登录用户名</param>
        /// <returns></returns>
        public bool ErrRecordRest(string userName)
        {
            bool bResult = false;
            if (HtErrLogin != null)
            {
                if (HtErrLogin.ContainsKey(userName))
                {
                    var oErrModel = (ErrUserModel)HtErrLogin[userName];
                    oErrModel.ErrTimes = 0;
                    lock (HtErrLogin)
                    {
                        HtErrLogin[userName] = oErrModel;
                    }
                    bResult = true;
                }
            }

            return bResult;
        }
        #endregion

        #region GetCashierList 收银员列表
        /// <summary>
        /// 收银员列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetCashierList(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "cashierlist", "");

            return response;
        }
        #endregion

        #region GetSmsBalance 短信余额
        /// <summary>
        /// 短信余额
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetSmsBalance(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "smsbalance", "");

            return response;
        }
        #endregion

        #region GetAppInfo 获得App应用信息
        /// <summary>
        /// 获得App应用信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetAppInfo(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "appinfo", "");

            return response;
        }
        #endregion

        #region GetAccountBasicConfig 获得店铺其他配置信息
        /// <summary>
        /// 获得App应用信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetAccountBasicConfig(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "basicconfig", "");

            return response;
        }
        #endregion

        #region GetDailyOperation 每日店铺运营数据
        public ProxyResponseModel GetDailyOperation(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getdailyoperation", strObj);

            return response;
        }
        #endregion

        #region UserInfo 店铺会员信息
        /// <summary>
        /// 店铺会员信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserInfo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "userinfo", strObj);

            return response;
        }
        #endregion

        #region UserBirthday 店铺今日生日会员
        /// <summary>
        /// 店铺今日生日会员
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserBirthday(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "userbirthday", "");

            return response;
        }
        #endregion

        #region UserBaseGroup 会员分组、标签
        /// <summary>
        /// 会员分组、标签
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserBaseGroup(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "userbasegroup", "");

            return response;
        }
        #endregion

        #region UserList 店铺会员列表
        /// <summary>
        /// 店铺会员列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "userlist", strObj);

            return response;
        }
        #endregion

        #region UserTagList 会员标签列表
        /// <summary>
        /// 会员标签列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserTagList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "usertaglist", strObj);

            return response;
        }
        #endregion

        #region UserBirthdayList 会员生日列表
        /// <summary>
        /// 会员生日列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserBirthdayList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "userbirthlist", strObj);

            return response;
        }
        #endregion

        #region UserGetNewNo 获得新会员卡号
        /// <summary>
        /// 获得新会员卡号
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserGetNewNo(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "usernewno", "");

            return response;
        }
        #endregion

        #region UserCheckPhone 会员手机号码重复检测
        /// <summary>
        /// 会员手机号码重复检测
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserCheckPhone(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "usercheckphone", strObj);

            return response;
        }
        #endregion

        #region UserCheckNo 会员卡号重复检测
        /// <summary>
        /// 会员卡号重复检测
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserCheckNo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "usercheckno", strObj);

            return response;
        }
        #endregion

        #region UserAdd 会员增加
        /// <summary>
        /// 会员增加
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserAdd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "useradd", strObj);

            return response;
        }
        #endregion

        #region UserGetNickList 获得会员称谓列表
        /// <summary>
        /// 获得会员称谓列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserGetNickList(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "usernick", "");

            return response;
        }
        #endregion

        #region UserSearch 会员搜索
        /// <summary>
        /// 会员搜索
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserSearch(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "usersearch", strObj);

            return response;
        }
        #endregion

        #region UserDel 会员删除
        /// <summary>
        /// 会员删除
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserDel(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "userdelete", strObj);

            return response;
        }
        #endregion

        #region UserStoreMoneyAdd 会员充值
        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserStoreMoneyAdd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "userstoremoneyadd", strObj);

            return response;
        }
        #endregion

        #region UserStoreTimesIn 会员充次
        /// <summary>
        /// 会员充次
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserStoreTimesIn(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "userstoretimesadd", strObj);

            return response;
        }
        #endregion

        #region UserIntegralExchange 积分兑换
        /// <summary>
        /// 积分兑换
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserIntegralExchange(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "userintegralexchange", strObj);

            return response;
        }
        #endregion

        #region UserSalesAdd 会员增加(销售后)
        /// <summary>
        /// 会员增加(销售后)
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserSalesAdd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "usersaleadd", strObj);

            return response;
        }
        #endregion

        #region UserEdit/UserEditEx 店铺会员编辑
        /// <summary>
        /// 店铺会员编辑
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserEdit(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "useredit", strObj);

            return response;
        }

        /// <summary>
        /// 店铺会员编辑提交方法
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserEditEx(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "usereditex", strObj);

            return response;
        }

        #endregion



        #region UserEditBirth 店铺会员生日编辑
        /// <summary>
        /// 店铺会员生日编辑
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel UserEditBirth(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "usereditbirth", strObj);

            return response;
        }
        #endregion


        #region SalesList 销售记录列表
        /// <summary>
        /// 销售记录列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel SalesList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "saleslist", strObj);

            return response;
        }
        #endregion

        #region SalesDetail 销售详情列表
        /// <summary>
        /// 销售详情列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel SalesDetail(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "salesdetail", strObj);

            return response;
        }
        #endregion

        #region SalesDel 销售删除
        /// <summary>
        /// 销售删除
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel SalesDel(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "salesdel", strObj);

            return response;
        }
        /// <summary>
        /// 销售删除 <para> V1.0 版本 舍弃</para>
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel SalesDelOld(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "salesdelOld", strObj);

            return response;
        }

        #endregion

        #region SalesAdd 销售记账
        /// <summary>
        /// 销售记账
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel SalesAdd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "salesadd", strObj);

            return response;
        }
        #endregion

        #region NoGoodsSaleAdd 无商品销售记账
        /// <summary>
        /// 销售记账
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel NoGoodsSalesAdd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "nogoodssaleadd", strObj);

            return response;
        }
        #endregion

        #region AliapyCreate 创建支付宝交易
        /// <summary>
        /// 创建支付宝交易
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel AliapyCreate(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "alipaycreate", strObj);

            return response;
        }
        #endregion

        #region AliapyQuery 查询支付宝状态
        /// <summary>
        /// 查询支付宝状态
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel AliapyQuery(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "alipayquery", strObj);

            return response;
        }
        #endregion


        #region GoodsGetClass 商品分类列表
        /// <summary>
        /// 商品分类列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsGetClass(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsclass", "");

            return response;
        }
        #endregion

        #region GoodsSearch 商品搜索
        /// <summary>
        /// 商品搜索
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsSearch(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsfind", strObj);

            return response;
        }
        #endregion

        #region GoodsDelete 商品删除
        /// <summary>
        /// 商品搜索
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsDelete(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsdel", strObj);

            return response;
        }
        #endregion

        #region GoodsAdd 商品增加
        /// <summary>
        /// 商品增加
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsAdd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsadd", strObj);

            return response;
        }
        #endregion

        #region ExistsGoods 检查重复商品
        /// <summary>
        /// 检查重复商品
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel ExistsGoods(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsexists", strObj);

            return response;
        }
        #endregion

        #region ExistsBarcode 检查重复编码
        /// <summary>
        /// 检查重复编码
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel ExistsBarcode(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodschkbarcode", strObj);

            return response;
        }
        #endregion

        #region GoodsMaxClassAdd 增加商品大分类
        /// <summary>
        /// 增加商品大分类
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsMaxClassAdd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsaddmaxclass", strObj);

            return response;
        }
        #endregion

        #region GoodsMinClassAdd 增加商品小分类
        /// <summary>
        /// 增加商品小分类
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsMinClassAdd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsaddminclass", strObj);

            return response;
        }
        #endregion

        #region GetGoodsList 获得商品列表
        /// <summary>
        /// 获得商品列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetGoodsList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsgetlist", strObj);

            return response;
        }
        #endregion

        #region GoodsStockIn 商品入库
        /// <summary>
        /// 商品入库
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsStockIn(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsstockin", strObj);

            return response;
        }
        #endregion

        #region GoodsStockOut 商品出库
        /// <summary>
        /// 商品出库
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsStockOut(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsstockout", strObj);

            return response;
        }
        #endregion

        #region GoodsStockOut 商品库存预警信息
        /// <summary>
        /// 商品库存预警信息
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetGoodsThresholdInfo(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsthresholdinfo", "");

            return response;
        }
        #endregion

        #region GoodsDetail 获得商品详情
        /// <summary>
        /// 获得商品详情
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsDetail(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsdetail", strObj);

            return response;
        }
        #endregion

        #region   IsShowCostPriceByPwd 根据管理员密码获取是否显示进价
        public ProxyResponseModel IsShowCostPriceByPwd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "isshowcostpricebypwd", strObj);
            return response;
        }
        #endregion


        #region GoodsEdit 修改商品信息
        /// <summary>
        /// 商品编辑
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsEdit(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsedit", strObj);
            return response;
        }

        #endregion

        #region GoodsInventory  商品盘点
        public ProxyResponseModel GoodsInventory(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsinventory", strObj);
            return response;
        }

        #endregion


        #region AccountCheck 店铺手机号重复检测
        /// <summary>
        /// 店铺手机号重复检测
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel AccountCheck(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "accountcheck", strObj);

            return response;
        }
        #endregion

        #region AccountReg 店铺注册(手机注册)
        /// <summary>
        /// 店铺注册(手机注册)
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel AccountReg(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "accountreg", strObj);

            return response;
        }
        #endregion

        #region ReSendVoicePin 重发语音验证码
        public ProxyResponseModel ReSendVoicePin(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "resendvoicePin", strObj);

            return response;
        }
        #endregion

        #region AccountRegEx 店铺注册(手机注册新)
        /// <summary>
        /// 店铺注册(手机注册新)
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel AccountRegEx(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "accountregex", strObj);

            return response;
        }
        #endregion

        #region ReSendRegCode 重发注册验证码
        /// <summary>
        /// 重发注册验证码
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel ReSendRegCode(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "resendcode", strObj);

            return response;
        }
        #endregion

        #region RegVerify 店铺注册激活(手机注册)
        /// <summary>
        /// 店铺注册激活(手机注册)
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel RegVerify(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "regverify", strObj);

            return response;
        }
        #endregion


        #region GetMobileWebUrl 获得手机橱窗Url地址
        /// <summary>
        /// 获得手机橱窗Url地址
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetMobileWebUrl(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "mobileweburl", "");

            return response;
        }
        #endregion

        #region BaseEdit 店铺其他信息编辑
        /// <summary>
        /// 店铺其他信息编辑
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel BaseEdit(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "storebanner", strObj);

            return response;
        }
        #endregion


        #region 修改店铺密码
        /// <summary>
        /// 修改店铺信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel UpdatePassword(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "updatepassword", strObj);

            return response;
        }


        #endregion

        #region 发送找回密码验证码
        /// <summary>
        /// 发送找回密码验证码
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getVerCode(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getvercode", strObj);

            return response;
        }

        #endregion

        #region 验证找回密码验证码
        /// <summary>
        /// 验证找回密码验证码
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel verifyCode(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "verifycode", strObj);

            return response;
        }

        #endregion

        #region 重置密码
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel resetPwd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "resetpwd", strObj);

            return response;
        }

        #endregion


        #region 删除商品大分类
        /// <summary>
        /// 删除商品大分类
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel deleteCategory(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "deletecategory", strObj);

            return response;
        }

        #endregion

        #region 删除商品小分类
        /// <summary>
        /// 删除商品大分类
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel deleteMinCategory(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "deletemincategory", strObj);

            return response;
        }

        #endregion


        #region 修改商品分类
        /// <summary>
        /// 修改商品分类
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel editCategory(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "editcategory", strObj);

            return response;
        }

        #endregion

        #region 获取店铺信息
        /// <summary>
        /// 获取店铺信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel accountInfo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "accountinfo", strObj);

            return response;
        }

        #endregion

        #region 修改店铺信息
        /// <summary>
        /// 修改店铺信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel modifyAcc(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "modifyacc", strObj);

            return response;
        }

        #endregion


        #region 获取店铺app信息
        /// <summary>
        /// 获取店铺app信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel accountAppInfo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "accountappinfo", strObj);

            return response;
        }

        #endregion

        #region 获取店铺当月生日会员列表
        /// <summary>
        /// 获取店铺当月生日会员列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getMonthBirthList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "monthbirthlist", strObj);

            return response;
        }

        #endregion

        #region 获取店铺销售信息汇总
        /// <summary>
        /// 获取店铺销售信息汇总
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getSaleInfo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getsaleinfo", strObj);

            return response;
        }

        #endregion

        #region 获取商品汇总信息
        /// <summary>
        /// 获取商品汇总信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getGoodsInfo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getgoodsinfo", strObj);

            return response;
        }

        #endregion

        #region 批量导入会员信息
        /// <summary>
        /// 批量导入会员信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel importUser(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "importuser", strObj);

            return response;
        }

        #endregion

        #region 获取商品分享地址
        /// <summary>
        /// 获取商品分享地址
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getGoodsAdd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getgoodsadd", strObj);

            return response;
        }

        #endregion

        #region 获取店铺销售金额信息
        /// <summary>
        /// 获取店铺销售金额信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel accountSaleInfo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "accountsaleinfo", strObj);

            return response;
        }

        #endregion

        #region 获取店铺优惠券信息
        /// <summary>
        /// 获取店铺优惠券信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getCouponList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getcouponlist", strObj);

            return response;
        }

        #endregion

        #region 获取会员优惠券绑定信息
        /// <summary>
        /// 获取会员优惠券绑定信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getUsrCoupon(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getusrcoupon", strObj);

            return response;
        }

        #endregion

        #region 获取会员计次卡绑定信息
        /// <summary>
        /// 获取会员计次卡绑定信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getUsrTimeCards(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getusrtimecards", strObj);

            return response;
        }

        #endregion

        #region 赠送优惠券
        /// <summary>
        /// 赠送优惠券
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel sendCoupon(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "sendcoupon", strObj);

            return response;
        }

        #endregion

        #region 每日签到
        /// <summary>
        /// 每日签到
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getSigned(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "signed", strObj);

            return response;
        }

        #endregion

        #region 获取广告信息
        /// <summary>
        /// 获取广告信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getAds(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getAds", strObj);

            return response;
        }

        #endregion

        #region 会员发送短信
        /// <summary>
        /// 会员发送短信
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SendSms(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "sendsms", strObj);

            return response;
        }

        #endregion

        #region 筛选会员
        /// <summary>
        /// 筛选会员
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel FilteredUserList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "usrfilter", strObj);

            return response;
        }

        #endregion

        #region 新销售列表
        /// <summary>
        /// 新销售列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SalesListEx(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "salelistex", strObj);

            return response;
        }

        #endregion

        #region 新销售详情
        /// <summary>
        /// 新销售详情
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SalesDetailEx(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "saledetailex", strObj);

            return response;
        }

        #endregion


        #region 条码查询接口
        /// <summary>
        /// 条码查询接口
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SearchBarcode(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "barcode", strObj);

            return response;
        }

        #endregion


        #region 销售分析数据
        /// <summary>
        /// 销售分析数据
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SalesGraphData(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "salegraphdata", strObj);

            return response;
        }

        #endregion

        #region SalesTypeData 销售类型分析
        /// <summary>
        /// 销售类型分析
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SalesTypeData(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "saletypedata", strObj);

            return response;
        }

        #endregion

        #region GoodsAnalyze 商品销售分析
        /// <summary>
        /// 商品销售分析
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GoodsAnalyze(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsanalyze", strObj);

            return response;
        }

        #endregion

        #region GetMemberView 销售概要分析
        /// <summary>
        /// 销售概要分析
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetMemberView(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "memberview", strObj);

            return response;
        }

        #endregion

        #region GetReturnPayLog 获取还款信息
        public ProxyResponseModel GetReturnPayLog(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "returnpaylog", strObj);

            return response;
        }
        #endregion 

        #region AddReturnInfo 添加还款信息
        public ProxyResponseModel AddReturnInfo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addreturnpay", strObj);

            return response;
        }
        #endregion 

        #region CheckSalePwd 校验会员消费密码
        public ProxyResponseModel CheckSalePwd(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "checksalepwd", strObj);

            return response;
        }
        #endregion 

        #region GetBbsLoginCode 获取论坛登录信息
        /// <summary>
        /// 获取论坛登录信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetBbsLoginCode(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getbbslogin", "");

            return response;
        }
        #endregion

        #region AddExpenditure 添加支出信息
        /// <summary>
        /// 添加支出信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel AddExpenditure(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addexpenditure", strObj);

            return response;
        }
        #endregion

        #region ModifyPayClass 修改支出分类
        /// <summary>
        /// 修改支出分类
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel ModifyPayClass(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "modifypayclass", strObj);

            return response;
        }
        #endregion

        #region DeletePayClass 删除支出分类
        /// <summary>
        /// 删除支出分类
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel DeletePayClass(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "deletepayclass", strObj);

            return response;
        }
        #endregion

        #region AddPayClass 新增支出分类
        /// <summary>
        /// 新增支出分类
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel AddPayClass(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addpayclass", strObj);

            return response;
        }
        #endregion

        #region EditPayInfo 修改支出信息
        /// <summary>
        /// 修改支出信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel EditPayInfo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "editpayinfo", strObj);

            return response;
        }
        #endregion

        #region GetPayClass 获取支出分类
        /// <summary>
        /// 获取支出分类
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetPayClass(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getpayclass", strObj);

            return response;
        }
        #endregion

        #region GetPayInfoList 获取分类详细
        /// <summary>
        /// 获取分类详细
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetPayInfoList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getpayinfolist", strObj);

            return response;
        }
        #endregion

        #region GetPayReport 获取支出报表
        /// <summary>
        /// 获取支出报表
        /// </summary>
        /// <param name="strObj"></param>
        /// <returns></returns>
        public ProxyResponseModel GetPayReport(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getpayreport", strObj);

            return response;
        }
        #endregion

        #region DeletePayInfo 删除支出信息
        /// <summary>
        /// 删除支出信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel DeletePayInfo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "deletepayinfo", strObj);

            return response;
        }
        #endregion

        #region 获取商品分享地址
        /// <summary>
        /// 获取商品分享地址
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel getGoodsSkuList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "goodsskulist", strObj);

            return response;
        }

        #endregion

        #region 获取商品地图数据
        /// <summary>
        /// 获取商品地图数据
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetMapData(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getmapdata", strObj);

            return response;
        }

        #endregion

        #region SendRegCode 移动端店铺注册（首先发送验证码版本）
        /// <summary>
        /// 移动端店铺注册（首先发送验证码版本）
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SendRegCode(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getregcode", strObj);

            return response;
        }
        #endregion

        #region VerifyRegCode 验证移动端注册验证码
        /// <summary>
        /// 验证移动端注册验证码
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel VerifyRegCode(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "verifyregcode", strObj);

            return response;
        }
        #endregion


        #region 挂单功能

        #region  GetCartInfo 得到单个挂单信息
        /// <summary>
        /// 得到挂单信息
        /// </summary>
        /// <param name="objVal"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetCartInfo(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "cartinfo", objVal);
            return response;
        }

        #endregion

        #region  GetSaleCartList 得到挂单列表
        /// <summary>
        /// 得到挂单列表
        /// </summary>
        /// <param name="objVal"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetSaleCartList(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "cartlist", objVal);
            return response;
        }

        #endregion

        #region  SaveSaleCart 挂单保存信息
        /// <summary>
        /// 挂单保存信息
        /// </summary>
        /// <param name="objVal"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SaveSaleCart(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addcart", objVal);
            return response;
        }

        #endregion

        #region  DeleteSaleCart 删除挂单信息
        /// <summary>
        /// 删除挂单信息
        /// </summary>
        /// <param name="objVal"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel DeleteSaleCart(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "delcart", objVal);
            return response;
        }

        #endregion

        #endregion

        #region 获取常用商品列表（iPad）
        /// <summary>
        /// 获取常用商品列表（iPad）
        /// </summary>
        /// <param name="objVal"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetCommonGoodsList(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "commongood", objVal);
            return response;
        }

        #endregion

        #region 新增常用商品
        public ProxyResponseModel AddCommonGoods(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addcommon", objVal);
            return response;
        }
        #endregion

        #region 删除常用商品
        public ProxyResponseModel DelCommonGoods(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "delcommon", objVal);
            return response;
        }
        #endregion 

        #region 获取供应商列表
        public ProxyResponseModel GetSupplierList(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "supplierlist", objVal);
            return response;
        }
        public ProxyResponseModel GetSupplierListNew(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "supplierlistnew", objVal);
            return response;
        }
        #endregion

        #region 获取单个供应商
        public ProxyResponseModel GetSingleSupplier(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "singlesupplier", objVal);
            return response;
        }
        #endregion

        #region 删除供应商
        public ProxyResponseModel DeleteSupplier(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "delsupplier", objVal);
            return response;
        }
        #endregion

        #region 新增供应商
        public ProxyResponseModel AddNewSupplier(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addsupplier", objVal);
            return response;
        }
        #endregion

        #region 修改供应商信息
        public ProxyResponseModel ModifySupplier(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "modifysupplier", objVal);
            return response;
        }
        #endregion

        #region 获取供应商商品
        public ProxyResponseModel GetSupplierGoods(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getsuppliergoods", objVal);
            return response;
        }
        #endregion

        #region 新增款项记录
        public ProxyResponseModel AddGsRepayment(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addgsrepaymentmodel", objVal);
            return response;
        }
        #endregion

        #region 新增还款记录
        public ProxyResponseModel AddGsRepaymentRecord(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addgsrepaymentrecord", objVal);
            return response;
        }
        #endregion

        #region 获取供应商款项记录列表
        public ProxyResponseModel GetGsRepaymentList(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getgsrepaymentlist", objVal);
            return response;
        }
        #endregion

        #region 获取单一款项记录的还款记录

        public ProxyResponseModel Getgsrepaymentrecordlist(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getgsrepaymentrecordlist", objVal);
            return response;
        }

        #endregion

        #region 获得是否绑定微信

        public ProxyResponseModel GetSingleAuthorizerByAccId(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getsingleauthorizerbyaccid", objVal);
            return response;
        }

        #endregion


        #region 得到移动端产品列表
        /// <summary>
        /// 得到移动端产品列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetOrderProjectList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "ordermobileproject", strObj);

            return response;
        }
        /// <summary>
        /// 确认订单金额
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetOrderInfoPrice(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "ordermobileprojectprice", strObj);

            return response;
        }
        /// <summary>
        /// 增加订单信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel AddOrderInfo(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addmobileorderinfo", strObj);

            return response;
        }
        /// <summary>
        /// 设置订单的支付凭证
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SetOrderTransactionReceiptByIOS(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "iosorderreceipt", strObj);

            return response;
        }
        /// <summary>
        /// 设置订单的支付凭证
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SetOrderTransactionReceiptByAndroid(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "androidorderreceipt", strObj);

            return response;
        }

        #endregion

        #region 得到打印数据
        /// <summary>
        /// 得到打印模板
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetPrintTemplate(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "mobileprint", strObj);

            return response;
        }
        #endregion


        #region 得到出入库记录
        /// <summary>
        /// 得到出入库记录
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetStockLogList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "stockloglist", strObj);

            return response;
        }
        #endregion
        #region 得到出入库记录汇总
        /// <summary>
        /// 得到出入库记录汇总
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetStockLogCount(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "stocklogcount", strObj);

            return response;
        }
        #endregion


        #region 录入广告日志
        /// <summary>
        /// 录入广告日志
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SetMobileAdvertiseLog(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "mobileadvertise", strObj);

            return response;
        }

        /// <summary>
        /// 录入广告日志
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel SetAdvertiseLog(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "setadvertise", strObj);

            return response;
        }


        #endregion


        #region 添加加料分类
        public ProxyResponseModel AddNewGoodsAddition(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addnewgoodsaddition", objVal);
            return response;
        }
        #endregion

        #region 更新加料分类
        public ProxyResponseModel UpdateGoodsAddition(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "updategoodsaddition", objVal);
            return response;
        }
        #endregion

        #region 添加加料
        public ProxyResponseModel AddNewGoodsAdditionItem(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addnewgoodsadditionitem", objVal);
            return response;
        }
        #endregion

        #region 更新加料
        public ProxyResponseModel UpdateGoodsAdditionItem(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "updategoodsadditionitem", objVal);
            return response;
        }
        #endregion

        #region 获取该分类下所有加料
        public ProxyResponseModel GetGoodsAdditionItemList(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getgoodsadditonitemlist", objVal);
            return response;
        }
        #endregion

        #region 获取所有加料分类名
        public ProxyResponseModel GetGoodsAdditionList(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getgoodsadditonlist", objVal);
            return response;
        }
        #endregion

        #region 获取所有单个加料详情
        public ProxyResponseModel GetSingleGoodsAdditonalitem(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getsinglegoodsadditonalitem", objVal);
            return response;
        }
        #endregion

        #region 获取全部分类和加料

        public ProxyResponseModel GetAllGoodsAddition(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getallgoodsaddition", objVal);
            return response;
        }

        #endregion


        #region 删除分类名
        public ProxyResponseModel DeleteGoodsAdditional(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "deletegoodsadditional", objVal);
            return response;
        }
        #endregion

        #region 删除加料
        public ProxyResponseModel DeleteGoodsAdditionalItem(string objVal, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "deletegoodsadditionalitem", objVal);
            return response;
        }
        #endregion


        #region 获取优惠券列表（抵值券且未过期）

        public ProxyResponseModel GetOrderCouponList(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getordercouponlist", strObj);

            return response;
        }

        #endregion


        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel Getorderlist(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getorderlist", strObj);

            return response;
        }

        #region GetLoginToken 创建登录Token信息

        /// <summary>
        /// 创建登录Token信息
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetLoginToken(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getlogintoken", strObj);

            return response;
        }
        #endregion

        #region 设置

        #region GetBaseSaleConfig 获取销售基本设置

        /// <summary>
        /// 获取销售基本设置
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetBaseSaleConfig(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getBaseSaleConfig", "");

            return response;
        }

        #endregion

        #region GetDiscountSaleConfig 获取销售预设折扣设置

        /// <summary>
        /// 获取销售预设折扣设置
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetDiscountSaleConfig(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getDiscountSaleConfig", "");

            return response;
        }

        #endregion

        #region GetSerialNumSaleConfig 获取销售流水单号设置

        /// <summary>
        /// 获取销售流水单号设置
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetSerialNumSaleConfig(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getSerialNumSaleConfig", "");

            return response;
        }

        #endregion

        #region GetStockAlertGoodsConfig 获取商品库存提醒设置

        /// <summary>
        /// 获取商品库存提醒设置
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetStockAlertGoodsConfig(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getStockAlertGoodsConfig", "");

            return response;
        }

        #endregion

        #region GetUserDiscountConfig 获取会员级别和折扣设置
        /// <summary>
        /// 获取会员级别和折扣设
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetUserDiscountConfig(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getUserDiscountConfig", "");

            return response;
        }

        #endregion

        #region GetUserGroupList 获取会员分组设置

        /// <summary>
        /// 获取会员分组设置
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetUserGroupList(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getUserGroupList", "");

            return response;
        }

        #endregion

        #region GetUserNickNameList 获取会员称谓设置

        /// <summary>
        /// 获取会员称谓设置
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetUserNickNameList(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getUserNickNameList", "");

            return response;
        }

        #endregion

        #region GetUserTagList 获取会员标签设置

        /// <summary>
        /// 获取会员标签设
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetUserTagList(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getUserTagList", "");

            return response;
        }

        #endregion

        #region GetUserTimesCardOutTimeConfig 获取会员计次卡过期天数设置
        /// <summary>
        /// 获取会员计次卡过期天数设置
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetUserTimesCardOutTimeConfig(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getUserTimesCardOutTimeConfig", "");

            return response;
        }

        #endregion

        #region GetAccountUserList 获取店员列表

        /// <summary>
        /// 获取店员列表
        /// </summary>
        /// <param name="oToken"></param>
        /// <returns></returns>
        public ProxyResponseModel GetAccountUserList(OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getAccountUserList", "");

            return response;
        }

        #endregion

        #endregion
        
        #region GetUserInfoForAndroidSale 移动端获取会员销售概要
        //移动端获取会员销售概要
        public ProxyResponseModel GetUserInfoForAndroidSale(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getuserinfoforandroidsale", strObj);

            return response;
        }
        #endregion

        #region AddSalePayTypeSort 新增销售排序
        //新增销售排序
        public ProxyResponseModel AddSalePayTypeSort(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "addsalepaytypesort", strObj);

            return response;
        }
        #endregion

        #region GetPayTypeSort 获取销售排序
        //新增销售排序
        public ProxyResponseModel GetPayTypeSort(string strObj, OpenRequestModel oToken)
        {
            var response = SendRequest(oToken, "getpaytypesort", strObj);

            return response;
        }
        #endregion
    }
}
