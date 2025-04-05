using System;
using System.Collections.Generic;

namespace InlämningSalonn.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
