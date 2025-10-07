using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItiProject_ms1.Models
{
    public class CourseStudents
    {
        [Key]
        public int Id { get; set; }
        [Range(0,100)]
        public double Degree { get; set; }

        public int CrsId { get; set; }
        [ForeignKey(nameof(CrsId))]
        [ValidateNever]
        public Course Course { get; set; }

        // FK -> Student (StdId)
        public int StdId { get; set; }
        [ForeignKey(nameof(StdId))]
        [ValidateNever]

        public Student Student { get; set; }
    }
}
