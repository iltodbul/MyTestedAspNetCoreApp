using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTestedAspNetCoreApp.Models;

namespace MyTestedAspNetCoreApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IdentityTestController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityTestController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            //return Json(this.User.IsInRole("Admin"));
            return Json(user);
        }

        public async Task<IActionResult> AddRole()
        {
            if (!await this._roleManager.RoleExistsAsync("Admin"))
            {
                await this._roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
            }
            var user = await this._userManager.GetUserAsync(this.User);

            await this._userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction(nameof(LogoutUser)); // след добаваня или смяна на роля на даден юзър за да се обнови бисквитката трябва да се разлогне и логне. 
        }

        public async Task<IActionResult> LogoutUser()
        {
            await this._signInManager.SignOutAsync();

            return this.Redirect("/");
        }

        public async Task<IActionResult> AddClaim()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            var result = await this._userManager.AddClaimAsync(user, new Claim("state", "GA"));
            var role = await this._roleManager.FindByNameAsync("Admin");
            await this._roleManager.AddClaimAsync(role, new Claim("adminType", "SuperAdmin"));

            return this.Json(result);
        }

        public IActionResult GetClaim()
        {
            var claim = this.User.FindFirst("state");

            return this.Json(claim?.Value);
        }
    }
}
