namespace ExamSystem.Backend.Web.DataModels
{
    using ExamSystem.Backend.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class DownloadPathDataModel
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime? AddDate { get; set; }

        public string Link { get; set; }


        public static Func<DownloadPath, DownloadPathDataModel> GetModel()
        {
            return e => new DownloadPathDataModel()
            {
                AddDate = e.AddDate,
                Link = e.Link,
                Message = e.Message,
                Id = e.Id
            };
        }

        public static Func<DownloadPathDataModel, DownloadPath> GetOriginal()
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