using System.ComponentModel.DataAnnotations.Schema;

namespace BringMe.Models
{
    public class FormProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public IFormFile Picture { get; set; }
        public string ImageUrl { get; set; } = "";
        public int Price { get; set; }
        public string Size { get; set; }
        public string Mass { get; set; }
        public string Description { get; set; }
    }
}
