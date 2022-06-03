using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Staff")]
    public class Staff
    {
        [Key]
        public int StaffID { get; set; }
        [Display(Name ="Designation Title")]
        public int DesignationID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Select Gender")]
        public int GenderID { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Contact No")]
        [Required]
        public string ContactNo { get; set; }
        [Display(Name = "Basic Salary")]
        [Required]
        [DataType(DataType.Currency)]
        public int BasicSalary { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Display(Name = "Picture ")]
        public string Photo { get; set; }
        public string Description { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
   
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }
        [Display(Name = "Do you have any disability?")]
        public bool Doyouhaveanydisability { get; set; }
        [Display(Name = "If yes then give us details")]
        public string Ifdisabilityyesthengiveusdetails { get; set; }
        [Display(Name = "Are you taking any medication?")]
        public bool Areyoutakinganymedication { get; set; }
        [Display(Name ="If yes then give us details")]
        public string Ifmedicationyesthengiveusdetail { get; set; }
        [Display(Name = "Any Police record you have?")]
        public bool AnyCriminaloffenceagain { get; set; }
        [Display(Name = "If Yes then give us details")]
        public string Ifcriminaloffenceyesgiveusdetails { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime RegistrationDate { get; set; }
        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }
        public User Users { get; set; }
        public Designation Designations { get; set; }
        public Gendertbl Gendertbls { get; set; }

    }
}
