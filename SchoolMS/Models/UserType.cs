using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("UserType")]
    public class UserType
    {
        [Key]
        [Required]
        public int UserTypeID { get; set; }
        [Display(Name ="User Type")]
        [Required(ErrorMessage ="Please enter the user type")]
        public string TypeName { get; set; }
        public string Description { get; set; }
    }
}