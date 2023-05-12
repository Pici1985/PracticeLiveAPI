using System.ComponentModel.DataAnnotations;

namespace PracticeFullstackApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? BlogPost { get; set; }
        public string? Date { get; set; }
        public string? Image { get; set; }
        public string? Snippet { get; set; }
        public string? Author { get; set; }
        public string? PhotoOwner { get; set; }
    }
}
