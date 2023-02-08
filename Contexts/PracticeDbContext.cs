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



        public List<Video> GetAllVideos() 
        {
            var videos = (from v in TestTable
                          select new Video() 
                          { 
                            Id= v.Id,
                            Title= v.Title,    
                            Date= v.DateTime,
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
                         }).FirstOrDefault();

            return video;
        }
    }
}
