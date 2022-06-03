using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("ProgramSession")]
    public class ProgramSession
    {
        [Key]
        public int ProgramSessionID { get; set; }
        [Required(ErrorMessage ="Please Select the session")]
        [Display(Name ="Select Session")]
        public int SessionID { get; set; }
        [Required(ErrorMessage ="Please select the program")]
        [Display(Name ="Select Session")]
        public int ProgramID { get; set; }
        public int UserID { get; set; }
        [Display(Name ="Title")]
        public string Details { get; set; }
        [Required(ErrorMessage ="Please enter registration Date")]
        [Display(Name ="Reg Date")]
        [DataType(DataType.Date)]
        public DateTime RegDate { get; set; }
        public string Description { get; set; }
        [Display(Name ="Status")]
        public bool IsActive { get; set; }

        public Program Programs { get; set; }
        public Session Sessions { get; set; }
        public User Users { get; set; }

    }
    
    
    
    
    
    
    
}