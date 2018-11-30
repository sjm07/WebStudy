using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OAuthWebApi.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("test")]
        public JObject test()
        {
            JObject resultObj = new JObject();
            resultObj["msg"] = "sjm";
            resultObj["flag"] = true;
            return resultObj;
        }
    }
}
