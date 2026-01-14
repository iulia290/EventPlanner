using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required, StringLength(60)]
        public string Name { get; set; } = string.Empty;

        public ICollection<EventItem>? EventItems { get; set; }
    }
}

