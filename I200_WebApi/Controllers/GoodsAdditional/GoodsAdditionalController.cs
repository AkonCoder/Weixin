using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonLib;
using Controls;
using Models;

namespace I200_WebApi.Controllers.GoodsAdditional
{
    public class GoodsAdditionalController : ApiController
    {
        public ResponseModel GetBase(string method, string id = "")
        {
            //加料信息
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            switch (method)
            {
                case "getgoodsadditonalitemlist":

                    #region 获取该分类下所有加料，若分类ID为空，则获取全部加料

                    if (id == "")
                    {
                        id = "-1";
                    }
                    var responseitemInfo = fnRequestProxy.GetGoodsAdditionItemList(id, oToken);
                    if (responseitemInfo.Status == 0)
                    {
                        if (responseitemInfo.StrObj == "[]")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel =
                                ReturnModel.Success(
                                    Helper.JsonDeserializeObject<List<ApiModel.T_GoodsAdditionalItemModel>>(responseitemInfo.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                    #endregion

                case "getgoodsadditonlalist":

                    #region 获取全部分类列表

                    var responseInfo = fnRequestProxy.GetGoodsAdditionList(id, oToken);
                    if (responseInfo.Status == 0)
                    {

                        responseModel =
                            ReturnModel.Success(
                                Helper.JsonDeserializeObject<ApiModel.T_GoodsAdditionalModelList>(responseInfo.StrObj));

                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                    #endregion

                case "getallgoodsaddition":
                    #region 获取全部分类列表

                    var responseallInfo = fnRequestProxy.GetAllGoodsAddition(id, oToken);
                    if (responseallInfo.Status == 0)
                    {

                        responseModel =
                            ReturnModel.Success(
                                Helper.JsonDeserializeObject<ApiModel.T_GoodsAdditionalModelListAll>(responseallInfo.StrObj));

                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;

                    #endregion

                case "delgoodsadditonal":
                    #region 删除分类
                    var objDeleteadditonal = fnRequestProxy.DeleteGoodsAdditional(id, oToken);
                    if (objDeleteadditonal.Status == 0)
                    {
                        responseModel = ReturnModel.Success("");
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion

                case "delgoodsadditonalitem":
                    #region 删除加料
                    var objDeleteadditonalItem = fnRequestProxy.DeleteGoodsAdditionalItem(id, oToken);
                    if (objDeleteadditonalItem.Status == 0)
                    {
                        responseModel = ReturnModel.Success("");
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "getsinglegoodsadditonalitem":
                    #region 获取单个加料详情
                    var objSingleadditonaliatem = fnRequestProxy.GetSingleGoodsAdditonalitem(id, oToken);
                    if (objSingleadditonaliatem.Status == 0)
                    {
                        responseModel =
                            ReturnModel.Success(
                                Helper.JsonDeserializeObject<ApiModel.T_GoodsAdditionalItemModel>(objSingleadditonaliatem.StrObj));
                    }
                    else if (objSingleadditonaliatem.Status == -2)
                    {
                        responseModel = new ResponseModel
                        {
                            Status = -2,
                            Ver = "1.0",
                            ErrCode = 0,
                            ErrMsg = "此加料信息已删除"
                        };
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

        // POST api/<controller>
        public ResponseModel Post(string type, [FromBody]ApiModel.T_GoodsAdditionalModel goodsmodel)
        {
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (type == "addgoodaddtional")
            {
                #region 增加分类

                if (oToken.AccId == 0)
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }
                var model = new ApiModel.T_GoodsAdditionalModel
                {
                    accId = oToken.AccId,
                    //accId = 397,
                    className = goodsmodel.className
                };
                var requestJson = Helper.JsonSerializeObject(model);

                var responseInfo = fnRequestProxy.AddNewGoodsAddition(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    responseModel = ReturnModel.Success(responseInfo.StrObj);
                }
                else if (responseInfo.Status == -2)
                {
                    responseModel = ReturnModel.Error(-2, "已存在此分类信息");
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
                #endregion
            }
            else if (type == "modifyaddtional")
            {
                #region 修改分类信息
                var model = new ApiModel.T_GoodsAdditionalModel
                {
                    accId = oToken.AccId,
                    //accId = 397,
                    className = goodsmodel.className,
                    classID = goodsmodel.classID
                };
                if (model.classID == null || model.classID == 0)
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }

                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.UpdateGoodsAddition(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    responseModel = ReturnModel.Success("Success");
                }
                else if (responseInfo.Status == -2)
                {
                    responseModel = ReturnModel.Error(-2, "已存在此分类信息");
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
                #endregion
            }


            return responseModel;
        }

        public ResponseModel Post(string method, [FromBody]ApiModel.T_GoodsAdditionalItemModel model)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            model.accId = oToken.AccId;
            //model.accId = 397;
            if (method == "addgoodaddtionalitem")
            {
                #region 增加加料
                if (model.accId == 0)
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }
                var requestJson = Helper.JsonSerializeObject(model);

                var responseInfo = fnRequestProxy.AddNewGoodsAdditionItem(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    responseModel = ReturnModel.Success(responseInfo.StrObj);
                }
                else if (responseInfo.Status == -2)
                {
                    responseModel = ReturnModel.Error(-2, "已存在此加料信息");
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
                #endregion
            }
            else if (method == "modifyaddtionalitem")
            {
                #region 修改分类信息

                if (model.itemId == null || model.itemId == 0)
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }

                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.UpdateGoodsAdditionItem(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    responseModel = ReturnModel.Success("Success");
                }
                else if (responseInfo.Status == -2)
                {
                    responseModel = ReturnModel.Error(-2, "已存在此加料信息");
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
                #endregion
            }

            return responseModel;
        }
    }
}