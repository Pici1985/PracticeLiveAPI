using Microsoft.EntityFrameworkCore;
using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;

namespace PracticeFullstackApp.Utilities
{
    public interface IUtility
    {
        bool DoesIdExistChecker(int id);
        List<Video> GetAllVideosToCheckID();
        List<Character_Avatar> GetCharAV();
    }   
     

    public class Utility : IUtility
    {
        internal readonly PracticeDbContext _context;

        public Utility(PracticeDbContext context)
        {
            _context = context;
        }

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

        public List<Character_Avatar> GetCharAV()
        {
            var items = (from cha in _context.Character_Avatar
                         select cha).ToList();

            return items;
        }
    }
}
