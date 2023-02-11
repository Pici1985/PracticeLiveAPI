using PracticeFullstackApp.Contexts;

namespace PracticeFullstackApp.Utilities
{
    public interface IUtility 
    {
        public bool DoesIdExistChecker(int id);
    }

    public class Utility : IUtility
    {
        internal readonly PracticeDbContext context;
        public Utility(PracticeDbContext context)
        {
            this.context = context;
        }

        public bool DoesIdExistChecker(int id)
        {
            var allVideos = context.GetAllVideos();

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
    }
}
