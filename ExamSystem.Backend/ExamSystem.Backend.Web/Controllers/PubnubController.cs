using ExamSystem.Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PubNubMessaging.Core;

namespace ExamSystem.Backend.Web.Controllers
{
    public class PubnubController : BaseApiController
    {
        private Pubnub pub;
        public PubnubController(IExamSystemData data)
            : base(data)
        {
            string publishKey= "pub-c-c693309f-b4aa-46ad-b8c2-b586283d5ac5";
            string subscribeKey = "sub-c-71fcc204-3f55-11e4-8637-02ee2ddab7fe";
            this.pub = new Pubnub(publishKey, subscribeKey);
            this.pub.Origin = "pubsub.pubnub.com";
            this.pub.Subscribe("exam-system", (obj) => {}, (obj) => {}, (obj) => {});
        }

        [HttpPost]
        public IHttpActionResult Send(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest();
            }

            try
            {
                this.pub.Publish<string>("exam-system", message, (obj) => { }, (obj) => { });
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
