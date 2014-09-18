namespace ExamSystem.Backend.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public virtual Problem Problem { get; set; }

        public virtual Exam Exam { get; set; }

        public Guid ExamId { get; set; }
    }
}
