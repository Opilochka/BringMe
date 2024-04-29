using System.ComponentModel.DataAnnotations;

namespace BringMe.Models
{
    public class Registration
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
        public string Role { get; set; }
    }
}
