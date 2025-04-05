namespace InlämningSalonn.DTOs
{
    public class SimpleCustomerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class GetAllCustomersDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

}
