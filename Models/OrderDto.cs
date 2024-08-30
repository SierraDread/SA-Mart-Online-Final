namespace Version2SAMart.Models
{
    public class OrderDto
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
