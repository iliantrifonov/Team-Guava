namespace ExamSystem.Backend.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.IO;
    using System.Threading.Tasks;

    using Spring.IO;

    using ExamSystem.Backend.Data;
    using ExamSystem.Backend.DropboxApi;
    using ExamSystem.Backend.Models;

    public class DownloadPathController : BaseApiController
    {
        private const string StoragePath = "/../../Temp/";
        DropBoxCloudConnector dropbox;

        public DownloadPathController(IExamSystemData data)
            : base(data)
        {
            this.dropbox = new DropBoxCloudConnector();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Add(string message, int problemId)
        {
            var problem = this.data.Problems.Find(problemId);

            if (problem == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new MultipartFormDataStreamProvider(Path.Combine(StoragePath, "Upload"));
                await Request.Content.ReadAsMultipartAsync(streamProvider);
                foreach (MultipartFileData fileData in streamProvider.FileData)
                {
                    if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                    }

                    string fileName = fileData.Headers.ContentDisposition.FileName;

                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if 
                        (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    var finalPath = Path.Combine(StoragePath, fileName);
                    File.Move(fileData.LocalFileName, finalPath);

                    var file = new FileResource(finalPath);
                    var uploadedfFile = this.dropbox.UploadFileToCloud(file);

                    var fileUrl = dropbox.GetFileLink(uploadedfFile.Path).Url;

                    var downloadPath = new DownloadPath()
                    {
                        AddDate = DateTime.Now,
                        Link = fileUrl,
                        Message = message,
                    };
                    try
                    {
                        File.Delete(finalPath);
                    }
                    catch (Exception)
                    {
                        
                    }

                    problem.DownloadPaths.Add(downloadPath);

                    try
                    {
                        this.data.SaveChanges();
                    }
                    catch (Exception)
                    {
                        Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, downloadPath);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
            }
        }
    }
}
