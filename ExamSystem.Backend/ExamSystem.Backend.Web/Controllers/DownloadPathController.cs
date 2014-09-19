namespace ExamSystem.Backend.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using ExamSystem.Backend.Data;

    public class DownloadPathController : BaseApiController
    {
        public DownloadPathController(IExamSystemData data)
            : base(data)
        {

        }

        [HttpPost]
        public IHttpActionResult Add()
        {
            return Ok();
        }
    }
}
