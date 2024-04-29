namespace BringMe.Models
{
    public class Locality
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public bool Warehouse { get; set; }
        public string IdUser { get; set; } = "";
    }
}
