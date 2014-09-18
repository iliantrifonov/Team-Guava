namespace ExamSystem.Backend.Web.DataModels
{
    using ExamSystem.Backend.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class DownloadPathDataModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime? AddDate { get; set; }

        public string Link { get; set; }


        public static Expression<Func<DownloadPath, DownloadPathDataModel>> GetModel
        {
            get
            {
                return e => new DownloadPathDataModel()
                {
                    AddDate = e.AddDate,
                    Link = e.Link,
                    Message = e.Message,
                    Id = e.Id
                };
            }
        }

        public static Expression<Func<DownloadPathDataModel, DownloadPath>> GetOriginal
        {
            get
            {
                return e => new DownloadPath()
                {
                    AddDate = e.AddDate,
                    Link = e.Link,
                    Message = e.Message,
                };
            }
        }
    }
}