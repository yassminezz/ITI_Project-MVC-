using ItiProject_ms1.Models;

namespace ItiProject_ms1.Views.ViewModel
{
    public class InstructorDetailsViewModel
    {
        public Instructor Instructor { get; set; }
        public string DepartmentName { get; set; }

        // Identity-related properties
        public bool IsAccountLinked { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
