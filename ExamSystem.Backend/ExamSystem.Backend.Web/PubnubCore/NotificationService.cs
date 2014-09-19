namespace ExamSystem.Backend.Web.PubnubCore
{
    using System;
    using System.Linq;

    using PubNubMessaging.Core;

    public class NotificationService : INotificationService
    {
        private Pubnub pub;

        public NotificationService()
        {
            string publishKey = "pub-c-c693309f-b4aa-46ad-b8c2-b586283d5ac5";
            string subscribeKey = "sub-c-71fcc204-3f55-11e4-8637-02ee2ddab7fe";
            this.pub = new Pubnub(publishKey, subscribeKey);
            this.pub.Origin = "pubsub.pubnub.com";
            this.pub.Subscribe("exam-system", (obj) => { }, (obj) => { }, (obj) => { });
        }

        public bool Send(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return false;
            }

            try
            {
                this.pub.Publish<string>("exam-system", message, (obj) => { }, (obj) => { });
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}