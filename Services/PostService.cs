using System;
using System.Linq;
using NLog.Web;

namespace BlogsConsole.Services
{
    public class PostService
    {
        // private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(@"C:\Users\jo\RiderProjects\BlogsConsole\nlog.config").GetCurrentClassLogger();
        BloggingContext db = new BloggingContext();
        private BlogService _blogService = new BlogService();
        private const string NewLine = "\n";
        
        public void DisplayPosts()
        {
            // Display all Blogs from the database
            var blogId = BlogIdValidation();

            Console.WriteLine(NewLine + "Total posts: " + db.Posts.Count(post => post.BlogId.Equals(blogId)) + NewLine);
            
            foreach (var post in db.Posts)
            {
                if (post.BlogId.Equals(blogId))
                {
                    Console.WriteLine("Title: " + post.Title + NewLine + "Content: " + post.Content + NewLine);
                }
            }
        }

        public void AddNewPost()
        {
            // Create and save a new Post

            var blogId = BlogIdValidation();
            
            Console.Write("Enter a title for a new Post: ");
            var title = Console.ReadLine();
            
            Console.Write("Enter the content for the new Post: ");
            var content = Console.ReadLine();

            var newPost = new Post
            {
                Title = title,
                Content = content,
                BlogId = blogId
            };
            db.AddPost(newPost);
            logger.Info("Post added - {title}", title);
            Console.WriteLine();
        }

        private int GetBlogId(string blogNameChoice)
        {
            int blogId = -1;
            foreach (var blog in db.Blogs)
            {
                if (blog.Name.Equals(blogNameChoice))
                {
                   blogId = blog.BlogId;
                }
            }
            return blogId;
        }

        private int BlogIdValidation()
        {
            int blogId;
            do
            {
                Console.WriteLine("Enter a blog name.");
                _blogService.DisplayBlogs();
                var blogNameChoice = Console.ReadLine();
                blogId = GetBlogId(blogNameChoice);
            } while (blogId < 0);

            return blogId;
        }
    }
}