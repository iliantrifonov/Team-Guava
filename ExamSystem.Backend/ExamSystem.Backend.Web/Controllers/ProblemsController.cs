namespace ExamSystem.Backend.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web;
    using System.Threading.Tasks;

    using ExamSystem.Backend.Data;
    using ExamSystem.Backend.Web.DataModels;
    using ExamSystem.Backend.Models;
    using ExamSystem.Backend.Web.PubnubCore;

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
                .Where(e => e.Id == idAsGuid)
                .FirstOrDefault();

            if (exam == null)
            {
                return NotFound();
            }

            var problems = exam.Problems
                .AsQueryable()
                .Select(ProblemDataModel.GetModel);

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

        //[Authorize(Roles="Admin")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]ProblemDataModelForAdding model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Exam exam;
            try
            {
                exam = this.data.Exams.Find(new Guid(model.ExamId));
            }
            catch (Exception)
            {
                return BadRequest();
            }

            if (exam == null)
            {
                return NotFound();
            }

            var problem = new Problem()
            {
                Name = model.Name,
            };

            exam.Problems.Add(problem);

            try
            {
                this.data.SaveChanges();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok(problem.Id);
        }
    }
}
