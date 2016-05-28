using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
  public  class MongoDBAPI
    {

        /// <summary>
        /// 版本
        /// </summary>
        public partial class VersionsLog
        {
            public int _id { get; set; }
            /// <summary>
            /// app 主健
            /// </summary>
            public int appKey { get; set; }
            /// <summary>
            /// 说明
            /// </summary>
            public string appName { get; set; }
            /// <summary>
            /// 版本
            /// </summary>
            public string Versions { get; set; }
            /// <summary>
            /// 时间戳
            /// </summary>
            public string Timestamp { get; set; }

            /// <summary>
            /// 下载路径
            /// </summary>
            public string DownloadUrl { get; set; }
            /// <summary>
            /// 提示信息
            /// </summary>
            public string Message { get; set; }
        }
        /// <summary>
        /// 店铺坐标
        /// </summary>
        public class AccountCoordinate
        {
            /// <summary>
            /// 店铺ID
            /// </summary>
            public int AccId { get; set; }
            /// <summary>
            /// 来源
            /// </summary>
            public string Source { get; set; }
            /// <summary>
            /// 经度
            /// </summary>
            public string Longitude { get; set; }
            /// <summary>
            /// 维度
            /// </summary>
            public string Latitude { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string Remark { get; set; }
        }

    }
}
