using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class Participant
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Full Name")]
        public string FullName => $"{LastName} {FirstName}";

        public ICollection<Registration>? Registrations { get; set; }
    }
}
