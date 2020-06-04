using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElizabethJames_CodeExample.Models
{
    public class ComputerGame
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Html)]
        public string Description { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Release Date must be of the format: dd-MM-yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        [Range(1, 10)]
        public int Rating { get; set; }
    }
}