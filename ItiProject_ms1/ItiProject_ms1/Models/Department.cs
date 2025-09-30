using System.ComponentModel.DataAnnotations;

namespace ItiProject_ms1.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string ManagerName { get; set; }
        List<Course> courses { get; set; }
        List<Instructor> instructors {  get; set; }
        List<Student>students { get; set; }
    }
}
