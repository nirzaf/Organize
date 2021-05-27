using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Organize.Shared.Enitites;
using Organize.TestFake;

namespace Organize.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private static readonly IList<User> Users = new List<User>();

        static UsersController()
        {
            var user = new User();
            user.Id = TestData.TestUser.Id;
            user.UserName = TestData.TestUser.UserName;
            user.Password = TestData.TestUser.Password;
            user.FirstName = TestData.TestUser.FirstName;
            user.LastName = TestData.TestUser.LastName;
            Users.Add(user);
        }
        public UsersController(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var id = int.Parse(Request.HttpContext.User.FindFirst("id").Value);
            var foundUser = Users.FirstOrDefault(u => u.Id == id);
            if (foundUser == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            return Ok(foundUser);
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] AuthUser user)
        {
            var foundUser = Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if(foundUser == null)
            {

                return BadRequest(new { message = "User name or password invalid" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new Claim[]
            {
                new Claim("id",foundUser.Id.ToString()), 
                new Claim(ClaimTypes.Role,"admin")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var returnUser = new User
            {
                Id = foundUser.Id,
                UserName = foundUser.UserName,
                FirstName = foundUser.FirstName,
                LastName = foundUser.LastName,
                GenderType = foundUser.GenderType,
                PhoneNumber = foundUser.PhoneNumber,
                Token = tokenHandler.WriteToken(securityToken)
            };

            return Ok(returnUser);
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post(User user)
        {
            var foundUser = Users.FirstOrDefault(u => u.UserName == user.UserName);
            if (foundUser != null)
            {
                return BadRequest(new { message = "User already exists" });
            }

            var newId = Users.Count == 0
                ? 1
                : Users.Max(i => i.Id) + 1;
            user.Id = newId;
            Users.Add(user);

            return Ok(user.Id);
        }
    }
}
