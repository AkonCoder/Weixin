using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace I200_WebApi.Controllers.User
{
    public class UserController : ApiController
    {
        // GET api/user
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public ApiModel.UserSubmitModel Get()
        {
            var testModel = new ApiModel.UserSubmitModel();
            var birthModel = new ApiModel.UserBirthModel();
            birthModel.Type = 1;
            testModel.UserBirth =new List<ApiModel.UserBirthModel>{ birthModel};
            return testModel;
        }

        public ResponseModel Get(int page, string keyWord = "",int group=-1,int grade=-1,int buygroup=-1,string firstword="",int subranch=0,int minintrgral=-1,int maxintegral=-1,int tag=-1,string order="")
        {
            //获取会员列表
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var userQuery = new ApiModel.UserQueryOrder();
            userQuery.page = page;
            userQuery.keyWord = keyWord;
            userQuery.uGroup = group;
            userQuery.uGrade = grade;
            userQuery.uBuyGroup = buygroup;
            userQuery.firstWord = firstword;
            userQuery.sunBranch = subranch;
            userQuery.minIntegral = minintrgral;
            userQuery.maxIntegral = maxintegral;
            userQuery.tag = tag;
            userQuery.odr = order;

            var userQueryObj=Helper.JsonSerializeObject(userQuery);

            var responseInfo = fnRequestProxy.UserList(userQueryObj, oToken);
            if (responseInfo.Status == 0)
            {
                if (responseInfo.StrObj == "none")
                {
                    responseModel = ReturnModel.SuccessNone();
                }
                else
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.UserListSummary>(responseInfo.StrObj));
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }


        public ResponseModel GetBase(string method,int classid=1,int days=-1,string phone="",string userno="",string keyword="")
        {
            //获得会员基础信息
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            switch (method)
            {
                case "birthday":
                    #region 今日生日会员列表
                    var responseInfo = fnRequestProxy.UserBirthday(oToken);
                    if (responseInfo.Status == 0)
                    {
                        if (responseInfo.StrObj == "none")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.UserBirthday>(responseInfo.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break; 
                    #endregion
                case "basegroup":
                    #region 会员分组、标签
                    var userBaseInfo = fnRequestProxy.UserBaseGroup(oToken);
                    if (userBaseInfo.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.UserBaseGroup>(userBaseInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break; 
                    #endregion
                case "birthlist":
                    #region 会员生日列表
                    var oType = new KeyValuePair<int, int>(classid, days);
                    var jsonStr = Helper.JsonSerializeObject(oType);
                    var birthdayObj = fnRequestProxy.UserBirthdayList(jsonStr, oToken);
                    if (birthdayObj.Status == 0)
                    {
                        if (birthdayObj.StrObj != "none")
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.UserBirthList>(birthdayObj.StrObj));
                        }
                        else
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break; 
                    #endregion
                case "newno":
                    #region 获得会员卡号
                    var objNewNo = fnRequestProxy.UserGetNewNo(oToken);
                    if (objNewNo.Status == 0)
                    {
                        if (objNewNo.StrObj == "-1")
                        {
                            responseModel = ReturnModel.Error(-3, "会员数已经达到当前版本的上限");
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(objNewNo.StrObj);
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break; 
                    #endregion
                case "checkphone":
                    #region 会员手机号码重复检测
                    var objPhoneNum = fnRequestProxy.UserCheckPhone(phone, oToken);
                    if (objPhoneNum.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objPhoneNum.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break; 
                    #endregion
                case "checkno":
                    #region 会员卡号重复检测
                    var objUserNo = fnRequestProxy.UserCheckNo(userno, oToken);
                    if (objUserNo.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objUserNo.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "nicklist":
                    #region 获得会员称谓列表
                    var objNickName = fnRequestProxy.UserGetNickList(oToken);
                    if (objNickName.Status == 0)
                    {
                        if (string.IsNullOrEmpty(objNickName.StrObj))
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.UserNickGroup>(objNickName.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break; 
                    #endregion
                case "search":
                    #region 会员搜索
                    var objUser = fnRequestProxy.UserSearch(keyword, oToken);
                    if (objUser.Status == 0)
                    {
                        if (objUser.StrObj == "none")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<List<ApiModel.UserTipsModel>>(objUser.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "monbirth":
                    #region 获取本月生日会员
                    var objMonBirthUser = fnRequestProxy.getMonthBirthList(keyword, oToken);
                    if (objMonBirthUser.Status == 0)
                    {
                        if (objMonBirthUser.StrObj == "none")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<KeyValuePair<int, DataSet>>(objMonBirthUser.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
            }

            return responseModel;
        }

        public ResponseModel GetUserTag(string taguid)
        {
            //会员标签列表
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var responseInfo = fnRequestProxy.UserTagList(taguid, oToken);

            if (responseInfo.Status == 0)
            {
                if (responseInfo.StrObj != "none")
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.UserTagListModel>(responseInfo.StrObj));
                }
                else
                {
                    responseModel = ReturnModel.SuccessNone();
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // GET api/user/5
        public ResponseModel Get(int id, string type = "basic", string keyword = "")
        {
            //店铺会员信息
            var responseInfo = new ProxyResponseModel();
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var userRequest = new OpenRequest.UserInfo();

            userRequest.UserId = id;
            userRequest.InfoType = type;

            var oToken = Request.GetAccId();
            var jsonStr = Helper.JsonSerializeObject(userRequest);
            if (type != "sales")
            {
                responseInfo = fnRequestProxy.UserInfo(jsonStr, oToken);
            }
            else
            {
                //销售列表
                var salesQuery = new ApiModel.SaleQueryOrderStr();
                salesQuery.userID = id;
                salesQuery.keyword = keyword;

                var salesQueryObj = Helper.JsonSerializeObject(salesQuery);

                responseInfo = fnRequestProxy.SalesList(salesQueryObj, oToken);
            }

            if (responseInfo.Status == 0)
            {
                var UserInfoModel = new object();
                if (!string.IsNullOrEmpty(responseInfo.StrObj))
                {
                    if (type == "basic")
                    {
                        //会员信息Lite
                        UserInfoModel = Helper.JsonDeserializeObject<ApiModel.UserInfoModel>(responseInfo.StrObj);
                    }
                    else if (type == "summary")
                    {
                        //会员信息(概要)
                        UserInfoModel = Helper.JsonDeserializeObject<ApiModel.UserSummary>(responseInfo.StrObj);
                    }
                    else if (type == "detail")
                    {
                        //会员信息(详细)
                        UserInfoModel = Helper.JsonDeserializeObject<ApiModel.UserDetailModle>(responseInfo.StrObj);
                    }
                    else if (type == "storemoney")
                    {
                        //会员信息(储值)
                        UserInfoModel = Helper.JsonDeserializeObject<ApiModel.UserStoreMoneyModle>(responseInfo.StrObj);
                    }
                    else if (type == "storetimes")
                    {
                        //会员信息(计次)
                        UserInfoModel = Helper.JsonDeserializeObject<ApiModel.UserStoreTimesModle>(responseInfo.StrObj);
                    }
                    else if (type == "integral")
                    {
                        //会员信息(积分)
                        UserInfoModel = Helper.JsonDeserializeObject<ApiModel.UserIntegralModel>(responseInfo.StrObj);
                    }
                    else if (type == "smsinfo")
                    {
                        //会员信息(短信)
                        UserInfoModel = Helper.JsonDeserializeObject<ApiModel.UserSmsListModel>(responseInfo.StrObj);
                    }
                    else if (type == "sales")
                    {
                        //会员信息(消费)
                        UserInfoModel = Helper.JsonDeserializeObject<ApiModel.SalesSummary>(responseInfo.StrObj);
                    }
                    else if (type == "coupon")
                    {
                        //会员信息(优惠券)
                        UserInfoModel = Helper.JsonDeserializeObject<List<ApiModel.CouponModel>>(responseInfo.StrObj);
                    }

                    responseModel = ReturnModel.Success(UserInfoModel);
                }
                else
                {
                    responseModel = ReturnModel.SuccessNone();
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // POST api/user
        public ResponseModel Post(string type, [FromBody]ApiModel.UserQueryOrderEx model)
        {
            //获取会员列表
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var query = Helper.JsonSerializeObject(model);

            if (type == "getFilteredUserList")
            {
                var responseInfo = fnRequestProxy.FilteredUserList(query, oToken);
                if (responseInfo.Status == 0)
                {
                    if (responseInfo.StrObj == "none")
                    {
                        responseModel = ReturnModel.SuccessNone();
                    }
                    else
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.UserListSummary>(responseInfo.StrObj));
                    }
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
            }
            
            return responseModel;
        }

        // POST api/user
        public ResponseModel Post([FromBody]ApiModel.UserSubmitModel model)
        {
            //新增会员
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.UserPhone) || string.IsNullOrEmpty(model.UserNo))
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var requestJson = Helper.JsonSerializeObject(model);
            var responseInfo = fnRequestProxy.UserAdd(requestJson, oToken);

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
                        responseModel = ReturnModel.Error(-5, "会员数量达到上限，您可以通过电脑登陆www.i200.cn升级高级版，无限存储会员");
                    }
                    else if (responseInfo.ErrDesc == "2")
                    {
                        responseModel = ReturnModel.Error(-2, "会员卡号存在重复");
                    }
                    else if (responseInfo.ErrDesc == "3")
                    {
                        responseModel = ReturnModel.Error(-3, "手机号码存在重复");
                    }
                    else
                    {
                        responseModel = ReturnModel.Error(-4, "未知错误");
                    }
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // POST api/user
        public ResponseModel Post(int id, string method, [FromBody]ApiModel.UserStoreMoneyInModel model)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (method == "storemoney")
            {
                #region 会员充值
                if (string.IsNullOrEmpty(model.Value.ToString()) || string.IsNullOrEmpty(model.RealMoney.ToString()))
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }

                model.UserId = id;
                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.UserStoreMoneyAdd(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    if (responseInfo.StrObj == "1")
                    {
                        //充值成功
                        responseModel = ReturnModel.Success(responseInfo.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.Error(-2, "充值失败");
                    }
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                } 
                #endregion
            }
            else if (method == "storetimes")
            {
                #region 会员充次
                if (string.IsNullOrEmpty(model.Value.ToString()) || string.IsNullOrEmpty(model.RealMoney.ToString()))
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }

                model.UserId = id;
                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.UserStoreTimesIn(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    if (responseInfo.StrObj == "1")
                    {
                        //充值成功
                        responseModel = ReturnModel.Success(responseInfo.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.Error(-2, "充值失败");
                    }
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
                #endregion
            }
            else if (method == "integralexchange")
            {
                #region 积分兑换
                if (string.IsNullOrEmpty(model.Value.ToString()))
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }

                model.UserId = id;
                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.UserIntegralExchange(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    if (responseInfo.StrObj == "1")
                    {
                        //兑换成功
                        responseModel = ReturnModel.Success(responseInfo.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.Error(-2, "兑换失败");
                    }
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
                #endregion
            }
            
            return responseModel;
        }

        public ResponseModel Post(string saleadd, [FromBody]ApiModel.UserSalesAddModel model)
        {
            //销售后增加会员
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (string.IsNullOrEmpty(model.UserPhone) || string.IsNullOrEmpty(model.UserName))
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var requestJson = Helper.JsonSerializeObject(model);
            var responseInfo = fnRequestProxy.UserSalesAdd(requestJson, oToken);

            if (responseInfo.Status == 0)
            {
                if (responseInfo.StrObj == "1")
                {
                    //无提示
                    responseModel = ReturnModel.Error(-1, "无处理");
                }
                else if (responseInfo.StrObj == "2")
                {
                    //会员添加成功
                    //responseModel.ErrMsg = "会员添加成功";
                    //responseModel.Data = "2";
                    responseModel = ReturnModel.Error(-2, "仅会员添加成功");
                }
                else if (responseInfo.StrObj == "11")
                {
                    //电子账单发送成功
                    //responseModel.ErrMsg = "电子账单发送成功";
                    //responseModel.Data = "11";
                    responseModel = ReturnModel.Error(-11, "仅电子账单发送成功");
                }
                else if (responseInfo.StrObj == "12")
                {
                    //会员新增电子账单发送成功
                    responseModel = ReturnModel.Success(responseInfo.StrObj);
                }
                else
                {
                    responseModel = ReturnModel.Error(-2, "添加失败");
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        public ResponseModel Post(string behave, ApiModel.SingleValue model)
        {
            #region 删除会员记录
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (string.IsNullOrEmpty(model.val) || behave != "delete")
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }


            //var requestJson = Helper.JsonSerializeObject(saleListID);
            var responseInfo = fnRequestProxy.UserDel(model.val, oToken);

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

            return responseModel;
            #endregion
        }

        public ResponseModel Post(string import, [FromBody]List<ApiModel.MUserImportModel> modelList)
        {
            //List<ApiModel.UserDetailModle>
            #region 批量导入会员
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (modelList == null || import != "true")
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            var requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(modelList);
            var responseInfo = fnRequestProxy.importUser(requestJson, oToken);

            if (responseInfo.Status == 0)
            {
                responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApiModel.MUserImportErrModel>>(responseInfo.StrObj));
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
            #endregion
        }

        // PUT api/user/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
        }
    }
}
