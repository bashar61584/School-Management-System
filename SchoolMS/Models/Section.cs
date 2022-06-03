
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolMS.Models
{
    [Table("Section")]
    public class Section
    {
        [Key]
        public int SectionID { get; set; }
        public int UserID{ get; set; }
        [Display(Name ="Section Title")]
        public string SectionName{ get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
        public User Users { get; set; }
    }
}