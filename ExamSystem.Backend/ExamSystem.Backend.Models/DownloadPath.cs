namespace ExamSystem.Backend.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class DownloadPath
    {
        public int Id { get; set; }

        public string Message { get; set; }

        [Required]
        public DateTime? AddDate { get; set; }

        [Required]
        public string Link { get; set; }
    }
}
