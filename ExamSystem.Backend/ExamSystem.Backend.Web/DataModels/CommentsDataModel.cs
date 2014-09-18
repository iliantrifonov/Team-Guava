namespace ExamSystem.Backend.Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using ExamSystem.Backend.Models;

    public class CommentsDataModel
    {
        public string Text { get; set; }

        public string ExamId { get; set; }

        public DateTime? Date { get; set; }

        public static Func<Comment, CommentsDataModel> GetModel()
        {
            return c => new CommentsDataModel()
            {
                Date = c.Date,
                Text = c.Text,
                ExamId = c.ExamId == null ? null : c.ExamId.ToString()
            };
        }
    }
}