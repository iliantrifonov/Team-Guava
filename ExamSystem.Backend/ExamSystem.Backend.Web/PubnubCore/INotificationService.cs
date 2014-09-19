namespace ExamSystem.Backend.Web.PubnubCore
{
    using System;

    public interface INotificationService
    {
        bool Send(string message);
    }
}
