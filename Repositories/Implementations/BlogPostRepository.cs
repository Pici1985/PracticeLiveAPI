using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Models;
using PracticeFullstackApp.Repositories.Interfaces;

namespace PracticeFullstackApp.Repositories.Implementations
{
    public class BlogPostRepository : IBlogPostRepository
    {
        internal readonly PracticeDbContext _context;

        public BlogPostRepository(PracticeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Post>? GetAll()
        {
            var allPosts = (from p in _context.BlogPosts
                            select new Post() 
                            { 
                                Id = p.Id,
                                Title = p.Title,
                                BlogPost = p.BlogPost,
                                Date = p.Date,
                                Image = p.Image,
                                Snippet = p.Snippet,
                                Author = p.Author,
                                PhotoOwner = p.PhotoOwner,
                            }).ToList();
                                 

            if (allPosts != null)
            {
                return allPosts;
            }
            return null;
        }

        public Post? GetOne(int id)
        {
            var post = (from p in _context.BlogPosts
                           where p.Id == id
                           select new Post()
                           {
                               Id = p.Id,
                               Title = p.Title,
                               BlogPost = p.BlogPost,
                               Date = p.Date,
                               Image = p.Image,
                               Snippet = p.Snippet,
                               Author = p.Author,
                               PhotoOwner = p.PhotoOwner,
                           }).FirstOrDefault();


            if (post != null)
            {
                return post;
            }
            return null;
        }

    }
}
