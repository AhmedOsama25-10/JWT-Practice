using APIREV.DTO;
using APIREV.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIREV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> Usermanger;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> _Usermanger , IConfiguration config)
        {
            Usermanger = _Usermanger;
            this.config = config;
        }
        [HttpPost("Registeration")]
        public async Task<IActionResult> Registeration(ApplicationUserDTO NewUser)
        {
            if (ModelState.IsValid==true)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = NewUser.UserName;
                user.Email = NewUser.Email;
                IdentityResult result = await Usermanger.CreateAsync(user, NewUser.Password);
                if (result.Succeeded)
                
                    return Ok("User had Created");
                else
                {
                        List<string> errors = new();

                    foreach (var item in result.Errors)
                    {
                        errors.Add(item.Description);
                    }
                    return BadRequest(errors.ToString());
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO LogUser)
        {
            if (ModelState.IsValid==true)
            {
                ApplicationUser user = await Usermanger.FindByNameAsync(LogUser.UserName);
                if (user !=null)
                {
                    bool found = await Usermanger.CheckPasswordAsync(user, LogUser.Password);
                    if (found)
                    {
                        var Claims = new List<Claim>();
                        Claims.Add(new Claim(ClaimTypes.Name,user.UserName));
                        Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var roles = await Usermanger.GetRolesAsync(user);
                        foreach (var item in roles)
                        {
                            Claims.Add(new Claim(ClaimTypes.Role, item));
                        }
                        SecurityKey securityKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
                        SigningCredentials signincred =
                           new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],//url web api
                            audience: config["JWT:ValidAudiance"],//url consumer angular
                            claims:Claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signincred
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo
                        });
                    }
                }
                return Unauthorized();

            }
            return Unauthorized();
        
        }
    }
}
        
    

