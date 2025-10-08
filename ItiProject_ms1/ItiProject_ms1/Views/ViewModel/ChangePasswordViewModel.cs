﻿namespace ItiProject_ms1.Views.ViewModel
{
    // Define this class in your Views.ViewModel namespace
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordViewModel
    {
        // The student will enter the temporary password here
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Temporary Password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
