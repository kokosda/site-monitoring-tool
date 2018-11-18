using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SiteMonitoringTool.Controllers.Resources;
using SiteMonitoringTool.Persistence;

namespace SiteMonitoringTool.Controllers
{
    public class AccountController : Controller
    {
        private readonly SiteMonitoringToolDbContext dbContext;
        private readonly IConfiguration configuration;

        public AccountController(SiteMonitoringToolDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        [HttpPost("/api/login")]
        public async Task<IActionResult> LogIn([FromBody] LoginResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Username == resource.UserName);

            if (user == null)
                return Unauthorized();

            if (!user.Password.Equals(resource.Password, StringComparison.InvariantCulture))
                return Unauthorized();

            var secretValue = configuration.GetValue<string>("AuthenticationSymmetricSecurityKey");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretValue));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: new List<Claim>(),
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            var result = Ok(new { Token = tokenString });
            return result;
        }
    }
}