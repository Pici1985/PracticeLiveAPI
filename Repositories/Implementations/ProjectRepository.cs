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

        public IEnumerable<Project> GetAll()
        {
            var allProjects = (from p in _context.Projects
                               select new Project() 
                               { 
                                    Id = p.Id,
                                    Title = p.Title,
                                    Description = p.Description,
                                    Detail = p.Detail,
                                    LiveUrl = p.LiveUrl,
                                    ImageUrl = p.ImageUrl,
                                    GitHubUrl = p.GitHubUrl,
                                    Icons = (from icon in _context.Icons
                                             join project_icon in _context.Project_Icon on icon.Id equals project_icon.Icon_Id
                                             join project in _context.Projects on project_icon.Project_Id equals project.Id
                                             where project_icon.Project_Id == p.Id
                                             select icon.IconUrl
                                            ).ToList()
                               }).ToList();

            if (allProjects != null)
            {
                return allProjects;
            }
            return Array.Empty<Project>();
        }
    }
}

