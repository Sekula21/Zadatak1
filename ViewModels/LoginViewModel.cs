using System.ComponentModel.DataAnnotations;


namespace Zadatak1.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public  string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool Purpose { get; set; }
        public bool Flag { get; set; }
    }
}
