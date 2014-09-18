namespace ExamSystem.Backend.Web.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    using ExamSystem.Backend.Models;

    public class ProblemDataModel
    {
        public ProblemDataModel()
        {
            this.DownloadPaths = new HashSet<DownloadPathDataModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<DownloadPathDataModel> DownloadPaths { get; set; }

        public static Func<Problem, ProblemDataModel> GetModel()
        {
            return e => new ProblemDataModel()
            {
                Id = e.Id,
                Name = e.Name,
                DownloadPaths = e.DownloadPaths
                    .Select(DownloadPathDataModel.GetModel()),
            };
        }
    }
}