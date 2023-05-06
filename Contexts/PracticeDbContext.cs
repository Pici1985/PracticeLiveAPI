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



        public bool DoesIdExistChecker(int id)
        {
            var allVideos = GetAllVideosToCheckID();

            var allExistingIds = new List<int>();

            foreach (var currentVideo in allVideos)
            {
                allExistingIds.Add(currentVideo.Id);
            }

            if (allExistingIds.Contains(id))
            {
                return true;
            }
            return false;
        }



        public List<Video> GetAllVideosToCheckID()
        {
            var videos = (from v in TestTable
                          select new Video()
                          {
                              Id = v.Id,
                              Title = v.Title,
                              Date = v.DateTime,
                              Url = v.imageUrls
                          }).ToList();

            return videos;
        }

        public async Task<List<UsersTable>> GetAllUsers()
        {
            var allUsers = (from user in Users
                            select user).ToList();

            return allUsers;
        }

        public async Task<UsersTable> GetUser(UserDto user)
        {
            var userToLogin = (from u in Users
                               where u.UserName == user.Username 
                               select u).FirstOrDefault();

            return userToLogin;
        }
        
        public async Task<List<Character_Avatar>> GetCharAV()
        {
            var items = (from cha in Character_Avatar
                         select cha).ToList();

            return items;
        }


    }
}
