
namespace ExamSystem.Backend.Data
{
    using System;
    using System.Linq;

    using ExamSystem.Backend.Data.Repositories;
    using ExamSystem.Backend.Models;

    public interface IExamSystemData
    {
        IRepository<User> Users { get; }

        IRepository<Comment> Comments { get; }

        IRepository<DownloadPath> DownloadPaths { get; }

        IRepository<Exam> Exams { get; }

        IRepository<Problem> Problems { get; }

        int SaveChanges();
    }
}
