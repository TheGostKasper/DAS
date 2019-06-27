using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Text;

using Basty.Models;
using Basty.Interfaces;
using Basty.Repository;
using Basty.Helpers;

namespace Basty.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IRepositoryWrapper _repoWrapper;
        private readonly AppSettings _appSettings;

         public UsersController(IRepositoryWrapper repoWrapper,IOptions<AppSettings> appSettings)
        {
            _repoWrapper = repoWrapper;
            _appSettings = appSettings.Value;
        }
       
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var roles=_repoWrapper.Role.SelectAll().ToList();
               return Ok(new { data= _repoWrapper.User.IncludeRels(e=>new User{
                   UserId=e.UserId,
                   UserName=e.UserName,
                   RoleId=e.RoleId,
                   CreatedDate=e.CreatedDate,
                   Password=e.Password,
                   Role=roles.SingleOrDefault(r=>r.Id==e.RoleId)
                   }).ToList(), message="Users Found Successfully"});
            //    return Ok(new { data= _repoWrapper.User.SelectAll().ToList(), message="Users Found Successfully"});
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var _obj =  _repoWrapper.User.FindOne(x => x.UserId==id);

                if (_obj == null)return NotFound();

               return Ok(new { data= _obj, message="User Found Successfully"});
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] User obj)
        {
            try
            {
                _repoWrapper.User.Create(obj);
                _repoWrapper.Save();

                var _obj=_repoWrapper.User.FindOne(e=>e.UserName==obj.UserName);
               return Ok(new { data= _obj, message="User Created Successfully"});
               
            }
            catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User obj)
        {
            try
            {
                _repoWrapper.User.Update(obj);
                _repoWrapper.Save();

               return Ok(new { data= obj, message="User Updated Successfully"});
            }
             catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var _obj =  _repoWrapper.User.FindOne(x => x.UserId==id);

                if (_obj == null) return NotFound();

                _repoWrapper.User.Delete(_obj);
                _repoWrapper.Save();
                
               return Ok(new { data= _obj, message="User Deleted Successfully"});

            }
             catch (Exception exp)
            {
                 return BadRequest(new { message = exp.Message });
            }
        }

       
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user =AuthenticateUser(userParam.UserName,userParam.Password);

            if (user == null)
                return BadRequest(new {data="", message = "Username or password is incorrect" });

            return Ok(new {data=user, message = "User Found" });
        }

      public User AuthenticateUser(string username, string password)
        {
             var user = _repoWrapper.User.FindOne(x => x.UserName == username && x.Password == password);
             user.Role=_repoWrapper.Role.FindOne(x => x.Id == user.RoleId);
             
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;
            return user;
        }
    
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
            {
                if (password == null) throw new ArgumentNullException("password");
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
            }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
}
}
