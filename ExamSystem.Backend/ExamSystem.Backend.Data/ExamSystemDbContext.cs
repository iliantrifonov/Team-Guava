namespace ExamSystem.Backend.Data
{
    using System.Data.Entity;
    using ExamSystem.Backend.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ExamSystem.Backend.Data.Migrations;

    public class ExamSystemDbContext : IdentityDbContext<User>
    {
        public ExamSystemDbContext()
            : base("ExamSystemDbCloud2", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ExamSystemDbContext, Configuration>());
        }

        public static ExamSystemDbContext Create()
        {
            return new ExamSystemDbContext();
        }
        
        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<DownloadPath> DownloadPaths { get; set; }

        public virtual IDbSet<Exam> Exams { get; set; }

        public virtual IDbSet<Problem> Problems { get; set; }
    }
}
