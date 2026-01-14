using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventPlanner.Models
{
    public class Registration
    {
        public int ID { get; set; }

        public int ParticipantId { get; set; }
        public Participant Participant { get; set; }

        public int EventItemId { get; set; }
        public EventItem EventItem { get; set; }
    }
}
