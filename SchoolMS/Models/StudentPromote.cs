using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("StudentPromote")]
    public class StudentPromote
    {
        public int StudentPromoteID { get; set; }
        [Required]
        [Display(Name ="Select Student")]
        public int StudentID { get; set; }
        [Display(Name = "Select Annual/Promote/Class Fee")]
        [Required]
        public int AnnualID { get; set; }
        [Display(Name ="Promote Date")]
        [DataType(DataType.Date)]
        public DateTime ProDate { get; set; }
        [Display(Name ="Is All Fee Cleared ?")]
        public bool IsSubmit { get; set; }
        [Display(Name ="Status")]
        public bool IsActive { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }

        public Student Students { get; set; }
        public Annual Annuals { get; set; }
        public User Users { get; set; }
    }
}