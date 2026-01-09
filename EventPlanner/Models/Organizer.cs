using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class Organizer
    {
        public int ID { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public ICollection<EventItem>? Events { get; set; }
    }
}
