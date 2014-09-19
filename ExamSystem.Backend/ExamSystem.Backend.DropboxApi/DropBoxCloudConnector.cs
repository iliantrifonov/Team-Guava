namespace ExamSystem.Backend.DropboxApi
{
    using System;
    using System.Linq;
    using Spring.IO;
    using Spring.Social.Dropbox.Api;

    public class DropBoxCloudConnector
    {
        private const string CollectionName = "exam-service";

        private readonly DropBoxCloud dropBoxCloud;

        public DropBoxCloudConnector()
            : this(new DropBoxCloud())
        {
        }

        public DropBoxCloudConnector(DropBoxCloud dropBoxCloud)
        {
            this.dropBoxCloud = dropBoxCloud;
        }

        public Entry UploadFileToCloud(FileResource resource)
        {
            string collection = "/" + CollectionName + "/" + resource.File.Name;
            var entry = this.dropBoxCloud.UploadToCloud(resource, collection);
            return entry;
        }

        public Entry GetAllFiles()
        {
            var pictures = this.dropBoxCloud.GetAllMediaFiles(CollectionName);

            return pictures;
        }

        public DropboxLink GetFileLink(string path)
        {
            var pictureLink = this.dropBoxCloud.GetMediaLink(path);

            return pictureLink;
        }
    }
}