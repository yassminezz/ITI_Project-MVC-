using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItiProject_ms1.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(40)]
        [MinLength(2)]
        public string Name { get; set; }
        [Range(8000,30000)]
        public double Salary { get; set; }
        [MaxLength(60)]
        [MinLength(10)]
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public int DeptId { get; set; }
        [ForeignKey("DeptId")]
        Department department { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public virtual Microsoft.AspNetCore.Identity.IdentityUser User { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
