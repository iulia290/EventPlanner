using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class EventItem
    {
        public int ID { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        [Range(1, 5000)]
        [Display(Name = "Maximum Participants")]
        public int MaxParticipants { get; set; }

        public int OrganizerID { get; set; }
        public Organizer? Organizer { get; set; }

        public ICollection<Registration>? Registrations { get; set; }

        public int? CategoryID { get; set; }
        public Category? Category { get; set; }

    }
}
