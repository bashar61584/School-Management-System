using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Designation")]
    public class Designation
    {
        [Key]
        public int DesignationID { get; set; }
        public int UserID { get; set; }
        [Required]
        [Display(Name ="Title")]
        public string Name { get; set; }
        [Display(Name ="Status")]
        public bool IsActive { get; set; }
        public User Users { get; set; }

    }
}