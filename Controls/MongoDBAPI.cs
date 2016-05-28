using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
namespace Controls
{
   public class MongoDBAPI
    {

       string MongoDBAPIHost = "http://" + System.Configuration.ConfigurationManager.AppSettings["MongoDBAPI"] + "/api/";


       public Models.MongoDBAPI.VersionsLog GetVersion(int key)
       {
           string url = MongoDBAPIHost + "VersionsLog/" + key.ToString();
           string downJson = CommonLib.Helper.RestGet(url,"", null, GetSignature());

           Models.MongoDBAPI.VersionsLog log = new Models.MongoDBAPI.VersionsLog();
           try
           {
               log = CommonLib.Helper.JsonDeserializeObject<Models.MongoDBAPI.VersionsLog>(downJson);
           }
           catch (Exception)
           {

           }
           return log;
       }
       /// <summary>
       /// 记录店铺的坐标信息
       /// </summary>
       /// <param name="acModel"></param>
       /// <returns></returns>
       public long SetAccountCoordinate(Models.MongoDBAPI.AccountCoordinate acModel)
       {
           string url = MongoDBAPIHost + "AccountCoordinate";

           Dictionary<string, string> postJson = new Dictionary<string, string>();
           postJson["AccId"] = acModel.AccId.ToString();
           postJson["Source"] = acModel.Source;
           postJson["Longitude"] = acModel.Longitude;
           postJson["Latitude"] = acModel.Latitude;
           postJson["Remark"] = acModel.Remark;

           try
           {
               string downJson = CommonLib.Helper.RestPost(url,"", postJson, GetSignature());
               return Convert.ToInt32(downJson);
           }
           catch (Exception)
           {
              // CommonLib.Logger.Error("录入店铺坐标信息", ex);
               return 0;
           }
       }


       public Dictionary<string, string> GetSignature()
       {
           Random ro = new Random();


           string strTimestamp = CommonLib.Helper.GetTimeStamp();
           string strNonce = ro.Next(4000, 9000).ToString();
           string strAppKey = "XKCE9P34TsqemfITS0W18RX6ewsxPK07MALZJ7Y";

           StringBuilder strSign = new StringBuilder();
           strSign.Append(strAppKey);
           strSign.Append(strTimestamp);
           strSign.Append(strNonce);



           string strAuthCode =CommonLib.Helper.Md5Hash(strSign.ToString());

           Dictionary<string, string> value = new Dictionary<string, string>();
           value["Signature"] = strAuthCode;
           value["Timestamp"] = strTimestamp;
           value["Nonce"] = strNonce;
           return value;
       }
    }
}
