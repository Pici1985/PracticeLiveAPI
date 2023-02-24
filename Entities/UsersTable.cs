using System.Security.Policy;

namespace PracticeFullstackApp.Entities
{
    public class UsersTable
    {
        public int Id { get; set; } 
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? IsAdmin { get; set; } 
    }
}
