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
    using ExamSystem.Backend.Models;

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

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var problem = this.data.Problems
                .All()
                .Where(p => p.Id == id)
                .Select(ProblemDataModel.GetModel())
                .FirstOrDefault();

            if (problem == null)
            {
                return NotFound();
            }

            return Ok(problem);
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody]ProblemDataModel problem, string examId)
        {
            if (problem == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var idAsGuid = new Guid(examId);

            var exam = this.data.Exams
                .All()
                .Where(e => e.Id == idAsGuid)
                .FirstOrDefault();

            if (exam == null)
            {
                return NotFound();
            }
            var problemToAdd = new Problem()
            {
                Name = problem.Name,
                DownloadPaths = problem.DownloadPaths
                        .Select(DownloadPathDataModel
                        .GetOriginal())
                        .ToList(),
            };

            exam.Problems.Add(problemToAdd);

            this.data.SaveChanges();

            return Ok(problemToAdd.Id);
        }
    }
}
