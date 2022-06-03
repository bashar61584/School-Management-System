using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Session")]
    public class Session
    {
        [Key]
        public int SessionID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        [Display(Name="Session Title")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select Start Date")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Please select End Date")]
        public DateTime Endate { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }
        public User Users { get; set; }
    }
}





    