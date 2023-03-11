using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;
using PracticeFullstackApp.Utilities;

namespace PracticeFullstackApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        internal readonly PracticeDbContext context;
        internal readonly IUtility utility;

        public VideoController(PracticeDbContext context, Utility utility)
        {
            this.context = context;
            this.utility = utility;
        }

        [Route("/videos")]
        //[AllowAnonymous]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllVideos()
        {

            return Ok(context.GetAllVideos());
        }

        [Route("/videos/{id}")]
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetVideo(int id)
        {
            return Ok(context.GetVideo(id));
        }

        [Route("/videos")]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> PostVideo([FromBody] Video video)
        {
            if (video != null)
            {
                await context.SaveVideo(video);
                return Ok(new { Message = "Video saved!" });
            }
            return BadRequest("Error");
        }

        [Route("/videos/{id}")]
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        //[Authorize]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            if (utility.DoesIdExistChecker(id))
            {
                await context.DeleteVideo(id);
                return Ok(new { Message = $"VideoID: {id} deleted!" });
            }
            return BadRequest(new { Message = $"{id} does not exist" });
        }

        [Route("/videos")]
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        //[Authorize]
        public async Task<IActionResult> DeleteVideos([FromBody] ItemsToDelete ids)
        {
            List<int> existingIds = new List<int>();

            if (ids.Ids != null && ids.Ids.Count > 0)
            {
                foreach (var id in ids.Ids)
                {
                    if (utility.DoesIdExistChecker(id))
                    {
                        existingIds.Add(id);
                    }
                }
                if (existingIds.Count != 0)
                {
                    await context.DeleteVideosInRange(existingIds);
                    string idsDeleted = string.Join(",", existingIds);
                    return Ok($"Videos with id: {idsDeleted} removed");
                }
                return BadRequest(new { Message = $"Nothing to delete!" });
            }
            return BadRequest(new { Message = $"Nothing to delete!" });
        }

        [Route("/videos")]
        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> UpdateVideo([FromBody] Video video)
        {
            if (utility.DoesIdExistChecker(video.Id))
            {
                await context.UpdateVideo(video);
                return Ok(new { Message = $"VideoID: {video.Id} updated!" });
            }
            return BadRequest(new { Message = $"VideoID: {video.Id} does not exist" });
        }
    }
}
