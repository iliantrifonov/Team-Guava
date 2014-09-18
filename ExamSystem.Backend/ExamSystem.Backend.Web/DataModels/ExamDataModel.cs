namespace ExamSystem.Backend.Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using ExamSystem.Backend.Models;

    public class ExamDataModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public static Func<Exam, ExamDataModel> GetModel()
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