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
            // TODO: Check if the student is registered for the exam
            var idAsGuid = new Guid(examId);

            var exam = this.data.Exams.All()
                .Where(e => e.Id == idAsGuid).FirstOrDefault();

            if (exam == null)
            {
                return NotFound();
            }

            var problems = exam.Problems.AsQueryable().Select(ProblemDataModel.GetModel);

            return Ok(problems);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            // TODO: Check if the student is registered for the exam
            var problem = this.data.Problems
                .All()
                .Where(p => p.Id == id)
                .Select(ProblemDataModel.GetModel)
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
            // TODO: This is not how this is supposed to work, fix it to work properly, and with files.
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
                DownloadPaths = problem.DownloadPaths.AsQueryable()
                        .Select(DownloadPathDataModel
                        .GetOriginal)
                        .ToList(),
            };

            exam.Problems.Add(problemToAdd);

            this.data.SaveChanges();

            return Ok(problemToAdd.Id);
        }
    }
}
