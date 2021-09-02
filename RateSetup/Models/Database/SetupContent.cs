using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateSetup.Models.Database
{
    public class SetupContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public ContentType Type { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string ThumbnailUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}
