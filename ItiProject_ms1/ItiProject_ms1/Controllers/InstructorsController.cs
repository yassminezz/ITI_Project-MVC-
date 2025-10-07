using ItiProject_ms1.Models;
using ItiProject_ms1.Repository;
using ItiProject_ms1.Views.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Rendering; // Needed for SelectListItem
using System.Linq;

namespace ItiProject_ms1.Controllers
{
    [Authorize(Roles = "Admin,Hr")]


    public class InstructorsController : Controller
    {
        private readonly IBaseRepository<Instructor> _instructorRepo;
        private readonly IBaseRepository<Department> _deptRepo;
        private readonly UserManager<IdentityUser> _userManager; // 1. Private field added
        private readonly RoleManager<IdentityRole> _roleManager; // <-- ADD THIS LINE HERE 👈

        // 2. Dependency Injection: Add UserManager<IdentityUser> to the constructor
        // Constructor in InstructorsController.cs
        public InstructorsController(
            IBaseRepository<Instructor> instructorRepo,
            IBaseRepository<Department> deptRepo,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager) // <-- Dependency MUST be here
        {
            _instructorRepo = instructorRepo;
            _deptRepo = deptRepo;
            _userManager = userManager;
            _roleManager = roleManager; // <-- Initialization MUST be here
        }

        // GET: Instructors
        public IActionResult Index()
        {
            InstsDepts model = new InstsDepts
            {
                instructors = _instructorRepo.GetAll(),
                depts = _deptRepo.GetAll()
            };
            return View(model);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var instructor = _instructorRepo.GetByID(id);
            if (instructor == null)
                return NotFound();

            var viewModel = new InstructorDetailsViewModel
            {
                Instructor = instructor,
                // 1. Get Department Name
                DepartmentName = _deptRepo.GetByID(instructor.DeptId)?.Name,
                // 2. Check if Account is Linked
                IsAccountLinked = instructor.UserId != null,
                UserId = instructor.UserId,
                UserName = null,
                Roles = Enumerable.Empty<string>() // Initialize as empty
            };

            // 3. Fetch User Details and Roles if an Identity Account is linked
            if (viewModel.IsAccountLinked && viewModel.UserId != null)
            {
                var user = await _userManager.FindByIdAsync(viewModel.UserId);
                if (user != null)
                {
                    viewModel.UserName = user.UserName;
                    // Fetch the roles for the user asynchronously
                    viewModel.Roles = await _userManager.GetRolesAsync(user);
                }
            }

            return View(viewModel);
        }

        private void PopulateViewModel(InstDeptUserViewModel vm)
    {
        // 1. Fetch Department list
        vm.Departments = _deptRepo.GetAll();

        // 2. Fetch Role list for the dropdown
        // Assumes _roleManager (RoleManager<IdentityRole>) is injected in the controller
        vm.AvailableRoles = _roleManager.Roles
            .Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            })
            .ToList();
    }

    // GET: Instructors/Create
    public IActionResult Create()
    {
        var vm = new InstDeptUserViewModel
        {
            Instructor = new Instructor()
            // Departments and AvailableRoles will be populated below
        };

        PopulateViewModel(vm); // Populate the ViewModel fields

        return View(vm);
    }

    // POST: Instructor/Create
    [HttpPost]
    public async Task<IActionResult> Create(InstDeptUserViewModel vm)
    {
        // 1. Initial Model State Validation Check
        if (!ModelState.IsValid)
        {
            PopulateViewModel(vm); // Reload dropdowns upon failure
            return View(vm);
        }

        // --- 2. CREATE IDENTITY USER ---
        var user = new IdentityUser { UserName = vm.UserName, Email = vm.UserName };
        var result = await _userManager.CreateAsync(user, vm.Password);

        if (result.Succeeded)
        {
            // 3. ASSIGN ROLE using the Admin's selection (vm.SelectedRole)
            // vm.RoleName is replaced by vm.SelectedRole from the dropdown
            var roleResult = await _userManager.AddToRoleAsync(user, vm.SelectedRole);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, token);
                if (roleResult.Succeeded)
            {
                // 4. LINK PROFILE
                vm.Instructor.UserId = user.Id;
                _instructorRepo.Add(vm.Instructor);

                return RedirectToAction("Index");
            }
            else
            {
                // Role assignment failed. Delete the user to avoid orphaned account.
                await _userManager.DeleteAsync(user);
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, $"Role Assignment Failed: {error.Description}");
                }
            }
        }
        else // User creation failed
        {
            // Add Identity errors (e.g., password complexity, duplicate user)
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we reached here, something failed. Reload the form and display errors.
        PopulateViewModel(vm);
        return View(vm);
    }
    // GET: Instructors/Edit/5
    public IActionResult Edit(int id)
        {
            var ins = _instructorRepo.GetByID(id);
            if (ins == null) return NotFound();
            return View("Edit", ins); // reuse Create view for editing
        }

        // POST: Instructors/Edit/5
        [HttpPost]
        public IActionResult Edit(Instructor updatedIns)
        {
            if (ModelState.IsValid)
            {
                _instructorRepo.Update(updatedIns);
                return RedirectToAction("Index");
            }
            return View(updatedIns);
        }

        // GET: Instructors/Delete/5
        public IActionResult Delete(int id)
        {
            var ins = _instructorRepo.GetByID(id);
            if (ins != null)
            {
                _instructorRepo.Delete(ins);
            }
            return RedirectToAction("Index");
        }
        // InstructorsController.cs

        // ... (other controller actions)

        // POST: Instructors/ResetUserPassword
        [HttpPost]
        [Authorize(Roles = "Admin,Hr")]
        public async Task<IActionResult> ResetUserPassword(string userId, string newPassword)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("User ID and new password are required.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found in Identity system.");
            }

            // Reset the password using a token mechanism
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            // Find the Instructor to redirect back to their details page
            var instructor = _instructorRepo.GetAll().FirstOrDefault(i => i.UserId == userId);
            int instructorId = instructor?.Id ?? 0;


            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Password successfully reset.";
                return RedirectToAction("Details", new { id = instructorId });
            }
            else
            {
                // Add errors to TempData to display on the Details page
                TempData["ErrorMessage"] = string.Join(" ", result.Errors.Select(e => e.Description));
                return RedirectToAction("Details", new { id = instructorId });
            }
        }

        // ... (end of controller)
    }
}
