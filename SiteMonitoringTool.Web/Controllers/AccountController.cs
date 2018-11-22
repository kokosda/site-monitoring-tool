using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
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

            var secretValue = configuration["Tokens:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretValue));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: configuration["Tokens:Issuer"],
                audience: configuration["Tokens:Issuer"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            var result = Ok(new { Token = tokenString });
            return result;
        }

        [Authorize]
        [HttpGet("/api/login/ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}