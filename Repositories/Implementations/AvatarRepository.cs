using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Repositories.Interfaces;

namespace PracticeFullstackApp.Repositories.Implementations
{
    public class AvatarRepository : IAvatarRepository
    {
        internal readonly PracticeDbContext _context;

        public AvatarRepository(PracticeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Avatars> GetAll()
        { 
            var allAvatars = (from av in _context.Avatars
                              select av).ToList();

            if (allAvatars != null) 
            { 
                return allAvatars;
            }
            return Array.Empty<Avatars>();
        }
    }
}
