using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Subject")]
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }
        public int UserID { get; set; }
        [Display(Name ="Subject Name")]
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Reg Date")]
        public DateTime RegDate { get; set; }
        [Display(Name ="Total Marks")]
        [Required]
        public int TotalMarks { get; set; }
        public string Description { get; set; }
        [Display(Name ="Status")]
        public bool IsActive { get; set; }

        public User Users { get; set; }
    }
}