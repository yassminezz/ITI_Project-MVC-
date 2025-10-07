using ItiProject_ms1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ItiProject_ms1.Views.ViewModel
{
    public class DeptCourseViewModel
    {
        public Course course { get; set; }
        [ValidateNever]
        public List<Department> departments { get; set; }
        [ValidateNever]
        public List<Instructor> instructors { get; set; }
    }
}
