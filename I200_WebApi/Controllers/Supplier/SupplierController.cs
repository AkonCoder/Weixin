using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Supplier
{
    public class SupplierController : ApiController
    {
        public ResponseModel GetBase(string method, string id = "",int pageIndex=1)
        {
            //供应商信息
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            switch (method)
            {
                case "supplierlist":
                    #region 供应商列表
                    var responseInfo = fnRequestProxy.GetSupplierList(id, oToken);
                    if (responseInfo.Status == 0)
                    {
                        if (responseInfo.StrObj == "")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<List<ApiModel.SupplierModel>>(responseInfo.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "supplierlistnew":
                    #region 供应商列表
                    var responseInfonew = fnRequestProxy.GetSupplierListNew(id, oToken);
                    if (responseInfonew.Status == 0)
                    {
                        if (responseInfonew.StrObj == "")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SupplierMobileModelList>(responseInfonew.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "singlesupplier":
                    #region 单个供应商
                    var objSingleSupplier = fnRequestProxy.GetSingleSupplier(id, oToken);
                    if (objSingleSupplier.Status == 0)
                    {
                        if (objSingleSupplier.StrObj == "")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SupplierModel>(objSingleSupplier.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "suppliergoods":
                    #region 供应商商品
                    ApiModel.GoodsQuery gQuery= new ApiModel.GoodsQuery();
                    gQuery.Page = pageIndex;
                    gQuery.SupplierId = int.Parse(id);
                    var query = Helper.JsonSerializeObject(gQuery);

                    var objSupplierGoods = fnRequestProxy.GetSupplierGoods(query, oToken);
                    if (objSupplierGoods.Status == 0)
                    {
                        if (objSupplierGoods.StrObj == "")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.SupplierGoodsDetail>(objSupplierGoods.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                    break;
                case "delsupplier":
                    #region 删除供应商
                    var objDeleteSupplier = fnRequestProxy.DeleteSupplier(id, oToken);
                    if (objDeleteSupplier.Status == 0)
                    {
                        responseModel = ReturnModel.Success("");
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion       
                case "getgsrepaymentlist":
                    #region 获取款项记录列表
                    ApiModel.T_GoodsSupplier_Query gsRepaySupplierQuery = new ApiModel.T_GoodsSupplier_Query();
                    gsRepaySupplierQuery.pageIndex = pageIndex;
                    gsRepaySupplierQuery.supplierId = int.Parse(id);
                    gsRepaySupplierQuery.column=String.Empty;
                    var gsQuery = Helper.JsonSerializeObject(gsRepaySupplierQuery);
                    var responseInfogslist = fnRequestProxy.GetGsRepaymentList(gsQuery, oToken);
                    if (responseInfogslist.Status == 0)
                    {
                        responseModel = responseInfogslist.StrObj == "" ? ReturnModel.SuccessNone() : ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.T_GoodsSupplier_RepaymentListModel>(responseInfogslist.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "getgsrepaymentrecordlist":
                    #region 供应商列表

                    ApiModel.T_GSRepaymentRecordQuery gsRepayRecordQueryQuery = new ApiModel.T_GSRepaymentRecordQuery
                    {
                        RepaymentId = int.Parse(id),
                        SupplierId = pageIndex
                    };
                    var gsrecordQuery = Helper.JsonSerializeObject(gsRepayRecordQueryQuery);
                    var responseInfogrecordslist = fnRequestProxy.Getgsrepaymentrecordlist(gsrecordQuery, oToken);
                    if (responseInfogrecordslist.Status == 0)
                    {
                        responseModel = responseInfogrecordslist.StrObj == "" ? ReturnModel.SuccessNone() : ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.T_GSRepaymentRecordList>(responseInfogrecordslist.StrObj));
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

        public ResponseModel Post(string method, [FromBody]ApiModel.SupplierModel model)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            model.AccId=oToken.AccId;

            if (method == "add")
            {
                #region 新增代理商
                if (model.AccId == 0)
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }
                var requestJson = Helper.JsonSerializeObject(model);

                var responseInfo = fnRequestProxy.AddNewSupplier(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    responseModel = ReturnModel.Success(responseInfo.StrObj);
                }
                else if (responseInfo.Status == -2)
                {
                    responseModel = ReturnModel.Error(-2, "已存在此供货商信息");
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
                #endregion
            }
            else if (method == "modify")
            {
                #region 修改供应商信息

                if (model.Id == null || model.Id == 0)
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }

                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.ModifySupplier(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    responseModel = ReturnModel.Success("Success");
                }
                else
                {
                    responseModel = ReturnModel.Error(-2, "修改失败");
                }
                #endregion
            }

            return responseModel;
        }

        public ResponseModel Post(string type, [FromBody] ApiModel.T_GoodsSupplier_RepaymentModel model)
        {
            var responseModel=new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            model.AccId = oToken.AccId;
            model.AccId = 397;
            if (type == "add")
            {
                #region 新增款项记录
                if (model.AccId == 0)
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }
                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.AddGsRepayment(requestJson, oToken);
                if (responseInfo.Status == 0)
                {
                    responseModel = ReturnModel.Success(responseInfo.StrObj);
                }
                else if (responseInfo.Status == -2)
                {
                    responseModel = ReturnModel.Error(-2, "保存失败，请重试");
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
                #endregion
            }
            return responseModel;
        }

        public ResponseModel Post(string function, [FromBody] ApiModel.T_GoodsSupplier_RepaymentRecordModel model)
        {
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            model.AccId = oToken.AccId;
            if (function == "add")
            {
                #region 新增款项记录
                if (model.AccId == 0)
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }
                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.AddGsRepaymentRecord(requestJson, oToken);
                if (responseInfo.Status == 0)
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.T_GoodsSupplier_RepaymentModel>(responseInfo.StrObj));
                }
                else if (responseInfo.Status == -2)
                {
                    responseModel = ReturnModel.Error(-2, "保存失败，请重试");
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
