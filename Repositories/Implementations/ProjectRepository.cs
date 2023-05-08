using Microsoft.EntityFrameworkCore;
using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;
using PracticeFullstackApp.Repositories.Interfaces;

namespace PracticeFullstackApp.Repositories.Implementations
{
    public class ProjectRepository : IProjectRepository
    {
        internal readonly PracticeDbContext _context;

        public ProjectRepository(PracticeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Projects> GetAll()
        {
            var allProjects = (from p in _context.Projects
                               select p).ToList();

            if (allProjects != null)
            {
                return allProjects;
            }
            return Array.Empty<Projects>();
        }
    }
}

