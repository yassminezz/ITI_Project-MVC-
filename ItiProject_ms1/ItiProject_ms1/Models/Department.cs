using System.ComponentModel.DataAnnotations;

namespace ItiProject_ms1.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [MaxLength(40)]
        [MinLength(2)]
        public string ManagerName { get; set; }
        List<Course> courses { get; set; }
        List<Instructor> instructors {  get; set; }
        List<Student>students { get; set; }
    }
}
