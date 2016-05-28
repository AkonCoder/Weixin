using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    /// <summary>
    /// 销售指标对比
    /// </summary>
    public class DailySaleOperationContrast
    {
        /// <summary>
        /// 当日销售额对比增或减（0增1减）
        /// </summary>
        public int StatusDaySaleMoney { get; set; }
        /// <summary>
        /// 当日销售额对比
        /// </summary>
        public decimal ContrastDaySaleMoney { get; set; }

        /// <summary>
        /// 当日订单数对比 增或减（0增1减）
        /// </summary>
        public int StatusDaySaleNum { get; set; }
        /// <summary>
        /// 当日订单数对比
        /// </summary>
        public decimal ContrastDaySaleNum { get; set; }

        /// <summary>
        /// 客单价对比 增或减（0增1减）
        /// </summary>
        public int StatusPerCustomerTransaction { get; set; }
        /// <summary>
        /// 客单价对比
        /// </summary>
        public decimal ContrastPerCustomerTransaction { get; set; }

        /// <summary>
        /// 连带率对比 增或减（0增1平2减）
        /// </summary>
        public int StatusAssociatedPurchaseRate { get; set; }
        /// <summary>
        /// 连带率对比
        /// </summary>
        public decimal ContrastAssociatedPurchaseRate { get; set; }
    }

    /// <summary>
    /// 销售指标
    /// </summary>
    public class DailySaleOperation
    {
        /// <summary>
        /// 当日销售额
        /// </summary>
        public decimal DaySaleMoney { get; set; }
        /// <summary>
        /// 当日订单数
        /// </summary>
        public int DaySaleNum { get; set; }
        /// <summary>
        /// 当日订单商品数
        /// </summary>
        public decimal DaySaleKinds { get; set; }
        /// <summary>
        /// 客单价
        /// </summary>
        public decimal PerCustomerTransaction { get; set; }
        /// <summary>
        /// 连带率
        /// </summary>
        public decimal AssociatedPurchaseRate { get; set; }
    }
    /// <summary>
    /// 销售业绩（按顾客类型）
    /// </summary>
    public class DailySalePercent
    {
        /// <summary>
        /// 当日非会员消费订单数
        /// </summary>
        public int DaySaleRetailNum { get; set; }
        /// <summary>
        /// 当日非会员消费订单数所占百分比
        /// </summary>
        public decimal DaySaleRetailPercent { get; set; }
    }
    /// <summary>
    /// 销售业绩（按支付方式）
    /// </summary>
    public class DailySalePayType
    {
        /// <summary>
        /// 当日储值消费总额
        /// </summary>
        public decimal DayStoreMoney { get; set; }
        /// <summary>
        /// 当日刷卡消费总额
        /// </summary>
        public decimal DayCardMoney { get; set; }
        /// <summary>
        /// 当日现金消费总额
        /// </summary>
        public decimal DayCashMoney { get; set; }
        /// <summary>
        /// 当日赊账消费总额
        /// </summary>
        public decimal DayUnpaidMoney { get; set; }
    }

    public class DailySaleCashier
    {
        public DailySaleCashier()
        {
            SaleCashierDic = new List<DailySaleCashierItem>();
        }

        /// <summary>
        /// key为店员ID，value为销售金额
        /// </summary>
        public List<DailySaleCashierItem> SaleCashierDic { get; set; }
    }

    /// <summary>
    /// 销售业绩（按店员业绩） Item
    /// </summary>
    public class DailySaleCashierItem
    {
        /// <summary>
        /// 店员Id
        /// </summary>
        public int CashierId { get; set; }

        /// <summary>
        /// 店员名称
        /// </summary>
        public string CashierName { get; set; }

        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal CashierSales { get; set; }
    }
    /// <summary>
    /// 经营日报
    /// </summary>
    public class DailyOperation
    {
        public DailyOperation()
        {
            DailySalePayType = new DailySalePayType();
            DailySalePercent = new DailySalePercent();
            DailySaleCashier = new DailySaleCashier();
            DailySaleOperation = new DailySaleOperation();
            DailySaleOperationContrast = new DailySaleOperationContrast();
        }

        /// <summary>
        /// 销售指标
        /// </summary>
        public DailySaleOperation DailySaleOperation { get; set; }
        /// <summary>
        /// 销售业绩（按顾客类型）
        /// </summary>
        public DailySalePercent DailySalePercent { get; set; }
        /// <summary>
        /// 销售业绩（按支付方式）
        /// </summary>
        public DailySalePayType DailySalePayType { get; set; }
        /// <summary>
        /// 销售业绩（按支付方式）
        /// </summary>
        public DailySaleCashier DailySaleCashier { get; set; }
        /// <summary>
        /// 销售指标对比
        /// </summary>
        public DailySaleOperationContrast DailySaleOperationContrast { get; set; }
    }
}
