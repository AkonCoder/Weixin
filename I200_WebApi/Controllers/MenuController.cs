using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Controls;
using Models;

namespace I200_WebApi
{
    public class MenuController : ApiController
    {
        public ResponseModel GetCacheHaseList(string hashname)
        {

            ApiModel.CacheHashListModel modelList = new ApiModel.CacheHashListModel();

            #region 测试静态model

            ApiModel.CacheMenuBase model = new ApiModel.CacheMenuBase
            {
                Id = 1,
                Title = "新增商品",
                Icon = "Menus.Icon.Goods.New",
                CanMove = true,
                CanHide = true,
                CanOffLine = false,
                Show = true,
                Action = "Push|GoodsAdd",
                Tip = 1,
                Start = 0,
                Expire = 0
            };
            ApiModel.CacheMenuBase model2 = new ApiModel.CacheMenuBase
            {
                Id = 2,
                Title = "新增会员",
                Icon = "Menus.Icon.Member.New",
                CanMove = true,
                CanHide = true,
                CanOffLine = false,
                Show = true,
                Action = "Push|MemberAdd",
                Tip = 3,
                Start = 0,
                Expire = 0
            };
            ApiModel.CacheMenuBase model3 = new ApiModel.CacheMenuBase
            {
                Id = 3,
                Title = "收银记账",
                Icon = "Menus.Icon.Cash",
                CanMove = true,
                CanHide = true,
                CanOffLine = false,
                Show = true,
                Action = "Push|SaleAdd",
                Tip = 1,
                Start = 0,
                Expire = 0
            };
            ApiModel.CacheMenuBase model4 = new ApiModel.CacheMenuBase
            {
                Id = 4,
                Title = "支出管理",
                Icon = "Menus.Icon.Expenses",
                CanMove = true,
                CanHide = true,
                CanOffLine = false,
                Show = true,
                Action = "Push|ExpenditureManage",
                Tip = 1,
                Start = 1449126482,
                Expire = 1449126542
            };
            ApiModel.CacheMenuBase model5 = new ApiModel.CacheMenuBase
            {
                Id = 5,
                Title = "商品管理",
                Icon = "Menus.Icon.Goods.Manage",
                CanMove = true,
                CanHide = true,
                CanOffLine = true,
                Show = true,
                Action = "Push|GoodsManage",
                Tip = 1,
                Start = 0,
                Index = 1,
                Expire = 0
            };
            ApiModel.CacheMenuBase model6 = new ApiModel.CacheMenuBase
            {
                Id = 6,
                Title = "会员管理",
                Icon = "Menus.Icon.Member.Manage",
                CanMove = true,
                CanHide = true,
                CanOffLine = false,
                Show = true,
                Action = "Push|MemberManage",
                Tip = 0,
                Start = 0,
                Expire = 0
            };
            ApiModel.CacheMenuBase model7 = new ApiModel.CacheMenuBase
            {
                Id = 7,
                Title = "查询销售",
                Icon = "Menus.Icon.Sales.Query",
                CanMove = true,
                CanHide = true,
                CanOffLine = false,
                Show = true,
                Action = "Push|SaleQuery"
            };
            ApiModel.CacheMenuBase model8 = new ApiModel.CacheMenuBase
            {
                Id = 8,
                Title = "智能分析",
                Icon = "Menus.Icon.Analysis",
                CanMove = true,
                CanHide = false,
                CanOffLine = false,
                Show = true,
                Action = "Push|AnalysisManage",
                Index = 99,
                Tip = 3
            };
            ApiModel.CacheMenuBase model9 = new ApiModel.CacheMenuBase
            {
                Id = 99,
                Title = "更多",
                Icon = "Menus.Icon.More",
                CanMove = false,
                CanHide = false,
                CanOffLine = false,
                Show = true,
                Index = 200
            };
            ApiModel.CacheMenuBase model10 = new ApiModel.CacheMenuBase
            {
                Id = 9,
                Title = "供应商管理",
                Icon = "Menus.Icon.Supplier",
                CanMove = true,
                CanHide = true,
                CanOffLine = false,
                Show = true,
                Index = 300
            };

            #endregion

            modelList.Hase = "2";
            if (hashname!=modelList.Hase)
            {
                modelList.MenubaseList.Add(model);
                modelList.MenubaseList.Add(model2);
                modelList.MenubaseList.Add(model3);
                modelList.MenubaseList.Add(model4);
                modelList.MenubaseList.Add(model5);
                modelList.MenubaseList.Add(model6);
                modelList.MenubaseList.Add(model7);
                modelList.MenubaseList.Add(model8);
                modelList.MenubaseList.Add(model9);
                modelList.MenubaseList.Add(model10);
            }
            var responseModel = ReturnModel.Success(modelList);
            return responseModel;
        }
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}