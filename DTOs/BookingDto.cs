using System.ComponentModel.DataAnnotations;

namespace InlämningSalonn.DTOs
{

    public class ShowAllBookingsDto
    {
        public int BookingId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Status { get; set; }

        public SimpleCustomerDto Customer { get; set; }
        public SimpleStaffDto Staff { get; set; }
        public SimpleServiceDto Service { get; set; }
    }

    public class CreateBookingDto
    {
        public int CustomerId { get; set; }
        public int? StaffId { get; set; } // Behövs inte tilldelas direkt 
        public int ServiceId { get; set; }

        public DateTime DateTime { get; set; }
        [Required]
        [RegularExpression("^(Booked|Canceled)$", ErrorMessage = "Status must be Booked, Canceled")]
        public string Status { get; set; }
    }



    public class BookingDto
    {
        public int BookingId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Status { get; set; }
    }
}
