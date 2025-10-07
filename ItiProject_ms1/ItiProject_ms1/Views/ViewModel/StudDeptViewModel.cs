using ItiProject_ms1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering; // Required for SelectListItem

namespace ItiProject_ms1.Views.ViewModel
{
    public class StudDeptViewModel
    {
        // --- Existing Profile Data ---
        public Student student { set; get; }

        [ValidateNever]
        public List<Department> departments { get; set; }

        // --- NEW FIELDS FOR IDENTITY CREATION ---

        [Required(ErrorMessage = "Username/Email is required")]
        [Display(Name = "Username (Email)")]
        // This is used for the IdentityUser's UserName and Email fields
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "A role must be assigned")]
        [Display(Name = "Assign Role")]
        // This holds the role name chosen by the Admin from the dropdown
        public string SelectedRole { get; set; }

        [ValidateNever]
        // This holds the list of available roles fetched from the RoleManager
        public IEnumerable<SelectListItem> AvailableRoles { get; set; }
    }
}