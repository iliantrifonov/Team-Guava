using ExamSystem.Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExamSystem.Backend.Web.Controllers
{
    //[Authorize]
    public abstract class BaseApiController : ApiController
    {
        protected IExamSystemData data;

        protected BaseApiController(IExamSystemData data)
        {
            this.data = data;
        }
    }
}
