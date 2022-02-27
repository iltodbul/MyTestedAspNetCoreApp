using Microsoft.AspNetCore.Identity;

namespace MyTestedAspNetCoreApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ApplicationUser : IdentityUser
    {
        public DateTime? Birthday { get; set; }
    }
}
