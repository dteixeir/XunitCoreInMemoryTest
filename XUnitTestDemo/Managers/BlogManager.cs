using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XUnitTestDemo.Context;
using XUnitTestDemo.Entities;

namespace XUnitTestDemo.Managers
{
    public class BlogManager
    {
        private readonly ConnectionStrings _settings;
        private ApplicationDbContext _context;
        private readonly int _takeCount = 10;


        public BlogManager(IOptions<ConnectionStrings> appSettings, ApplicationDbContext context)
        {
            _settings = appSettings.Value;
            _context = context;
        }

        public BlogManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Blog> Add(Blog blog)
        {
            var result = await _context.Blogs.AddAsync(blog);
            _context.SaveChanges();

            return blog;
        }

        public async void Delete(int id)
        {
            Blog blog = await _context.Blogs.SingleOrDefaultAsync(x => x.BlogId == id);
            _context.Blogs.Remove(blog);
            var result = await _context.SaveChangesAsync();

            return;
        }

        public async Task<List<Blog>> Get()
        {
            var blogs = await _context.Blogs.Take(_takeCount).ToListAsync();
            return blogs;
        }

        public async Task<Blog> Get(int id)
        {
            Blog blog = await _context.Blogs.SingleOrDefaultAsync(x => x.BlogId == id);
            return blog;
        }

        public async Task<Blog> Update(Blog blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
            return blog;
        }
    }
}
