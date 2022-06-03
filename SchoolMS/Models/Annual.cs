using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Annual")]
    public class Annual
    {
        [Key]
        public int AnnualID { get; set; }
        [Required]
        [Display(Name ="Class Title")]
        public int ClassSectionID { get; set; }
        [Required]
        [Display(Name = "Session Title")]
        public int SessionID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Annual/Year Fee")]
        [Required]
        public int Fee { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
        public ClassSection ClassSections { get; set; }
        public Session Sessions { get; set; }
        public User Users { get; set; }

    }
}
