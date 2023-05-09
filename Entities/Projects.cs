using System.ComponentModel.DataAnnotations;

namespace PracticeFullstackApp.Entities
{
    public class Projects
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public string? LiveUrl { get; set; }
        public string? ImageUrl { get; set; }
        public string? GitHubUrl { get; set; }
    }
}
