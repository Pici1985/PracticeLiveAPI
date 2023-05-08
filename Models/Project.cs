namespace PracticeFullstackApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public string? LiveUrl { get; set; }
        public string? ImageUrl { get; set; }
        public string? GitHubUrl { get; set; }
        public List<string> Icons { get; set; }
    }
}
