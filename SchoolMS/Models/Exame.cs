using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Exame")]
    public class Exame
    {
        [Key]
        public int ExamID { get; set; }
        [Required]
        public int ExamTypeID { get; set; }
        [Required]
        public int UserID { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name="Start Date")]
        //[DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)]
        public DateTime StartDate { get; set; }
        [Display(Name="End Date")]
        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        [Display(Name="Status")]
        public bool IsActive { get; set; }
        public User Users { get; set; }
        public ExamType ExamTypes { get; set; }
    }
}