using Azure.Identity;
using PracticeFullstackApp.Entities;

namespace PracticeFullstackApp.Models
{
    public class User
    {
        public string? Username { get; set; } 
        public string? Password { get; set; }
        public bool? isAdmin { get; set; }

        // This is interesting 
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User otherUser = (User)obj;

            return this.Username == otherUser.Username &&
                   this.Password == otherUser.Password &&
                   this.isAdmin == otherUser.isAdmin;
        }

        public override int GetHashCode()
        {
            return (this.Username + this.Password).GetHashCode();
        }
    }
}
