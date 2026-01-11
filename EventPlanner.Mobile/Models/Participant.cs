using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Mobile.Models;

public class Participant
{
    public int ID { get; set; }

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";

}

