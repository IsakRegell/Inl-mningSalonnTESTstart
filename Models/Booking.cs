using System;
using System.Collections.Generic;

namespace InlämningSalonn.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? CustomerId { get; set; }

    public int? StaffId { get; set; }

    public int? ServiceId { get; set; }

    public DateTime? DateTime { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Service? Service { get; set; }

    public virtual Staff? Staff { get; set; }
}
