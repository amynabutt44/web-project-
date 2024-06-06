namespace WebApplication2.Models
{
    public class Orderr
    {


        public String Name { get; set; }
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        // Navigation property

    }
}
