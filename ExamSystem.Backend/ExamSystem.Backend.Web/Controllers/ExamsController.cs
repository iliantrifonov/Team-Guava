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

    public class ExamsController : BaseApiController
    {
        public ExamsController(IExamSystemData data)
            : base(data)
        {

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
                .Select(ExamDataModel.GetModel);

            return Ok(exams);
        }
    }
}
