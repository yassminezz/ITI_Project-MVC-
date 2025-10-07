using ItiProject_ms1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ItiProject_ms1.Views.ViewModel
{
    public class CourseStudentViewModel
    {
        public CourseStudents courseStudents { get; set; }
        [ValidateNever]
        public List<Course> courses { set; get; }
        [ValidateNever]
        public List<Student> students { set;get; }
    }
}
