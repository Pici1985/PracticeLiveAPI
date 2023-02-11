using Microsoft.AspNetCore.Mvc;
using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;
using PracticeFullstackApp.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PracticeFullstackApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class TestController : ControllerBase
    {
        internal readonly PracticeDbContext context;
        internal readonly IUtility utility;
        

        public TestController(PracticeDbContext context, Utility utility)
        {
            this.context = context;
            this.utility = utility;
        }
                
        [Route("/videos")]
        [HttpGet]
        public async Task<IActionResult> GetAllVideos()
        {
            
            return Ok(context.GetAllVideos());
        }
        
        [Route("/videos/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetVideo(int id)
        {
            
            return Ok(context.GetVideo(id));
        }

        [Route("/videos")]
        [HttpPost]
        public async Task<IActionResult> PostVideo([FromBody] Video video)
        {   
            if (video != null) 
            { 
                await context.SaveVideo(video);
                return Ok(new { Message = "Video saved!"});
            }
            return BadRequest("Error");

        }
        
        [Route("/videos/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            if (utility.DoesIdExistChecker(id)) 
            { 
                await context.DeleteVideo(id);                            
                return Ok(new { Message = $"VideoID: {id} deleted!"});            
            }
            return BadRequest(new { Message = $"{ id } does not exist"});
        } 
        
        [Route("/videos")]
        [HttpPut]
        public async Task<IActionResult> UpdateVideo([FromBody] Video video)
        {
            if (utility.DoesIdExistChecker(video.Id)) 
            { 
                await context.UpdateVideo(video);                            
                return Ok(new { Message = $"VideoID: {video.Id} updated!"});            
            }
            return BadRequest(new { Message = $"VideoID: { video.Id } does not exist"});
        } 
    }
}
