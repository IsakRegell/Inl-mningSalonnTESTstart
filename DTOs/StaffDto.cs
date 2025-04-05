namespace InlämningSalonn.DTOs
{
    public class SimpleStaffDto
    {
        public string Name { get; set; }
    }

    public class UpdateStaffDto
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class CreateStaffDto
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class FindStaffById
    {
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<BookingDto> Bookings { get; set; }
    }
}

