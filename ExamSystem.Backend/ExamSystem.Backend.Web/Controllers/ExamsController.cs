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

    public class ExamsController : BaseApiController
    {
        public ExamsController(IExamSystemData data)
            : base(data)
        {

        }

        [HttpGet]
        public IHttpActionResult AllProblemIds(string examId)
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

            var problemIds = exam.Problems.AsQueryable()
                .Select(p => p.Id);

            return Ok(problemIds);
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var examsToReturn = this.data.Exams
                .All()
                .OrderBy(e => e.StartTime)
                .Select(ExamDataModel.GetModel);

            return Ok(examsToReturn);
        }

        [HttpGet]
        public IHttpActionResult All(int countPerPage, int page)
        {
            var examsToReturn = this.data.Exams
                .All()
                .OrderBy(e => e.StartTime)
                .Skip(countPerPage * (page - 1))
                .Take(countPerPage)
                .Select(ExamDataModel.GetModel);

            return Ok(examsToReturn);
        }

        [HttpGet]
        public IHttpActionResult ExamId(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length < 36)
            {
                return BadRequest();
            }
            var idAsGuid = new Guid(id);

            var exam = this.data.Exams
                .All()
                .Where(e => e.Id == idAsGuid)
                .Select(ExamDataModel.GetModel)
                .FirstOrDefault();

            if (exam == null)
            {
                return NotFound();
            }

            return Ok(exam);
        }

        [HttpGet]
        public IHttpActionResult StudentId(string id)
        {
            var user = this.data.Users
                .All()
                .Where(u => u.Id == id)
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            var exams = user.Exams.AsQueryable()
                .OrderBy(e => e.StartTime)
                .Select(ExamDataModel.GetModel);

            return Ok(exams);
        }

        //[Authorize(Roles="Admin")]
        [HttpPost]
        public IHttpActionResult Add(ExamDataModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var exam = new Exam()
            {
                Name = model.Name,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            };

            this.data.Exams.Add(exam);

            try
            {
                this.data.SaveChanges();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            // TODO: Add location
            return Ok(exam.Id);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult AddUser(ExamDataModelForAdding model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Guid examIdAsGuid;
            Guid userIdAsGuid;
            try 
	        {	  
                examIdAsGuid = new Guid(model.ExamId);
                userIdAsGuid = new Guid(model.UserId);
	        }
	        catch (Exception)
	        {
		        return BadRequest();
	        }

            var exam = this.data.Exams.Find(examIdAsGuid);
            var user = this.data.Users.Find(userIdAsGuid);

            if (exam == null || user == null)
            {
                return NotFound();
            }

            exam.Users.Add(user);

            try
            {
                this.data.SaveChanges();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
