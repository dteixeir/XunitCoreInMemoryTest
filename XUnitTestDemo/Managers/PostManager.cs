using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using XUnitTestDemo.Context;
using XUnitTestDemo.Entities;

namespace XUnitTestDemo.Managers
{

    public class PostManager
    {
        private readonly ConnectionStrings _settings;
        private ApplicationDbContext _context;


        public PostManager(IOptions<ConnectionStrings> appSettings, ApplicationDbContext context)
        {
            _settings = appSettings.Value;
            _context = context;
        }

        public List<Post> GetPosts()
        {
            return _context.Posts.Take(5).ToList();
        }
    }
}
