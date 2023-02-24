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
        //public static User user = new User();
        internal readonly PracticeDbContext _context;
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration, PracticeDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User request) 
        { 
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                        
            var user = new UsersTable() 
            { 
                UserName = request.Username,
                Password = request.Password,
                IsAdmin = request.isAdmin
            };            

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto request)
        {
            //var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var allUsers = await _context.GetAllUsers();
                                    
            var user = await _context.GetUser(request);

            var users = new List<User>();

            var userToLogin = new User()
            {
                Username= user.UserName,
                Password = user.Password,
                isAdmin = user.IsAdmin
            };

            //foreach (var s in allUsers) 
            //{
            //    Console.ForegroundColor= ConsoleColor.Green;
            //    Console.WriteLine($"{s.UserName}, {s.Password}");
            //}

            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"{userToLogin.Username}, {userToLogin.Password}");


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

            if (users.Any(x => x.Equals(userToLogin))) 
            {
                string token = CreateToken(userToLogin);
                return Ok(token);                
            }
            return NotFound ("User not found.");
        }

        private string CreateToken(User user)
        {

            Console.WriteLine(user.isAdmin.ToString());

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, "https://localhost:7273" ),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString() )
            };

            if (user.isAdmin == "true")
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
