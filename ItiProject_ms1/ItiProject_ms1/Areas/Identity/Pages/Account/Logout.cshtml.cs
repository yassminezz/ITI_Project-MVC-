// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ItiProject_ms1.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        // Inside your LogoutModel class (e.g., in Areas/Identity/Pages/Account/Logout.cshtml.cs)

        public async Task<IActionResult> OnGetAsync()
        {
            // Check if the user is authenticated before signing out
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            // Redirect to the Home Index page. "~/" represents the application root.
            return LocalRedirect("~/");
        }

        // Optionally, you can simplify the OnPostAsync as well, 
        // ensuring it also redirects to home if no returnUrl is provided.
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // Default to home page if no specific return URL is given
                return LocalRedirect("~/");
            }
        }
    }
}
