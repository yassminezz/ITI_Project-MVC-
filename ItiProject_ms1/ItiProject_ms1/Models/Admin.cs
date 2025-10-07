using System.ComponentModel.DataAnnotations;

namespace ItiProject_ms1.Models
{

    public class Admin
    {
        [Key] 
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
       
    }
}
