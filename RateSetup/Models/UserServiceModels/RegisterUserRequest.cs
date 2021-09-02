﻿using System.ComponentModel.DataAnnotations;

namespace RateSetup.Models.UserServiceModels
{
    public class RegisterUserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ProfileImage { get; set; }
    }
}
