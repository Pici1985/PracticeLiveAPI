using Microsoft.AspNetCore.Mvc;
using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Models;
using PracticeFullstackApp.Repositories.Interfaces;
using PracticeFullstackApp.Utilities;


namespace PracticeFullstackApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        internal readonly PracticeDbContext context;
        internal readonly IVideoRepository videoRepo;
        internal readonly IUtility utils;

        public VideoController(PracticeDbContext context, IVideoRepository videoRepo, IUtility utils)
        {
            this.context = context;
            this.videoRepo = videoRepo;
            this.utils = utils;
        }

        [Route("/videos")]
        //[AllowAnonymous]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllVideos()
        {

            return Ok(videoRepo.GetAllVideos());
        }

        [Route("/videos/{id}")]
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetVideo(int id)
        {
            return Ok(videoRepo.GetVideo(id));
        }

        [Route("/videos")]
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> PostVideo([FromBody] Video video)
        {
            if (video != null)
            {
                videoRepo.SaveVideo(video);
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
            if (utils.DoesIdExistChecker(id))
            {
                videoRepo.DeleteVideo(id);
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
                    if (utils.DoesIdExistChecker(id))
                    {
                        existingIds.Add(id);
                    }
                }
                if (existingIds.Count != 0)
                {
                    videoRepo.DeleteVideosInRange(existingIds);
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
            if (utils.DoesIdExistChecker(video.Id))
            {
                videoRepo.UpdateVideo(video);
                return Ok(new { Message = $"VideoID: {video.Id} updated!" });
            }
            return BadRequest(new { Message = $"VideoID: {video.Id} does not exist" });
        }
        
        //[Route("/test")]
        //[HttpGet]
        ////[Authorize]
        //public async Task<IActionResult> testendpoint()
        //{
        //    var items = context.GetCharAV();

        //    return Ok(items);
        //}
    }
}
