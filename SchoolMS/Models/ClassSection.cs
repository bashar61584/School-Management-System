using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("ClassSection")]
    public class ClassSection
    {
        [Key]
        
        public int ClassSectionID { get; set; }
        [Display(Name ="Select Section")]
        [Required(ErrorMessage ="Please select the Section")]
        public int SectionID { get; set; }
        [Display(Name ="Select Class")]
        [Required(ErrorMessage ="Please select the class")]
        public int ClassID { get; set; }
        public int UserID { get; set; }
        [Display(Name ="Title")]
        public string Name { get; set; }
        [Display(Name ="Status")]
        public bool IsActive { get; set; }
        public User Users { get; set; }
        public Section Sections  { get; set; }
        public ClassModel ClassModels { get; set; }

    }
}