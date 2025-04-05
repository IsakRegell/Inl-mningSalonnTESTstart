using System;
using System.Collections.Generic;

namespace InlämningSalonn.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public int? Duration { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
