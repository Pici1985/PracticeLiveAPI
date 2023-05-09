using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PracticeFullstackApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        
        internal readonly PracticeDbContext _context;
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration, PracticeDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request) 
        { 
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            try 
            { 
                var user = new UsersTable() 
                { 
                    UserName = request.Username,
                    Password = passwordHash,
                    IsAdmin = false
                };            

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return BadRequest(new { Message = "Username already taken!" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            var allUsers = _context.GetAllUsers();
                                    
            var user = _context.GetUser(request);

            var users = new List<User>();

            var userToLogin = new User();
           
            if (user != null) 
            {
                userToLogin.Username = user.UserName;
                userToLogin.Password = user.Password;
                userToLogin.isAdmin = user.IsAdmin; 
            }
                        
            foreach (var userTable in allUsers)
            {
                var something = new User()
                {
                    Username = userTable.UserName,
                    Password = userTable.Password,
                    isAdmin= userTable.IsAdmin
                };
                users.Add(something);
            }
                        
            bool verified = false;

            if (user != null) 
            {
                verified = BCrypt.Net.BCrypt.Verify(request.Password, userToLogin.Password);            
            }

            if (users.Any(x => x.Equals(userToLogin)) && verified) 
            {
                string token = CreateToken(userToLogin);
                return Ok(token);                
            }
            return NotFound ("User not found.");
        }

        private string CreateToken(User user)
        {

            //Console.WriteLine(user.isAdmin.ToString());

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, "https://localhost:7273" ),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString() )
            };

            if (user.isAdmin == true)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds,
                    issuer: "dotnet-user-jwts"
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
