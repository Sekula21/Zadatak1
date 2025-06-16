using System.ComponentModel.DataAnnotations;

namespace Zadatak1.ViewModels
{
    public class RegisterViewModel
    {
        [MinLength(3)]
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The must be minimum 8 characters long!")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Enter your name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter your last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Select your gender.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Gmail is required!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your birth date")]
        public DateTime BirthDate { get; set; }
    }
}
