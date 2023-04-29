using PracticeFullstackApp.Entities;

namespace PracticeFullstackApp.Repositories.Interfaces
{
    public interface IAvatarRepository
    {
        IEnumerable<Avatars> GetAll();
    }       
}
