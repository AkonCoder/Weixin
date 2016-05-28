using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Goods
{
    public class GoodsController : ApiController
    {
        //// GET api/goods
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public ResponseModel GetGoodsList(int Page, int ClassType, int ClassVal, string KeyWord, string SortName, string SortType, decimal MinVal, decimal MaxVal)
        {

            //获得商品列表
            var responseModel = new ResponseModel();
            var objQuery = new ApiModel.GoodsQuery();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            objQuery.Page = Page;
            objQuery.ClassType = (ClassType == null ? 0 : ClassType);
            objQuery.ClassVal = (ClassVal == null ? 0 : ClassVal);
            objQuery.KeyWord = (KeyWord == null ? "" : KeyWord);
            objQuery.SortName = (SortName == null ? "" : SortName);
            objQuery.SortType = (SortType == null ? "" : SortType);
			if (MinVal == 0 && MaxVal == 0)
			{
				objQuery.MinVal = null;
				objQuery.MaxVal = null;
			}
			else
			{
				objQuery.MinVal = MinVal;
				if (MaxVal == 0)
				{
					objQuery.MaxVal = null;
				}
				else
				{
					objQuery.MaxVal = MaxVal;
				}
			}
            objQuery.AccId = oToken.AccId;

            var objString = Helper.JsonSerializeObject(objQuery);

            var objGoodsList = fnRequestProxy.GetGoodsList(objString, oToken);
            if (objGoodsList.Status == 0)
            {
                if (objGoodsList.ErrDesc == "none")
                {
                    responseModel = ReturnModel.SuccessNone();
                }
                else
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.GoodsInfoModel>(objGoodsList.StrObj));
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        public ResponseModel GetBase(string method,string keyword="")
        {
            //获得商品基础信息
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            switch (method)
            {
                case "class":
                    #region 商品分类
                    var responseInfo = fnRequestProxy.GoodsGetClass(oToken);
                    if (responseInfo.Status == 0)
                    {
                        if (responseInfo.StrObj == "none")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.GoodsClass>(responseInfo.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "search":
                    #region 商品搜索
                    var objUser = fnRequestProxy.GoodsSearch(keyword, oToken);
                    if (objUser.Status == 0)
                    {
                        if (objUser.StrObj == "none")
                        {
                            responseModel = ReturnModel.SuccessNone();
                        }
                        else
                        {
                            responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<List<ApiModel.GoodsTip>>(objUser.StrObj));
                        }
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "chkbarcode":
                    #region 检测重复条码
                    var objBarcode = fnRequestProxy.ExistsBarcode(keyword, oToken);
                    if (objBarcode.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.GoodsExists>(objBarcode.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "addmaxclass":
                    #region 增加商品大分类
                    var objMaxClass = fnRequestProxy.GoodsMaxClassAdd(keyword, oToken);
                    if (objMaxClass.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objMaxClass.StrObj);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "getgoodsinfo":
                    #region 获取商品汇总信息
                    var objGoodsInfo = fnRequestProxy.getGoodsInfo(keyword, oToken);
                    if (objGoodsInfo.Status == 0)
                    {
                        responseModel = ReturnModel.Success(Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.GoodsSummaryModel>(objGoodsInfo.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "getshareadd":
                    #region 获取商品分享地址
                    var objGoodsAdd = fnRequestProxy.getGoodsAdd(keyword, oToken);
                    if (objGoodsAdd.Status == 0)
                    {
                        responseModel = new ResponseModel
                        {
                            Status = 0,
                            Ver = "1.0",
                            ErrCode = 0,
                            ErrMsg = objGoodsAdd.ErrDesc,
                            Data = objGoodsAdd.StrObj
                        };
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    break;
                    #endregion
                case "skulist":
                    #region 得到SKUList
                    
                    var objSkuList = fnRequestProxy.getGoodsSkuList(keyword, oToken);
                    if (objSkuList.Status == 0)
                    {
                        List<ApiModel.SkuItem> itemList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApiModel.SkuItem>>(objSkuList.StrObj);
                        responseModel = ReturnModel.Success(itemList);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    #endregion
                    break;
                case "commonlist":
                    #region 获取常用商品列表（iPad）
                    var objCommonList = fnRequestProxy.GetCommonGoodsList(keyword, oToken);
                    if (objCommonList.Status == 0)
                    {
                        ApiModel.GoodsInfoModel itemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiModel.GoodsInfoModel>(objCommonList.StrObj);
                        responseModel = ReturnModel.Success(itemList);
                    }
                    else
                    {
                        responseModel = ReturnModel.TokenFail();
                    }
                    #endregion
                    break;
                case "addcommon":
                    #region 新增常用商品（iPad）
                    var objAddCommon = fnRequestProxy.AddCommonGoods(keyword, oToken);
                    if (objAddCommon.Status == 0)
                    {
                        responseModel = ReturnModel.Success(objAddCommon.StrObj);
                    }
                    else
                    {
                        responseModel = new ResponseModel
                        {
                            Status = objAddCommon.Status,
                            Ver = "1.0",
                            ErrCode = objAddCommon.Status,
                            ErrMsg=objAddCommon.ErrDesc,
                            Data = ""
                        };
                    }
                    #endregion
                    break;
                case "delcommon":
                    #region 删除常用商品（iPad）
                    var objDelCommon = fnRequestProxy.DelCommonGoods(keyword, oToken);
                    if (objDelCommon.Status == 0)
                    {
                        ReturnModel.Success(objDelCommon.StrObj);
                    }
                    else
                    {
                        responseModel = new ResponseModel
                        {
                            Status = objDelCommon.Status,
                            Ver = "1.0",
                            ErrCode = objDelCommon.Status,
                            ErrMsg = objDelCommon.ErrDesc,
                            Data = ""
                        };
                    }
                    #endregion
                    break;
                case "isshowcostpricebypwd":
                    #region
                    var objIsShow = fnRequestProxy.IsShowCostPriceByPwd(keyword, oToken);
                    if (objIsShow.Status==0)
                    {
                        ReturnModel.Success(objIsShow.StrObj);
                    }
                    else
                    {
                        responseModel = new ResponseModel
                        {
                            Status = objIsShow.Status,
                            Ver = "1.0",
                            ErrCode = objIsShow.Status,
                            ErrMsg = objIsShow.ErrDesc,
                            Data = ""
                        };
                    }
                    #endregion
                    break;
				case "thresholdInfo":
					#region 商品库存预警信息
					var thresholdInfo = fnRequestProxy.GetGoodsThresholdInfo(oToken);
					if (thresholdInfo.Status == 0)
					{
						responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<Dictionary<string, decimal>>(thresholdInfo.StrObj));
					}
					else
					{
						responseModel =ReturnModel.TokenFail();
					}
					#endregion
		            break;
            }

            string p = Newtonsoft.Json.JsonConvert.SerializeObject(responseModel);

            return responseModel;
        }

        public ResponseModel GetCheckExists(int maxid, string minclass)
        {
            //增加商品小分类
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var objGoods = new ApiModel.GoodsInfo();
            objGoods.gMaxID = maxid;
            objGoods.gMinName = minclass;

            var objString = Helper.JsonSerializeObject(objGoods);

            var objClassId = fnRequestProxy.GoodsMinClassAdd(objString, oToken);
            if (objClassId.Status == 0)
            {
                responseModel = ReturnModel.Success(objClassId.StrObj);
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        public ResponseModel GetCheckExists(int maxid, int minid, string gname)
        {
            //检测重复商品信息
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var objGoods = new ApiModel.GoodsInfo();
            objGoods.gMaxID = maxid;
            objGoods.gMinID = minid;
            objGoods.gName = gname;

            var objString = Helper.JsonSerializeObject(objGoods);

            var objUser = fnRequestProxy.ExistsGoods(objString, oToken);
            if (objUser.Status == 0)
            {
                responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.GoodsExists>(objUser.StrObj));
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // GET api/goods/5
        public ResponseModel Get(int id)
        {
            //获得商品详情
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            var objGoods = fnRequestProxy.GoodsDetail(id.ToString(), oToken);
            if (objGoods.Status == 0)
            {
                if (objGoods.ErrDesc == "")
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.GoodsItemModel>(objGoods.StrObj));
                }
                else
                {
                    responseModel = ReturnModel.Error(-1, objGoods.ErrDesc);
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        // POST api/goods
        public ResponseModel Post([FromBody]ApiModel.GoodsInfo model)
        {
            var responseModel = new ResponseModel();
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            if (model.gMaxID == null || string.IsNullOrEmpty(model.gName) || model.gPrice == null)
            {
                //缺少必须参数
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
                return responseModel;
            }

            model.gOperatorID = oToken.UserId;

            if (model.SupplierId == null || model.SupplierId == 0)
            {
                model.SupplierId = 0;
            }

            var requestJson = Helper.JsonSerializeObject(model);
            var responseInfo = fnRequestProxy.GoodsAdd(requestJson, oToken);

            if (responseInfo.Status == 0)
            {
                if (responseInfo.ErrDesc == "")
                {
                    responseModel = ReturnModel.Success(responseInfo.StrObj);
                }
                else
                {
                    if (responseInfo.ErrDesc == "-2")
                    {
                        responseModel = ReturnModel.Error(-2, "存在相同名称商品");
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




        public ResponseModel Post(int id, string method, [FromBody]ApiModel.StockInOut model)
        {
            var responseModel = new ResponseModel();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            var ipAddress= Request.GetClientIpAddress();

            if (method == "stockin")
            {
                #region 商品入库
                model.GoodsId = id;
                model.OperatorId = oToken.UserId;
                model.OperatorIp = ipAddress;
                if (model.GoodsNum == 0  || model.GoodsId <= 0)
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }

                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.GoodsStockIn(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    if (responseInfo.StrObj == "1")
                    {
                        //成功
                        responseModel = ReturnModel.Success("Success");
                    }
                    else
                    {
                        responseModel = ReturnModel.Error(-2, "失败");
                    }
                }
                else
                {
                    responseModel = ReturnModel.TokenFail();
                }
                #endregion
            }
            else if (method == "stockout")
            {
                #region 商品出库
                model.GoodsId = id;
                model.OperatorId = oToken.UserId;
                model.OperatorIp = ipAddress;
                if (model.GoodsNum == null || model.GoodsId == null)
                {
                    //缺少必须参数
                    responseModel = ReturnModel.Error(-3, "缺少必须参数");
                    return responseModel;
                }

                var requestJson = Helper.JsonSerializeObject(model);
                var responseInfo = fnRequestProxy.GoodsStockOut(requestJson, oToken);

                if (responseInfo.Status == 0)
                {
                    if (responseInfo.StrObj == "1")
                    {
                        //成功
                        responseModel = ReturnModel.Success("Success");
                    }
                    else
                    {
                        responseModel = ReturnModel.Error(-2, "失败");
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

        // POST api/goods/5
        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel Post(int id, [FromBody]ApiModel.GoodsInfo model)
        {
            var responseModel = new ResponseModel();

            if (id < 0 || model.gMaxID < 0 || model.gName.Trim().Length < 1 || model.gPrice < 0)
            {
                responseModel = ReturnModel.Error(-3, "缺少必须参数");
            }
            model.gid = id;
            var requestJson = Helper.JsonSerializeObject(model);
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            var responseInfo = fnRequestProxy.GoodsEdit(requestJson, oToken);
            if (responseInfo.Status == 0)
            {
                if (responseInfo.StrObj == "ok")
                {
                    //成功
                    responseModel = ReturnModel.Success("ok");
                }
                else
                {
                    responseModel = ReturnModel.Error(-2, responseInfo.StrObj);
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }


        public ResponseModel Post(string inventory, [FromBody] List<ApiModel.GoodsInventory> model)
        {
            var responseModel = new ResponseModel();

            foreach (ApiModel.GoodsInventory item in model)
            {
                item.Ip = Request.GetClientIpAddress();
            }
            var requestJson = Helper.JsonSerializeObject(model);
            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();
            var responseInfo = fnRequestProxy.GoodsInventory(requestJson, oToken);
            
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

        public ResponseModel Post(string behave, ApiModel.SingleValue model)
        {
            #region 删除商品记录
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
            var responseInfo = fnRequestProxy.GoodsDelete(model.val, oToken);

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

                //responseModel = ReturnModel.Success(responseInfo.StrObj);
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
            #endregion
        }

        // PUT api/goods/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/goods/5
        public void Delete(int id)
        {
        }
    }
}
