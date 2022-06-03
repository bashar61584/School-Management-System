using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("ClassSubject")]
    public class ClassSubject
    {
        [Key]
        public int ClassSubjectID { get; set; }
        [Required]
        [Display(Name="Select Subject")]
        public int SubjectID { get; set; }
        [Required]
        [Display(Name = "Select ClaSS")]
        public int ClassSectionID { get; set; }
        public int UserID { get; set; }
        [Display(Name="Class Subject Title")]
        public string Name { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
        public Subject Subjects { get; set; }
        public ClassSection ClassSections{ get; set; }
        public User Users { get; set; }
    }
}





        