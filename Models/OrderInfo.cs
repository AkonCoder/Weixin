using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    /// <summary>
    /// 移动雄产品信息
    /// </summary>
    public class OrderInfo
    {
        #region 移动端产品 基本
        /// <summary>
        /// 移动端产品 基本
        /// </summary>
        public partial class T_OrderMobileProjectBasis
        {
            /// <summary>
            /// 移动端产品ID
            /// </summary>
            public int mpId { get; set; }
            /// <summary>
            /// 产品ID
            /// </summary>
            public int busId { get; set; }
            /// <summary>
            /// IOS KEY
            /// </summary>
            public string StoreKey { get; set; }
            /// <summary>
            ///产品名称
            /// </summary>
            public string mpName { get; set; }
            /// <summary>
            ///  产品包含数量
            /// </summary>
			public decimal mpQuantity { get; set; }
			/// <summary>
			/// 应付金额
			/// </summary>
			public decimal mpAbleMoney { get; set; }
            /// <summary>
            /// 产品金额
            /// </summary>
            public decimal mpMoney { get; set; }
            /// <summary>
            /// 产品图片
            /// </summary>
            public string mpPic { get; set; }

            /// <summary>
            /// 产品描述
            /// </summary>
            public string mpComment { get; set; }
        }
        #endregion

        #region OrderCreateStatus 订单生成结果
        /// <summary>
        /// 订单生成结果
        /// </summary>
        public class OrderCreateStatus
        {
            /// <summary>
            /// 订单状态
            /// </summary>
            public int Status { get; set; }

            /// <summary>
            /// 订单Id
            /// </summary>
            public int OrderId { get; set; }

            /// <summary>
            /// 订单编号
            /// </summary>
            public string OrderNo { get; set; }

            /// <summary>
            /// 订单业务名称
            /// </summary>
            public string OrderBusName { get; set; }

            /// <summary>
            /// 订单支付金额
            /// </summary>
            public decimal OrderMoney { get; set; }

            /// <summary>
            /// 错误描述
            /// </summary>
            public string ErrMsg { get; set; }
        }
        #endregion
        #region OrderShowModel 订单确认信息
        /// <summary>
        /// 订单确认信息
        /// </summary>
        public class OrderShowModel
        {
            /// <summary>
            /// 原业务Id BusId改为 GoodsId 业务和实体统一使用GoodsId
            /// </summary>
            public int GoodsId { get; set; }
            /// <summary>
            /// 商品类型 1-业务 2-实体 3-第三方jd
            /// </summary>
            public int GoodsType { get; set; }
            /// <summary>
            /// 业务类型
            /// </summary>
            public int BusType { get; set; }

            /// <summary>
            /// 商品数量
            /// </summary>
            public int GoodsQuantity { get; set; }

            /// <summary>
            /// 显示名称
            /// </summary>
            public string DisplayName { get; set; }

            /// <summary>
            /// 附加描述
            /// </summary>
            public string AdditionDesc { get; set; }

            /// <summary>
            /// 应付金额
            /// </summary>
            public decimal AbleMoney { get; set; }

            /// <summary>
            /// 实付金额
            /// </summary>
            public decimal RealMoney { get; set; }

            /// <summary>
            /// 折扣金额
            /// </summary>
            public decimal DiscountMoney { get; set; }

            /// <summary>
            /// 优惠券编码
            /// </summary>
            public string CouponCode { get; set; }

            /// <summary>
            /// 优惠金额
            /// </summary>
            public decimal CouponMoney { get; set; }


            /// <summary>
            /// 具体购买项目Item GoodsType=2 时需要赋值
            /// </summary>
            public List<OrderProjectListModel> ListModels { get; set; }

            /// <summary>
            /// 可用优惠券列表
            /// </summary>
            public OrderCouponModel CouponList { get; set; }

            /// <summary>
            /// 积分抵值金额
            /// </summary>
            public decimal CommuteIntegralMoney { get; set; }
            /// <summary>
            /// 实际积分抵值金额（便于给页面传参 实际处理时以 CommuteIntegralMoney 为准）
            /// </summary>
            public decimal ActualCommuteIntegralMoney { get; set; }
            /// <summary>
            /// 总可用积分数
            /// </summary>
            public int TotalIntegral { get; set; }
            /// <summary>
            /// 消费积分数
            /// </summary>
            public int CommuteIntegral { get; set; }
            /// <summary>
            /// 剩余积分数
            /// </summary>
            public int LeftIntegral { get; set; }
            /// <summary>
            /// 安卓端手机抵现文字
            /// </summary>
            public string MobileIntegralShowText { get; set; }
        }
        #endregion

        #region OrderCouponModel 优惠券列表
        /// <summary>
        /// 优惠券列表
        /// </summary>
        public class OrderCouponModel
        {

            /// <summary>
            /// 优惠券详细列表
            /// </summary>
            public List<OrderCouponListModel> CouponListModels { get; set; }

            /// <summary>
            /// 记录总数
            /// </summary>
            public int ListMaxRows { get; set; }

            /// <summary>
            /// 分页Html
            /// </summary>
            public string PageHtml { get; set; }
        }
        #endregion
        #region OrderProjectListModel 业务计划Item
        /// <summary>
        /// 业务计划Item
        /// </summary>
        public class OrderProjectListModel
        {
            /// <summary>
            /// id
            /// </summary>
            public int id { get; set; }

            /// <summary>
            /// 业务ID
            /// </summary>
            public int busId { get; set; }

            /// <summary>
            /// 业务ItemId
            /// </summary>
            public int itemId { get; set; }

            /// <summary>
            /// 业务数量
            /// </summary>
            public int itemQuantity { get; set; }

            /// <summary>
            /// 业务名称
            /// </summary>
            public string itemName { get; set; }

            /// <summary>
            /// 业务描述
            /// </summary>
            public string itemDesc { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime createDate { get; set; }
            /// <summary>
            /// 业务单位
            /// </summary>
            public string itemUnit { get; set; }
        }
        #endregion
        #region OrderCouponListModel 优惠券详情Model
        /// <summary>
        /// 优惠券详情Model
        /// </summary>
        public class OrderCouponListModel
        {
            /// <summary>
            /// 优惠券Id
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 优惠券类型
            /// </summary>
            public int CouponType { get; set; }

            /// <summary>
            /// 优惠券类型Desc
            /// </summary>
            public string CouponTypeDesc { get; set; }

            /// <summary>
            /// 绑定类型
            /// </summary>
            public int BindType { get; set; }

            /// <summary>
            /// 绑定类型Desc
            /// </summary>
            public string BindTypeDesc { get; set; }

            /// <summary>
            /// 绑定分类id
            /// </summary>
            public int BindValue { get; set; }

            /// <summary>
            /// 绑定名称
            /// </summary>
            public string BindName { get; set; }

            /// <summary>
            /// 限制类型
            /// </summary>
            public int RuleType { get; set; }

            /// <summary>
            /// 限制类型Desc
            /// </summary>
            public string RuleTypeDesc { get; set; }

            /// <summary>
            /// 限制金额
            /// </summary>
            public int RuleValue { get; set; }

            /// <summary>
            /// 优惠券描述
            /// </summary>
            public string CouponDesc { get; set; }

            /// <summary>
            /// 优惠券批次ID
            /// </summary>
            public int GroupId { get; set; }

            /// <summary>
            /// 优惠券Code
            /// </summary>
            public string CouponId { get; set; }

            /// <summary>
            /// 优惠券Val
            /// </summary>
            public int CouponValue { get; set; }

            /// <summary>
            /// 优惠券状态
            /// </summary>
            public int CouponStatus { get; set; }

            /// <summary>
            /// 优惠券状态描述
            /// </summary>
            public string CouponStatusDesc { get; set; }

            /// <summary>
            /// 创建日期
            /// </summary>
            public DateTime CreateDate { get; set; }

            /// <summary>
            /// 结束日期
            /// </summary>
            public DateTime EndDate { get; set; }

            /// <summary>
            /// 绑定日期
            /// </summary>
            public DateTime? ReceiveDate { get; set; }

            /// <summary>
            /// 使用日期
            /// </summary>
            public DateTime? UsedDate { get; set; }

            /// <summary>
            /// 绑定店铺Id
            /// </summary>
            public int ToAccId { get; set; }

            /// <summary>
            /// 使用店铺Id
            /// </summary>
            public int? UseAccId { get; set; }

            /// <summary>
            /// 备注信息
            /// </summary>
            public string Remark { get; set; }

            /// <summary>
            /// 绑定方式
            /// </summary>
            public int BindWay { get; set; }

            /// <summary>
            /// 附加信息
            /// </summary>
            public string Flag { get; set; }

            /// <summary>
            /// 版本代理商前缀
            /// </summary>
            public int PrefixAgent { get; set; }
        }
        #endregion

		/// <summary>
		/// 端订单列表
		/// </summary>
		public class OrderListModelForApp
		{
			/// <summary>
			/// 总行数
			/// </summary>
			public int ListMaxRows { get; set; }
			/// <summary>
			/// 当前页码
			/// </summary>
			public int PageNow { get; set; }

			/// <summary>
			/// 订单详情Item
			/// </summary>
			public List<OrderListItemForApp> OrderListItems { get; set; }
		}

		/// <summary>
		/// 订单详情Item
		/// </summary>
		public class OrderListItemForApp
		{
			/// <summary>
			/// 订单Id
			/// </summary>
			public int Oid { get; set; }

			/// <summary>
			/// 订单时间
			/// </summary>
			public DateTime CreateDate { get; set; }

			/// <summary>
			/// 订单状态
			/// </summary>
			public int OrderStatus { get; set; }

			/// <summary>
			/// 订单状态描述
			/// </summary>
			public string OrderStatusDesc { get; set; }

			/// <summary>
			/// 业务id
			/// </summary>
			public int BusId { get; set; }

			/// <summary>
			/// 业务名称
			/// </summary>
			public string BusDesc { get; set; }

			/// <summary>
			/// 业务数量
			/// </summary>
			public int BusQuantity { get; set; }

			/// <summary>
			/// 订单实际支付金额
			/// </summary>
			public decimal RealPayMoney { get; set; }
			/// <summary>
			/// 单价
			/// </summary>
			public decimal BusPrice { get; set; }
			/// <summary>
			/// 订单应付总金额
			/// </summary>
			public decimal BusSumMoney { get; set; }

			/// <summary>
			/// 订单产品类别 1业务 2实物 3第三方 4 手机充值
			/// </summary>
			public int OrderTypeId { get; set; }
			/// <summary>
			/// 第三方订单状态
			/// </summary>
			public string ThirdPartOrderStatus { get; set; }

			/// <summary>
			/// 第三方订单状态说明
			/// </summary>
			public string ThirdPartOrderStatusDesc { get; set; }
			public string Remark { get; set; }
		}
    }
}
