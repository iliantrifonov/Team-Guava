namespace ExamSystem.Backend.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    // TODO: Add constraints
    public class User : IdentityUser
    {
        public User()
        {
            // TODO: Make field
            this.Exams = new HashSet<Exam>();
        }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string StudentNumber { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }

        /// <summary>
        /// Does stuff
        /// </summary>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
