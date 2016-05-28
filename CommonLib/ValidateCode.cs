using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CommonLib
{
    public static class ValidateCode
    {
        #region CreateValidateGraphic 生成验证码
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="codeVal"></param>
        /// <returns></returns>
        public static byte[] CreateValidateGraphic(string codeVal)
        {
            string chkCode = string.Empty;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };

            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };

            //验证码的字符集，去掉了一些容易混淆的字符 
            //char[] character = { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            //Random rnd = new Random();
            //生成验证码字符串 
            //rnd.Next();
            //for (int i = 0; i < 4; i++)
            //{
            //    chkCode += character[rnd.Next(character.Length)];
            //}
            chkCode = codeVal;

            Random rnd = new Random();
            Bitmap bmp = new Bitmap(100, 40);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < 2; i++)
            {
                int x1 = rnd.Next(100);
                int y1 = rnd.Next(40);
                int x2 = rnd.Next(100);
                int y2 = rnd.Next(40);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, 18);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 + 8, (float)8);
            }
            //画噪点 
            for (int i = 0; i < 50; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }

            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream stream = new MemoryStream();
            try
            {
                bmp.Save(stream, ImageFormat.Png);
                bmp.Dispose();
                return stream.ToArray();
            }
            finally
            {
                //显式释放资源 
                bmp.Dispose();
                g.Dispose();

            }
        } 
        #endregion

        #region CreateValidateCode 生成验证码字符
        /// <summary>
        /// 生成验证码字符
        /// </summary>
        /// <returns></returns>
        public static string CreateValidateCode()
        {
            string chkCode = string.Empty;
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random rnd = new Random();
            //生成验证码字符串 
            rnd.Next();
            for (int i = 0; i < 4; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            return chkCode;
        } 
        #endregion

        #region CreateValidateUrl 生成验证码Url链接
        /// <summary>
        /// 生成验证码Url链接
        /// </summary>
        /// <param name="strCode">加密验证码字符串</param>
        /// <returns></returns>
        public static string CreateValidateUrl(string strCode)
        {
            return string.Format("http://api.i200.cn/Verifycode?code={0}", strCode);
        } 
        #endregion

        #region EnCodeVal 加密字符串
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public static string EnCodeVal(string strCode)
        {
            byte[] desKey = { 11, 22, 33, 44, 55, 66, 77, 88 };
            string strPwd = Helper.EncryptDES(strCode, desKey);
            return strPwd;
        } 
        #endregion

        #region DeCodeVal 解密字符串
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public static string DeCodeVal(string strCode)
        {
            byte[] desKey = { 11, 22, 33, 44, 55, 66, 77, 88 };
            string strPwd = Helper.DecryptDES(strCode, desKey);
            return strPwd;
        } 
        #endregion
    }
}
