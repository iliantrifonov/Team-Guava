namespace ExamSystem.Backend.Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using ExamSystem.Backend.Models;
    using System.Linq.Expressions;

    public class ExamDataModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public static Expression<Func<Exam, ExamDataModel>> GetModel
        {
            get
            {
                return e => new ExamDataModel()
                {
                    Id = e.Id.ToString(),
                    Name = e.Name,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime
                };
            }
        }
    }
}