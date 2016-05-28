using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Setting
{
    public class SettingController : ApiController
    {


        public string Get(int id)
        {
            return "";
        }

        public ResponseModel GetBase(string method)
        {
            //获得会员基础信息
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            switch (method)
            {
                #region 获取销售基本设置

                case "getBaseSaleConfig":
                    var getBaseSaleConfigResult = fnRequestProxy.GetBaseSaleConfig(oToken);
                    if (getBaseSaleConfigResult.Status == 0)
                    {
                        responseModel = ReturnModel.Success(getBaseSaleConfigResult.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                #endregion

                #region 获取销售预设折扣设置

                case "getDiscountSaleConfig":
                    var getDiscountSaleConfigResult = fnRequestProxy.GetDiscountSaleConfig(oToken);
                    if (getDiscountSaleConfigResult.Status == 0)
                    {
                        responseModel = ReturnModel.Success(getDiscountSaleConfigResult.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                #endregion

                #region 获取销售流水单号设置

                case "getSerialNumSaleConfig":
                    var getSerialNumSaleConfigResult = fnRequestProxy.GetSerialNumSaleConfig(oToken);
                    if (getSerialNumSaleConfigResult.Status == 0)
                    {
                        responseModel = ReturnModel.Success(getSerialNumSaleConfigResult.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                #endregion

                #region 获取商品库存提醒设置

                case "getStockAlertGoodsConfig":
                    var getStockAlertGoodsConfigResult = fnRequestProxy.GetStockAlertGoodsConfig(oToken);
                    if (getStockAlertGoodsConfigResult.Status == 0)
                    {
                        responseModel = ReturnModel.Success(getStockAlertGoodsConfigResult.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                #endregion

                #region 获取会员级别和折扣设置

                case "getUserDiscountConfig":
                    var getUserDiscountConfigResult = fnRequestProxy.GetUserDiscountConfig(oToken);
                    if (getUserDiscountConfigResult.Status == 0)
                    {
                        responseModel = ReturnModel.Success(getUserDiscountConfigResult.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                #endregion

                #region 获取会员分组设置

                case "getUserGroupList":
                    var getUserGroupListResult = fnRequestProxy.GetUserGroupList(oToken);
                    if (getUserGroupListResult.Status == 0)
                    {
                        responseModel = ReturnModel.Success(getUserGroupListResult.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                #endregion

                #region 获取会员称谓设置

                case "getUserNickNameList":
                    var getUserNickNameListResult = fnRequestProxy.GetUserNickNameList(oToken);
                    if (getUserNickNameListResult.Status == 0)
                    {
                        responseModel = ReturnModel.Success(getUserNickNameListResult.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                #endregion

                #region 获取会员标签设置

                case "getUserTagList":
                    var getUserTagListResult = fnRequestProxy.GetUserTagList(oToken);
                    if (getUserTagListResult.Status == 0)
                    {
                        responseModel = ReturnModel.Success(getUserTagListResult.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                #endregion

                #region 获取会员计次卡过期天数设置

                case "getUserTimesCardOutTimeConfig":
                    var getUserTimesCardOutTimeConfigResult = fnRequestProxy.GetUserTimesCardOutTimeConfig(oToken);
                    if (getUserTimesCardOutTimeConfigResult.Status == 0)
                    {
                        responseModel = ReturnModel.Success(getUserTimesCardOutTimeConfigResult.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                #endregion

                #region 获取店员列表

                case "getAccountUserList":
                    var getAccountUserListResult = fnRequestProxy.GetAccountUserList(oToken);
                    if (getAccountUserListResult.Status == 0)
                    {
                        responseModel = ReturnModel.Success(getAccountUserListResult.StrObj);
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
    }
}