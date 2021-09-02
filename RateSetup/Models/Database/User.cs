using RateSetup.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateSetup.Models.Database
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int Activated { get; set; }

        public DateTime DateRegistered { get; set; }

        public string ProfileImage { get; set; }

        public UserType UserType { get; set; }

        public string ActivateKey { get; set; }
    }
}
