using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage ="Please Select User Type")]
        [Display(Name ="User Type")]
        public int UserTypeID { get; set; }
        [Required(ErrorMessage = "Please enter the Full Name")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = ("Please enter the User Name"))]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter the Password")]
        public string Password { get; set; }
        [Display(Name = "Contact No")]

        public string ContactNo { get; set; }
        [Display(Name = "Email Address")]

        public string EmailAddress { get; set; }
        public string Address { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        public UserType Usertypes { get; set; }

    }
}