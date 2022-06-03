using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Months")]
    public class MonthsModel
    {
        [Key]
        public int MonthID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Month Title")]
        [Required]
        public string Name { get; set; }
        [Display(Name ="Status")]
        public bool IsActive { get; set; }
        public User Users { get; set; }

    }
}