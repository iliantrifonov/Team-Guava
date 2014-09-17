using ExamSystem.Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google;
using Google.Apis.Services;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Drive.v2;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var context = new ExamSystemData();
            //context.Comments.Add(new ExamSystem.Backend.Models.Comment());

            var a = new DropNet.Models.ChunkedUpload();
            FilesResource.InsertMediaUpload request;
        }
    }
}
