using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItiProject_ms1.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [MaxLength(150)]
        public string Address { get; set; }
        public double Grade { get; set; }
        public int DeptId { get; set; }
        [ForeignKey("DeptId")]
        Department department { get; set; }
        public List<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();


    }
}
