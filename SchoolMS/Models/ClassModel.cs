using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Class")]
    public class ClassModel
    {
        [Key]
        public int ClassID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Select Program Title")]
        [Required]
        public int ProgramID { get; set; }
        [Display(Name = "Class Title")]
        [Required(ErrorMessage = "Please Enter Class Title")]
        public string Name { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
        [Display(Name = "Per Year Fee")]
        [Required(ErrorMessage = "Please enter the Year Fee")]
        [DataType(DataType.Currency)]
        public int Amount { get; set; }
        public User Users { get; set; }
        public Program Programs { get; set; }


    }
}