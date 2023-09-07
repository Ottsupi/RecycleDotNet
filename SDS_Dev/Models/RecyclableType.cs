using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDS_Dev.Models
{
    public class RecyclableType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public decimal MinKg { get; set; }

        [Required]
        public decimal MaxKg { get; set; }
    }
}