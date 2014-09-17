namespace ExamSystem.Backend.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using ExamSystem.Backend.Data;
    using ExamSystem.Backend.Web.DataModels;

    public class ProblemsController : BaseApiController
    {
        public ProblemsController(IExamSystemData data)
            : base(data)
        {

        }

        [HttpGet]
        public IHttpActionResult All(string examId)
        {
            var idAsGuid = new Guid(examId);

            var exam = this.data.Exams.All()
                .Where(e => e.Id == idAsGuid).FirstOrDefault();

            if (exam == null)
            {
                return NotFound();
            }

            var problems = exam.Problems.Select(ProblemDataModel.GetModel());

            return Ok(problems);
        }
    }
}
