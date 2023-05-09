using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;

namespace PracticeFullstackApp.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
    }
}
