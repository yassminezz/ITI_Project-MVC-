using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItiProject_ms1.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public double Degree { get; set; }
        public string MinimumDegree { get; set; }
        public double Hours { get; set; }
        public int DeptId { get; set; }

        [ForeignKey(nameof(DeptId))]
        Department department { get; set; }

        public List<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();

        List<CourseStudents> courseStudents { get; set; }
   
    }
}
