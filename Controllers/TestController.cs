using Microsoft.AspNetCore.Mvc;
using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PracticeFullstackApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class TestController : ControllerBase
    {
        internal readonly PracticeDbContext context;

        public TestController(PracticeDbContext context)
        {
            this.context = context;
        }

        // GET: api/<TestController>
        [Route("/videos")]
        [HttpGet]
        public async Task<IActionResult> GetAllVideos()
        {
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Ok(context.GetAllVideos());
        }
        
        [Route("/videos/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetVideo(int id)
        {
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Ok(context.GetVideo(id));
        }
        
        [Route("/test")]
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Ok(new { Message = "Success!!"});
        }

        
    }
}
