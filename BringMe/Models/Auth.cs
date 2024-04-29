namespace BringMe.Models
{
    public class Auth
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
        
    }
}
