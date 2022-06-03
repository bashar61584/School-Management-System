using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Program")]
    public class Program
    {
        [Key]
        public int ProgramID { get; set; }
        [Required]
        public int UserID   { get; set; }
        [Required]
        [Display(Name="Program Title")]
        public string Name{ get; set; }
        [Display(Name = "Start Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate{ get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
        public User Users { get; set; }

    }
}