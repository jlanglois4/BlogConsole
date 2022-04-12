using System;
using System.Linq;
using BlogsConsole.Services;
using NLog.Web;

namespace BlogsConsole.Interfaces
{
    public class MainInterface
    {
        // create static instance of Logger         - I was not able to figure out how to get this to work without providing the absolute path of the nlog.config. It could not locate the file location.
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(@"C:\Users\jo\RiderProjects\BlogsConsole\nlog.config").GetCurrentClassLogger();
        private const string NewLine = "\n";
        public MainInterface()
        {
            logger.Info("Program started");
            BlogService blogService = new BlogService();
            PostService postService = new PostService();
            var choice = true;
            do
            {
                try
                {
                    Console.WriteLine(
                        "1. Display Blogs" + NewLine +
                        "2. Add Blog" + NewLine +
                        "3. Display Posts" + NewLine +
                        "4. Add Post" + NewLine +
                        "Enter anything else to exit the program");
                    string pickedChoice = Console.ReadLine();
                    switch (pickedChoice)
                    {
                        case "1":
                            blogService.DisplayBlogs();
                            break;
                        case "2":
                            blogService.AddNewBlog();
                            break;
                        case "3":
                            postService.DisplayPosts();
                            break;
                        case "4":
                            postService.AddNewPost();
                            break;
                        default:
                            choice = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            } while (choice);
            logger.Info("Program ended");
        }
    }
}