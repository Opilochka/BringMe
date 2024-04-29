namespace BringMe.Models
{
    public class ViewsDelivery
    {
        public int PickupPoint {  get; set; }
        public DateTime DataDelivery { get; set; }
        public string TypeTransport { get; set; }
        public string DeliverySelect { get; set; }
        public int PriceRange { get; set; }

        public int create_delivery_button { get; set; }
    }
}
