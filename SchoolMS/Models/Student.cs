using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public int GenderID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date OF Birth")]
        public DateTime DateOfBirth { get; set; }
        public string CNIC { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Religion { get; set; }
        [Display(Name = "Tribe Or Cast")]
        public string TribeOrCast { get; set; }
        [Display(Name = "Contact No")]
        [Required]
        public string ContactNo { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name ="Select Picture")]
        public string Photo { get; set; }
        [Display(Name = "Previous School")]
        public string PreviousSchool { get; set; }
        [Display(Name = "Previous Percentage")]
        public int PrivousPercentage { get; set; }
        [Required]
        [Display(Name = "Temperary Address")]
        public string TempAddress { get; set; }
        [Required]
        [Display(Name = "Permanent Address")]
        public string PermAddress { get; set; }
        [Display(Name = "Addmission Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime AddmissionDate { get; set; }
        [Display(Name = "Father Name")]
        [Required]
        public string FatherName { get; set; }
        [Required]
        [Display(Name = "Father NIC")]
        public string FCNIC { get; set; }
        [Display(Name = "Father Occupation")]
        public string FatherGaurdianOccupation { get; set; }
        [Display(Name = "Father Postal Address")]
        public string FatherGaurdianPostalAddress { get; set; }
        [Display(Name = "Office Phone")]
        public string OfficePhone { get; set; }
        [Required]
        [Display(Name = "Resident Phone")]
        public string ResidentPhone { get; set; }
        public string  Description { get; set; }
        [Display(Name ="Status")]
        public bool IsActive { get; set; }
        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }
        public Gendertbl Gendertbls { get; set; }
        public User Users { get; set; }
    }
}

