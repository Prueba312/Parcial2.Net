using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Login.Models;

namespace Login.Controllers
{
    [Route("api/Authenti")]
    [ApiController]
    public class LController : ControllerBase
    {
        [HttpPost, Route("Login")]

        public IActionResult Login([FromBody]LModel user)
        {
            if (user == null)
            {
                return BadRequest("Request invalid");
            }
            if (user.UserName == "Loginprueba1" && user.Password == "Prueba098@")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@123"));
                var signincredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenoptions = new JwtSecurityToken(
                    issuer: "http://localhots:5000",
                    audience: "http://localhots:5000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials:  signincredentials
                );
                var tokensString = new JwtSecurityTokenHandler().WriteToken(tokenoptions);
                return Ok(new { Token =tokensString});
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}