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
        INotificationService notification;

        public ProblemsController(IExamSystemData data, INotificationService notification)
            : base(data)
        {
            this.notification = notification;
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

        // Currently not accessible without removing the method parameters.
        // The problem is that we send the file in form-data format, while the controller is expecting x-www-form-urlencoded
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

            //// Check if the request contains multipart/form-data.
            //if (!Request.Content.IsMimeMultipartContent())
            //{
            //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //}

            //string root = HttpContext.Current.Server.MapPath("~/App_Data");
            //var provider = new MultipartFormDataStreamProvider(root);

            //try
            //{
            //    // Read the form data.
            //    Request.Content.ReadAsMultipartAsync(provider);

            //    var fileNames = new List<string>();

            //    // This illustrates how to get the file names.
            //    foreach (MultipartFileData file in provider.FileData)
            //    {
            //        fileNames.Add(file.Headers.ContentDisposition.FileName + @"\" + file.LocalFileName);
            //    }

            //    return Ok(fileNames);
            //}
            //catch (Exception)
            //{
            //    return InternalServerError();
            //}

            //// TODO: This is not how this is supposed to work, fix it to work properly, and with files.
            //if (problem == null || !ModelState.IsValid)
            //{
            //	return BadRequest();
            //}

            //var idAsGuid = new Guid(examId);

            //var exam = this.data.Exams
            //	.All()
            //	.Where(e => e.Id == idAsGuid)
            //	.FirstOrDefault();

            //if (exam == null)
            //{
            //	return NotFound();
            //}
            //var problemToAdd = new Problem()
            //{
            //	Name = problem.Name,
            //	DownloadPaths = problem.DownloadPaths.AsQueryable()
            //			.Select(DownloadPathDataModel
            //			.GetOriginal)
            //			.ToList(),
            //};

            //exam.Problems.Add(problemToAdd);

            //this.data.SaveChanges();

            //return Ok(problemToAdd.Id);
        }
    }
}
