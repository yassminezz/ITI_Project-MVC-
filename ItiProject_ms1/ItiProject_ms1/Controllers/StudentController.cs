using ItiProject_ms1.Filters;
using ItiProject_ms1.Models;
using ItiProject_ms1.Repository;
using ItiProject_ms1.Views.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ItiProject_ms1.Controllers
{
    [AuthurizeStudentFilter]
    [Authorize(Roles = "Admin,Hr,Student")]
    public class StudentController : Controller
    {
        private readonly IBaseRepository<Student> _studentRepo;
        private readonly IBaseRepository<Department> _deptRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; // <-- 1. DECLARE FIELD HERE ⚠️
        private readonly SignInManager<IdentityUser> _signInManager; // <-- Add this field


        public StudentController(
           UserManager<IdentityUser> userManager,
           IBaseRepository<Student> studentRepo,
           IBaseRepository<Department> deptRepo,
           RoleManager<IdentityRole> roleManager,
           SignInManager<IdentityUser> signInManager) // <-- Inject here
        {
            _studentRepo = studentRepo;
            _deptRepo = deptRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager; // <-- Initialize here
        }



        // GET: List all students
        [Authorize(Roles = "Admin,Hr")]

        public IActionResult ShowStuds()
        {
            var sd = new StudentDeptViewModel
            {
                students = _studentRepo.GetAll(),
                departments = _deptRepo.GetAll()
            };
            return View("Index", sd);
        }

        // GET: Edit student form
        public IActionResult UpdateStud(int id)
        {
            var s = _studentRepo.GetByID(id);
            if (s == null) return NotFound();

            var sd = new StudDeptViewModel
            {
                student = s,
                departments = _deptRepo.GetAll()
            };

            return View("Form", sd);
        }

        [HttpPost]
        public IActionResult UpdateStud(StudDeptViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.departments = _deptRepo.GetAll(); // reload dropdown
                return View("Form", vm);
            }

            var s = _studentRepo.GetByID(vm.student.Id);
            if (s == null) return NotFound();

            s.Name = vm.student.Name;
            s.Address = vm.student.Address;
            s.Grade = vm.student.Grade;
            s.DeptId = vm.student.DeptId;

            _studentRepo.Update(s);
            return RedirectToAction("ShowStuds");
        }

        // GET: Add new student form
        // StudentController.cs (The relevant methods only)
        // Assuming private fields _userManager, _roleManager, _studentRepo, _deptRepo exist.

        // Helper method to populate common ViewModel fields (You should add this)
        private void PopulateViewModel(StudDeptViewModel vm)
        {
            vm.departments = _deptRepo.GetAll();

            // Populate the AvailableRoles property
            vm.AvailableRoles = _roleManager.Roles
                .Select(r => new SelectListItem { Value = r.Name, Text = r.Name })
                .ToList();
        }


        // GET: Add new student form
        public IActionResult AddStud()
        {
            var sd = new StudDeptViewModel
            {
                student = new Student(),
            };

            PopulateViewModel(sd); // Populate departments and roles

            // Set default selected role to "Student" if desired
            sd.SelectedRole = "Student";

            return View("Form", sd);
        }

        // POST: Add new student
        [HttpPost]
        public async Task<IActionResult> AddStud(StudDeptViewModel sd) // CRITICAL: Must be async Task<IActionResult>
        {
            if (!ModelState.IsValid)
            {
                // Reload departments and roles upon failure
                PopulateViewModel(sd);
                return View("Form", sd);
            }

            // --- 1. CREATE IDENTITY USER ---
            var user = new IdentityUser { UserName = sd.UserName, Email = sd.UserName };
            var result = await _userManager.CreateAsync(user, sd.Password);

            if (result.Succeeded)
            {
                // 2. ASSIGN ROLE using the Admin's selection
                var roleResult = await _userManager.AddToRoleAsync(user, sd.SelectedRole);

                // 3. CRITICAL: Manually confirm email/account (for immediate login)
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, token); // Sets EmailConfirmed = true

                if (roleResult.Succeeded)
                {
                    // 4. LINK PROFILE
                    sd.student.UserId = user.Id;
                    _studentRepo.Add(sd.student);

                    return RedirectToAction("ShowStuds");
                }
                else
                {
                    // Role assignment failed: Delete user to prevent orphaned account.
                    await _userManager.DeleteAsync(user);
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, $"Role Assignment Failed: {error.Description}");
                    }
                }
            }
            else // User creation failed (password, username duplicate)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If any part of the process failed, reload the view with errors.
            PopulateViewModel(sd);
            return View("Form", sd);
        }

        // GET: Delete student
        public IActionResult deleteStud(int id)
        {
            var st = _studentRepo.GetByID(id);
            if (st != null)
            {
                _studentRepo.Delete(st);
            }
            return RedirectToAction("ShowStuds");
        }

        [Authorize(Roles = "Admin,Student,Hr")]

        public async Task<IActionResult> ShowInfo(int? id)
        {
            Student student;

            var userId = _userManager.GetUserId(User);
            var isStudent = User.IsInRole("Student");

            if (isStudent)
            {
                // Student sees only their own profile
                student = _studentRepo.GetAll().FirstOrDefault(s => s.UserId == userId);
                if (student == null)
                    return NotFound();
            }
            else 
            {
                if (id == null)
                    return BadRequest("Student ID is required");

                student = _studentRepo.GetByID(id.Value);
                if (student == null)
                    return NotFound();

            }
            var viewModel = new StudentDetailsViewModel
            {
                Student = student,
                DepartmentName = _deptRepo.GetByID(student.DeptId)?.Name
            };

            if (!string.IsNullOrEmpty(student.UserId))
            {
                var identityUser = await _userManager.FindByIdAsync(student.UserId);
                if (identityUser != null)
                {
                    viewModel.UserId = identityUser.Id;
                    viewModel.UserName = identityUser.UserName;
                    viewModel.Roles = await _userManager.GetRolesAsync(identityUser);
                }
            }

            return View("StudInfo", viewModel);
        }


        // StudentController.cs
        // GET: Display the self-service Change Password form
        [Authorize] // Any logged-in user can change their password
        [HttpGet]
        public IActionResult ChangePassword()
        {
            // You'll need a ChangePasswordViewModel for the form inputs
            return View();
        }

        // POST: Handle the self-service password change
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 1. Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // This means the user is somehow logged in but not found in the Identity database
                ModelState.AddModelError(string.Empty, "User account not found or session expired.");
                return View(model);
            }

            // 2. Attempt to change the password, which requires the old password (the temporary one)
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
                TempData["SuccessMessage"] = "Your password has been changed successfully. You can now use your new password.";
                return RedirectToAction("ShowInfo");
            }

            // 3. Handle errors (e.g., old password was wrong, or new password failed complexity rules)
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }


    }
}
