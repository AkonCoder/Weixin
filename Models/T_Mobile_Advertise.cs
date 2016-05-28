using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{

    /// <summary>
    ///移动端广告	
    ///</summary>
    public partial class T_Mobile_Advertise
    { 
        private int _logtype; 
        private string _machinecode;
        private string _logip; 
        private string _logmark;
        private string _remark;
         
        /// <summary>
        /// 日志类别
        /// <para>｛有钱通知：1，客户端上报：2｝</para>
        /// </summary>		
        public int logType
        {
            get { return _logtype; }
            set { _logtype = value; }
        } 
        /// <summary>
        /// 机器码
        /// </summary>		
        public string machineCode
        {
            get { return _machinecode; }
            set { _machinecode = value; }
        }
        /// <summary>
        /// IP
        /// </summary>		
        public string logIp
        {
            get { return _logip; }
            set { _logip = value; }
        }  
        /// <summary>
        /// 标示
        /// </summary>		
        public string logmark
        {
            get { return _logmark; }
            set { _logmark = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>		
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

    }
}
