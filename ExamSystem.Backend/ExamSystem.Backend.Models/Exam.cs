namespace ExamSystem.Backend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Exam
    {
        public Exam()
        {
            // TODO: Fix to fields
            this.Id = Guid.NewGuid();
            this.Users = new HashSet<User>();
            this.Problems = new HashSet<Problem>();
            this.Comment = new HashSet<Comment>();
        }

        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime? StartTime { get; set; }

        [Required]
        public DateTime? EndTime { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Problem> Problems { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
    }
}
