using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItiProject_ms1.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public double Salary { get; set; }
        [MaxLength(150)]

        public string Address { get; set; }
        public string ImagePath { get; set; }
        public int DeptId { get; set; }
        [ForeignKey("DeptId")]
        Department department { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
