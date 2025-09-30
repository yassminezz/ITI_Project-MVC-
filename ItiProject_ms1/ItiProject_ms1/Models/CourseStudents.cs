using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItiProject_ms1.Models
{
    public class CourseStudents
    {
        [Key]
        public int Id { get; set; }
        public double Degree { get; set; }

        public int CrsId { get; set; }
        [ForeignKey(nameof(CrsId))]
        public Course Course { get; set; }

        // FK -> Student (StdId)
        public int StdId { get; set; }
        [ForeignKey(nameof(StdId))]
        public Student Student { get; set; }
    }
}
