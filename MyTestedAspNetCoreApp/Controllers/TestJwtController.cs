using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MyTestedAspNetCoreApp.Data;
using MyTestedAspNetCoreApp.Settings;

namespace MyTestedAspNetCoreApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class TestJwtController : ControllerBase
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly ApplicationDbContext _dbContext;

        public TestJwtController(IOptions<JwtSettings> jwtSettings, ApplicationDbContext dbContext)
        {
            _jwtSettings = jwtSettings;
            _dbContext = dbContext;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public ActionResult<UserModel> Login(LoginInputModel input)
        {
            // Проверяваме в базата дали има такъв юзър. В случая с цел тестване приемаме, че  съществува.
            //var user = _dbContext.Users.FirstOrDefault(x =>
            //    x.UserName == login.Username && x.PasswordHash == login.Password);
            //if (user == null)
            //{
            //    return null;
            //}

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._jwtSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, input.Username),
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenAsString = tokenHandler.WriteToken(token);

            return new UserModel { Token = tokenAsString };
        }

        public class LoginInputModel
        {
            public string Username { get; set; }

            public string Password { get; set; }
        }

        public class UserModel
        {
            public string Token { get; set; }
        }
    }
}
