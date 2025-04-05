using System;
using System.Collections.Generic;

namespace InlämningSalonn.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? BookingId { get; set; }

    public string? Type { get; set; }

    public DateTime? SentTime { get; set; }

    public virtual Booking? Booking { get; set; }
}
