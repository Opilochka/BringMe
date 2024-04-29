namespace BringMe.Models
{
    public class Way
    {
        public int Id { get; set; }
        public int IdLocalityA { get; set; }
        public int IdLocalityB { get; set; }
        public int Price { get; set; }
        public int Distance { get; set; }
        public string Transport { get; set; }
        public int Time { get; set; }
        public string IdUser { get; set; } = "";
    }
}
