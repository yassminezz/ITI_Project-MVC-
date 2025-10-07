using ItiProject_ms1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ItiProject_ms1.Views.ViewModel
{
    public class InstDeptUserViewModel
    {
        public Instructor Instructor { get; set; }

        [ValidateNever]
        public List<Department> Departments { get; set; }

        // --- IDENTITY FIELDS ---
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        // -----------------------

        // --- ROLE SELECTION FIELDS ---
        [Required]
        [Display(Name = "Assign Role")]
        public string SelectedRole { get; set; } // The chosen role name (e.g., "Instructor")

        [ValidateNever]
        // This holds the list of roles to populate the dropdown
        public IEnumerable<SelectListItem> AvailableRoles { get; set; }
    }
}
