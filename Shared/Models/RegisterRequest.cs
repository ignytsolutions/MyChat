using System.ComponentModel.DataAnnotations;

namespace MyChat.Shared.Models {
    public class RegisterRequest {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Surname { get; set; }

        [Required(ErrorMessage = "user Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string PasswordConfirm { get; set; }
    }
}
