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
using PubNubMessaging.Core;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ExamSystemData();
            context.Comments.Add(new ExamSystem.Backend.Models.Comment());

            //var a = new DropNet.Models.ChunkedUpload();
            //FilesResource.InsertMediaUpload request;
            //string publishKey= "pub-c-c693309f-b4aa-46ad-b8c2-b586283d5ac5";
            //string subscribeKey = "sub-c-71fcc204-3f55-11e4-8637-02ee2ddab7fe";

            //Pubnub pub = new Pubnub(publishKey, subscribeKey);
            //pub.Origin = "pubsub.pubnub.com";
            //pub.Subscribe("exam-system", UserCallback, UserCallback, ErrorCallback);

            //pub.Publish<string>("exam-system", "My favorite message", DisplayReturnMessage, DisplayErrorMessage);
            //Thread.Sleep(3000);
            //Console.WriteLine("Done?");
        }

        private static void DisplayErrorMessage(PubnubClientError error)
        {
            Console.WriteLine(error.Message);
        }

        public static void DisplayReturnMessage(string message)
        {
            Console.WriteLine("Return message: " + message);
        }

        public static void UserCallback(object obj)
        {
            Console.WriteLine("UserCallback called!");
        }

        public static void ConnectCallback(object obj)
        {
            Console.WriteLine("ConnectCallback called!");
        }

        public static void ErrorCallback(PubnubClientError error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
