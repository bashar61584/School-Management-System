using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("ExamType")]
    public class ExamType
    {
        [Key]
        public int ExamTypeID { get; set; }
        [Required]
        [Display(Name="Title")]
        public string Name { get; set; }
        public int UserID { get; set; }
        [Display(Name ="Status")]
        public bool IsActive{ get; set; }
        public User Users { get; set; }

    }
}