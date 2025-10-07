using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItiProject_ms1.Models
{

    public class Student
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        public string? ImagePath { get; set; }
        [MaxLength(60)]
        [MinLength(20)]
        public string Address { get; set; }
        [Range(0,100)]
        public double Grade { get; set; }
        public int DeptId { get; set; }
        [ForeignKey("DeptId")]
        [ValidateNever]
       public Department Department { get; set; }
        [ValidateNever]

        public List<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();
        //[ForeignKey("User")]
        //public int? UserId { get; set; }          
        //[ValidateNever]
        //public User? User { get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public virtual Microsoft.AspNetCore.Identity.IdentityUser User { get; set; }

    }
}
