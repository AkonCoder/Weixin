using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{ 
    #region LoginModel 登录相关Model
    /// <summary>
    /// 登录相关Model
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 登录验证码
        /// </summary>
        public string VerifyCode { get; set; }
        /// <summary>
        /// 登录访问Ip
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// App标识
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 设备ID号
        /// </summary>

        public string deviceId { get; set; }
    }
	/// <summary>
	/// 在线时长Model
	/// </summary>
	public class OnlineDuration
	{
		/// <summary>
		/// 登录时间
		/// </summary>
		public DateTime startTime { get; set; }
		/// <summary>
		/// 登出时间
		/// </summary>
		public DateTime endTime { get; set; }
		/// <summary>
		/// 设备信息
		/// </summary>
		public string deviceInfo { get; set; }
	}

	public class testModel
	{
		public string durationList { get; set; }
	}

	#endregion

    #region ErrUserModel 错误登陆信息
    /// <summary>
    /// 错误登陆信息Mode
    /// </summary>
    public class ErrUserModel
    {
        /// <summary>
        /// 错误次数
        /// </summary>
        public int ErrTimes { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }
    } 
    #endregion
}
