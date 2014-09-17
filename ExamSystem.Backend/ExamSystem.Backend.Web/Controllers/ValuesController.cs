namespace ExamSystem.Backend.Web.Controllers
{
    using ExamSystem.Backend.Data;
    using ExamSystem.Backend.Web.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Cors;

    [EnableCors("*", "*", "*")]
    public class ValuesController : BaseApiController
    {
        private IUserIdProvider userIdProvider;

        public ValuesController(IExamSystemData data, IUserIdProvider userIdProvider)
            : base(data)
        {
            this.userIdProvider = userIdProvider;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
