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
    using System.Web;

    public class DownloadPathController : BaseApiController
    {
        private static string StoragePath = HttpRuntime.AppDomainAppPath + "/../";
        DropBoxCloudConnector dropbox;

        public DownloadPathController(IExamSystemData data)
            : base(data)
        {
            this.dropbox = new DropBoxCloudConnector();
        }

        //[Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IHttpActionResult> Add(string message, int problemId)
        {
            var problem = this.data.Problems.Find(problemId);

            if (problem == null)
            {
                return NotFound();
            }

            if (Request.Content.IsMimeMultipartContent())
            {
                var path = Path.GetTempPath();
                var streamProvider = new MultipartFormDataStreamProvider(path);
                await Request.Content.ReadAsMultipartAsync(streamProvider);
                foreach (MultipartFileData fileData in streamProvider.FileData)
                {
                    if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                    {
                        return BadRequest();
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

                    problem.DownloadPaths.Add(downloadPath);

                    try
                    {
                        this.data.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }

                    return Ok(downloadPath.Link);
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
