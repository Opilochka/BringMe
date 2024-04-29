using System.ComponentModel.DataAnnotations.Schema;

namespace BringMe.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public string Mass { get; set; }
        public string Description { get; set; }
        public string IdUser { get; set;}
    }
}
