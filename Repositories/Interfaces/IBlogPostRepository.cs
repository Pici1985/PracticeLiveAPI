using PracticeFullstackApp.Models;

namespace PracticeFullstackApp.Repositories.Interfaces
{
    public interface IBlogPostRepository
    {
        IEnumerable<Post>? GetAll();

        Post GetOne(int id);
    }
}
