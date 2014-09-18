using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	public class DriveExample
	{
		public static void Example()
		{
			UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
				new ClientSecrets
				{
					ClientId = "252332885106-hj9up6iivaem6mmmhnpnlae13v17p31l.apps.googleusercontent.com",
					ClientSecret = "pARg_uFjDeBryKQkTZVr0jk1",
				},
				new[] { DriveService.Scope.Drive },
				"user",
				CancellationToken.None).Result;

			// Create the service.
			var service = new DriveService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = "ExamSystem",
			});

			File body = new File();
			body.Title = "My document";
			body.Description = "A test document";
			body.MimeType = "text/plain";

			byte[] byteArray = System.IO.File.ReadAllBytes("document.txt");
			System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

			FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, "text/plain");
			request.Upload();

			File file = request.ResponseBody;
			Console.WriteLine("File id: " + file.Id);
			Console.WriteLine("Press Enter to end this process.");
			Console.ReadLine();
		}
	}
}
