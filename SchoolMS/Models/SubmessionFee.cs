using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolMS.Models
{
    [Table("SubmessionFee")]
    public class SubmessionFee
    {
        [Key]
        public int SubmissionFeeID { get; set; }
        [Display(Name ="Class Title")]
        [Required]
        public int StudentPromoteID { get; set; }
        public int UserID { get; set; }
        [Display(Name="Amount / Fee's")]
        [Required]
        public int Amount { get; set; }
        [Required]
        [Display(Name ="Date")]
        [DataType(DataType.Date)]
        public DateTime SubmissionDate { get; set; }
        [Required]
        [Display(Name = "Select Fee Month")]
        public int MonthID { get; set; }
        public string Description { get; set; }
        [Display(Name = "Status")]


        public User Users { get; set; }
        public StudentPromote StudentPromotes { get; set; }
        public MonthsModel MonthsModels { get; set; }
    }
}
