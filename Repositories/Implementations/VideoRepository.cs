using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;
using PracticeFullstackApp.Repositories.Interfaces;

namespace PracticeFullstackApp.Repositories.Implementations
{
    public class VideoRepository : IVideoRepository
    {
        internal readonly PracticeDbContext _context;

        public VideoRepository(PracticeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Video> GetAllVideos()
        {
            var videos = (from v in _context.TestTable
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
            var video = (from v in _context.TestTable
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

        public Video SaveVideo(Video video)
        {
            var videoToSave = new TestTable()
            {
                Id = video.Id,
                Title = video.Title,
                DateTime = DateTime.Now,
                imageUrls = video.Url
            };

            _context.TestTable.Add(videoToSave);
            _context.SaveChanges();

            return video;
        }

        public int DeleteVideo(int id)
        {
            var videoToDelete = new TestTable()
            {
                Id = id,
            };

            _context.TestTable.Remove(videoToDelete);
            _context.SaveChanges();

            return id;
        }

        public IEnumerable<int> DeleteVideosInRange(List<int> ids)
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

            _context.TestTable.RemoveRange(videoListToDelete);
            _context.SaveChanges();

            return ids;
        }

        public TestTable UpdateVideo(Video video)
        {
            var updatedVideo = new TestTable()
            {
                Id = video.Id,
                Title = video.Title,
                DateTime = DateTime.Now,
                imageUrls = video.Url
            };

            _context.TestTable.Update(updatedVideo);
            _context.SaveChanges();

            return updatedVideo;
        }
    }
}
