using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public enum JobName
    {

    }
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "FirstMidName")]
        public string FirstMidName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        public JobName? JobName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Employlement Start:")]
        public DateTime EmploymentStart { get; set; }
    }
}
