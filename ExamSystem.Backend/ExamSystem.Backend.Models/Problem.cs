﻿namespace ExamSystem.Backend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Problem
    {
        public Problem()
        {
            this.DownloadPaths = new HashSet<DownloadPath>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<DownloadPath> DownloadPaths { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
