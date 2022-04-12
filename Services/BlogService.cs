using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NLog.Web;

namespace BlogsConsole.Services
{
    public class BlogService
    {
        // private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        private static NLog.Logger _logger = NLogBuilder.ConfigureNLog(@"C:\Users\jo\RiderProjects\BlogsConsole\nlog.config").GetCurrentClassLogger();
        private BloggingContext _db = new BloggingContext();
        private const string NewLine = "\n";
        public void DisplayBlogs()
        {
            // Display all Blogs from the database
            var query = _db.Blogs.OrderBy(b => b.BlogId);

            Console.WriteLine("All blogs in the database:" + NewLine);

            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
        }

        public void AddNewBlog()
        {
            // Create and save a new Blog

            var name = BlogNameValidation();
            var blog = new Blog { Name = name };
            
            _db.AddBlog(blog);
            _logger.Info("Blog added - {name}", name);
            Console.WriteLine();
        }

        private string BlogNameValidation()
        {
            List<string> blogNames = new List<string>();
            
            bool boolean;
            string name;

            var query = _db.Blogs.OrderBy(b => b.BlogId);
            foreach (var blog in query)
            {
                blogNames.Add(blog.Name);
            }
            
            
            /*var query = _db.Blogs.OrderBy(b => b.BlogId).ToString();
            blogNames.Add(query);*/
            do
            {
                boolean = false;
                Console.WriteLine("Enter a name for a new Blog: ");
                name = Console.ReadLine();

                foreach (var b in blogNames)
                {
                    if (b.Equals(name))
                    {
                        Console.WriteLine("Please enter a unique Blog name." + NewLine);
                        boolean = true;
                        break;
                    }
                }
            } while (boolean);

            return name;
        }
    }
}