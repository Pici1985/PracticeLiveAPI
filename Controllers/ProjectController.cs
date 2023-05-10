using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeFullstackApp.Models;
using PracticeFullstackApp.Repositories.Implementations;
using PracticeFullstackApp.Repositories.Interfaces;

namespace PracticeFullstackApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        internal readonly IProjectRepository _iprojectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _iprojectRepository = projectRepository;
        }

        [Route("/projects")]
        [HttpGet]
        [EnableCors]
        public async Task<IActionResult> GetAllProjects()
        {
            IEnumerable<Project>? projects = _iprojectRepository.GetAll();

            if(projects != null) 
            { 
                return Ok(projects);
            }
            return BadRequest();

        }
        
        [Route("/projects/{id}")]
        [HttpGet]
        [EnableCors]
        public async Task<IActionResult> GetOneProject(int id)
        {
            Project? project = _iprojectRepository.GetOne(id);

            if (project != null) 
            { 
                return Ok(project);
            }
            return BadRequest();
        }
    }
}
