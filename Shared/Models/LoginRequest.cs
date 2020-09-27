﻿using System.ComponentModel.DataAnnotations;

namespace MyChat.Shared.Models {
    public class LoginRequest {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
