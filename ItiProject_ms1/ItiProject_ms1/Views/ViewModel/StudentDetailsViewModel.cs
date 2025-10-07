using ItiProject_ms1.Models;

namespace ItiProject_ms1.Views.ViewModel
{
    public class StudentDetailsViewModel
    {
        public Student Student { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; } // The Identity GUID
        public string DepartmentName { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
        public bool IsAccountLinked => !string.IsNullOrEmpty(Student?.UserId);
    }
}
