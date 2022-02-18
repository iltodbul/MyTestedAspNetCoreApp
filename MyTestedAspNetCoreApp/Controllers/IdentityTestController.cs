using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyTestedAspNetCoreApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IdentityTestController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly RoleManager<IdentityUser> _roleManager;

        public IdentityTestController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_roleManager = roleManager;
        }

        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            return Json(user);
        }

        //public async Task<IActionResult> AddRole()
        //{
        //    var user = await this._userManager.GetUserAsync(this.User);
        //    await this._userManager.AddToRoleAsync(user, "admin");

        //    return RedirectToAction(nameof(GetCurrentUser));
        //}

        public async Task<IActionResult> LogoutUser()
        {
            await this._signInManager.SignOutAsync();

            return this.Redirect("/");
        }
    }
}
