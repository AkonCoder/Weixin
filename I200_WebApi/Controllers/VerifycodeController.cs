using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace I200_WebApi.Controllers
{
    public class VerifycodeController : Controller
    {
        //
        // GET: /Verifycode/

        public ActionResult Index(string code)
        {
            string strCodeVal = "";
            if (!string.IsNullOrEmpty(code))
            {
                strCodeVal = CommonLib.ValidateCode.DeCodeVal(code);
            }
            else
            {
                strCodeVal = "";
            }

            byte[] codeByte = CommonLib.ValidateCode.CreateValidateGraphic(strCodeVal);
            return File(codeByte, @"image/png");
        }

    }
}
