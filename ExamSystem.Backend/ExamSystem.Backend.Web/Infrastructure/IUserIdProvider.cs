namespace ExamSystem.Backend.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public interface IUserIdProvider
    {
        string GetUserId();
    }
}
