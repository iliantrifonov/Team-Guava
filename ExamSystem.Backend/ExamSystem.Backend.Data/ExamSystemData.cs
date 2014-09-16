namespace ExamSystem.Backend.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using ExamSystem.Backend.Data.Repositories;
    using ExamSystem.Backend.Models;

    public class ExamSystemData : IExamSystemData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public ExamSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public ExamSystemData() : this(new ExamSystemDbContext())
        {
            
        }

        public IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
                
            }
        }

        public IRepository<DownloadPath> DownloadPaths
        {
            get
            {
                return this.GetRepository<DownloadPath>();
                
            }
        }

        public IRepository<Exam> Exams
        {
            get
            {
                return this.GetRepository<Exam>();
                
            }
        }

        public IRepository<Problem> Problems
        {
            get
            {
                return this.GetRepository<Problem>();
                
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T); 

            if (!this.repositories.ContainsKey(typeOfRepository)) 
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
                
            } 
            
            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
