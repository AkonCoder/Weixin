using CommonLib;
using Controls;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.StockLog
{
    public class StockLogController : ApiController
    { 

        // GET api/stocklog
        /// <summary>
        /// 得到出入库列表
        /// </summary>
        /// <param name="dateType">时间类别</param>
        /// <param name="diffday">时间间隔</param>
        /// <param name="logType">日志类型</param>
        /// <param name="maxName">大分类名称</param>
        /// <param name="minName">小分类名称</param>
        /// <param name="gName">商品名称</param>
        /// <param name="gid">产品ID</param>
        /// <param name="iPage">当前页数</param>
        /// <param name="bgDate">开始日期</param>
        /// <param name="edDate">结束日期</param>
        /// <returns></returns>
        public ResponseModel GetLogList(string dateType, int diffday, int logType, string maxName, string minName, string gName, int gid, int iPage, DateTime? bgDate = null, DateTime? edDate = null, string dataType="list")
        {
            //获得商品出入库列表
            var responseModel = new ResponseModel();
            var objQuery = new ApiModel.GetStockLogPost();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            objQuery.accID = oToken.AccId;
            objQuery.logType = logType;
            objQuery.maxName = maxName;
            objQuery.minName = minName;
            objQuery.gName = gName;
            objQuery.gid = gid;
            objQuery.iPage = iPage;


            if (dateType == "time")
            {
                //时间间隔
                bgDate =DateTime.Now;
                if (diffday > 0)
                {
                    bgDate = bgDate.Value.AddDays(Convert.ToDouble(0 - diffday));
                }
                edDate = DateTime.Now;
            }
            else if (dateType == "oth")
            {
                if (bgDate == null)
                {
                    bgDate = DateTime.Now.Date;
                }
                if (edDate == null)
                {
                    edDate = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));
                }
            }
            else
            {
                //不限定时间范围
                bgDate = DateTime.Now.Date;
                edDate = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));
            }

            if (bgDate != null)
            {
                objQuery.bgDate = bgDate.Value.Date;
            }
            if (edDate != null)
            {
                objQuery.edDate = edDate.Value.Add(new TimeSpan(23, 59, 59));
            }


            
            var objString = Helper.JsonSerializeObject(objQuery);

            ProxyResponseModel objGoodsList = new ProxyResponseModel();
            objGoodsList.Status = -1;

            if (dataType == "count")
            {
                objGoodsList = fnRequestProxy.GetStockLogCount(objString, oToken);
            }
            else
            {
                objGoodsList = fnRequestProxy.GetStockLogList(objString, oToken);
            }

            if (objGoodsList.Status == 0)
            {
                if (objGoodsList.ErrDesc == "none")
                {
                    responseModel = ReturnModel.SuccessNone();
                }
                else
                {
                    if (dataType == "count")
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.GoodsStockCount>(objGoodsList.StrObj));
                    }
                    else
                    {
                        responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.GoodsStockLog>(objGoodsList.StrObj));
                    }
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }

        /// <summary>
        /// 得到出入库汇总
        /// </summary>
        /// <param name="dateType">时间类别</param>
        /// <param name="diffday">时间间隔</param> 
        /// <param name="maxName">大分类名称</param>
        /// <param name="minName">小分类名称</param>
        /// <param name="gName">商品名称</param>
        /// <param name="gid">产品ID</param> 
        /// <param name="bgDate">开始日期</param>
        /// <param name="edDate">结束日期</param>
        /// <returns></returns>
        public ResponseModel GetLogCount(string dateType, int diffday, string maxName, string minName, string gName, int gid, DateTime? bgDate = null, DateTime? edDate = null)
        {
            //获得商品出入库列表
            var responseModel = new ResponseModel();
            var objQuery = new ApiModel.GetStockLogPost();

            var fnRequestProxy = new RequestProxy();
            var oToken = Request.GetAccId();

            objQuery.accID = oToken.AccId;
            objQuery.logType = 0;
            objQuery.maxName = maxName;
            objQuery.minName = minName;
            objQuery.gName = gName;
            objQuery.gid = gid;
            objQuery.iPage = 1;


            if (dateType == "time")
            {
                //时间间隔
                bgDate = DateTime.Now;
                if (diffday > 0)
                {
                    bgDate = bgDate.Value.AddDays(Convert.ToDouble(0 - diffday));
                }
                edDate = DateTime.Now;
            }
            else if (dateType == "oth")
            {
                if (bgDate == null)
                {
                    bgDate = DateTime.Now.Date;
                }
                if (edDate == null)
                {
                    edDate = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));
                }
            }
            else
            {
                //不限定时间范围
                bgDate = DateTime.Now.Date;
                edDate = DateTime.Now.Date.Add(new TimeSpan(23, 59, 59));
            }

            if (bgDate != null)
            {
                objQuery.bgDate = bgDate.Value.Date;
            }
            if (edDate != null)
            {
                objQuery.edDate = edDate.Value.Add(new TimeSpan(23, 59, 59));
            }



            var objString = Helper.JsonSerializeObject(objQuery);

            ProxyResponseModel objGoodsList =fnRequestProxy.GetStockLogCount(objString, oToken); 

            if (objGoodsList.Status == 0)
            {
                if (objGoodsList.ErrDesc == "none")
                {
                    responseModel = ReturnModel.SuccessNone();
                }
                else
                {
                    responseModel = ReturnModel.Success(Helper.JsonDeserializeObject<ApiModel.GoodsStockCount>(objGoodsList.StrObj));
                }
            }
            else
            {
                responseModel = ReturnModel.TokenFail();
            }

            return responseModel;
        }


        //// POST api/stocklog
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/stocklog/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/stocklog/5
        //public void Delete(int id)
        //{
        //}
    }
}
