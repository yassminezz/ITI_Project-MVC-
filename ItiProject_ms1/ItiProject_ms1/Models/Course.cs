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

        [Range(50, 100, ErrorMessage = "Degree must be between 50 and 100")]
        [CustomValidation(typeof(Course), nameof(ValidateDegree), ErrorMessage = "Minimum degree must be less than Degree")]
        public double Degree { get; set; }

        [Range(20, 50, ErrorMessage = "Minimum degree must be between 20 and 50")]
        public double MinimumDegree { get; set; }

        [Range(1, 8)]
        public double Hours { get; set; }

        public int DeptId { get; set; }

        [ForeignKey(nameof(DeptId))]
        public Department? department { get; set; } // Added '?' for nullability if Department is not guaranteed

        public int InstructorId { get; set; }

        [ForeignKey(nameof(InstructorId))]
        public Instructor? Instructor { get; set; }

        public List<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();

        // Custom static validation method
        public static ValidationResult? ValidateDegree(
            object? degreeValue, // This parameter is required by CustomValidation, but we often ignore it to check the whole object
            ValidationContext context)
        {
            var course = (Course)context.ObjectInstance;
            if (course.MinimumDegree >= course.Degree)
            {
                return new ValidationResult("Minimum degree must be less than Degree");
            }

            return ValidationResult.Success;
        }
    }
}