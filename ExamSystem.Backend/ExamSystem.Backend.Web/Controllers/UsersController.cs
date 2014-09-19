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

    public class UsersController : BaseApiController
    {
        public UsersController(IExamSystemData data)
            : base(data)
        {

        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult All()
        {
            var users = this.data.Users.All()
                .OrderBy(u => u.UserName)
                .Select(u => new UserDataModelForSending()
            {
                Email = u.Email,
                Id = u.Id
            });

            return Ok(users);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult ByExamId(string id)
        {
            Guid idAsGuid;
            try
            {
                idAsGuid = new Guid(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var exam = this.data.Exams.Find(idAsGuid);

            if (exam == null)
            {
                return NotFound();
            }

            var users = exam.Users.AsQueryable()
                .OrderBy(u => u.UserName)
                .Select(u => new UserDataModelForSending()
                {
                    Email = u.Email,
                    Id = u.Id
                });

            return Ok(users);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult ByUserId(string id)
        {
            Guid idAsGuid;
            try
            {
                idAsGuid = new Guid(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var user = this.data.Users.Find(idAsGuid);

            if (user == null)
            {
                return NotFound();
            }

            var userForSending = new UserDataModelForSending()
            {
                Email = user.Email,
                Id = user.Id
            };

            return Ok(userForSending);
        }
    }
}
