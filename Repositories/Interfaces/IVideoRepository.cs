using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;

namespace PracticeFullstackApp.Repositories.Interfaces
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetAllVideos();
        Video GetVideo(int id);
        Video SaveVideo(Video video);
        int DeleteVideo(int id);
        IEnumerable<int> DeleteVideosInRange(List<int> ids);
        TestTable UpdateVideo(Video video);
    }
}
