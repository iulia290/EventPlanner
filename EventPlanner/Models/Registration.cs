using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class Registration
    {
        public int ID { get; set; }

        public int EventItemID { get; set; }
        public EventItem? EventItem { get; set; }

        public int ParticipantID { get; set; }
        public Participant? Participant { get; set; }
    }
}
