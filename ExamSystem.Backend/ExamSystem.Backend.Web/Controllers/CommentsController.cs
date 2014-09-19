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
    using ExamSystem.Backend.Web.PubnubCore;

    public class CommentsController : BaseApiController
    {
        INotificationService notification;

        public CommentsController(IExamSystemData data, 
        INotificationService notification)
            : base(data)
        {
            this.notification = notification;
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var comment = this.data.Comments.All()
                .Where(c => c.Id == id)
                .Select(CommentsDataModel.GetModel)
                .FirstOrDefault();

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
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

            var comments = exam.Comments.AsQueryable()
                .Select(CommentsDataModel.GetModel);

            return Ok(comments);
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody]CommentsDataModel comment)
        {
            if (comment == null && !ModelState.IsValid)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(comment.ExamId) || comment.ExamId.Length < 36)
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

            try
            {
                this.data.SaveChanges();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            try
            {
                this.notification.Send(exam.Id.ToString());
            }
            catch (Exception)
            {
                
            }

            return Ok(commentForDb.Id);
        }
    }
}
