using Microsoft.AspNetCore.Mvc;
using MyTestedAspNetCoreApp.Data;
using MyTestedAspNetCoreApp.ViewModel.Home.ViewComponents;

namespace MyTestedAspNetCoreApp.TagHelpers.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RegisteredUsersViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public RegisteredUsersViewComponent(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IViewComponentResult Invoke(string title)
        {
            var users = this._db.Users.Count();
            var viewModel = new RegisteredUsersViewModel
            {
                Title = title,
                Users = users
            };

            return this.View(viewModel);
        }
    }
}
