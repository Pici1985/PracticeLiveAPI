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



        public List<Video> GetAllVideos()
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

        public Video GetVideo(int id)
        {
            var video = (from v in TestTable
                         where v.Id == id
                         select new Video()
                         {
                             Id = v.Id,
                             Title = v.Title,
                             Date = v.DateTime,
                             Url = v.imageUrls
                         }).FirstOrDefault();

            return video;
        }

        public async Task<Video> SaveVideo(Video video)
        {
            var videoToSave = new TestTable()
            {
                Id = video.Id,
                Title = video.Title,
                DateTime = DateTime.Now,
                imageUrls = video.Url
            };

            TestTable.Add(videoToSave);
            await SaveChangesAsync();

            return video;
        }

        public async Task<int> DeleteVideo(int id)
        {
            var videoToDelete = new TestTable()
            {
                Id = id,
            };

            TestTable.Remove(videoToDelete);
            await SaveChangesAsync();

            return id;
        }
        
        public async Task<List<int>> DeleteVideosInRange(List<int> ids)
        {
            var videoListToDelete = new List<TestTable>();

            foreach (var video in ids) 
            { 
                var videoToDelete = new TestTable()
                {
                    Id = video,
                };
                videoListToDelete.Add(videoToDelete);
            }

            TestTable.RemoveRange(videoListToDelete);
            await SaveChangesAsync();

            return ids;
        }



        public async Task<TestTable> UpdateVideo(Video video)
        {
            var updatedVideo = new TestTable()
            {
                Id = video.Id,
                Title = video.Title,
                DateTime = DateTime.Now,
                imageUrls = video.Url
            };

            TestTable.Update(updatedVideo);
            await SaveChangesAsync();

            return updatedVideo;
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
    }
}
