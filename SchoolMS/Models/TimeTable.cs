using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("TimeTable")]
    public class TimeTable
    {
        [Key]
        public int TimeTableID { get; set; }
        [Display(Name ="Class Subject Title")]
        [Required]
        public int ClassSubjectID { get; set; }
        [Required]
        [Display(Name = "Teacher")]
        public int StaffID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Start Time")]
        [Required]
        [DataType(DataType.Time)]
    
        public DateTime StartTime { get; set; }
        [Display(Name = "End Time")]
        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTitme { get; set; }
        public string Day { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        public ClassSubject ClassSubjects { get; set; }
        public Staff Staffs { get; set; }
        public User Users { get; set; }
        
    }
}