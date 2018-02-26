using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoApplication.Controllers
{
    [RoutePrefix("api")]
    public class GiftAidController : ApiController
    {
        [HttpGet]
        [Route("GiftAid/v1/get/{referenceId}")]
        public string GetValue(Guid referenceId)
        {
            return "value";
        }

        [HttpPost]
        [Route("GiftAid/v1/Post")]
        public void Post([FromBody]GiftAid giftAid)
        {

        }
    }

    public class GiftAid
    {
        public Decimal GiftAmount;
    }
}
