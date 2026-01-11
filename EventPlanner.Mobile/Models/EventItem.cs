using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Mobile.Models;

public class EventItem
{
    public int ID { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; } = "";
    public int MaxParticipants { get; set; }

    public int OrganizerID { get; set; }
}

