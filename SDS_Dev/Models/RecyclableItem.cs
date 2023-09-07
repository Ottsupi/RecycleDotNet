using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDS_Dev.Models
{
    public class RecyclableItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RecyclableTypeId { get; set; }

        [Required]
        public decimal Weight { get; set; }

        public decimal? ComputedRate { get; set; }

        public string ItemDescription { get; set; }
    }
}