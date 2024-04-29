namespace BringMe.Models
{
    public class DeliveryWay
    {
        public int Id { get; set; }
        public int IdDelivery {  get; set; }
        public int IdWay { get; set; }
        public bool Status { get; set; } = false;
    }
}
