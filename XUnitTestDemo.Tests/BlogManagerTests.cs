using Xunit;
using XUnitTestDemo.Context;
using XUnitTestDemo.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace XUnitTestDemo.Tests
{
    public class BlogManagerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Managers.BlogManager _blogManager;

        public BlogManagerTests()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;

            _context = new ApplicationDbContext(options);
            _blogManager = new Managers.BlogManager(_context);

            InsertRecords();
        }

        public void InsertRecords()
        {
            _context.Blogs.Add(new Blog { BlogId = 1, Url = "www.test1.com", Posts = null });
            _context.Blogs.Add(new Blog { BlogId = 2, Url = "www.test2.com", Posts = null });
            _context.Blogs.Add(new Blog { BlogId = 3, Url = "www.test3.com", Posts = null });
            _context.Blogs.Add(new Blog { BlogId = 4, Url = "www.test4.com", Posts = null });
            _context.Blogs.Add(new Blog { BlogId = 5, Url = "www.test5.com", Posts = null });

            _context.Posts.Add(new Post { PostId = 1, Title = "Title1", Content = "Content1", BlogId = 1 });
            _context.Posts.Add(new Post { PostId = 2, Title = "Title2", Content = "Content2", BlogId = 2 });
            _context.Posts.Add(new Post { PostId = 3, Title = "Title3", Content = "Content3", BlogId = 2 });
            _context.Posts.Add(new Post { PostId = 4, Title = "Title4", Content = "Content4", BlogId = 3 });
            _context.SaveChanges();
        }

        [Fact]
        public async void TestAdd()
        {
            Blog blog = new Blog { BlogId = 6, Url = "www.text1.com", Posts = null };
            Blog result = await _blogManager.Add(blog);
            result.Url = "www.text2.com";
            Blog result2 = await _blogManager.Update(result);

            Assert.Equal(result2.Url, "www.text2.com");
        }

        [Fact]
        public async void TestBlogPosts()
        {
            Blog result = await _blogManager.Get(2);

            Assert.NotNull(result);
            Assert.NotNull(result.Posts);
            Assert.Equal(result.Posts.Count, 2);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public async void TestGet(int id)
        {
            var result = await _blogManager.Get(id);
            Assert.Equal(result.Url, ("www.test" + id +".com"));
        }
    }
}
