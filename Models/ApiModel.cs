using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Models
{
    public class ApiModel
    {
        #region LoginUserInfo 登录缓存信息

        /// <summary>
        /// 登录缓存信息
        /// </summary>
        public class LoginUserInfo
        {
            /// <summary>
            /// 店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 状态Token
            /// </summary>
            public string Token { get; set; }

            /// <summary>
            /// 状态过期时间
            /// </summary>
            public DateTime ExpireTime { get; set; }

            /// <summary>
            /// 登录信息Model
            /// </summary>
            public AccountModel LoginAccountModel { get; set; }
        }

        #endregion

        #region AccountModel 店铺登录

        /// <summary>
        /// 店铺登录Model
        /// </summary>
        public class AccountModel
        {
            public AccountModel()
            {
                UserRankCfg = new RankConfig();
            }

            /// <summary>
            /// 登录状态
            /// </summary>
            public int Status { get; set; }

            /// <summary>
            /// 登录Token
            /// </summary>
            public string Token { get; set; }

            /// <summary>
            /// 店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 店铺名称
            /// </summary>
            public string AccName { get; set; }

            /// <summary>
            /// 店主姓名
            /// </summary>
            public string ManageName { get; set; }

            /// <summary>
            /// 店铺自定义Logo
            /// </summary>
            public string AccLogoUrl { get; set; }

            /// <summary>
            /// 总店id
            /// </summary>
            public int ParentId { get; set; }

            /// <summary>
            /// 版本前缀名称
            /// </summary>
            public string PrefixName { get; set; }

            /// <summary>
            /// 所属代理Id
            /// </summary>
            public int AgentId { get; set; }

            /// <summary>
            /// 论坛Uid
            /// </summary>
            public int BbsUid { get; set; }

            /// <summary>
            /// 店铺登陆次数
            /// </summary>
            public int LoginTimes { get; set; }

            /// <summary>
            /// 登录用户Id
            /// </summary>
            public int LgUserId { get; set; }

            /// <summary>
            /// 登录使用账号
            /// </summary>
            public string LgAccount { get; set; }

            /// <summary>
            /// 登录用户姓名
            /// </summary>
            public string LgUserName { get; set; }

            /// <summary>
            /// 登录用户角色
            /// </summary>
            public int LgUserRole { get; set; }

            /// <summary>
            /// 登录用户权限
            /// </summary>
            public int LgUserPower { get; set; }

            /// <summary>
            /// 登录店铺积分比例
            /// </summary>
            public string ConfigIntegral { get; set; }

            /// <summary>
            /// 店铺版本
            /// </summary>
            public int ConfigVersion { get; set; }

            /// <summary>
            /// 店铺版本描述
            /// </summary>
            public string ConfigVersionDesc { get; set; }

            /// <summary>
            /// 分店会员检索权限
            /// </summary>
            public int BranchUser { get; set; }

            /// <summary>
            /// 分店总店账号
            /// </summary>
            public int BranchMaster { get; set; }

            /// <summary>
            /// 会员等级配置
            /// </summary>
            public RankConfig UserRankCfg { get; set; }
        }

        #endregion

        #region RankConfig 店铺等级配置

        /// <summary>
        /// 店铺等级配置
        /// </summary>
        public class RankConfig
        {
            public RankConfig()
            {
                RankItems = new Dictionary<string, RankConfigItem>();
            }
            public Dictionary<string, RankConfigItem> RankItems { get; set; }
        }

        /// <summary>
        /// 等级配置项
        /// </summary>
        public class RankConfigItem
        {
            /// <summary>
            /// ID
            /// </summary>
            public int rankID { get; set; }

            /// <summary>
            /// 等级名称
            /// </summary>
            public string rankName { get; set; }

            /// <summary>
            /// 级别
            /// </summary>
            public int rankLv { get; set; }

            /// <summary>
            /// 最低积分
            /// </summary>
            public int integralLow { get; set; }

            /// <summary>
            /// 最高积分
            /// </summary>
            public int integralHigh { get; set; }

            /// <summary>
            /// 销售折扣
            /// </summary>
            public float Discount { get; set; }
        }

        #endregion


        #region BaseModel 全局相关

        #region SalesManList 店员列表

        /// <summary>
        /// 店员列表
        /// </summary>
        public class SalesManList
        {
            public List<SalesMan> SalesManGroup { get; set; }
        }

        /// <summary>
        /// 店员
        /// </summary>
        public class SalesMan
        {
            /// <summary>
            /// 店员ID
            /// </summary>
            public int manID { get; set; }

            /// <summary>
            /// 店员名称
            /// </summary>
            public string manName { get; set; }
        }

        #endregion

        #region AppHomeModel App功能信息

        /// <summary>
        /// App功能信息
        /// </summary>
        public class AppHomeModel
        {
            /// <summary>
            /// 手机橱窗功能状态
            /// </summary>
            public int MobileWebStatus { get; set; }

            /// <summary>
            /// 优惠券功能
            /// </summary>
            public AppCouponModel CouponItem { get; set; }
        }

        /// <summary>
        /// 优惠券功能
        /// </summary>
        public class AppCouponModel
        {
            /// <summary>
            /// 优惠券功能状态
            /// </summary>
            public int CouponStatus { get; set; }

            /// <summary>
            /// 默认发送优惠券Groupid
            /// </summary>
            public int DefaultGroupId { get; set; }

            /// <summary>
            /// 是否在手机橱窗显示
            /// </summary>
            public int DefaultShow { get; set; }
        }

        #endregion

        #region AccountBasicConfig 店铺基本配置

        /// <summary>
        /// 店铺基本配置
        /// </summary>
        public class AccountBasicConfig
        {
            /// <summary>
            /// 手机橱窗大Banner
            /// </summary>
            public string t_StoreBanner { get; set; }



        }

        #endregion

        #endregion

        #region UserModel 会员相关

        #region UserInfoModel 会员信息(Lite)

        /// <summary>
        /// 会员信息(Lite)
        /// </summary>
        public class UserInfoModel
        {
            /// <summary>
            /// 会员ID
            /// </summary>
            public int uid { get; set; }

            /// <summary>
            /// 归属店铺ID
            /// </summary>
            public int accID { get; set; }

            /// <summary>
            /// 会员卡号
            /// </summary>
            public string uNumber { get; set; }

            /// <summary>
            /// 会员姓名
            /// </summary>
            public string uName { get; set; }

            /// <summary>
            /// 会员手机
            /// </summary>
            public string uPhone { get; set; }

            /// <summary>
            /// 会员QQ
            /// </summary>
            public string uQQ { get; set; }

            /// <summary>
            /// 等级
            /// </summary>
            public int uRank { get; set; }

            /// <summary>
            /// 注册时间
            /// </summary>
            public DateTime uRegTime { get; set; }

            /// <summary>
            /// 可用积分
            /// </summary>
            public int uIntegral { get; set; }

            /// <summary>
            /// 已兑换积分
            /// </summary>
            public int uIntegralUsed { get; set; }

            /// <summary>
            /// 储值金额
            /// </summary>
            public double uStoreMoney { get; set; }

            /// <summary>
            /// 是否设置消费密码
            /// </summary>
            public int? uNeedPwd { get; set; }

            /// <summary>
            /// 储值次数
            /// </summary>
            public int? uStoreTimes { get; set; }

            /// <summary>
            /// 最后一次购物时间
            /// </summary>
            public DateTime? uLastBuyDate { get; set; }

            /// <summary>
            /// 等级折扣
            /// </summary>
            public float? rankDiscount { get; set; }

            /// <summary>
            /// 会员归属店铺名称
            /// </summary>
            public string OriginAccName { get; set; }

            /// <summary>
            /// 是否有优惠券权限
            /// </summary>
            public int IsCoupon { get; set; }

            /// <summary>
            /// 计次卡列表
            /// </summary>
            public List<StoreCard> TimeCard { get; set; }

            /// <summary>
            /// 会员地址
            /// </summary>
            public string UserAddress { get; set; }

            /// <summary>
            /// 优惠券数量
            /// </summary>
            public int? Coupons { get; set; }

            /// <summary>
            /// 计次卡数量
            /// </summary>
            public int? TimeCards { get; set; }
        }

        #endregion

        /// <summary>
        /// 计次卡列表
        /// </summary>
        public class StoreCard
        {
            /// <summary>
            /// Id
            /// </summary>
            public int StId { get; set; }

            /// <summary>
            /// 店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 会员Id
            /// </summary>
            public int Uid { get; set; }

            /// <summary>
            /// 剩余次数
            /// </summary>
            public int StoreTimes { get; set; }

            /// <summary>
            /// 剩余金额
            /// </summary>
            public decimal StoreMoney { get; set; }

            /// <summary>
            /// 修改日期
            /// </summary>
            public DateTime EditTime { get; set; }

            /// <summary>
            /// 卡片名称
            /// </summary>
            public string CardName { get; set; }

            /// <summary>
            /// 绑定商品分类
            /// </summary>
            public int BindGoodsId { get; set; }

            /// <summary>
            /// 绑定商品名称
            /// </summary>
            public string BindGoodsName { get; set; }

            /// <summary>
            /// 截至日期
            /// </summary>
            public DateTime CloseDate { get; set; }

            /// <summary>
            /// 截至日期格式化
            /// </summary>
            public string CloseDateDisplay { get; set; }
        }

        #region UserSummary 会员信息(概要)

        /// <summary>
        /// 会员信息概要
        /// </summary>
        public class UserSummary
        {
            /// <summary>
            /// 会员id
            /// </summary>
            public string uid { get; set; }

            /// <summary>
            /// 会员姓名
            /// </summary>
            public string uName { get; set; }

            /// <summary>
            /// 会员分组
            /// </summary>
            public string gpName { get; set; }

            /// <summary>
            /// 会员手机
            /// </summary>
            public string uPhone { get; set; }

            /// <summary>
            /// 会员积分
            /// </summary>
            public string uIntegral { get; set; }

            /// <summary>
            /// 会员购物次数
            /// </summary>
            public string buyTimes { get; set; }

            /// <summary>
            /// 会员购物数量
            /// </summary>
            public string buyNum { get; set; }

            /// <summary>
            /// 会员累计消费金额
            /// </summary>
            public string buyMoney { get; set; }

            /// <summary>
            /// 最后一次购物时间
            /// </summary>
            public string lastBuyTime { get; set; }

            /// <summary>
            /// 上次购物距今天数
            /// </summary>
            public string lastBuyDay { get; set; }

            /// <summary>
            /// 备注信息
            /// </summary>
            public string uRemark { get; set; }

            /// <summary>
            /// 会员头像
            /// </summary>
            public string uPortrait { get; set; }

            /// <summary>
            /// 剩余积分
            /// </summary>
            public string leftIntegral { get; set; }
        }

        #endregion

        #region UserDetailModle 会员信息(详细)

        /// <summary>
        /// 会员详细信息
        /// </summary>
        public partial class UserDetailModle
        {
            /// <summary>
            /// 会员ID
            /// </summary>
            public int uid { get; set; }

            /// <summary>
            /// 会员卡号
            /// </summary>
            public string uNumber { get; set; }

            /// <summary>
            /// 会员姓名
            /// </summary>
            public string uName { get; set; }

            /// <summary>
            /// 会员性别
            /// </summary>
            public int uSex { get; set; }

            /// <summary>
            /// 会员昵称
            /// </summary>
            public string uNickName { get; set; }

            /// <summary>
            /// 手机号码
            /// </summary>
            public string uPhone { get; set; }

            /// <summary>
            /// QQ号码
            /// </summary>
            public string uQQ { get; set; }

            /// <summary>
            /// 邮箱
            /// </summary>
            public string uEmail { get; set; }

            /// <summary>
            /// 会员地址
            /// </summary>
            public string uAddress { get; set; }

            /// <summary>
            /// 用户喜好
            /// </summary>
            public string uLike { get; set; }

            /// <summary>
            /// 备注信息
            /// </summary>
            public string uRemark { get; set; }

            /// <summary>
            /// 会员等级
            /// </summary>
            public int uRank { get; set; }

            /// <summary>
            /// 会员分组
            /// </summary>
            public string uGroupName { get; set; }

            /// <summary>
            /// 注册时间
            /// </summary>
            public DateTime uRegTime { get; set; }

            /// <summary>
            /// 会员生日
            /// </summary>
            public List<UserBirthdayModle> birthList { get; set; }

            /// <summary>
            /// 会员头像
            /// </summary>
            public string uPortrait { get; set; }
            /// <summary>
            /// 其他电话
            /// </summary>
            public string uOtherPhone { get; set; }
            /// <summary>
            /// 会员微信号
            /// </summary>
            public string Weinxin { get; set; }
        }

        /// <summary>
        /// 会员生日
        /// </summary>
        public partial class UserBirthdayModle
        {
            /// <summary>
            /// 生日记录ID
            /// </summary>
            public int birthID { get; set; }

            /// <summary>
            /// 会员ID
            /// </summary>
            public int uid { get; set; }

            /// <summary>
            /// 是否农历生日
            /// </summary>
            public int IsLunar { get; set; }

            /// <summary>
            /// 纪念日名称
            /// </summary>
            public string bdName { get; set; }

            /// <summary>
            /// 年份
            /// </summary>
            public int bdYear { get; set; }

            /// <summary>
            /// 生日
            /// </summary>
            public DateTime bdDate { get; set; }

            /// <summary>
            /// 显示格式
            /// </summary>
            public string DateDisplay { get; set; }

            /// <summary>
            /// 月
            /// </summary>
            public int monthDisplay { get; set; }

            /// <summary>
            /// 日
            /// </summary>
            public int daysDisplay { get; set; }

            /// <summary>
            /// 修改时间
            /// </summary>
            public DateTime editTime { get; set; }
        }

        #endregion

        #region UserStoreMoneyModle 会员信息(储值)

        /// <summary>
        /// 会员储值信息
        /// </summary>
        public partial class UserStoreMoneyModle
        {
            public int uid { get; set; }

            /// <summary>
            /// 储值金额
            /// </summary>
            public decimal storeMoney { get; set; }

            /// <summary>
            /// 上次储值时间
            /// </summary>
            public DateTime? lastStoreTime { get; set; }

            /// <summary>
            /// 消费累计金额
            /// </summary>
            public decimal SaleMoney { get; set; }

            /// <summary>
            /// 储值累计金额
            /// </summary>
            public decimal storeAllMoney { get; set; }

            /// <summary>
            /// 储值记录
            /// </summary>
            public List<StoreMoneyList> recList { get; set; }
        }

        /// <summary>
        /// 会员储值记录
        /// </summary>
        public partial class StoreMoneyList
        {
            /// <summary>
            /// 储值记录ID
            /// </summary>
            public int recID { get; set; }

            /// <summary>
            /// 储值类型
            /// </summary>
            public int itemType { get; set; }

            /// <summary>
            /// 类型名称
            /// </summary>
            public string typeName { get; set; }

            /// <summary>
            /// 原始值
            /// </summary>
            public decimal orginVal { get; set; }

            /// <summary>
            /// 变动值
            /// </summary>
            public decimal editVal { get; set; }

            /// <summary>
            /// 最新值
            /// </summary>
            public decimal lastVal { get; set; }

            /// <summary>
            /// 操作日期
            /// </summary>
            public DateTime? logTime { get; set; }

            /// <summary>
            /// 操作员
            /// </summary>
            public string operater { get; set; }

            /// <summary>
            /// 备注信息
            /// </summary>
            public string remark { get; set; }

            /// <summary>
            /// 实收金额
            /// </summary>
            public decimal editMoney { get; set; }

            /// <summary>
            /// 充值方式(1-现金 2-刷卡)
            /// </summary>
            public int moneyType { get; set; }
        }

        #endregion

        #region UserStoreTimesModle 会员信息(计次)

        /// <summary>
        /// 会员信息(计次)
        /// </summary>
        public partial class UserStoreTimesModle
        {
            public int uid { get; set; }

            /// <summary>
            /// 储值次数
            /// </summary>
            public int storeTimes { get; set; }

            /// <summary>
            /// 上次充值时间
            /// </summary>
            public DateTime? lastStoreTime { get; set; }

            /// <summary>
            /// 消费累计次数
            /// </summary>
            public int saleTimes { get; set; }

            /// <summary>
            /// 储值累计次数
            /// </summary>
            public int storeAllTimes { get; set; }

            /// <summary>
            /// 计次记录
            /// </summary>
            public List<StoreTimesList> recList { get; set; }

        }

        public partial class StoreTimesList
        {
            /// <summary>
            /// 储值记录ID
            /// </summary>
            public int recID { get; set; }

            /// <summary>
            /// 储值类型
            /// </summary>
            public int itemType { get; set; }

            /// <summary>
            /// 类型名称
            /// </summary>
            public string typeName { get; set; }

            /// <summary>
            /// 原始值
            /// </summary>
            public double orginVal { get; set; }

            /// <summary>
            /// 变动值
            /// </summary>
            public double editVal { get; set; }

            /// <summary>
            /// 最新值
            /// </summary>
            public double lastVal { get; set; }

            /// <summary>
            /// 操作日期
            /// </summary>
            public DateTime? logTime { get; set; }

            /// <summary>
            /// 操作员
            /// </summary>
            public string operater { get; set; }

            /// <summary>
            /// 备注信息
            /// </summary>
            public string remark { get; set; }

            /// <summary>
            /// 变动金额
            /// </summary>
            public double EditMoney { get; set; }

            /// <summary>
            /// 变动金额类型 1-现金 2-刷卡
            /// </summary>
            public int EditMoneyType { get; set; }
        }

        #endregion

        #region UserIntegralModel 会员信息(积分)

        /// <summary>
        /// 会员积分Model
        /// </summary>
        public class UserIntegralModel
        {
            /// <summary>
            /// 会员id
            /// </summary>
            public int Uid { get; set; }

            /// <summary>
            /// 可用积分
            /// </summary>
            public int Integral { get; set; }

            /// <summary>
            /// 已兑换积分
            /// </summary>
            public int IntegralUsed { get; set; }

            /// <summary>
            /// 所有积分
            /// </summary>
            public int IntegralAll { get; set; }

            /// <summary>
            /// 上次兑换日期
            /// </summary>
            public string LastIntegralDate { get; set; }

            /// <summary>
            /// 会员等级
            /// </summary>
            public int UserRank { get; set; }

            /// <summary>
            /// 等级名称
            /// </summary>
            public string UserRankName { get; set; }

            /// <summary>
            /// 等级折扣
            /// </summary>
            public decimal RankDiscount { get; set; }

            /// <summary>
            /// 等级锁定
            /// </summary>
            public int RankLock { get; set; }

            /// <summary>
            /// 日志记录
            /// </summary>
            public List<UserIntegralItem> IntegralList { get; set; }
        }

        public class UserIntegralItem
        {
            /// <summary>
            /// 变动积分
            /// </summary>
            public int EditVal { get; set; }

            /// <summary>
            /// 最终积分
            /// </summary>
            public int FinalVal { get; set; }

            /// <summary>
            /// 记录时间
            /// </summary>
            public string LogTime { get; set; }

            /// <summary>
            /// 原始积分
            /// </summary>
            public int OriginalVal { get; set; }

            /// <summary>
            /// 备注信息
            /// </summary>
            public string Remark { get; set; }

            /// <summary>
            /// 记录类型
            /// </summary>
            public int itemType { get; set; }

            /// <summary>
            /// 操作员
            /// </summary>
            public string operatorName { get; set; }

            /// <summary>
            /// 操作员ID
            /// </summary>
            public int operatorID { get; set; }

            /// <summary>
            /// 操作日期
            /// </summary>
            public string operatorTime { get; set; }

            /// <summary>
            /// 会员id
            /// </summary>
            public int uid { get; set; }

            /// <summary>
            /// 日志id
            /// </summary>
            public int userLogID { get; set; }
        }

        #endregion

        #region UserSmsListModel 会员信息(短信)

        /// <summary>
        /// 会员短信Model
        /// </summary>
        public class UserSmsListModel
        {
            /// <summary>
            /// 短信条数
            /// </summary>
            public int SmsCnt { get; set; }

            /// <summary>
            /// 短信内容
            /// </summary>
            public List<UserSmsListItem> SmsList { get; set; }
        }

        public class UserSmsListItem
        {
            /// <summary>
            /// 手机号码
            /// </summary>
            public string PhoneNum { get; set; }

            /// <summary>
            /// 发送时间
            /// </summary>
            public string SendTime { get; set; }

            /// <summary>
            /// 短信条数
            /// </summary>
            public int SmsCnt { get; set; }

            /// <summary>
            /// 短信内容
            /// </summary>
            public string SmsContent { get; set; }

            /// <summary>
            /// 短信类型
            /// </summary>
            public int SmsType { get; set; }
        }

        #endregion

        #region UserBirthday 会员生日Model

        /// <summary>
        /// 会员生日Model
        /// </summary>
        public class UserBirthday
        {
            public List<UserBirthdayItem> TodayBirth { get; set; }
        }

        public class UserBirthdayItem
        {
            /// <summary>
            /// 生日Id
            /// </summary>
            public int birthdayID { get; set; }

            /// <summary>
            /// 会员id
            /// </summary>
            public int uid { get; set; }

            /// <summary>
            /// 会员名称
            /// </summary>
            public string uName { get; set; }

            /// <summary>
            /// 是否农历生日
            /// </summary>
            public int IsLunar { get; set; }

            /// <summary>
            /// 生日名称
            /// </summary>
            public string bdName { get; set; }

            /// <summary>
            /// 年份
            /// </summary>
            public int bdYear { get; set; }

            /// <summary>
            /// 生日
            /// </summary>
            public string bdDate { get; set; }

            /// <summary>
            /// 显示名称
            /// </summary>
            public string display { get; set; }
        }

        #endregion

        #region UserBaseGroup 会员基础分组信息

        /// <summary>
        /// 会员基础分组信息
        /// </summary>
        public class UserBaseGroup
        {
            /// <summary>
            /// 会员分组
            /// </summary>
            public List<UserGroupModel> UserGroup { get; set; }

            /// <summary>
            /// 会员标签
            /// </summary>
            public List<UserTagModel> UserTag { get; set; }

            /// <summary>
            /// 店铺等级
            /// </summary>
            public Dictionary<int, RankConfigItem> RankItems { get; set; }
        }

        /// <summary>
        /// 会员分组Model
        /// </summary>
        public class UserGroupModel
        {
            /// <summary>
            /// 分组Id
            /// </summary>
            public int GroupId { get; set; }

            /// <summary>
            /// 分组名称
            /// </summary>
            public string GroupName { get; set; }
        }

        /// <summary>
        /// 会员标签
        /// </summary>
        public class UserTagModel
        {
            /// <summary>
            /// TagId
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 颜色
            /// </summary>
            public string TagColor { get; set; }

            /// <summary>
            /// 标签名称
            /// </summary>
            public string TagName { get; set; }
        }

        #endregion

        #region UserTagListModel 会员标签列表

        /// <summary>
        /// 会员标签列表
        /// </summary>
        public class UserTagListModel
        {
            public int ListNum { get; set; }
            public List<UserTagListItem> UserTagList { get; set; }
        }

        /// <summary>
        /// 会员标签
        /// </summary>
        public class UserTagListItem
        {
            public int Id { get; set; }
            public string Color { get; set; }
            public string TagName { get; set; }
            public int UserId { get; set; }
        }

        #endregion

        #region UserBirthList 会员生日列表

        /// <summary>
        /// 会员生日列表
        /// </summary>
        public class UserBirthList
        {
            /// <summary>
            /// 人数
            /// </summary>
            public int ListUserNum { get; set; }

            /// <summary>
            /// 记录数
            /// </summary>
            public int ListNum { get; set; }

            /// <summary>
            /// 生日Item
            /// </summary>
            public List<UserBirthItem> BirthList { get; set; }
        }

        public class UserBirthItem
        {
            /// <summary>
            /// 是否为农历
            /// </summary>
            public int IsLunar { get; set; }

            /// <summary>
            /// 生日日期
            /// </summary>
            public string BdDate { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string BdName { get; set; }

            /// <summary>
            /// 生日年份
            /// </summary>
            public int BdYear { get; set; }

            /// <summary>
            /// 生日显示名称
            /// </summary>
            public string BirthDisName { get; set; }

            /// <summary>
            /// 生日id
            /// </summary>
            public int BirthdayId { get; set; }

            /// <summary>
            /// 距今天数
            /// </summary>
            public int DiffToday { get; set; }

            /// <summary>
            /// 会员等级
            /// </summary>
            public int RankLv { get; set; }

            /// <summary>
            /// 等级名称
            /// </summary>
            public string RankName { get; set; }

            /// <summary>
            /// 积分
            /// </summary>
            public int Integral { get; set; }

            /// <summary>
            /// 会员姓名
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 会员手机
            /// </summary>
            public string UserPhone { get; set; }

            /// <summary>
            /// 会员qq
            /// </summary>
            public string UserQq { get; set; }

            /// <summary>
            /// 会员id
            /// </summary>
            public int UserId { get; set; }
        }

        #endregion

        #region UserQueryOrder 会员列表查询参数

        /// <summary>
        /// 会员列表查询参数
        /// </summary>
        public class UserQueryOrder
        {
            /// <summary>
            /// 关键词
            /// </summary>
            public string keyWord { get; set; }

            /// <summary>
            /// 会员分组
            /// </summary>
            public int? uGroup { get; set; }

            /// <summary>
            /// 会员等级 -2:自定义积分范围
            /// </summary>
            public int? uGrade { get; set; }

            /// <summary>
            /// 会员流失度 -2:自定义购买日期
            /// </summary>
            public int? uBuyGroup { get; set; }

            /// <summary>
            /// 首字母
            /// </summary>
            public string firstWord { get; set; }

            /// <summary>
            /// 是否显示分店 1-显示分店会员
            /// </summary>
            public int? sunBranch { get; set; }

            /// <summary>
            /// 自定 会员开始积分
            /// </summary>
            public int? minIntegral { get; set; }

            /// <summary>
            /// 自定 会员结束积分
            /// </summary>
            public int? maxIntegral { get; set; }

            /// <summary>
            /// 页数
            /// </summary>
            public int page { get; set; }

            /// <summary>
            /// 标签
            /// </summary>
            public int tag { get; set; }

            /// <summary>
            /// 排序
            /// </summary>
            public string odr { get; set; }
        }

        #region UserQueryOrderEx 会员列表查询参数扩展

        /// <summary>
        /// 会员列表查询参数扩展
        /// </summary>
        public class UserQueryOrderEx
        {
            /// <summary>
            /// 关键词
            /// </summary>
            public string keyWord { get; set; }

            /// <summary>
            /// 会员分组
            /// </summary>
            public int[] uGroup { get; set; }

            /// <summary>
            /// 会员等级 -2:自定义积分范围
            /// </summary>
            public int[] uGrade { get; set; }

            /// <summary>
            /// 会员流失度 -2:自定义购买日期
            /// </summary>
            public int? uBuyGroup { get; set; }

            /// <summary>
            /// 首字母
            /// </summary>
            public string firstWord { get; set; }

            /// <summary>
            /// 是否显示分店 1-显示分店会员
            /// </summary>
            public int? sunBranch { get; set; }

            /// <summary>
            /// 自定 会员开始积分
            /// </summary>
            public int? minIntegral { get; set; }

            /// <summary>
            /// 自定 会员结束积分
            /// </summary>
            public int? maxIntegral { get; set; }

            /// <summary>
            /// 页数
            /// </summary>
            public int page { get; set; }

            /// <summary>
            /// 标签
            /// </summary>
            public int[] tag { get; set; }

            /// <summary>
            /// 会员生日范围
            /// </summary>
            public string birthdayRange { get; set; }

            /// <summary>
            /// 是否储值
            /// </summary>
            public int storeMoney { get; set; }


            /// <summary>
            /// 消费总额
            /// </summary>
            public double consumeCount { get; set; }

            /// <summary>
            /// 有消费记录时限
            /// </summary>
            public int consumeInRange { get; set; }

            /// <summary>
            /// 无消费记录时限
            /// </summary>
            public int consumeOutRange { get; set; }

            /// <summary>
            /// 排序
            /// </summary>
            public string odr { get; set; }

            /// <summary>
            /// 按积分排序 
            /// <para>04月22日 新增加</para>
            /// </summary>
            public string IntegralOrder { get; set; }

            /// <summary>
            /// 按积分排序
            /// </summary>
            public string StoreMoneyOrder { get; set; }
        }

        #endregion

        /// <summary>
        /// 会员列表概要信息
        /// </summary>
        public class UserListSummary
        {
            /// <summary>
            /// 会员总数
            /// </summary>
            public int UserCnt { get; set; }

            /// <summary>
            /// 当前页数
            /// </summary>
            public int nowPage { get; set; }

            /// <summary>
            /// 分页信息
            /// </summary>
            public string pageHtml { get; set; }

            /// <summary>
            /// 会员列表
            /// </summary>
            public List<UserList> UserItems { get; set; }
        }

        /// <summary>
        /// 会员列表信息
        /// </summary>
        public class UserList
        {
            /// <summary>
            /// 会员ID
            /// </summary>
            public int uid { get; set; }

            /// <summary>
            /// 会员所属店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 会员卡号
            /// </summary>
            public string uNumber { get; set; }

            /// <summary>
            /// 会员姓名
            /// </summary>
            public string uName { get; set; }

            /// <summary>
            /// 性别
            /// </summary>
            public int uSex { get; set; }

            /// <summary>
            /// 称谓
            /// </summary>
            public string uNickName { get; set; }

            /// <summary>
            /// 会员手机
            /// </summary>
            public string uPhone { get; set; }

            /// <summary>
            /// 会员QQ
            /// </summary>
            public string uQQ { get; set; }

            /// <summary>
            /// 会员备注
            /// </summary>
            public string uRemark { get; set; }

            /// <summary>
            /// 会员等级
            /// </summary>
            public int uRank { get; set; }

            /// <summary>
            /// 会员等级名称
            /// </summary>
            public string uRankName { get; set; }

            /// <summary>
            /// 会员分组
            /// </summary>
            public int uGroup { get; set; }

            /// <summary>
            /// 会员分组名称
            /// </summary>
            public string uGroupName { get; set; }

            /// <summary>
            /// 会员积分
            /// </summary>
            public int uIntegral { get; set; }

            /// <summary>
            /// 会员储值金额
            /// </summary>
            public double uStoreMoney { get; set; }

            /// <summary>
            /// 会员所属店铺名称
            /// </summary>
            public string accName { get; set; }

            /// <summary>
            /// 会员第一个生日类型
            /// </summary>
            public int? uBirthType { get; set; }

            /// <summary>
            /// 会员第一个生日
            /// </summary>
            public DateTime? uBirthDay { get; set; }

            /// <summary>
            /// 会员第一个生日距今天数
            /// </summary>
            public int? uBirthDiffDays { get; set; }

            /// <summary>
            /// 最后一次购物时间
            /// </summary>
            public DateTime? uLastBuyDate { get; set; }

            /// <summary>
            /// 会员头像
            /// </summary>
            public string uPortrait { get; set; }

            /// <summary>
            /// 欠款
            /// </summary>
            public decimal UnPaidMoney { get; set; }

            /// <summary>
            /// 是否是总店会员
            /// </summary>
            public bool IsHeadStore { get; set; }

            /// <summary>
            /// 计次卡张数
            /// </summary>
            public int TimeCardsCount { get; set; }
        }

        #endregion

        #region UserSubmitModel 新增会员Model

        /// <summary>
        /// 新增会员Model
        /// </summary>
        public class UserSubmitModel
        {
            public UserSubmitModel()
            {
                BirthInfo = new EditBirthModel();
            }

            /// <summary>
            /// 会员Id
            /// </summary>
            public int UsrId { get; set; }

            /// <summary>
            /// 店铺id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 会员卡号
            /// </summary>
            public string UserNo { get; set; }

            /// <summary>
            /// 会员名称
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 会员性别
            /// </summary>
            public int UserSex { get; set; }

            /// <summary>
            /// 会员手机
            /// </summary>
            public string UserPhone { get; set; }

            /// <summary>
            /// 其他电话
            /// </summary>
            public string OtherPhone { get; set; }

            /// <summary>
            /// 会员等级
            /// </summary>
            public int UserRank { get; set; }

            /// <summary>
            /// 会员分组
            /// </summary>
            public int UserGroup { get; set; }

            /// <summary>
            /// 会员QQ
            /// </summary>
            public string UserQq { get; set; }

            /// <summary>
            /// 会员Email
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// 会员地址
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// 会员喜好
            /// </summary>
            public string UserLike { get; set; }

            /// <summary>
            /// 会员备注
            /// </summary>
            public string Remark { get; set; }

            /// <summary>
            /// 是否发送短信
            /// </summary>
            public int IsSendSms { get; set; }

            /// <summary>
            /// 会员生日
            /// </summary>
            public List<UserBirthModel> UserBirth { get; set; }

            /// <summary>
            /// 会员微信号
            /// </summary>
            public string Weinxin { get; set; }

            /// <summary>
            /// 会员支付宝账号
            /// </summary>
            public string Alipay { get; set; }

            /// <summary>
            /// 会员头像
            /// </summary>
            public string Portrait { get; set; }
            /// <summary>
            /// 其他电话
            /// </summary>
            public string uOtherPhone { get; set; }
            /// <summary>
            /// 会员生日修改
            /// </summary>
            public EditBirthModel BirthInfo { get; set; }
        }

        /// <summary>
        /// 会员生日
        /// </summary>
        public class UserBirthModel
        {
            /// <summary>
            /// 生日类型
            /// </summary>
            public int Type { get; set; }

            /// <summary>
            /// 生日名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 生日年份
            /// </summary>
            public int Year { get; set; }

            /// <summary>
            /// 生日月份
            /// </summary>
            public int Month { get; set; }

            /// <summary>
            /// 生日Day
            /// </summary>
            public int Day { get; set; }
        }

        public class EditBirthModel : UserBirthModel
        {
            /// <summary>
            /// 会员Id
            /// </summary>
            public int UserId { get; set; }

            /// <summary>
            /// 生日Id
            /// </summary>
            public int BirthId { get; set; }
        }

        #endregion

        #region UserNickGroup 会员称谓列表

        /// <summary>
        /// 会员称谓列表
        /// </summary>
        public class UserNickGroup
        {
            /// <summary>
            /// 称谓列表 ID-名称
            /// </summary>
            public Dictionary<string, string> NickList { get; set; }
        }

        #endregion

        #region UserTipsModel 会员TipsModel

        /// <summary>
        /// 会员TipsModel
        /// </summary>
        public class UserTipsModel
        {
            /// <summary>
            /// 会员Id
            /// </summary>
            public int UserId { get; set; }

            /// <summary>
            /// 会员卡号
            /// </summary>
            public string UserNo { get; set; }

            /// <summary>
            /// 会员名称
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 会员手机
            /// </summary>
            public string Phone { get; set; }

            /// <summary>
            /// 积分
            /// </summary>
            public int Integral { get; set; }

            /// <summary>
            /// 储值余额
            /// </summary>
            public decimal StoreMoney { get; set; }

            /// <summary>
            /// 储值计次
            /// </summary>
            public int StoreTimes { get; set; }
        }

        #endregion

        #region UserStoreMoneyInModel 会员充值Model

        /// <summary>
        /// 会员充值Model
        /// </summary>
        public class UserStoreMoneyInModel
        {
            /// <summary>
            /// 店铺id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 会员id
            /// </summary>
            public int UserId { get; set; }

            /// <summary>
            /// 操作类型 1-充值
            /// </summary>
            public int FuncType { get; set; }

            /// <summary>
            /// 充值金额
            /// </summary>
            public decimal Value { get; set; }

            /// <summary>
            /// 备注信息
            /// </summary>
            public string Remark { get; set; }

            /// <summary>
            /// 操作人员id
            /// </summary>
            public int OperatorId { get; set; }

            /// <summary>
            /// Ip
            /// </summary>
            public string OperatorIp { get; set; }

            /// <summary>
            /// 实收金额
            /// </summary>
            public decimal RealMoney { get; set; }

            /// <summary>
            /// 收款方式
            /// </summary>
            public int MoneyType { get; set; }

            /// <summary>
            /// 发送短信提醒
            /// </summary>
            public int SendSms { get; set; }
        }

        #endregion

        #region UserSalesAddModel 增加会员(销售后)

        /// <summary>
        /// 增加会员Model（销售后）
        /// </summary>
        public class UserSalesAddModel
        {
            /// <summary>
            /// 店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 会员名称
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 会员性别
            /// </summary>
            public int UserSex { get; set; }

            /// <summary>
            /// 会员手机
            /// </summary>
            public string UserPhone { get; set; }

            /// <summary>
            /// 销售Id
            /// </summary>
            public int SaleId { get; set; }

            /// <summary>
            /// 操作人员id
            /// </summary>
            public int OperatorId { get; set; }
        }

        #endregion

        #endregion

        #region SalesModel 销售相关

        #region SalesSummary 历史销售列表

        /// <summary>
        /// 销售查询条件
        /// </summary>
        public class SaleQueryOrderStr
        {
            /// <summary>
            /// 当前页数
            /// </summary>
            public int iPage { get; set; }

            /// <summary>
            /// 会员ID
            /// </summary>
            public int? userID { get; set; }

            /// <summary>
            /// 日期类型 on-间隔时间 off-指定时间
            /// </summary>
            public string dateType { get; set; }

            /// <summary>
            /// 时间间隔
            /// </summary>
            public int? dateDiff { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime? dateBg { get; set; }

            /// <summary>
            /// 截止时间
            /// </summary>
            public DateTime? dateEd { get; set; }

            /// <summary>
            /// 关键字
            /// </summary>
            public string keyword { get; set; }

            /// <summary>
            /// 商品ID
            /// </summary>
            public int? goodsID { get; set; }

            /// <summary>
            /// 销售类型
            /// </summary>
            public string saleType { get; set; }

            /// <summary>
            /// 支付类型
            /// </summary>
            public int? payType { get; set; }

            /// <summary>
            /// 大分类ID
            /// </summary>
            public int? maxID { get; set; }

            /// <summary>
            /// 小分类ID
            /// </summary>
            public int? minID { get; set; }
        }

        /// <summary>
        /// 销售查询条件扩展
        /// </summary>
        public class SaleQueryOrderStrEx : SaleQueryOrderStr
        {
            /// <summary>
            /// 查询年份
            /// </summary>
            public int Year { get; set; }

            /// <summary>
            /// 查询月份
            /// </summary>
            public int Month { get; set; }
        }

        /// <summary>
        /// 历史销售记录
        /// </summary>
        public class SalesSummary
        {
            /// <summary>
            /// 合计总金额
            /// </summary>
            public double sumMoney { get; set; }

            /// <summary>
            /// 未付款总额
            /// </summary>
            public double? sumUnpaid { get; set; }

            /// <summary>
            /// 合计总数量
            /// </summary>
            public double sumNumber { get; set; }

            /// <summary>
            /// 合计项目条数
            /// </summary>
            public int sumListCnt { get; set; }

            /// <summary>
            /// 当前页数
            /// </summary>
            public int nowPage { get; set; }

            /// <summary>
            /// 分页信息
            /// </summary>
            public string pageHtml { get; set; }

            /// <summary>
            /// 销售列表
            /// </summary>
            public List<SalesRecord> SalesRecordList { get; set; }
        }

        /// <summary>
        /// 历史销售记录列表
        /// </summary>
        public class SalesRecord
        {
            /// <summary>
            /// 销售概要ID
            /// </summary>		
            public int saleID { get; set; }

            /// <summary>
            /// 销售流水号
            /// </summary>		
            public string saleNo { get; set; }

            /// <summary>
            /// 是否零售
            /// </summary>		
            public int isRetail { get; set; }

            /// <summary>
            /// 销售类型(零售/会员)
            /// </summary>
            public string salesType { get; set; }

            /// <summary>
            /// 会员ID
            /// </summary>		
            public int uid { get; set; }

            /// <summary>
            /// 会员姓名
            /// </summary>
            public string userName { get; set; }

            /// <summary>
            /// 销售时间
            /// </summary>		
            public DateTime saleTime { get; set; }

            /// <summary>
            /// 销售时间(HH:mm)
            /// </summary>
            public string saleShortTime { get; set; }

            /// <summary>
            /// 记录插入时间
            /// </summary>		
            public DateTime insertTime { get; set; }

            /// <summary>
            /// 商品种类
            /// </summary>		
            public int saleKind { get; set; }

            /// <summary>
            /// 销售数量
            /// </summary>		
            public double saleNum { get; set; }

            /// <summary>
            /// 支付方式
            /// </summary>		
            public int payType { get; set; }

            /// <summary>
            /// 支付方式名称
            /// </summary>
            public string payTypeName { get; set; }

            /// <summary>
            /// 应付金额
            /// </summary>		
            public decimal AbleMoney { get; set; }

            /// <summary>
            /// 实付金额
            /// </summary>		
            public decimal RealMoney { get; set; }

            /// <summary>
            /// 应付与实付金额差值
            /// </summary>		
            public decimal DiffMoney { get; set; }

            /// <summary>
            /// StoreTimes
            /// </summary>		
            public int StoreTimes { get; set; }

            /// <summary>
            /// 储值支付金额
            /// </summary>		
            public decimal StoreMoney { get; set; }

            /// <summary>
            /// 现金支付金额
            /// </summary>		
            public decimal CashMoney { get; set; }

            /// <summary>
            /// CardMoney
            /// </summary>		
            public decimal CardMoney { get; set; }

            /// <summary>
            /// 未支付金额
            /// </summary>		
            public decimal UnpaidMoney { get; set; }

            /// <summary>
            /// 优惠券金额
            /// </summary>
            public int? CouponMoney { get; set; }

            /// <summary>
            /// 销售详细列表
            /// </summary>
            public List<SalesDetail> SalesList { get; set; }
        }

        /// <summary>
        /// 历史销售详细列表
        /// </summary>
        public class SalesDetail
        {
            /// <summary>
            /// 销售列表ID
            /// </summary>		
            public int saleListID { get; set; }

            /// <summary>
            /// saleID
            /// </summary>		
            public int saleID { get; set; }

            /// <summary>
            /// 流水号
            /// </summary>		
            public string saleNo { get; set; }

            /// <summary>
            /// 会员ID
            /// </summary>		
            public int userID { get; set; }

            /// <summary>
            /// 会员姓名
            /// </summary>
            public string userName { get; set; }

            /// <summary>
            /// 是否零售
            /// </summary>		
            public int isRetail { get; set; }

            /// <summary>
            /// 大分类名称
            /// </summary>		
            public string maxClass { get; set; }

            /// <summary>
            /// 小分类名称
            /// </summary>		
            public string minClass { get; set; }

            /// <summary>
            /// 大分类ID
            /// </summary>		
            public int maxClassID { get; set; }

            /// <summary>
            /// 小分类ID
            /// </summary>		
            public int minClassID { get; set; }

            /// <summary>
            /// 商品ID
            /// </summary>		
            public int GoodsID { get; set; }

            /// <summary>
            /// 商品名称
            /// </summary>		
            public string GoodsName { get; set; }

            /// <summary>
            /// 商品规格
            /// </summary>		
            public string Specification { get; set; }

            /// <summary>
            /// 商品数量
            /// </summary>		
            public decimal GoodsNum { get; set; }

            /// <summary>
            /// 商品单价
            /// </summary>		
            public decimal Price { get; set; }

            /// <summary>
            /// 商品折扣
            /// </summary>		
            public decimal Discount { get; set; }

            /// <summary>
            /// 应付金额
            /// </summary>		
            public decimal AbleMoney { get; set; }

            /// <summary>
            /// 与实收金额差值平摊到该商品金额
            /// </summary>		
            public decimal FixMoney { get; set; }

            /// <summary>
            /// 实收金额
            /// </summary>		
            public decimal RealMoney { get; set; }

            /// <summary>
            /// 是否积分
            /// </summary>		
            public int isIntegral { get; set; }

            /// <summary>
            /// 收银员
            /// </summary>		
            public string CashierName { get; set; }

            /// <summary>
            /// 备注信息
            /// </summary>		
            public string Remark { get; set; }

            /// <summary>
            /// 销售日期
            /// </summary>		
            public DateTime saleTime { get; set; }

            /// <summary>
            /// 销售时间(HH:mm)
            /// </summary>
            public string saleShortTime { get; set; }

            /// <summary>
            /// 记录插入日期
            /// </summary>		
            public DateTime insertTime { get; set; }

            /// <summary>
            /// 退货标记
            /// </summary>
            public int returnStatus { get; set; }

            /// <summary>
            /// 退货类型
            /// </summary>
            public int returnFlag { get; set; }

            /// <summary>
            /// 退货原因
            /// </summary>
            public string returnReason { get; set; }

            /// <summary>
            /// 退货时间
            /// </summary>
            public DateTime returnTime { get; set; }

            /// <summary>
            /// 流水单号
            /// </summary>
            public long SerialNum { get; set; }

            /// <summary>
            /// 商品图片
            /// </summary>
            public string PicUrl { get; set; }

            /// <summary>
            /// 商品进价
            /// </summary>
            public decimal CostPrice { get; set; }
        }

        #endregion

        public class SaleDetailWithStat
        {
            /// <summary>
            /// 共计支付
            /// </summary>
            public decimal Total { get; set; }

            /// <summary>
            /// 成本
            /// </summary>
            public decimal Cost { get; set; }

            /// <summary>
            /// 毛利润
            /// </summary>
            public decimal Profit { get; set; }

            /// <summary>
            /// 整单备注信息
            /// </summary>		
            public string WholeRemark { get; set; }

            /// <summary>
            /// 销售详细列表
            /// </summary>
            public List<SalesDetail> SalesList { get; set; }
        }

        #region T_Sales_Model 销售请求Model

        /// <summary>
        /// 销售请求Model
        /// </summary>
        public partial class T_Sales_Model
        {
            //销售概要
            public T_Sales_Model()
            {
            }

            #region Model

            private int? _saleid;
            private string _saleno;
            private int? _accid;
            private int? _isretail;
            private string _guid;
            private string _gDate;
            private DateTime? _inserttime;
            private int? _salekind;
            private decimal? _salenum;
            private string _gPayType;
            private decimal? _ablemoney;
            private string _gRealMoney;
            private decimal? _diffmoney;
            private decimal? _storemoney;
            private decimal? _cashmoney;
            private decimal? _unpaidmoney;
            private int? _issendsms;
            private int? _issendfreesms;
            private int? _isprintticket;
            private int? _operatorid;
            private DateTime? _operatortime;
            private string _operatorip;
            private string _remark;
                 

            /// <summary>
            /// 会员消费密码
            /// </summary>
            public string userPwd { get; set; }

            /// <summary>
            /// 积分商品合计金额
            /// </summary>
            public decimal intrgralMoney { get; set; }


            /// <summary>
            /// 销售详细列表
            /// </summary>
            public Dictionary<string, SalesList> gList { get; set; }

            /// <summary>
            /// 优惠券ID
            /// </summary>
            public int? CouponId { get; set; }

            /// <summary>
            /// 优惠券金额
            /// </summary>
            public int? CouponValue { get; set; }

            /// <summary>
            /// 优惠券编码
            /// </summary>
            public string CouponCode { get; set; }

            /// <summary>
            /// 挂单Id
            /// </summary>
            public int SaleCartId { get; set; }

            /// <summary>
            /// 优惠短信发送
            /// </summary>
            public int CouponSms { get; set; }

            /// <summary>
            /// 计次卡项目
            /// </summary>
            public Dictionary<int, TimeCard> TimeCard { get; set; }

            /// <summary>
            /// 销售概要ID
            /// </summary>
            public int? saleID
            {
                set { _saleid = value; }
                get { return _saleid; }
            }

            /// <summary>
            /// 销售流水号
            /// </summary>
            public string saleNo
            {
                set { _saleno = value; }
                get { return _saleno; }
            }

            /// <summary>
            /// 店铺ID
            /// </summary>
            public int? accID
            {
                set { _accid = value; }
                get { return _accid; }
            }

            /// <summary>
            /// 是否零售
            /// </summary>
            public int? isRetail
            {
                set { _isretail = value; }
                get { return _isretail; }
            }

            /// <summary>
            /// 会员ID
            /// </summary>
            public string guid
            {
                set { _guid = value; }
                get { return _guid; }
            }

            /// <summary>
            /// 销售时间
            /// </summary>
            public string gDate
            {
                set { _gDate = value; }
                get { return _gDate; }
            }

            /// <summary>
            /// 记录插入时间
            /// </summary>
            public DateTime? insertTime
            {
                set { _inserttime = value; }
                get { return _inserttime; }
            }

            /// <summary>
            /// 商品种类
            /// </summary>
            public int? saleKind
            {
                set { _salekind = value; }
                get { return _salekind; }
            }

            /// <summary>
            /// 销售数量
            /// </summary>
            public decimal? saleNum
            {
                set { _salenum = value; }
                get { return _salenum; }
            }

            /// <summary>
            /// 支付方式
            /// </summary>
            public string gPayType
            {
                set { _gPayType = value; }
                get { return _gPayType; }
            }

            /// <summary>
            /// 应付金额
            /// </summary>
            public decimal? AbleMoney
            {
                set { _ablemoney = value; }
                get { return _ablemoney; }
            }

            /// <summary>
            /// 实付金额
            /// </summary>
            public string gRealMoney
            {
                set { _gRealMoney = value; }
                get { return _gRealMoney; }
            }

            /// <summary>
            /// 应付与实付金额差值
            /// </summary>
            public decimal? DiffMoney
            {
                set { _diffmoney = value; }
                get { return _diffmoney; }
            }

            /// <summary>
            /// 储值支付金额
            /// </summary>
            public decimal? StoreMoney
            {
                set { _storemoney = value; }
                get { return _storemoney; }
            }

            /// <summary>
            /// 现金支付金额
            /// </summary>
            public decimal? CashMoney
            {
                set { _cashmoney = value; }
                get { return _cashmoney; }
            }

            /// <summary>
            /// 未支付金额
            /// </summary>
            public decimal? UnpaidMoney
            {
                set { _unpaidmoney = value; }
                get { return _unpaidmoney; }
            }

            /// <summary>
            /// 是否发送短信账单
            /// </summary>
            public int? isSendSMS
            {
                set { _issendsms = value; }
                get { return _issendsms; }
            }

            /// <summary>
            /// 是否发送免费短信账单
            /// </summary>
            public int? isSendFreeSms
            {
                set { _issendfreesms = value; }
                get { return _issendfreesms; }
            }

            /// <summary>
            /// 是否打印小票
            /// </summary>
            public int? isPrintTicket
            {
                set { _isprintticket = value; }
                get { return _isprintticket; }
            }

            /// <summary>
            /// 操作员ID
            /// </summary>
            public int? operatorID
            {
                set { _operatorid = value; }
                get { return _operatorid; }
            }

            /// <summary>
            /// 操作时间
            /// </summary>
            public DateTime? operatorTime
            {
                set { _operatortime = value; }
                get { return _operatortime; }
            }

            /// <summary>
            /// 操作IP
            /// </summary>
            public string operatorIP
            {
                set { _operatorip = value; }
                get { return _operatorip; }
            }

            /// <summary>
            /// 整单备注
            /// </summary>
            public string Remark
            {
                set { _remark = value; }
                get { return _remark; }
            }

            #endregion Model
        }

        /// <summary>
        /// 计次卡列表
        /// </summary>
        public class TimeCard
        {
            /// <summary>
            /// 计次卡Id
            /// </summary>
            public int StId { get; set; }

            /// <summary>
            /// 计次卡名称
            /// </summary>
            public string CardName { get; set; }

            /// <summary>
            /// 卡片剩余次数
            /// </summary>
            public int StoreTimes { get; set; }

            /// <summary>
            /// 本次消费次数
            /// </summary>
            public int SumTimes { get; set; }

            /// <summary>
            /// 卡片原始剩余次数
            /// </summary>
            public int CardTimes { get; set; }

            /// <summary>
            /// 商品列表
            /// </summary>
            public List<SalesList> Data { get; set; }
        }

        public class SalesList
        {
            //销售详情列表

            #region Model

            private int? _salelistid;
            private string _saleno;
            private int? _accid;
            private int? _userid;
            private int? _isretail;
            private string _gMaxNm;
            private string _gMinNm;
            private string _gMax;
            private string _gMin;
            private int? _gid;
            private string _gName;
            private string _gGuige;
            private string _gNum;
            private string _gPrice;
            private string _gDiscount;
            private string _gMoney;
            private decimal? _fixmoney;
            private decimal? _realmoney;
            private decimal? _costprice;
            private int? _isintegral;
            private string _gOPuser;
            private string _gRemark;
            private DateTime? _saletime;
            private DateTime? _inserttime;
            private DateTime? _updatetime;
            private int? _operatorid;
            private DateTime? _operatortime;
            private string _barcode;
            private int _isService = 0;
            private decimal _gQuantity = 0;
            

            /// <summary>
            /// 销售列表ID
            /// </summary>
            public int? saleListID
            {
                set { _salelistid = value; }
                get { return _salelistid; }
            }

            /// <summary>
            /// 流水号
            /// </summary>
            public string saleNo
            {
                set { _saleno = value; }
                get { return _saleno; }
            }

            /// <summary>
            /// 店铺ID
            /// </summary>
            public int? accID
            {
                set { _accid = value; }
                get { return _accid; }
            }

            /// <summary>
            /// 会员ID
            /// </summary>
            public int? userID
            {
                set { _userid = value; }
                get { return _userid; }
            }

            /// <summary>
            /// 是否零售
            /// </summary>
            public int? isRetail
            {
                set { _isretail = value; }
                get { return _isretail; }
            }

            /// <summary>
            /// 大分类名称
            /// </summary>
            public string gMaxNm
            {
                set { _gMaxNm = value; }
                get { return _gMaxNm; }
            }

            /// <summary>
            /// 小分类名称
            /// </summary>
            public string gMinNm
            {
                set { _gMinNm = value; }
                get { return _gMinNm; }
            }

            /// <summary>
            /// 大分类ID
            /// </summary>
            public string gMax
            {
                set { _gMax = value; }
                get { return _gMax; }
            }

            /// <summary>
            /// 小分类ID
            /// </summary>
            public string gMin
            {
                set { _gMin = value; }
                get { return _gMin; }
            }

            /// <summary>
            /// 商品ID
            /// </summary>
            public int? gid
            {
                set { _gid = value; }
                get { return _gid; }
            }

            /// <summary>
            /// 商品名称
            /// </summary>
            public string gName
            {
                set { _gName = value; }
                get { return _gName; }
            }

            /// <summary>
            /// 商品规格
            /// </summary>
            public string gGuige
            {
                set { _gGuige = value; }
                get { return _gGuige; }
            }

            /// <summary>
            /// 商品数量
            /// </summary>
            public string gNum
            {
                set { _gNum = value; }
                get { return _gNum; }
            }

            /// <summary>
            /// 商品单价
            /// </summary>
            public string gPrice
            {
                set { _gPrice = value; }
                get { return _gPrice; }
            }

            /// <summary>
            /// 商品折扣
            /// </summary>
            public string gDiscount
            {
                set { _gDiscount = value; }
                get { return _gDiscount; }
            }

            /// <summary>
            /// 应付金额
            /// </summary>
            public string gMoney
            {
                set { _gMoney = value; }
                get { return _gMoney; }
            }

            /// <summary>
            /// 与实收金额差值平摊到该商品金额
            /// </summary>
            public decimal? FixMoney
            {
                set { _fixmoney = value; }
                get { return _fixmoney; }
            }

            /// <summary>
            /// 实收金额
            /// </summary>
            public decimal? RealMoney
            {
                set { _realmoney = value; }
                get { return _realmoney; }
            }

            /// <summary>
            /// 商品进价
            /// </summary>
            public decimal? CostPrice
            {
                set { _costprice = value; }
                get { return _costprice; }
            }

            /// <summary>
            /// 是否积分
            /// </summary>
            public int? isIntegral
            {
                set { _isintegral = value; }
                get { return _isintegral; }
            }

            /// <summary>
            /// 收银员ID
            /// </summary>
            public string gOPuser
            {
                set { _gOPuser = value; }
                get { return _gOPuser; }
            }

            /// <summary>
            /// 备注信息
            /// </summary>
            public string gRemark
            {
                set { _gRemark = value; }
                get { return _gRemark; }
            }

            /// <summary>
            /// 销售日期
            /// </summary>
            public DateTime? saleTime
            {
                set { _saletime = value; }
                get { return _saletime; }
            }

            /// <summary>
            /// 记录插入日期
            /// </summary>
            public DateTime? insertTime
            {
                set { _inserttime = value; }
                get { return _inserttime; }
            }

            /// <summary>
            /// 商品信息更新时间
            /// </summary>
            public DateTime? UpdateTime
            {
                set { _updatetime = value; }
                get { return _updatetime; }
            }

            /// <summary>
            /// 操作人ID
            /// </summary>
            public int? operatorID
            {
                set { _operatorid = value; }
                get { return _operatorid; }
            }

            /// <summary>
            /// 操作员时间
            /// </summary>
            public DateTime? operatorTime
            {
                set { _operatortime = value; }
                get { return _operatortime; }
            }

            /// <summary>
            /// 商品编码
            /// </summary>
            public string Barcode
            {
                get { return _barcode; }
                set { _barcode = value; }
            }

            /// <summary>
            /// 是否为服务类
            /// </summary>
            public int isService
            {
                set { _isService = value; }
                get { return _isService; }
            }

            /// <summary>
            /// 商品库存
            /// </summary>
            public decimal gQuantity
            {
                set { _gQuantity = value; }
                get { return _gQuantity; }
            }

            /// <summary>
            /// 挂单商品图片
            /// </summary>
            public string gPic { get; set; }


            /// <summary>
            /// 流水单号
            /// </summary>
            public long SerialNum { get; set; }

            /// <summary>
            /// 计次卡Id
            /// </summary>
            public int TimeCardId { get; set; }

            /// <summary>
            /// 商品原始折扣
            /// </summary>
            public decimal? gDiscountOrigin { get; set; }

            /// <summary>
            /// 是否商品扩展属性
            /// </summary>
            public int IsExtend { get; set; }

            /// <summary>
            /// 商品Sku属性Id
            /// </summary>
            public int SkuId { get; set; }


            /// <summary>
            /// 加料详细列表
            /// </summary>
            public SalesList()
            {
                additionalList = new List<T_AdditionalSaleListModel>();
            }
            public List<T_AdditionalSaleListModel> additionalList { get; set; }
            #endregion Model
        }

        /// <summary>
        /// 销售处理结果
        /// </summary>
        public class SaleResult
        {
            /// <summary>
            /// 处理结果
            /// </summary>
            public int okVal { get; set; }

            /// <summary>
            /// 销售记录ID
            /// </summary>
            public int saleID { get; set; }

            /// <summary>
            /// 是否为零售
            /// </summary>
            public int? isRetail { get; set; }

            /// <summary>
            /// 支付方式
            /// </summary>
            public int? payType { get; set; }

            /// <summary>
            /// 应收金额
            /// </summary>
            public decimal? ableMoney { get; set; }

            /// <summary>
            /// 实收金额
            /// </summary>
            public decimal? RealMoney { get; set; }

            /// <summary>
            /// 支付次数总额
            /// </summary>
            public int? storeTimes { get; set; }

            /// <summary>
            /// 支付储值总额
            /// </summary>
            public decimal? storeMoney { get; set; }

            /// <summary>
            /// 支付现金总额
            /// </summary>
            public decimal? cashMoney { get; set; }

            /// <summary>
            /// 支付刷卡总额
            /// </summary>
            public decimal? cardMoney { get; set; }

            /// <summary>
            /// 未付款总额
            /// </summary>
            public decimal? unpaidMoney { get; set; }
        }

        #endregion

        #endregion

        #region GoodsModel 商品相关

        #region GoodsClass 商品分类

        /// <summary>
        /// 商品分类Model
        /// </summary>
        public class GoodsClass
        {
            /// <summary>
            /// 大分类数量
            /// </summary>
            public int MaxCount { get; set; }

            /// <summary>
            /// 小分类数量
            /// </summary>
            public int MinCount { get; set; }

            /// <summary>
            /// 商品分类List
            /// </summary>
            public Dictionary<string, GoodsClassItem> ClassList { get; set; }
        }

        /// <summary>
        /// 商品分类Item
        /// </summary>
        public class GoodsClassItem
        {
            /// <summary>
            /// 大分类ID
            /// </summary>
            public int MaxID { get; set; }

            /// <summary>
            /// 大分类名称
            /// </summary>
            public string MaxName { get; set; }

            /// <summary>
            /// 分类的商品数量
            /// </summary>
            public int GoodsCount { get; set; }

            /// <summary>
            /// 小分类列表
            /// </summary>
            public Dictionary<string, string> MinClassList { get; set; }
        }

        #endregion

        #region GoodsTip 商品概要信息Tip

        /// <summary>
        /// 商品概要信息Tip
        /// </summary>
        public class GoodsTip
        {
            /// <summary>
            /// 商品ID
            /// </summary>
            public int gid { get; set; }

            /// <summary>
            /// 商品名称
            /// </summary>
            public string gName { get; set; }

            /// <summary>
            /// 商品数量
            /// </summary>
            public decimal gNum { get; set; }

            /// <summary>
            /// 商品规格
            /// </summary>
            public string gSpec { get; set; }

            /// <summary>
            /// 商品单价
            /// </summary>
            public decimal gPrice { get; set; }

            /// <summary>
            /// 商品大分类名称
            /// </summary>
            public string gMaxName { get; set; }

            /// <summary>
            /// 商品小分类名称
            /// </summary>
            public string gMinName { get; set; }

            /// <summary>
            /// 大分类ID
            /// </summary>
            public int gMaxID { get; set; }

            /// <summary>
            /// 小分类ID
            /// </summary>
            public int gMinID { get; set; }

            /// <summary>
            /// 商品折扣
            /// </summary>
            public decimal gDiscount { set; get; }

            /// <summary>
            /// 是否为服务
            /// </summary>
            public int isService { get; set; }
            /// <summary>
            /// 图片地址
            /// </summary>
            public string gPicUrl { get; set; }
        }

        #endregion

        #region GoodsInfo 商品信息

        /// <summary>
        /// 商品信息
        /// </summary>
        public class GoodsInfo
        {
            private int? _gid;
            private int _accid;
            private int _isservice = 0;
            private int _isdown = 0;
            private DateTime? _isdowntime;
            private string _gmaxname = "";
            private int _gmaxid = 0;
            private string _gminname = "";
            private int _gminid = 0;
            private string _gname;
            private decimal _gquantity;
            private decimal _gdiscount = -10;
            private string _gspecification;
            private decimal _gprice;
            private decimal? _gcostprice;
            private DateTime _gaddtime;
            private string _gbarcode;
            private string _gremark;
            private string _gpicurl;
            private string _gpy;
            private string _gpinyin;
            private string _gunionkey;
            private DateTime? _ginsertdate;
            private int? _goperatorid;
            private DateTime? _gcheckdate;
            private string[] _gpicurls;

            /// <summary>
            /// 商品ID
            /// </summary>		
            public int? gid
            {
                get { return _gid; }
                set { _gid = value; }
            }

            /// <summary>
            /// 店铺ID
            /// </summary>		
            public int accID
            {
                get { return _accid; }
                set { _accid = value; }
            }

            /// <summary>
            /// 是否为服务类型 ｛0：商品，1：服务｝
            /// </summary>		
            public int isService
            {
                get { return _isservice; }
                set { _isservice = value; }
            }

            /// <summary>
            /// 是否已下架 ｛0：正常，1：下架｝
            /// </summary>		
            public int isDown
            {
                get { return _isdown; }
                set { _isdown = value; }
            }

            /// <summary>
            /// 下架时间
            /// </summary>		
            public DateTime? isDownTime
            {
                get { return _isdowntime; }
                set { _isdowntime = value; }
            }

            /// <summary>
            /// 大分类名称
            /// </summary>		
            public string gMaxName
            {
                get { return _gmaxname; }
                set { _gmaxname = value; }
            }

            /// <summary>
            /// 大分类ID
            /// </summary>		
            public int gMaxID
            {
                get { return _gmaxid; }
                set { _gmaxid = value; }
            }

            /// <summary>
            /// 小分类名称
            /// </summary>		
            public string gMinName
            {
                get { return _gminname; }
                set { _gminname = value; }
            }

            /// <summary>
            /// 小分类ID
            /// </summary>		
            public int gMinID
            {
                get { return _gminid; }
                set { _gminid = value; }
            }

            /// <summary>
            /// 商品名称
            /// </summary>		
            public string gName
            {
                get { return _gname; }
                set { _gname = value; }
            }

            /// <summary>
            /// 商品数量
            /// </summary>		
            public decimal gQuantity
            {
                get { return _gquantity; }
                set { _gquantity = value; }
            }

            /// <summary>
            /// 商品折扣{ 0 大于 折扣 小于等于 10 }
            /// </summary>		
            public decimal gDiscount
            {
                get { return _gdiscount; }
                set { _gdiscount = value; }
            }

            /// <summary>
            /// 商品规格
            /// </summary>		
            public string gSpecification
            {
                get { return _gspecification; }
                set { _gspecification = value; }
            }

            /// <summary>
            /// 商品价格
            /// </summary>		
            public decimal gPrice
            {
                get { return _gprice; }
                set { _gprice = value; }
            }

            /// <summary>
            /// 商品进价
            /// </summary>		
            public decimal? gCostPrice
            {
                get { return _gcostprice; }
                set { _gcostprice = value; }
            }

            /// <summary>
            /// 商品添加时间
            /// </summary>		
            public DateTime gAddTime
            {
                get { return _gaddtime; }
                set { _gaddtime = value; }
            }

            /// <summary>
            /// 商品编码
            /// </summary>		
            public string gBarcode
            {
                get { return _gbarcode; }
                set { _gbarcode = value; }
            }

            /// <summary>
            /// 商品备注
            /// </summary>		
            public string gRemark
            {
                get { return _gremark; }
                set { _gremark = value; }
            }

            /// <summary>
            /// 商品图片
            /// <para>因为多图片 此属性 停用</para>
            /// </summary>		
            public string gPicUrl
            {
                get { return _gpicurl; }
                set { _gpicurl = value; }
            }

            /// <summary>
            /// 拼音首字母
            /// </summary>		
            public string gPY
            {
                get { return _gpy; }
                set { _gpy = value; }
            }

            /// <summary>
            /// 拼音全拼
            /// </summary>		
            public string gPinYin
            {
                get { return _gpinyin; }
                set { _gpinyin = value; }
            }

            /// <summary>
            /// 联合查询值(拼音,编码)
            /// </summary>		
            public string gUnionKey
            {
                get { return _gunionkey; }
                set { _gunionkey = value; }
            }

            /// <summary>
            /// 记录写入时间
            /// </summary>		
            public DateTime? gInsertDate
            {
                get { return _ginsertdate; }
                set { _ginsertdate = value; }
            }

            /// <summary>
            /// 操作人员ID
            /// </summary>		
            public int? gOperatorID
            {
                get { return _goperatorid; }
                set { _goperatorid = value; }
            }

            /// <summary>
            /// 盘点时间
            /// </summary>		
            public DateTime? gCheckDate
            {
                get { return _gcheckdate; }
                set { _gcheckdate = value; }
            }

            /// <summary>
            /// 上次盘点距今天数
            /// </summary>
            public int? CheckDays { get; set; }

            /// <summary>
            /// 商品图片列表
            /// </summary>
            public string[] gPicUrls
            {
                get { return _gpicurls; }
                set { _gpicurls = value; }
            }

            /// <summary>
            /// 供应商ID
            /// </summary>
            public int SupplierId { get; set; }
        }

        #endregion

        #region GoodsExists 重复商品检测信息

        /// <summary>
        /// 重复商品检测信息
        /// </summary>
        public class GoodsExists
        {
            /// <summary>
            /// 状态
            /// </summary>
            public int status { get; set; }

            /// <summary>
            /// 大分类Id
            /// </summary>
            public int gMaxID { get; set; }

            /// <summary>
            /// 小分类Id
            /// </summary>
            public int gMinID { get; set; }

            /// <summary>
            /// 大分类名称
            /// </summary>
            public string gMaxName { get; set; }

            /// <summary>
            /// 小分类名称
            /// </summary>
            public string gMinName { get; set; }

            /// <summary>
            /// 商品名称
            /// </summary>
            public string gName { get; set; }
        }

        #endregion

        #region GoodsQuery 商品列表查询参数

        /// <summary>
        /// 商品列表查询参数
        /// </summary>
        public class GoodsQuery
        {
            /// <summary>
            /// 店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 页数
            /// </summary>
            public int Page { get; set; }

            /// <summary>
            /// 分类类型
            /// </summary>
            public int ClassType { get; set; }

            /// <summary>
            /// 分类Id
            /// </summary>
            public int ClassVal { get; set; }

            /// <summary>
            /// 关键词
            /// </summary>
            public string KeyWord { get; set; }

            /// <summary>
            /// 排序名称
            /// </summary>
            public string SortName { get; set; }

            /// <summary>
            /// 排序类型
            /// </summary>
            public string SortType { get; set; }

            /// <summary>
            /// 最小库存
            /// </summary>
            public decimal? MinVal { get; set; }

            /// <summary>
            /// 最大库存
            /// </summary>
            public decimal? MaxVal { get; set; }
            /// <summary>
            /// 供应商Id
            /// </summary>
            public int? SupplierId { get; set; }
        }

        #endregion

        #region GoodsInfoModel 商品信息

        /// <summary>
        /// 商品信息
        /// </summary>
        public class GoodsInfoModel
        {
            /// <summary>
            /// 当前页码
            /// </summary>
            public int PageNow { get; set; }

            /// <summary>
            /// 总行数
            /// </summary>
            public int RowCnt { get; set; }

            /// <summary>
            /// 下架商品数量
            /// </summary>
            public int StockDown { get; set; }

            /// <summary>
            /// 正库存数量
            /// </summary>
            public decimal PositiveNum { get; set; }

            /// <summary>
            /// 正库存金额
            /// </summary>
            public decimal PositiveSum { get; set; }

            /// <summary>
            /// 负库存数量
            /// </summary>
            public decimal NegativeNum { get; set; }

            /// <summary>
            /// 负库存金额
            /// </summary>
            public decimal NegativeSum { get; set; }

            /// <summary>
            /// 详细列表
            /// </summary>
            public List<GoodsItemModel> Data { get; set; }
        }

        public class GoodsItemModel
        {
            /// <summary>
            /// 商品Id
            /// </summary>
            public int Gid { get; set; }

            /// <summary>
            /// 是否服务类商品
            /// </summary>
            public int IsService { get; set; }

            /// <summary>
            /// 是否已下架商品
            /// </summary>
            public int IsDown { get; set; }

            /// <summary>
            /// 大分类Id
            /// </summary>
            public int MaxClassId { get; set; }

            /// <summary>
            /// 大分类名称
            /// </summary>
            public string MaxClassName { get; set; }

            /// <summary>
            /// 小分类Id
            /// </summary>
            public int MinClassId { get; set; }

            /// <summary>
            /// 小分类名称
            /// </summary>
            public string MinClassName { get; set; }

            /// <summary>
            /// 商品名称
            /// </summary>
            public string GoodsName { get; set; }

            /// <summary>
            /// 商品库存
            /// </summary>
            public decimal Quantity { get; set; }

            /// <summary>
            /// 折扣
            /// </summary>
            public decimal Discount { get; set; }

            /// <summary>
            /// 规格信息
            /// </summary>
            public string Specification { get; set; }

            /// <summary>
            /// 销售价格
            /// </summary>
            public decimal Price { get; set; }

            /// <summary>
            /// 进货价格
            /// </summary>
            public decimal CostPrice { get; set; }

            /// <summary>
            /// 添加日期
            /// </summary>
            public DateTime AddTime { get; set; }

            /// <summary>
            /// 商品编码
            /// </summary>
            public string Barcode { get; set; }

            /// <summary>
            /// 商品备注
            /// </summary>
            public string Remark { get; set; }

            /// <summary>
            /// 商品图片Url
            /// </summary>
            public string PicUrl { get; set; }

            /// <summary>
            /// 盘点日期
            /// </summary>
            public DateTime? CheckDate { get; set; }

            /// <summary>
            /// 产品图片列表
            /// </summary>
            public string[] PicUrls { get; set; }

            /// <summary>
            /// 是否存在扩展属性
            /// </summary>
            public int IsExtend { get; set; }
            /// <summary>
            /// 供应商ID
            /// </summary>
            public int SupplierId { get; set; }
            /// <summary>
            /// 供应商姓名
            /// </summary>
            public string SupplierName { get; set; }
            /// <summary>
            /// 是否显示进价（移动端）
            /// </summary>
            public bool IsShowCostPrice { get; set; }
        }

        #endregion

        #region StockInOut 入库出库Model

        /// <summary>
        /// 入库出库Model
        /// </summary>
        public class StockInOut
        {
            /// <summary>
            /// 店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 商品Id
            /// </summary>
            public int GoodsId { get; set; }

            /// <summary>
            /// 商品数量
            /// </summary>
            public decimal GoodsNum { get; set; }

            /// <summary>
            /// 分类
            /// </summary>
            public int Class { get; set; }

            /// <summary>
            /// 操作日期
            /// </summary>
            public DateTime OpDate { get; set; }

            /// <summary>
            /// 备注信息
            /// </summary>
            public string Remark { get; set; }

            /// <summary>
            /// 操作人员Id
            /// </summary>
            public int OperatorId { get; set; }

            /// <summary>
            /// 操作人员Ip
            /// </summary>
            public string OperatorIp { get; set; }
        }

        #endregion

        #region 商品概要信息

        /// <summary>
        /// 商品概要信息
        /// </summary>
        public class GoodsSummaryModel
        {
            /// <summary>
            /// 店铺ID
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 大分类数量
            /// </summary>
            public int MaxClassCnt { get; set; }

            /// <summary>
            /// 小分类数量
            /// </summary>
            public int MinClassCnt { get; set; }

            /// <summary>
            /// 商品种类
            /// </summary>
            public int GoodsNum { get; set; }

            /// <summary>
            /// 正库存数量
            /// </summary>
            public decimal PlusGoodsNum { get; set; }

            /// <summary>
            /// 正库存成本
            /// </summary>
            public decimal PlusGoodsSum { get; set; }

            /// <summary>
            /// 负库存数量
            /// </summary>
            public decimal MinusGoodsNum { get; set; }

            /// <summary>
            /// 负库存成本
            /// </summary>
            public decimal MinusGoodsSum { get; set; }

            /// <summary>
            /// 预计营收金额
            /// </summary>
            public decimal futrueRevenue { get; set; }
        }

        #endregion

        #region 商品 SKUList

        /// <summary>
        /// 商品SKUList
        /// </summary>
        public class SkuItem
        {
            /// <summary>
            /// 扩展Id
            /// </summary>
            public int gsId { get; set; }

            /// <summary>
            /// 商品Id
            /// </summary>
            public int gid { get; set; }

            /// <summary>
            /// 商品数量
            /// </summary>
            public decimal gsQuantity { get; set; }

            /// <summary>
            /// 商品折扣
            /// </summary>
            public decimal gsDiscount { get; set; }

            /// <summary>
            /// 商品单价
            /// </summary>
            public decimal gsPrice { get; set; }

            /// <summary>
            /// 商品进价
            /// </summary>
            public decimal gsCostPrice { get; set; }

            /// <summary>
            /// 商品编码
            /// </summary>
            public string gsBarcode { get; set; }

            /// <summary>
            /// 盘点日期
            /// </summary>
            public DateTime? gsCheckDate { get; set; }

            /// <summary>
            /// 添加日期
            /// </summary>
            public DateTime gsTime { get; set; }

            /// <summary>
            /// 店铺Id
            /// </summary>
            public int Accid { get; set; }

            /// <summary>
            /// 扩展属性内容
            /// </summary>
            public string ExtendVal { get; set; }

            /// <summary>
            /// 自定义属性列表
            /// </summary>
            public AttributeItem[] Attributes { get; set; }

            /// <summary>
            /// 上次盘点距今天数
            /// </summary>
            public int? CheckDays { get; set; }
        }

        /// <summary>
        /// 自定义属性Item
        /// </summary>
        public class AttributeItem
        {
            /// <summary>
            /// 属性关联Id
            /// </summary>
            public int grId { get; set; }

            /// <summary>
            /// 属性关系Id
            /// </summary>
            public int gaId { get; set; }

            /// <summary>
            /// 自定义属性Id
            /// </summary>
            public string gaName { get; set; }

            /// <summary>
            /// 自定义属性名称
            /// </summary>
            public int gaVid { get; set; }

            /// <summary>
            /// 自定义属性内容
            /// </summary>
            public string gaVName { get; set; }
        }

        #endregion

        #endregion

        #region CouponModel 优惠券相关

        #region CouponModel 优惠券Model

        /// <summary>
        /// 优惠券Model
        /// </summary>
        public class CouponModel
        {
            /// <summary>
            /// 优惠券ID
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 优惠券概要ID
            /// </summary>
            public int GroupId { get; set; }

            /// <summary>
            /// 店铺ID
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 优惠券编码
            /// </summary>
            public string CouponId { get; set; }

            /// <summary>
            /// 优惠券Value
            /// </summary>
            public int CouponValue { get; set; }

            /// <summary>
            /// 优惠券状态
            /// </summary>
            public int CouponStatus { get; set; }

            /// <summary>
            /// 优惠券状态Desc
            /// </summary>
            public string CouponStatusDesc { get; set; }

            /// <summary>
            /// 优惠券生成日期
            /// </summary>
            public DateTime CreateDate { get; set; }

            /// <summary>
            /// 优惠券截止日期
            /// </summary>
            public DateTime EndDate { get; set; }

            /// <summary>
            /// 优惠券使用时间
            /// </summary>
            public DateTime? UseDate { get; set; }

            /// <summary>
            /// 优惠券绑定会员ID
            /// </summary>
            public int ToUserId { get; set; }

            /// <summary>
            /// 优惠券绑定会员Name
            /// </summary>
            public string ToUserName { get; set; }

            /// <summary>
            /// 优惠券使用会员ID
            /// </summary>
            public int UseUserId { get; set; }

            /// <summary>
            /// 优惠券使用会员Name
            /// </summary>
            public string UseUserName { get; set; }

            /// <summary>
            /// 附加信息
            /// </summary>
            public string Flag { get; set; }

            /// <summary>
            /// 优惠券类型
            /// </summary>
            public string CouponClass { get; set; }

            /// <summary>
            /// 优惠券类别
            /// </summary>
            public string CouponType { get; set; }

            /// <summary>
            /// 优惠券规则Id
            /// </summary>
            public int CouponRuleId { get; set; }

            /// <summary>
            /// 优惠券规则描述
            /// </summary>
            public string CouponRuleDesc { get; set; }

            /// <summary>
            /// 优惠券规则描述
            /// </summary>
            public string CouponRuleRemark { get; set; }

            /// <summary>
            /// 优惠券规则Value
            /// </summary>
            public int CouponRuleVal { get; set; }

            /// <summary>
            /// 优惠券状态
            /// </summary>
            public int CouponInfoStatus { get; set; }

            /// <summary>
            /// 优惠券状态描述
            /// </summary>
            public string CouponInfoStatusDesc { get; set; }

            /// <summary>
            /// 优惠券描述
            /// </summary>
            public string CouponDesc { get; set; }
        }

        /// <summary>
        /// 优惠券概要Model
        /// </summary>
        [Serializable]
        public class CouponInfoModel
        {
            /// <summary>
            /// 记录总数
            /// </summary>
            public int MaxCount { get; set; }

            /// <summary>
            /// 当前页
            /// </summary>
            public int NowPage { get; set; }

            /// <summary>
            /// 分页Html
            /// </summary>
            public string PageHtml { get; set; }

            /// <summary>
            /// 总计优惠券批次
            /// </summary>
            public int SumCount { get; set; }

            /// <summary>
            /// 优惠券概要信息Item
            /// </summary>
            public List<CouponInfoItem> CouponInfoItems { get; set; }
        }

        /// <summary>
        /// 优惠券概要信息Item
        /// </summary>
        public class CouponInfoItem
        {
            /// <summary>
            /// 优惠券批次ID
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 店铺ID
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 优惠券类型
            /// </summary>
            public string CouponClass { get; set; }

            /// <summary>
            /// 优惠券类别
            /// </summary>
            public string CouponType { get; set; }

            /// <summary>
            /// 优惠券规则Id
            /// </summary>
            public int CouponRuleId { get; set; }

            /// <summary>
            /// 优惠券规则描述
            /// </summary>
            public string CouponRuleDesc { get; set; }

            /// <summary>
            /// 优惠券规则描述
            /// </summary>
            public string CouponRuleRemark { get; set; }

            /// <summary>
            /// 优惠券规则Value
            /// </summary>
            public int CouponRuleVal { get; set; }

            /// <summary>
            /// 优惠内容Value
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
            /// 优惠券描述
            /// </summary>
            public string CouponDesc { get; set; }

            /// <summary>
            /// 限制最大数量
            /// </summary>
            public int MaxLimitNum { get; set; }

            /// <summary>
            /// 创建日期
            /// </summary>
            public DateTime CreateDate { get; set; }

            /// <summary>
            /// 截至日期
            /// </summary>
            public DateTime EndDate { get; set; }

            /// <summary>
            /// 创建人员ID
            /// </summary>
            public int OperatorId { get; set; }

            /// <summary>
            /// 创建人员姓名
            /// </summary>
            public string OperatorName { get; set; }

            /// <summary>
            /// 优惠码数量
            /// </summary>
            public int CouponListCount { get; set; }

            /// <summary>
            /// 剩余可用数
            /// </summary>
            public int BalanceCount { get; set; }

        }

        #endregion

        #endregion

        #region RegAccount 店铺注册信息

        /// <summary>
        /// 店铺注册信息
        /// </summary>
        public class RegAccount
        {
            /// <summary>
            /// 店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 手机号码
            /// </summary>
            public string PhoneNum { get; set; }

            /// <summary>
            /// 店铺名称
            /// </summary>
            public string StoreName { get; set; }

            /// <summary>
            /// 店主姓名
            /// </summary>
            public string RealName { get; set; }

            /// <summary>
            /// 登录密码
            /// </summary>
            public string PassWord { get; set; }

            /// <summary>
            /// 注册验证码
            /// </summary>
            public string RandomNum { get; set; }

            /// <summary>
            /// 注册状态
            /// </summary>
            public int Status { get; set; }

            /// <summary>
            /// 代理商Id
            /// </summary>
            public int AgentId { get; set; }

            /// <summary>
            /// 附加信息(注册来源)
            /// </summary>
            public string RegFlag { get; set; }

            /// <summary>
            /// 附加信息
            /// </summary>
            public string FlagText { get; set; }

            /// <summary>
            /// 推荐信息
            /// </summary>
            public string RecommandCode { get; set; }

            /// <summary>
            /// 注册来源
            /// </summary>
            public string FromName { get; set; }

            /// <summary>
            /// 用户IP
            /// </summary>
            public string UserIp { get; set; }
        }

        #endregion

        #region MobileWebSite 手机橱窗Url

        /// <summary>
        /// 手机橱窗Url
        /// </summary>
        public class MobileWebSite
        {
            /// <summary>
            /// 店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 应用Key
            /// </summary>
            public string AppKey { get; set; }

            /// <summary>
            /// 原始Url
            /// </summary>
            public string OriginUrl { get; set; }

            /// <summary>
            /// 短链Url
            /// </summary>
            public string ShortUrl { get; set; }
        }

        #endregion

        #region GoodsInventory

        public class GoodsInventory
        {
            /// <summary>
            /// 商品ID
            /// </summary>
            public int gid { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            public string num { get; set; }

            /// <summary>
            /// 旧数量
            /// </summary>
            public string oldnum { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public string remark { get; set; }

            /// <summary>
            /// 处理人IP
            /// </summary>
            public string Ip { get; set; }
        }

        #endregion


        #region NoGoodsSaleQueryModel 无商品URL传值对象

        /// <summary>
        /// 无商品URL传值对象
        /// </summary>
        public class NoGoodsSaleQueryModel
        {
            /// <summary>
            /// 
            /// </summary>
            public string saleNo { get; set; }

            /// <summary>
            /// 店铺ID
            /// </summary>
            public int accid { get; set; }

            /// <summary>
            /// 是否零售
            /// </summary>
            public int? isRetail { get; set; }

            /// <summary>
            /// 会员ID
            /// </summary>
            public int guid { get; set; }

            /// <summary>
            /// 会员密码
            /// </summary>
            public string userPwd { get; set; }

            /// <summary>
            /// 支付方式
            /// </summary>
            public string gPayType { get; set; }

            /// <summary>
            /// 消费金额
            /// </summary>
            public string gRealMoney { get; set; }

            /// <summary>
            /// 是否发送短信
            /// </summary>
            public int? isSendSMS { get; set; }

            /// <summary>
            /// 销售时间
            /// </summary>
            public string gDate { get; set; }

            /// <summary>
            /// 收银员
            /// </summary>
            public int CashierId { get; set; }

            /// <summary>
            /// 无商品销售备注
            /// </summary>
            public string Remark { get; set; }

            /// <summary>
            /// 加料list
            /// </summary>
            public NoGoodsSaleQueryModel()
            {
                additionalList = new List<T_AdditionalSaleListModel>();
            }

            public List<T_AdditionalSaleListModel> additionalList { get; set; }

        }

        #endregion

        #region AlipayRequestLite 支付宝支付请求

        /// <summary>
        /// 支付宝支付请求
        /// </summary>
        public class AlipayRequestLite
        {

            /// <summary>
            /// 店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 操作员Id
            /// </summary>
            public int OperatorId { get; set; }

            /// <summary>
            /// 销售Id
            /// </summary>
            public int SaleId { get; set; }

            /// <summary>
            /// 订单标题
            /// </summary>
            public string Subject { get; set; }

            /// <summary>
            /// 订单金额
            /// </summary>
            public decimal TotalMoney { get; set; }

            /// <summary>
            /// 支付宝收款码
            /// </summary>
            public string Dynamicid { get; set; }
        }

        #endregion

        #region AlipayStatus 支付宝返回信息

        /// <summary>
        /// 支付宝返回信息
        /// </summary>
        public class AlipayStatus
        {
            /// <summary>
            /// 请求状态
            /// </summary>
            public bool QueryStatus { get; set; }

            /// <summary>
            /// 支付状态
            /// </summary>
            public int PayStatus { get; set; }

            /// <summary>
            /// 签名方式
            /// </summary>
            public string SignType { get; set; }

            /// <summary>
            /// 签名
            /// </summary>
            public string Sign { get; set; }

            /// <summary>
            /// 错误代码
            /// </summary>
            public string Error { get; set; }

            /// <summary>
            /// 支付宝交易号
            /// </summary>
            public string TradeNo { get; set; }

            /// <summary>
            /// 订单号码
            /// </summary>
            public string OrderNo { get; set; }

            /// <summary>
            /// 买家支付宝用户号
            /// </summary>
            public string BuyerId { get; set; }

            /// <summary>
            /// 买家支付宝账号
            /// </summary>
            public string BuyerAccount { get; set; }

            /// <summary>
            /// 响应代码
            /// </summary>
            public string ResultCode { get; set; }

            /// <summary>
            /// 详细错误码
            /// </summary>
            public string ResultDetailCode { get; set; }

            /// <summary>
            /// 详细错误描述
            /// </summary>
            public string ResultDetailDesc { get; set; }

            /// <summary>
            /// 扩展信息
            /// </summary>
            public string ExtendInfo { get; set; }

            /// <summary>
            /// 支付单据信息
            /// </summary>
            public string BillList { get; set; }

            /// <summary>
            /// 交易付款时间
            /// </summary>
            public DateTime? PaymentTime { get; set; }
        }

        /// <summary>
        /// 支付异步通知信息
        /// </summary>
        public class AlipayAsyncStatus : AlipayStatus
        {
            /// <summary>
            /// 通知Id
            /// </summary>
            public string NotifyId { get; set; }

            /// <summary>
            /// 通知时间
            /// </summary>
            public DateTime NotifyTime { get; set; }

            /// <summary>
            /// 通知类型
            /// </summary>
            public string NotifyType { get; set; }

            /// <summary>
            /// 通知动作类型
            /// </summary>
            public string NotifyActType { get; set; }

            /// <summary>
            /// 订单标题
            /// </summary>
            public string Subject { get; set; }

            /// <summary>
            /// 交易状态
            /// </summary>
            public string TradeStatus { get; set; }

            /// <summary>
            /// 卖家用户号
            /// </summary>
            public string SellerId { get; set; }

            /// <summary>
            /// 卖家支付宝账号
            /// </summary>
            public string SellerAccount { get; set; }

            /// <summary>
            /// 交易金额
            /// </summary>
            public decimal TotalMoney { get; set; }

            /// <summary>
            /// 退款金额
            /// </summary>
            public decimal RefundMoney { get; set; }

            /// <summary>
            /// 商户业务号
            /// </summary>
            public string BizNo { get; set; }

            /// <summary>
            /// 支付金额信息
            /// </summary>
            public string PayInfo { get; set; }

            /// <summary>
            /// 打款至卖家账号时间
            /// </summary>
            public string SendPayDate { get; set; }
        }

        #endregion

        #region QueryRequest 查询请求参数

        /// <summary>
        /// 查询请求参数
        /// </summary>
        public class QueryRequest
        {
            /// <summary>
            /// 店铺Id
            /// </summary>
            public int AccId { get; set; }

            /// <summary>
            /// 订单编号
            /// </summary>
            public string OrderNo { get; set; }

            /// <summary>
            /// 交易编号
            /// </summary>
            public string TradeNo { get; set; }
        }

        #endregion

        #region VerifyCode 找回密码验证

        /// <summary>
        /// 找回密码验证
        /// </summary>
        public class VerifyCode
        {
            /// <summary>
            /// 店铺/// 登录手机号
            /// </summary>
            public string PhoneNum { get; set; }

            /// <summary>
            /// 状态Token
            /// </summary>
            public string Token { get; set; }

            /// <summary>
            /// 验证码
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Pwd { get; set; }
        }

        #endregion

        #region SingleParamModel 单值Post模型

        /// <summary>
        /// 单值Post模型
        /// </summary>
        public class SingleValue
        {
            public string val { get; set; }
        }

        #endregion

        #region CategoryModel 分类信息

        /// <summary>
        /// 单值Post模型
        /// </summary>
        public class CategoryModel
        {
            /// <summary>
            /// 当前大分类ID
            /// </summary>
            public string curCatID { get; set; }

            /// <summary>
            /// 移动到的大分类ID
            /// </summary>
            public string newCatID { get; set; }

            /// <summary>
            /// 小分类处理类型
            /// 1-删除小分类 2-移动小分类
            /// </summary>
            public int type { get; set; }

            /// <summary>
            /// 分类名称
            /// </summary>
            public string catName { get; set; }
        }

        #endregion

        #region 店铺基本信息

        /// <summary>
        /// 店铺基本信息
        /// </summary>
        public class AccountInfo
        {
            /// <summary>
            /// 账号id
            /// </summary>
            public int? accID { get; set; }

            /// <summary>
            /// 店铺状态
            /// </summary>
            public int? accStatus { get; set; }

            /// <summary>
            /// 店铺全称
            /// </summary>
            public string accName { get; set; }

            /// <summary>
            /// 店铺简称
            /// </summary>
            public string accShortName { get; set; }

            /// <summary>
            /// 店铺地址
            /// </summary>
            public string accAdress { get; set; }

            /// <summary>
            /// 店主姓名
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 注册日期
            /// </summary>
            public DateTime? RegDate { get; set; }

            /// <summary>
            /// 手机号码
            /// </summary>
            public string UserPhone { get; set; }

            /// <summary>
            /// 电子信箱
            /// </summary>
            public string UserEmail { get; set; }

            /// <summary>
            /// 固定电话
            /// </summary>
            public string UserTel { get; set; }

            /// <summary>
            /// 店铺版本
            /// </summary>
            public int? accVer { get; set; }

            /// <summary>
            /// 店铺版本名称
            /// </summary>
            public string accVerName { get; set; }

            /// <summary>
            /// 名称前缀
            /// </summary>
            public string VerprefixName { get; set; }

            /// <summary>
            /// 版本开始时间
            /// </summary>
            public DateTime? verBgDate { get; set; }

            /// <summary>
            /// 版本结束时间
            /// </summary>
            public DateTime? verEdDate { get; set; }

            /// <summary>
            /// 剩余短信数
            /// </summary>
            public int? accSms { get; set; }

            /// <summary>
            /// 会员最大数
            /// </summary>
            public int? accMaxUser { get; set; }

            /// <summary>
            /// 店铺积分
            /// </summary>
            public int? accIntegral { get; set; }

            /// <summary>
            /// 会员积分设置-积分比例
            /// </summary>
            public decimal? propIntegralVal { get; set; }

            /// <summary>
            /// 会员积分设置-金额比例
            /// </summary>
            public decimal? propIntegralMoney { get; set; }

            /// <summary>
            /// 代理商ID
            /// </summary>
            public int? AgentId { get; set; }

            /// <summary>
            /// 行业类型
            /// </summary>
            public string ShopType { get; set; }
        }

        #endregion

        #region AccountModify 店铺信息修改

        /// <summary>
        /// 店铺信息修改
        /// </summary>
        public class AccountModify
        {
            /// <summary>
            /// 字段值
            /// </summary>
            public string field { get; set; }

            /// <summary>
            /// 值
            /// </summary>
            public string val { get; set; }
        }

        #endregion

        #region 应用程序权限

        /// <summary>
        /// 应用程序权限
        /// </summary>
        public class AppHomePower
        {
            /// <summary>
            /// 应用程序列表
            /// </summary>
            public Dictionary<int, AppHomePowerItem> AppList { get; set; }
        }

        #endregion

        #region 应用程序权限Item

        /// <summary>
        /// 应用程序权限Item
        /// </summary>
        public class AppHomePowerItem
        {
            /// <summary>
            /// App标识值
            /// </summary>
            public int AppKey { get; set; }

            /// <summary>
            /// App名称
            /// </summary>
            public string AppName { get; set; }

            /// <summary>
            /// 状态
            /// </summary>
            public int Status { get; set; }

            /// <summary>
            /// 截至日期
            /// </summary>
            public DateTime EdDate { get; set; }

            /// <summary>
            /// 访问短链接
            /// </summary>
            public string ShortUrl { get; set; }

            /// <summary>
            /// 权限版本
            /// </summary>
            public int Version { get; set; }
        }

        #endregion

        #region 销售分析

        /// <summary>
        /// 销售分析
        /// </summary>
        public class SalesView
        {
            /// <summary>
            /// 销售金额
            /// </summary>
            public decimal SalesSum { get; set; }

            /// <summary>
            /// 毛利润
            /// </summary>
            public decimal ProfitSum { get; set; }

            /// <summary>
            /// 销售数量
            /// </summary>
            public decimal QuantitySum { get; set; }

            /// <summary>
            /// 支出总额
            /// </summary>
            public decimal PaySum { get; set; }

            /// <summary>
            /// 净利润
            /// </summary>
            public decimal NetPorfit { get; set; }

            /// <summary>
            /// 商品成本
            /// </summary>
            public decimal CostSum { get; set; }

            /// <summary>
            /// 累计会员
            /// </summary>
            public int MemberSum { get; set; }

            //public List<salesItem> Data { get; set; }
        }

        /// <summary>
        /// 销售分析
        /// </summary>
        public class SalesViews
        {
            /// <summary>
            /// 销售金额
            /// </summary>
            public decimal SalesSum { get; set; }

            /// <summary>
            /// 毛利润
            /// </summary>
            public decimal ProfitSum { get; set; }

            /// <summary>
            /// 销售数量
            /// </summary>
            public decimal QuantitySum { get; set; }

            /// <summary>
            /// 支出总额
            /// </summary>
            public decimal PaySum { get; set; }

            /// <summary>
            /// 净利润
            /// </summary>
            public decimal NetPorfit { get; set; }

            /// <summary>
            /// 商品成本
            /// </summary>
            public decimal CostSum { get; set; }

            /// <summary>
            /// 累计会员
            /// </summary>
            public int MemberSum { get; set; }

            public List<salesItem> Data { get; set; }

            /// <summary>
            /// 按小时显示的JSON  用于显示当天数据
            /// </summary>
            public List<salesItem> HourData { get; set; }


        }

        public class salesItem
        {
            /// <summary>
            /// 会员数量
            /// </summary>
            public int MemberCnt { get; set; }

            /// <summary>
            /// 销售数量
            /// </summary>
            public decimal SalesCount { get; set; }

            /// <summary>
            /// 销售金额
            /// </summary>
            public decimal SalesSum { get; set; }

            /// <summary>
            /// 销售成本
            /// </summary>
            public decimal CostSum { get; set; }

            /// <summary>
            /// 毛利润
            /// </summary>
            public decimal Profit { get; set; }

            /// <summary>
            /// 支出金额
            /// </summary>
            public decimal PaySum { get; set; }

            /// <summary>
            /// 显示日期
            /// </summary>
            public string DateDisplay { get; set; }
        }

        #endregion

        #region MUserImportModel 移动端会员导入Model

        /// <summary>
        /// 移动端会员导入Model
        /// </summary>
        public class MUserImportModel
        {
            /// <summary>
            /// 会员名称
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 会员手机号
            /// </summary>
            public string UserPhone { get; set; }

            /// <summary>
            /// 会员生日
            /// </summary>
            public string UserBirthday { get; set; }

        }

        #endregion

        #region MUserImportErrModel 移动端会员导入出错返回Model

        /// <summary>
        /// 移动端会员导入出错返回Model
        /// </summary>
        public class MUserImportErrModel
        {
            /// <summary>
            /// 会员名称
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 错误类型
            /// </summary>
            public string ErrMsg { get; set; }

        }

        #endregion

        #region 销售金额对比信息

        /// <summary>
        /// 销售金额对比信息
        /// </summary>
        public class SaleNumCompare
        {
            public decimal saleNumToday { get; set; }
            public decimal saleNumYesterday { get; set; }
            public decimal saleNumLatest7days { get; set; }
            public decimal saleNumPeer7days { get; set; }
            public decimal saleNumMonth { get; set; }
            public decimal saleNumPeerMonth { get; set; }

            /// <summary>
            /// 店铺销售的商品种类数量
            /// </summary>
            public int GoodsCategoryNum { get; set; }
        }

        #endregion

        #region 获取会员扩展支付方式

        /// <summary>
        /// 获取会员扩展支付方式
        /// </summary>
        public class UserPayment
        {
            public int userId { get; set; }
            public decimal totalMoney { get; set; }
        }

        #endregion

        #region 赠送优惠券参数

        /// <summary>
        /// 赠送优惠券参数
        /// </summary>
        public class SendCoupon
        {
            public int accId { get; set; }
            public int userId { get; set; }
            public int couponId { get; set; }
            public int iSendSms { get; set; }
        }

        #endregion

        #region 签到信息

        /// <summary>
        /// 签到信息
        /// </summary>
        public class SignInModel
        {
            /// <summary>
            /// 签到状态
            /// </summary>
            public int Status { get; set; }

            /// <summary>
            /// 连续签到天数
            /// </summary>
            public int SerialDay { get; set; }

            /// <summary>
            /// 签到增加会员空间数
            /// </summary>
            public int AddReg { get; set; }

            /// <summary>
            /// 签到增加积分数
            /// </summary>
            public int AddIntegral { get; set; }
            /// <summary>
            /// 当前积分
            /// </summary>
            public int Now_Integral { get; set; }

            /// <summary>
            /// 签到选项 1-名人名言 2-系统文章 3-系统公告
            /// </summary>
            public int SignOption { get; set; }

            /// <summary>
            /// 签到名人名言
            /// </summary>
            public string SignWord { get; set; }

            /// <summary>
            /// 签到图片
            /// </summary>
            public SignInDetail SignPic { get; set; }

            /// <summary>
            /// 签到信息列表
            /// </summary>
            public List<SignInDetail> SignList { get; set; }
        }

        #endregion

        #region 签到信息

        public class SignInDetail
        {
            /// <summary>
            /// 签到文章id
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 签到文章标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 签到文章内容
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// 签到文章图片
            /// </summary>
            public string Pic { get; set; }

            /// <summary>
            /// 签到文章作者
            /// </summary>
            public string Author { get; set; }

            /// <summary>
            /// 签到文章日期
            /// </summary>
            public string PublishDate { get; set; }
        }

        #endregion

        #region 获取广告

        public class Sys_AdModel
        {
            public Sys_AdModel()
            {
                adList = new List<Ads>();
            }

            public List<Ads> adList { get; set; }
        }

        /// <summary>
        /// 广告信息
        /// </summary>
        public class Ads
        {
            public string title { get; set; }
            public string picString { get; set; }
            public string adString { get; set; }
            public DateTime createTime { get; set; }
            public DateTime startTime { get; set; }
            public DateTime endTime { get; set; }
            public int status { get; set; }
            public int viewNums { get; set; }
            public int operateId { get; set; }
        }

        #endregion

        #region 移动端销售列表相关Model

        /// <summary>
        /// 移动端历史销售记录
        /// </summary>
        public class SalesSummaryForApp
        {
            /// <summary>
            /// 合计总金额
            /// </summary>
            public double sumMoney { get; set; }

            /// <summary>
            /// 未付款总额
            /// </summary>
            public double? sumUnpaid { get; set; }

            /// <summary>
            /// 合计总数量
            /// </summary>
            public double sumNumber { get; set; }

            /// <summary>
            /// 合计项目条数
            /// </summary>
            public int sumListCnt { get; set; }

            /// <summary>
            /// 当前页数
            /// </summary>
            public int nowPage { get; set; }

            /// <summary>
            /// 分页信息
            /// </summary>
            public string pageHtml { get; set; }

            /// <summary>
            /// 本月毛利润
            /// </summary>
            public double MonthProfit { get; set; }

            /// <summary>
            /// 本月月份
            /// </summary>
            public int Month { get; set; }

            /// <summary>
            /// 销售列表
            /// </summary>
            public List<SalesDaily> SalesDailyList { get; set; }
        }

        /// <summary>
        /// 每日销售列表
        /// </summary>
        public class SalesDaily
        {
            /// <summary>
            /// 销售日期
            /// </summary>
            public DateTime Date { get; set; }

            /// <summary>
            /// 销售日期对应的星期
            /// </summary>
            public string Day { get; set; }

            /// <summary>
            /// 日销售额
            /// </summary>
            public double SumDailySale { get; set; }

            /// <summary>
            /// 日毛利润
            /// </summary>
            public double SumDailyProfit { get; set; }

            /// <summary>
            /// 每日销售列表
            /// </summary>
            public List<SalesRecord> DailySalesList { get; set; }
        }

        #endregion

        #region 会员短信Model

        /// <summary>
        /// 会员短信Model
        /// </summary>
        public class SmsSendModel
        {
            /// <summary>
            /// 店铺ID
            /// </summary>
            public int accid { get; set; }

            /// <summary>
            /// 短信内容
            /// </summary>
            public string smsContent { get; set; }

            /// <summary>
            /// 余额不足是否发送
            /// </summary>
            public bool useBalance { get; set; }

            /// <summary>
            /// 定时短信发送
            /// </summary>
            public DateTime regularTime { get; set; }

            /// <summary>
            /// 会员uid列表
            /// </summary>
            public string[] userList { get; set; }

            /// <summary>
            /// 是否全选
            /// </summary>
            public int selectAll { get; set; }

            /// <summary>
            /// 会员等级
            /// </summary>
            public int usrGrade { get; set; }

            /// <summary>
            /// 会员分组
            /// </summary>
            public int usrClass { get; set; }

            /// <summary>
            /// 过滤最近发送过短信会员
            /// </summary>
            public int filterSmsDays { get; set; }

            /// <summary>
            /// 过滤最近销售过的会员
            /// </summary>
            public int filterBuyDays { get; set; }

        }

        #endregion


        #region Barcode 条码查询

        /// <summary>
        /// 条码查询
        /// </summary>
        public class Barcode
        {
            /// <summary>
            /// 请求错误码
            /// </summary>
            public string code { get; set; }

            /// <summary>
            /// 请求错误说明
            /// </summary>
            public string message { get; set; }

            /// <summary>
            /// 条码
            /// </summary>
            public string barcode { get; set; }

            /// <summary>
            /// 商品名称
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 品牌
            /// </summary>
            public string brand { get; set; }

            /// <summary>
            /// 规格
            /// </summary>
            public string spec { get; set; }

            /// <summary>
            /// 厂商
            /// </summary>
            public string company { get; set; }

            /// <summary>
            /// 参考价格
            /// </summary>
            public string price { get; set; }

            /// <summary>
            /// 源图片地址
            /// </summary>
            public string remoteUrl { get; set; }

            /// <summary>
            /// 图片地址
            /// </summary>
            public string imgUrl { get; set; }
        }

        #endregion


        #region 删除店铺信息

        public class DeleteSaleList
        {
            /// <summary>
            /// 销售详情ID
            /// </summary>
            public int saleListId { get; set; }

            /// <summary>
            /// 删除信息说明选择标示
            /// </summary>
            public int itemFlag { get; set; }

            /// <summary>
            /// 删除信息说明
            /// </summary>
            public string itemDesc { get; set; }

            /// <summary>
            /// 备注信息
            /// </summary>
            public string reReason { get; set; }

            /// <summary>
            /// 退货数
            /// </summary>
            public double reNumber { get; set; }

            /// <summary>
            /// 删除类别 <para>1、为退货，2、为删除</para>
            /// </summary>
            public int deleteType { get; set; }

            /// <summary>
            /// 请求 版本
            /// </summary>
            public int Ver { get; set; }

            /// <summary>
            /// 旧版本兼容，已舍弃
            /// </summary>
            public string val { get; set; }

        }

        #endregion

        #region 批量退货

        public class DeleteSaleLists
        {
            /// <summary>
            /// 退换货list
            /// </summary>
            public List<DeleteSaleList> DeleteSales { get; set; }
        }

        #endregion


        #region 数据分析相关

        #region MemberView 销售概要分析

        public class MemberView
        {
            /// <summary>
            /// 总合
            /// </summary>
            public decimal SumMoney { get; set; }

            /// <summary>
            /// 会员销售总
            /// </summary>
            public MemberItem Member { get; set; }

            /// <summary>
            /// 零售销售总
            /// </summary>
            public MemberItem Retail { get; set; }

            /// <summary>
            /// 会员销售详情
            /// </summary>
            public Dictionary<string, decimal> MemberDetail { get; set; }

            /// <summary>
            /// 零售销售详情
            /// </summary>
            public Dictionary<string, decimal> RetailDetail { get; set; }

            /// <summary>
            /// 会员 top 前销售商品
            /// </summary>
            public List<MemberTop> MemberTop { get; set; }

            /// <summary>
            /// 零售 top 前 销售商品
            /// </summary>
            public List<MemberTop> RetailTop { get; set; }
        }

        public class MemberItem
        {
            /// <summary>
            /// 销售总额
            /// </summary>
            public decimal SumMoney { get; set; }

            /// <summary>
            /// 成本总额
            /// </summary>
            public decimal SumCost { get; set; }

            /// <summary>
            /// 销售笔数
            /// </summary>
            public int SalesCount { get; set; }

            /// <summary>
            /// 平均销售
            /// </summary>
            public decimal AvgSales { get; set; }

            /// <summary>
            /// 平均利润
            /// </summary>
            public decimal AvgProfit { get; set; }
        }

        public class MemberTop
        {
            /// <summary>
            /// Index
            /// </summary>
            public int Index { get; set; }

            /// <summary>
            /// 商品名称
            /// </summary>
            public string GoodsName { get; set; }

            /// <summary>
            /// 大分类
            /// </summary>
            public string MaxName { get; set; }

            /// <summary>
            /// 小分类
            /// </summary>
            public string MinName { get; set; }

            /// <summary>
            /// 商品Gid
            /// </summary>
            public int GoodsId { get; set; }

            /// <summary>
            /// 销售数量
            /// </summary>
            public decimal SalesCount { get; set; }

            /// <summary>
            /// 销售金额
            /// </summary>
            public decimal SalesMoney { get; set; }
        }

        #endregion

        #region GoodsView 商品分析

        /// <summary>
        /// 商品分析
        /// </summary>
        public class GoodsView
        {
            /// <summary>
            /// 销售金额
            /// </summary>
            public decimal SalesSum { get; set; }

            /// <summary>
            /// 毛利润
            /// </summary>
            public decimal ProfitSum { get; set; }

            /// <summary>
            /// 销售数量
            /// </summary>
            public decimal QuantitySum { get; set; }

            public List<GoodsTopItem> Data { get; set; }
        }

        public class GoodsTopItem
        {
            /// <summary>
            /// 商品Id
            /// </summary>
            public int GoodsId { get; set; }

            /// <summary>
            /// 大分类
            /// </summary>
            public string MaxName { get; set; }

            /// <summary>
            /// 小分类
            /// </summary>
            public string MinName { get; set; }

            /// <summary>
            /// 商品名称
            /// </summary>
            public string GoodsName { get; set; }

            /// <summary>
            /// 会员
            /// </summary>
            public int MemberCount { get; set; }

            /// <summary>
            /// 散客
            /// </summary>
            public int RetailCount { get; set; }

            /// <summary>
            /// 销售总额
            /// </summary>
            public decimal SalesMoney { get; set; }

            /// <summary>
            /// 销售比率
            /// </summary>
            public decimal SalesRate { get; set; }

            /// <summary>
            /// 毛利润
            /// </summary>
            public decimal ProfitMoney { get; set; }

            /// <summary>
            /// 成本金额
            /// </summary>
            public decimal CostMoney { get; set; }

            /// <summary>
            /// 销售数量
            /// </summary>
            public decimal Quantity { get; set; }

            /// <summary>
            /// 库存
            /// </summary>
            public decimal Inventory { get; set; }

            /// <summary>
            /// 日均利润
            /// </summary>
            public decimal AvgProfit { get; set; }
        }

        #endregion


        #region SalesClass 销售分类

        /// <summary>
        /// 销售分类
        /// </summary>
        public class SalesClass
        {
            /// <summary>
            /// 销售金额
            /// </summary>
            public decimal SalesSum { get; set; }

            /// <summary>
            /// 销售数量
            /// </summary>
            public decimal SalesCount { get; set; }

            /// <summary>
            /// 毛利润
            /// </summary>
            public decimal ProfitSum { get; set; }

            /// <summary>
            /// 大分类Id
            /// </summary>
            public int MaxId { get; set; }

            public List<SalesClassItem> Data { get; set; }
        }

        public class SalesClassItem
        {
            /// <summary>
            /// 大分类
            /// </summary>
            public string MaxName { get; set; }

            /// <summary>
            /// 小分类
            /// </summary>
            public string MinName { get; set; }

            /// <summary>
            /// 大分类Id
            /// </summary>
            public int MaxId { get; set; }

            /// <summary>
            /// 小分类Id
            /// </summary>
            public int MinId { get; set; }

            /// <summary>
            /// 销售数量
            /// </summary>
            public decimal SalesCount { get; set; }

            /// <summary>
            /// 销售金额
            /// </summary>
            public decimal SalesSum { get; set; }

            /// <summary>
            /// 毛利润
            /// </summary>
            public decimal ProfitSum { get; set; }

            /// <summary>
            /// 比例
            /// </summary>
            public decimal Proportion { get; set; }
        }

        #endregion

        #region MemberSex 会员消费分析

        /// <summary>
        /// 会员消费分析
        /// </summary>
        public class MemberSex
        {
            public decimal SumMoney { get; set; }
            public MemberItem Man { get; set; }
            public MemberItem Woman { get; set; }
            public Dictionary<string, decimal> ManDetail { get; set; }
            public Dictionary<string, decimal> WomanDetail { get; set; }
            public List<MemberTop> ManTop { get; set; }
            public List<MemberTop> WomanTop { get; set; }
        }

        #endregion

        #region 分析查询Model

        /// <summary>
        /// 销售分析查询条件
        /// </summary>
        public class AnalyseQueryStr
        {
            /// <summary>
            /// 间隔天数
            /// </summary>
            public string diff { get; set; }

            /// <summary>
            /// 请求类型
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 大分类ID
            /// </summary>
            public int maxid { get; set; }

            /// <summary>
            /// 销售商品前几位
            /// </summary>
            public int top { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime startTime { get; set; }

            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime endTime { get; set; }
        }

        #endregion

        #endregion

        #region 还款日志

        /// <summary>
        /// 还款日志List
        /// </summary>
        public class ReturnLogList
        {
            /// <summary>
            /// 还款记录条数
            /// </summary>
            public int reLogCnt { get; set; }

            /// <summary>
            /// 已还款总额
            /// </summary>
            public decimal reMoney { get; set; }

            /// <summary>
            /// 还款日志Item
            /// </summary>
            public List<ReturnLogItem> reLogList { get; set; }
        }

        /// <summary>
        /// 还款日志Item
        /// </summary>
        public class ReturnLogItem
        {
            /// <summary>
            /// 日志ID
            /// </summary>
            public int saleLogID { get; set; }

            /// <summary>
            /// 销售概要ID
            /// </summary>
            public int saleID { get; set; }

            /// <summary>
            /// 还款金额
            /// </summary>
            public decimal reMoney { get; set; }

            /// <summary>
            /// 还款时间
            /// </summary>
            public DateTime LogTime { get; set; }

            /// <summary>
            /// 还款备注
            /// </summary>
            public string remark { get; set; }
        }

        /// <summary>
        /// 还款操作提交信息
        /// </summary>
        public class ReturnPayModel
        {
            /// <summary>
            /// 销售ID
            /// </summary>
            public int SaleId { get; set; }

            /// <summary>
            /// 还款金额
            /// </summary>
            public double PayMoney { get; set; }

            /// <summary>
            /// 还款备注
            /// </summary>
            public string Remark { get; set; }
        }

        #endregion

        #region 会员消费密码校验

        /// <summary>
        /// 验证会员储值密码
        /// </summary>
        public class UserPwdCheck
        {
            /// <summary>
            /// 会员ID
            /// </summary>
            public int Uid { get; set; }

            /// <summary>
            /// 会员消费密码
            /// </summary>
            public string Pwd { get; set; }

            /// <summary>
            /// 分店标志
            /// </summary>
            public int iBranch { get; set; }
        }

        #endregion

        #region 支出Model

        /// <summary>
        ///支出信息	
        ///</summary>
        [Serializable]
        public partial class t_PayRecord
        {
            private long _id;
            private DateTime _paydate = DateTime.Now;
            private decimal _paysum = 0;
            private string _paymaxtype;
            private string _payname;
            private string _paymintype;
            private string _randomnumber;
            private long _shopperid;
            private int _ifloan;
            private string _maxid;

            /// <summary>
            /// ID
            /// </summary>		
            public long ID
            {
                get { return _id; }
                set { _id = value; }
            }

            /// <summary>
            /// 支出时间
            /// </summary>		
            public DateTime PayDate
            {
                get { return _paydate; }
                set { _paydate = value; }
            }

            /// <summary>
            /// 支出金额
            /// </summary>		
            public decimal PaySum
            {
                get { return _paysum; }
                set { _paysum = value; }
            }

            /// <summary>
            /// 支出大分类
            /// </summary>		
            public string PayMaxType
            {
                get { return _paymaxtype; }
                set { _paymaxtype = value; }
            }

            /// <summary>
            /// 支出说明
            /// </summary>		
            public string PayName
            {
                get { return _payname; }
                set { _payname = value; }
            }

            /// <summary>
            /// 支出小分类
            /// </summary>		
            public string PayMinType
            {
                get { return _paymintype; }
                set { _paymintype = value; }
            }

            /// <summary>
            /// RandomNumber
            /// </summary>		
            public string RandomNumber
            {
                get { return _randomnumber; }
                set { _randomnumber = value; }
            }

            /// <summary>
            /// 店铺ID
            /// </summary>		
            public long ShopperId
            {
                get { return _shopperid; }
                set { _shopperid = value; }
            }

            /// <summary>
            /// IfLoan
            /// </summary>		
            public int IfLoan
            {
                get { return _ifloan; }
                set { _ifloan = value; }
            }

            /// <summary>
            /// 大分类ID
            /// </summary>
            public string MaxId
            {
                get { return _maxid; }
                set { _maxid = value; }
            }

        }

        public class PayItemModel
        {
            public int Id { get; set; }
            public DateTime PayDate { get; set; }
            public decimal PaySum { get; set; }
            public string MaxName { get; set; }
            public string MinName { get; set; }
            public string PayName { get; set; }
        }

        public class UpdateModel
        {
            public int MaxId { get; set; }
            public int MinId { get; set; }
            public string Val { get; set; }
        }

        public class PayClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<PayClassMin> MinData { get; set; }
        }

        public class PayClassMin
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class PayQueryModel
        {
            public int Page { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }
            public int MaxId { get; set; }
            public int MinId { get; set; }
        }

        public class DailyPayModel
        {
            public DailyPayModel()
            {
                DayList = new List<PayItemModel>();
            }

            public string WeekDay { get; set; }
            public DateTime Date { get; set; }
            public decimal Sum { get; set; }
            public List<PayItemModel> DayList { get; set; }
        }

        public class PayListModel
        {
            public PayListModel()
            {
                data = new List<DailyPayModel>();
            }

            public List<DailyPayModel> data { get; set; }
            public int RowCount { get; set; }
            public decimal AllMoney { get; set; }
            public int PageSum { get; set; }
        }

        public class PayReportMax
        {
            public PayReportMax()
            {
                MinList = new List<PayReportMin>();
            }

            public string MaxName { get; set; }
            public decimal MaxAll { get; set; }
            public List<PayReportMin> MinList { get; set; }
        }

        public class PayReportMin
        {
            public string MinName { get; set; }
            public decimal MinVal { get; set; }
            public string Percent { get; set; }
        }

        public class PayReportQuery
        {
            public int Year { get; set; }
            public int Month { get; set; }
        }

        #endregion

        #region 手机商品地图信息

        public class MobileMapModel
        {
            public int id { get; set; }
            public string gName { get; set; }
            public int gid { get; set; }
            public int accid { get; set; }
            public string accountName { get; set; }
            public string maxClass { get; set; }
            public string minClass { get; set; }
            public int goodsNum { get; set; }
            public decimal price { get; set; }
            public int accountid { get; set; }
            public string TrueConty { get; set; }
            public string picUrl { get; set; }
            public string CompanyName { get; set; }
            public string PhoneNumber { get; set; }
        }

        #endregion


        #region 挂单
        /// <summary>
        /// 挂单项目
        /// </summary>
        public class T_Sale_Cart
        {
            /// <summary>
            /// 记录id
            /// </summary>
            public int id { get; set; }

            /// <summary>
            /// 销售Id
            /// </summary>
            public string saleNo { get; set; }

            /// <summary>
            /// 店铺Id
            /// </summary>
            public int accID { get; set; }

            /// <summary>
            /// 零售状态
            /// </summary>
            public int isRetail { get; set; }

            /// <summary>
            /// 会员Id
            /// </summary>
            public int uid { get; set; }

            /// <summary>
            /// 记录时间
            /// </summary>
            public DateTime insertTime { get; set; }

            /// <summary>
            /// 操作人员Id
            /// </summary>
            public int operatorID { get; set; }

            /// <summary>
            /// 操作时间
            /// </summary>
            public DateTime operatorTime { get; set; }

            /// <summary>
            /// 销售详细列表
            /// </summary>
            public Dictionary<string, SalesList> gList { get; set; }

            /// <summary>
            /// reName
            /// </summary>
            public string reName { get; set; }

            /// <summary>
            /// 最新一条挂单（用于二次打印）
            /// </summary>
            public Dictionary<string, SalesList> latestList { get; set; }
            /// <summary>
            /// 是否需要打印
            /// </summary>
            public string checkPrint { get; set; }
        }
        public class SaleCartList
        {
            /// <summary>
            /// 挂单功能状态
            /// </summary>
            public int FuncStatus { get; set; }

            /// <summary>
            /// 挂单项目
            /// </summary>
            public int ItemCnt { get; set; }

            /// <summary>
            /// 商品类别
            /// </summary>
            public int Kinds { get; set; }

            /// <summary>
            /// 商品数量
            /// </summary>
            public decimal GoodsNum { get; set; }

            /// <summary>
            /// 商品金额
            /// </summary>
            public decimal GoodsMoney { get; set; }

            /// <summary>
            /// 销售列表
            /// </summary>
            public List<SaleCartItem> SaleList { get; set; }
        }
        public class SaleCartItem : T_Sale_Cart
        {
            /// <summary>
            /// 销售种类
            /// </summary>
            public int SaleKinds { get; set; }

            /// <summary>
            /// 销售数量
            /// </summary>
            public decimal SaleNumber { get; set; }

            /// <summary>
            /// 销售金额
            /// </summary>
            public decimal SaleMoney { get; set; }
        }
        #endregion

        #region 供应商
        public class SupplierModel
        {
            public int Id { get; set; }
            /// <summary>
            /// 供应商姓名
            /// </summary>
            public string SupplierName { get; set; }
            /// <summary>
            /// 供应商电话
            /// </summary>
            public string SupplierPhone { get; set; }
            /// <summary>
            /// 供应商地址
            /// </summary>
            public string SupplierAddress { get; set; }
            /// <summary>
            /// 供应商QQ
            /// </summary>
            public string SupplierQQ { get; set; }
            /// <summary>
            /// 供应商微信
            /// </summary>
            public string SupplierWeixin { get; set; }
            /// <summary>
            /// 供应商支付宝
            /// </summary>
            public string SupplierAlipay { get; set; }
            /// <summary>
            /// 供应商对应AccId
            /// </summary>
            public int AccId { get; set; }
            /// <summary>
            /// 供应商类型
            /// 1-普通供应商
            /// </summary>
            public int SupplierType { get; set; }
            /// <summary>
            /// 备注信息 
            /// </summary>
            public string Remark { get; set; }
            /// <summary>
            /// 供应商状态
            /// 0-无效
            /// 1-有效
            /// </summary>
            public int SupplierStatus { get; set; }
            /// <summary>
            /// 记录时间
            /// </summary>
            public DateTime InsertTime { get; set; }
            /// <summary>
            /// 供应商头像
            /// </summary>
            public string SupplierAvatar { get; set; }
            /// <summary>
            /// 供应商品种类
            /// </summary>
            public int SupplyGoodsNum { get; set; }
            /// <summary>
            /// 银行卡号
            /// </summary>
            public string SupplierBankId { get; set; }
            /// <summary>
            /// 银行名
            /// </summary>
            public string SupplierBankName { get; set; }
            /// <summary>
            /// 联系人
            /// </summary>
            public string SupplierContextMan { get; set; }
            /// <summary>
            /// 供应商备注
            /// </summary>
            public string SupplierRemark { get; set; }
            /// <summary>
            /// 供应商欠款总金额
            /// </summary>
            public decimal? SupplierTotalArrearMoney { get; set; }
        }

        public class SupplierMobileModelList
        {
            public SupplierMobileModelList()
            {
                SupplierModelList = new List<SupplierModel>();
                DicSum = new Dictionary<string, decimal>();
            }

            public Dictionary<string, decimal> DicSum { get; set; }

            /// <summary>
            /// 记录列表
            /// </summary>
            public List<SupplierModel> SupplierModelList { get; set; }
        }

        public class T_GoodsSupplier_RepaymentModel
        {
            /// <summary>
            /// ID
            /// </summary>		
            public int? ID { get; set; }
            /// <summary>
            /// 关联供应商ID
            /// </summary>		
            public int SupplierId { get; set; }
            /// <summary>
            /// 总应付金额
            /// </summary>		
            public decimal? TotalValue { get; set; }
            /// <summary>
            /// 已付金额
            /// </summary>		
            public decimal? PaidValue { get; set; }
            /// <summary>
            /// 欠款金额
            /// </summary>		
            public decimal? ArrearValue { get; set; }
            /// <summary>
            /// 备注
            /// </summary>		
            public string Remark { get; set; }
            /// <summary>
            /// 插入时间
            /// </summary>		
            public DateTime InsertTime { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>		
            public DateTime? UpdateTime { get; set; }
            /// <summary>
            /// 是否删除，1已删除，0未删除
            /// </summary>		
            public int? IsDelete { get; set; }
            /// <summary>
            /// 批次号
            /// </summary>		
            public string BatchNumber { get; set; }
            /// <summary>
            /// 是否还款状态，0是未还清，1是已全部还款
            /// </summary>		
            public int? Status { get; set; }
            /// <summary>
            /// 店铺ID
            /// </summary>		
            public int AccId { get; set; }
        }

        public class T_GoodsSupplier_RepaymentTotal
        {
            /// <summary>
            /// 总欠款
            /// </summary>
            public decimal? SupplierTotalArrearMoney { get; set; }
            /// <summary>
            /// 总应付金额
            /// </summary>
            public decimal? SupplierTotalMoney { get; set; }
            /// <summary>
            /// 总实付金额
            /// </summary>
            public decimal? SupplierPaidMoney { get; set; }
            /// <summary>
            /// 供应商ID
            /// </summary>
            public int SupplierId { get; set; }
            /// <summary>
            /// 店铺ID
            /// </summary>
            public decimal AccId { get; set; }
        }

        public class T_GoodsSupplier_Query
        {
            public int supplierId { get; set; }
            public int pageIndex { get; set; }
            public int accId { get; set; }
            public string column { get; set; }
        }

        /// <summary>
        /// 手机用获取列表
        /// </summary>
        public class T_GoodsSupplier_RepaymentListModel
        {
            public T_GoodsSupplier_RepaymentListModel()
            {
                Listitem = new List<T_GoodsSupplier_RepaymentModel>();
            }
            /// <summary>
            /// 总页数
            /// </summary>
            public int PageCount { get; set; }
            /// <summary>
            /// 总数量
            /// </summary>
            public int RowCount { get; set; }
            /// <summary>
            /// 当前页数
            /// </summary>
            public int PageIndex { get; set; }
            /// <summary>
            /// 列表
            /// </summary>
            public List<T_GoodsSupplier_RepaymentModel> Listitem { get; set; }
        }

        /// <summary>
        /// 手机用还款记录列表
        /// </summary>
        public class T_GSRepaymentRecordList
        {
            public T_GSRepaymentRecordList()
            {
                Listitem = new List<T_GoodsSupplier_RepaymentRecordModel>();
            }

            public List<T_GoodsSupplier_RepaymentRecordModel> Listitem { get; set; }
        }
        /// <summary>
        /// 还款记录
        /// </summary>
        public class T_GoodsSupplier_RepaymentRecordModel
        {
            /// <summary>
            /// ID
            /// </summary>		
            public int ID { get; set; }
            /// <summary>
            /// 供应商ID
            /// </summary>		
            public int SupplierId { get; set; }
            /// <summary>
            /// 店铺ID
            /// </summary>		
            public int AccId { get; set; }
            /// <summary>
            /// 关联ID
            /// </summary>		
            public int RepaymentId { get; set; }
            /// <summary>
            /// 操作IP
            /// </summary>		
            public string OperatorIP { get; set; }
            /// <summary>
            /// 操作ID
            /// </summary>		
            public int OperatorUserId { get; set; }
            /// <summary>
            /// 当前欠款
            /// </summary>		
            public decimal? CurrentValue { get; set; }
            /// <summary>
            /// 操作金额
            /// </summary>		
            public decimal? EditValue { get; set; }
            /// <summary>
            /// 平衡后金额
            /// </summary>		
            public decimal? BalanceValue { get; set; }
            /// <summary>
            /// 备注
            /// </summary>		
            public string Remark { get; set; }
            /// <summary>
            /// 是否是编辑金额，正常还款为0，修改还款金额是为1
            /// </summary>		
            public int IsModifiedTotal { get; set; }
            /// <summary>
            /// 新增时间
            /// </summary>		
            public DateTime InsertTime { get; set; }
        }

        /// <summary>
        /// 手机用查看还款记录必须参数
        /// </summary>
        public class T_GSRepaymentRecordQuery
        {
            public int RepaymentId { get; set; }
            public int AccId { get; set; }
            public int SupplierId { get; set; }
        }
        #endregion

        #region 商品列表项目信息
        /// <summary>
        /// 商品列表项目信息
        /// </summary>
        public partial class T_GoodsListItem
        {
            /// <summary>
            /// 商品ID
            /// </summary>
            public int gid { get; set; }
            /// <summary>
            /// 大分类
            /// </summary>
            public string gMaxName { get; set; }
            /// <summary>
            /// 大分类ID
            /// </summary>
            public int gMaxID { get; set; }
            /// <summary>
            /// 小分类
            /// </summary>
            public string gMinName { get; set; }
            /// <summary>
            /// 小分类ID
            /// </summary>
            public int gMinID { get; set; }
            /// <summary>
            /// 商品名称
            /// </summary>
            public string gName { get; set; }
            /// <summary>
            /// 规格
            /// </summary>
            public string gSpecification { get; set; }
            /// <summary>
            /// 商品价格
            /// </summary>
            public decimal gPrice { get; set; }
            /// <summary>
            /// 商品数量
            /// </summary>
            public decimal gQuantity { get; set; }
            /// <summary>
            /// 是否服务
            /// </summary>
            public int isService { get; set; }
            /// <summary>
            /// 是否有其他SKU
            /// </summary>
            public int IsExtend { get; set; }
            /// <summary>
            /// 商品图片
            /// </summary>
            public List<T_GoodsPicBasis> gPicS { get; set; }
            /// <summary>
            /// SKU 数量
            /// </summary>
            public int SkuNum { get; set; }
            /// <summary>
            /// SKU 中最小价格
            /// </summary>
            public decimal SkuMinPrice { get; set; }
            /// <summary>
            /// SKU中最大价格
            /// </summary>
            public decimal SkuMaxPrice { get; set; }
            /// <summary>
            /// SKU库存
            /// </summary>
            public decimal SkuQuantity { get; set; }
            /// <summary>
            /// 增加时间
            /// </summary>
            public DateTime gAddTime { get; set; }
            /// <summary>
            /// 供应商Id
            /// </summary>
            public int? SupplierId { get; set; }
            /// <summary>
            /// 商品进价
            /// </summary>
            public decimal gCostPrice { get; set; }
        }
        #endregion

        #region 商品库存概要信息
        /// <summary>
        /// 商品库存概要信息
        /// </summary>
        public class GoodsSummary
        {
            /// <summary>
            /// 商品种类
            /// </summary>
            public int gKinds { get; set; }

            /// <summary>
            /// 商品库存数量
            /// </summary>
            public decimal gNumber { get; set; }

            /// <summary>
            /// 商品总计成本金额
            /// </summary>
            public decimal gSumCost { get; set; }

            /// <summary>
            /// 商品总计销售金额
            /// </summary>
            public decimal gSumPrice { get; set; }
        }
        #endregion

        #region 商品图片基础信息
        /// <summary>
        /// 商品图片基础信息
        /// </summary>
        public partial class T_GoodsPicBasis
        {

            /// <summary>
            /// id
            /// </summary>		
            public int id { get; set; }
            /// <summary>
            /// 产品图片
            /// </summary>		
            public string gPicUrl { get; set; }
            /// <summary>
            /// 图片排序
            /// </summary>		
            public int gPicOrder { get; set; }
        }
        #endregion


        #region 供应商 店铺信息
        public class SupplierGoodsDetail
        {
            public SupplierGoodsDetail()
            {
                summary = new GoodsSummary();
                goodsList = new List<T_GoodsListItem>();
            }
            public GoodsSummary summary { get; set; }
            public List<T_GoodsListItem> goodsList { get; set; }
        }

        #endregion



        #region 打印小票

        /// <summary>
        /// 模板Model
        /// </summary>
        public class PrintTemplate
        {
            /// <summary>
            /// 模板类型
            /// </summary>
            public int ModelType { get; set; }

            /// <summary>
            /// 是否小票模板
            /// </summary>
            public int NoReceipt { get; set; }

            /// <summary>
            /// 模板内容
            /// </summary>
            public string Template { get; set; }

            /// <summary>
            /// 模板备注
            /// </summary>
            public string Remark { get; set; }

            /// <summary>
            /// 临时，是否为新模板
            /// </summary>
            public int NewTemplate { get; set; }
            /// <summary>
            /// 移动端模板
            /// </summary>
            public Dictionary<string, string> MobileTemplate { get; set; }
            /// <summary>
            /// 移动端模板KEY 顺序 只有移动端使用
            /// </summary>
            public string MobileTemplateKey { get; set; }
        }
        #endregion

        #region 出入库
        /// <summary>
        /// 得到日志POST 值
        /// </summary>
        public class GetStockLogPost
        {
            public GetStockLogPost()
            {
                bgDate = Convert.ToDateTime("1900-1-1");
                edDate = Convert.ToDateTime("1900-1-1");
            }

            ///<summary>
            ///店铺ID
            ///</summary>
            public int accID { get; set; }
            ///<summary>
            ///日志类型
            ///</summary>
            public int logType { get; set; }
            ///<summary>
            ///大分类名称
            ///</summary>
            public string maxName { get; set; }
            ///<summary>
            ///小分类名称
            ///</summary>
            public string minName { get; set; }
            ///<summary>
            ///商品名称
            ///</summary>
            public string gName { get; set; }
            ///<summary>
            ///商品ID
            ///</summary>
            public int gid { get; set; }
            ///<summary>
            ///开始日期
            ///</summary>
            public DateTime bgDate { get; set; }
            ///<summary>
            ///结束日期
            ///</summary>
            public DateTime edDate { get; set; }
            ///<summary>
            ///当前页数
            ///</summary>
            public int iPage { get; set; }
        }


        /// <summary>
        /// 出入库记录Model
        /// </summary>
        public class GoodsStockLog
        {
            /// <summary>
            /// 当前页数
            /// </summary>
            public int nowPage { get; set; }

            /// <summary>
            /// 记录总数
            /// </summary>
            public int recCount { get; set; }

            /// <summary>
            /// 变动数量
            /// </summary>
            public decimal sumEdit { get; set; }

            /// <summary>
            /// 变动金额
            /// </summary>
            public decimal sumMoney { get; set; }

            /// <summary>
            /// 分页数据
            /// </summary>
            public string pageHtml { get; set; }

            /// <summary>
            /// 日志列表
            /// </summary>
            public List<T_Goods_StockLog> stockList { get; set; }
        }
        /// <summary>
        ///T_Goods_StockLog	
        ///</summary>
        public partial class T_Goods_StockLog
        {
            private int _id;
            private int _accid;
            private int _logtype;
            private int _gid;
            private string _gmaxname;
            private string _gminname;
            private string _gname;
            private DateTime _addtime;
            private decimal? _originalval;
            private decimal _editval;
            private decimal _finalval;
            private string _remark;
            private string _flag;
            private int _operatorid;
            private DateTime _operatordate;
            private string _operatorip;
            private decimal? _moneycharge;

            /// <summary>
            /// 出入库ID
            /// </summary>		
            public int id
            {
                get { return _id; }
                set { _id = value; }
            }
            /// <summary>
            /// 店铺ID
            /// </summary>		
            public int accID
            {
                get { return _accid; }
                set { _accid = value; }
            }
            /// <summary>
            /// 日志类型 {}
            /// </summary>		
            public int LogType
            {
                get { return _logtype; }
                set { _logtype = value; }
            }
            /// <summary>
            /// 商品ID
            /// </summary>		
            public int gid
            {
                get { return _gid; }
                set { _gid = value; }
            }
            /// <summary>
            /// 大分类名称
            /// </summary>		
            public string gMaxName
            {
                get { return _gmaxname; }
                set { _gmaxname = value; }
            }
            /// <summary>
            /// 小分类名称
            /// </summary>		
            public string gMinName
            {
                get { return _gminname; }
                set { _gminname = value; }
            }
            /// <summary>
            /// 商品名称
            /// </summary>		
            public string gName
            {
                get { return _gname; }
                set { _gname = value; }
            }
            /// <summary>
            /// 添加时间
            /// </summary>		
            public DateTime addTime
            {
                get { return _addtime; }
                set { _addtime = value; }
            }
            /// <summary>
            /// 原始值
            /// </summary>		
            public decimal? OriginalVal
            {
                get { return _originalval; }
                set { _originalval = value; }
            }
            /// <summary>
            /// 变动值
            /// </summary>		
            public decimal EditVal
            {
                get { return _editval; }
                set { _editval = value; }
            }
            /// <summary>
            /// 最终值
            /// </summary>		
            public decimal FinalVal
            {
                get { return _finalval; }
                set { _finalval = value; }
            }
            /// <summary>
            /// 备注信息
            /// </summary>		
            public string Remark
            {
                get { return _remark; }
                set { _remark = value; }
            }
            /// <summary>
            /// 附加信息
            /// </summary>		
            public string Flag
            {
                get { return _flag; }
                set { _flag = value; }
            }
            /// <summary>
            /// 操作人员ID
            /// </summary>		
            public int OperatorID
            {
                get { return _operatorid; }
                set { _operatorid = value; }
            }
            /// <summary>
            /// 操作日期
            /// </summary>		
            public DateTime OperatorDate
            {
                get { return _operatordate; }
                set { _operatordate = value; }
            }
            /// <summary>
            /// 操作人员IP
            /// </summary>		
            public string OperatorIP
            {
                get { return _operatorip; }
                set { _operatorip = value; }
            }
            /// <summary>
            /// 金额变动
            /// </summary>		
            public decimal? MoneyCharge
            {
                get { return _moneycharge; }
                set { _moneycharge = value; }
            }

            /// <summary>
            /// 操作人员姓名
            /// </summary>
            public string OperatorName { get; set; }
        }


        /// <summary>
        /// 库存统计，暂时有移动端使用
        /// </summary>
        public class GoodsStockCount
        {
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime startTime { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime endTime { get; set; }
            /// <summary>
            /// 库存数
            /// </summary>
            public decimal stockCount { get; set; }
            /// <summary>
            /// 正库存
            /// </summary>
            public decimal positiveStock { get; set; }
            /// <summary>
            /// 负库存
            /// </summary>
            public decimal minusStock { get; set; }
            /// <summary>
            /// 出库数
            /// </summary>
            public decimal EXStock { get; set; }
            /// <summary>
            /// 入库数
            /// </summary>
            public decimal PutStock { get; set; }
        }
        #endregion

        #region 商品加料
        /// <summary>
        /// 全部分类包括分类下全部分料
        /// </summary>
        public class T_GoodsAdditionalModelListAll
        {
            public T_GoodsAdditionalModelListAll()
            {
                goodsAdditionalModelList = new List<T_GoodsAdditionalWithItem>();
            }
            /// <summary>
            /// 总行数
            /// </summary>
            public int RowCount { get; set; }

            /// <summary>
            /// 记录列表
            /// </summary>
            public List<T_GoodsAdditionalWithItem> goodsAdditionalModelList { get; set; }
        }

        public class T_GoodsAdditionalWithItem : T_GoodsAdditionalModel
        {
            public T_GoodsAdditionalWithItem()
            {
                goodsAdditionalitemModelList = new List<T_GoodsAdditionalItemModel>();
            }

            public List<T_GoodsAdditionalItemModel> goodsAdditionalitemModelList { get; set; }
        }

        public class T_GoodsAdditionalModel
        {

            /// <summary>
            /// 主键，分类ID
            /// </summary>		
            public int classID { get; set; }
            /// <summary>
            /// 店铺ID
            /// </summary>		
            public int accId { get; set; }
            /// <summary>
            /// 分类名字
            /// </summary>		
            public string className { get; set; }
            /// <summary>
            /// 状态
            /// </summary>		
            public int status { get; set; }
            /// <summary>
            /// 添加时间
            /// </summary>		
            public DateTime insertTime { get; set; }

            /// <summary>
            /// 分类下数量
            /// </summary>
            public int itemCount { get; set; }

        }

        public class T_GoodsAdditionalItemModel
        {

            /// <summary>
            /// 主键
            /// </summary>		
            public int itemId { get; set; }
            /// <summary>
            /// 分类名称
            /// </summary>		
            public int classId { get; set; }
            /// <summary>
            /// 分类名字
            /// </summary>		
            public string className { get; set; }
            /// <summary>
            /// 店铺ID
            /// </summary>		
            public int accId { get; set; }
            /// <summary>
            /// 属性名字
            /// </summary>		
            public string itemName { get; set; }
            /// <summary>
            /// 属性价格
            /// </summary>		
            public decimal itemPrice { get; set; }
            /// <summary>
            /// 属性图片
            /// </summary>		
            public string itemPic { get; set; }
            /// <summary>
            /// 属性进价
            /// </summary>		
            public decimal itemCostPrice { get; set; }
            /// <summary>
            /// 属性规格
            /// </summary>		
            public string itemSpecification { get; set; }
            /// <summary>
            /// 添加时间
            /// </summary>		
            public DateTime insertTime { get; set; }
            /// <summary>
            /// 数量
            /// </summary>		
            public decimal itemQuantity { get; set; }
            /// <summary>
            /// 属性条码
            /// </summary>		
            public string itemBarcode { get; set; }
            /// <summary>
            /// 属性状态
            /// </summary>		
            public int itemStatus { get; set; }
            /// <summary>
            /// 属性拼音首字母
            /// </summary>		
            public string itemPY { get; set; }
            /// <summary>
            /// 属性拼音
            /// </summary>		
            public string itemPinYin { get; set; }
            /// <summary>
            /// 属性联合查询值(拼音,编码)
            /// </summary>		
            public string itemUnionKey { get; set; }

        }

        public class T_GoodsAdditionalModelList
        {
            public T_GoodsAdditionalModelList()
            {
                goodsAdditionalModelList = new List<T_GoodsAdditionalModel>();
            }
            /// <summary>
            /// 总行数
            /// </summary>
            public int RowCount { get; set; }

            /// <summary>
            /// 记录列表
            /// </summary>
            public List<T_GoodsAdditionalModel> goodsAdditionalModelList { get; set; }
        }

        public class T_AdditionalSaleListModel
        {

            /// <summary>
            /// Id
            /// </summary>		
            public int Id { get; set; }
            /// <summary>
            /// 属性销售ID
            /// </summary>		
            public string AdditionalSaleId { get; set; }
            /// <summary>
            /// 店铺ID
            /// </summary>		
            public int accId { get; set; }
            /// <summary>
            /// 属性名字
            /// </summary>		
            public string itemName { get; set; }
            /// <summary>
            /// 属性价格
            /// </summary>		
            public string itemPrice { get; set; }
            /// <summary>
            /// 属性图片地址
            /// </summary>		
            public string itemPic { get; set; }
            /// <summary>
            /// 属性进价
            /// </summary>		
            public string itemCostPrice { get; set; }
            /// <summary>
            /// 属性规格
            /// </summary>		
            public string itemSpecification { get; set; }
            /// <summary>
            /// 添加时间
            /// </summary>		
            public DateTime insertTime { get; set; }
            /// <summary>
            /// 属性数量
            /// </summary>		
            public decimal itemQuantity { get; set; }
            /// <summary>
            /// 属相条码
            /// </summary>		
            public string itemBarcode { get; set; }
            /// <summary>
            /// 属性分类ID
            /// </summary>		
            public int classId { get; set; }
            /// <summary>
            /// 属性分类名
            /// </summary>		
            public string className { get; set; }

        }
        #endregion

        #region iOS缓存相关model

        /// <summary>
        /// 缓存基类
        /// </summary>
        public class CacheBase
        {
            public string Cachenamespace { get; set; }
            public string Key { get; set; }
            public string Hash { get; set; }
        }
        /// <summary>
        /// 缓存数据列表基类
        /// </summary>

        public class CacheListBase : CacheBase
        {
            public bool Large { get; set; }
        }
        /// <summary>
        /// 获取缓存远程数据基类
        /// </summary>
        public class CacheModelBase : CacheBase
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }
        /// <summary>
        /// 缓存数据列表
        /// </summary>

        public class CacheList
        {
            public CacheList()
            {
                cachelist = new List<CacheListBase>();
            }

            public List<CacheListBase> cachelist { get; set; }
        }

        /// <summary>
        /// 获取缓存远程数据列表
        /// </summary>
        public class CacheModelList
        {
            public CacheModelList()
            {
                cachemodellist=new List<CacheModelBase>();
            }

            public List<CacheModelBase> cachemodellist { get; set; }
        }

        public class CacheMenuBase
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Icon { get; set; }
            public bool CanMove { get; set; }
            public bool CanHide { get; set; }
            public bool CanOffLine { get; set; }
            public bool Show { get; set; }
            public string Action { get; set; }
            public int Tip { get; set; }
            public int Start { get; set; }
            public int Expire { get; set; }
            public int Index { get; set; }

        }

        public class CacheHashListModel
        {
            public CacheHashListModel()
            {
                MenubaseList=new List<CacheMenuBase>();
                DeleList=new List<int>();
            }

            public string Hase { get; set; }
            public List<CacheMenuBase> MenubaseList { get; set; }
            public List<int> DeleList { get; set; }
        }

        #endregion
        
        /// <summary>
        /// 时间段
        /// </summary>
        public class DateTimeRange
        {
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime beginDateTime { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime endDateTime { get; set; }
        }

        #region UserInfoForAndroidSale 安卓端销售时加载数据
        public class UserInfoForAndroidSale
        {
            public UserInfoForAndroidSale()
            {
                BuyTimes = 0;
                BuyNum = 0;
                BuyMoney = 0M;
                StoreMoney = 0M;
                UnpaidMoney = 0M;
                Data = new List<StoreCard>();
            }

            //购物次数
            public int BuyTimes { get; set; }
            //累计购物数量
            public int BuyNum { get; set; }
            //累计消费金额
            public decimal BuyMoney { get; set; }
            //储值金额
            public decimal StoreMoney { get; set; }
            //未付款余额
            public decimal UnpaidMoney { get; set; }
            //可用计次卡列表
            public List<StoreCard> Data { get; set; }
        }
        #endregion


        #region 移动端排序model
        public class T_Sale_PayTypeSortModelBase
        {
            /// <summary>
            /// 支付方式，详情见枚举
            /// </summary>		
            public string PayTypeName { get; set; }
            /// <summary>
            /// 支付方式，详情见枚举
            /// </summary>		
            public int PayType { get; set; }
            /// <summary>
            /// 排序顺序
            /// </summary>		
            public int Sort { get; set; }
            /// <summary>
            /// 支付方式图片
            /// </summary>
            public string PayTypePic { get; set; }
            /// <summary>
            /// 是否可以被移除
            /// </summary>        
            public bool IsDelete { get; set; }
            /// <summary>
            /// 是否显示
            /// </summary>
            public bool IsShow { get; set; }
        }
        /// <summary>
        /// 返回model
        /// </summary>
        public class T_Sale_PayTypeSort
        {
            public List<T_Sale_PayTypeSortModelBase> ListModel { get; set; }
        } 
        #endregion
    }
}