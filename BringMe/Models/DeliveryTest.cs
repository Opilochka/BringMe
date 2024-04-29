namespace BringMe.Models
{
    public class DeliveryTest
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public string IdUser { get; set; }
        public DateTime DateTime { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; } = false;
    }
}
