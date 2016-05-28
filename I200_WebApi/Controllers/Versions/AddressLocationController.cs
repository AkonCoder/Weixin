using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace I200_WebApi.Controllers.Versions
{
    public class AddressLocationController : ApiController
    { 
        // POST api/addresslocation
        public long Post([FromBody]Models.MongoDBAPI.AccountCoordinate model)
        {
            MongoDBAPI dbapi = new MongoDBAPI();
            if (model.Source == null || model.Source.ToString() == "")
            {
                model.Source = Request.Headers.GetValues("AppKey").First();
            }
           return dbapi.SetAccountCoordinate(model);
        }
         
    }
}
