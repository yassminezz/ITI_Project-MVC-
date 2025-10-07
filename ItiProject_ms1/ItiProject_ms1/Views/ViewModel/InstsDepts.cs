using ItiProject_ms1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ItiProject_ms1.Views.ViewModel
{
    public class InstsDepts
    {
       public List<Instructor> instructors { get; set; }
        [ValidateNever]
        public  List<Department>depts { get; set; }
    }
}
