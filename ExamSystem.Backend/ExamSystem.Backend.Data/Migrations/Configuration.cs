namespace ExamSystem.Backend.Data.Migrations
{
    using ExamSystem.Backend.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ExamSystem.Backend.Data.ExamSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ExamSystemDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //\

            if (context.Exams.Any(a => true))
            {
                return;
            }

            var exam = new Exam()
            {
                Name = "Test name",
                EndTime = DateTime.Now,
                Comments = new[] 
                { 
                    new Comment()
                    {
                        Date = DateTime.Now,
                        Text = "Some text",
                    }
                },
                Problems = new[] 
                {
                    new Problem()
                    {
                        DownloadPaths = new [] 
                        { 
                            new DownloadPath() { AddDate = DateTime.Now, Link= "some link.com", Message="some message for the download path", },
                            new DownloadPath() { AddDate = DateTime.Now, Link= "some other link.com", Message="2some other message for the download path", }
                        },
                        Name = "Task 1",
                    },
                    new Problem()
                    {
                        DownloadPaths = new [] 
                        { 
                            new DownloadPath() { AddDate = DateTime.Now, Link= "SASSlink.com", Message="ASsASA message for the download path", },
                            new DownloadPath() { AddDate = DateTime.Now, Link= "ASSA other link.com", Message="ASSASAS other message for the download path", }
                        },
                        Name = "Task 2",
                    }
                },
                StartTime = DateTime.Now,
            };

            context.Exams.Add(exam);
            context.SaveChanges();
        }
    }
}
