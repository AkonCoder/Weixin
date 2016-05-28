using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class MessageModel
    {
        /// <summary>
        /// Auth Model
        /// </summary>
        public class Auth
        {
            public string appkey { get; set; }
            public string timestamp { get; set; }
            public string nonce { get; set; }
            public string signature { get; set; }
        }

        public class Message
        {
            public string _id { get; set; }
            public int msgType { get; set; }
            public int msgClass { get; set; }
            public string msgTitle { get; set; }
            public string msgContent { get; set; }
            public string msgUrl { get; set; }
            public DateTime createTime { get; set; }
            public DateTime readTime { get; set; }
            public int isRead { get; set; }
            public string boardCastId { get; set; }
            public int global { get; set; }
            public string contentType { get; set; }
            public string displayTime { get; set; }
            public string contentUrl { get; set; }
        }
    }
}
