using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controls
{
    public static class ReturnModel
    {
        #region TokenFail 登录状态失效
        /// <summary>
        /// 登录状态失效
        /// </summary>
        /// <returns></returns>
        public static ResponseModel TokenFail()
        {
            var model = new ResponseModel
            {
                Status = -1,
                Ver = "1.0",
                ErrCode = 0,
                ErrMsg = "状态过期，请重新登录"
            };
            return model;
        } 
        #endregion

        #region Success 请求成功
        /// <summary>
        /// 请求成功
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>
        public static ResponseModel Success(object objModel)
        {
            var model = new ResponseModel
            {
                Status = 0,
                Ver = "1.0",
                ErrCode = 0,
                Data = objModel
            };
            return model;
        }
        #endregion

        #region SuccessNone 请求成功(无数据)
        /// <summary>
        /// 请求成功(无数据)
        /// </summary>
        /// <returns></returns>
        public static ResponseModel SuccessNone()
        {
            var model = new ResponseModel
            {
                Status = 0,
                Ver = "1.0",
                ErrCode = 1,
                ErrMsg = "没有数据",
                Data = null
            };
            return model;
        }
        #endregion

        #region Error 请求失败
        /// <summary>
        /// 请求失败
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>
        public static ResponseModel Error(int errCode,string errMsg)
        {
            var model = new ResponseModel
            {
                Status = 0,
                Ver = "1.0",
                ErrCode = errCode,
                ErrMsg = errMsg
            };
            return model;
        }
        #endregion
    }
}
