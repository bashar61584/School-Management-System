using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Gendertb")]
    public class Gendertbl
    {
        [Key]
        public int GenderID { get; set; }
        public int UserID { get; set; }
        [Display(Name="Gender Title")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        public User Users { get; set; }
    }
}