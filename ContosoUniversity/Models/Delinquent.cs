using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public enum Violation
    {
        CleanRecord, KilledSomeone, Deathrow
    }
    public class Delinquent
    {
        [Key]
        public int ID { get; set; }
        public string LastName { get; set; }   
        public string FirstMidName { get; set; } 
        public Violation? Violation { get; set; }

    }
}
