using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateSetup.Models.Database
{
    public class Setup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string DesktopSetup { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public List<SetupContent> Content { get; set; }
    }
}
