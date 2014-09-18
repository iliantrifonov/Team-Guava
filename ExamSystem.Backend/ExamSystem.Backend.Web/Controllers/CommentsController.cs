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

    public class CommentsController : BaseApiController
    {
        public CommentsController(IExamSystemData data)
            : base(data)
        {

        }

        [HttpGet]
        public IHttpActionResult All(string examId)
        {
            if (string.IsNullOrWhiteSpace(examId) || examId.Length < 36)
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

            var comments = exam.Comments
                .Select(CommentsDataModel.GetModel());

            return Ok(comments);
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody]CommentsDataModel comment)
        {
            if (comment == null && !ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var idAsGuid = new Guid(comment.ExamId);
            var exam = this.data.Exams
                .All()
                .Where(e => e.Id == idAsGuid)
                .FirstOrDefault();

            if (exam == null)
            {
                return NotFound();
            }

            var commentForDb = new Comment()
            {
                Date = DateTime.Now,
                Text = comment.Text
            };

            exam.Comments.Add(commentForDb);

            this.data.SaveChanges();

            return Ok(commentForDb.Id);
        }
    }
}
