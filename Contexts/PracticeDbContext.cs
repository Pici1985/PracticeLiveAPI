using Microsoft.EntityFrameworkCore;
using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;

namespace PracticeFullstackApp.Contexts
{
    public class PracticeDbContext : DbContext
    {
        public PracticeDbContext(DbContextOptions<PracticeDbContext> options) : base(options)
        {

        }

        public DbSet<TestTable> TestTable { get; set; }
        public DbSet<UsersTable> Users { get; set; }
        public DbSet<Characters> Characters { get; set; }
        public DbSet<CharactersALT> CharactersALT { get; set; }
        public DbSet<Avatars> Avatars { get; set; }
        public DbSet<Character_Avatar> Character_Avatar { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Project_Icon> Project_Icon { get; set; }
        public DbSet<Icons> Icons { get; set; }
        public DbSet<BlogPosts> BlogPosts { get; set; }




        //this will need a repository and controller
        public List<UsersTable> GetAllUsers()
        {
            var allUsers = (from user in Users
                            select user).ToList();

            return allUsers;
        }

        public UsersTable GetUser(UserDto user)
        {
            var userToLogin = (from u in Users
                               where u.UserName == user.Username
                               select u).FirstOrDefault();

            return userToLogin;
        }
    }
}
