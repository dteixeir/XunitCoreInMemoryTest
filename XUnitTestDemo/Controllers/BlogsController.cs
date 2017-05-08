using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XUnitTestDemo.Context;
using XUnitTestDemo.Managers;
using Microsoft.Extensions.Options;
using XUnitTestDemo.Entities;
using System.Collections.Generic;

namespace XUnitTestDemo.Controllers
{
    [Route("api/[controller]/")]
    public class BlogsController : Controller
    {
        private readonly ConnectionStrings _settings;
        private ApplicationDbContext _context;
        public BlogManager _blogManager;

        public BlogsController(IOptions<ConnectionStrings> appSettings, ApplicationDbContext context)
        {
            _settings = appSettings.Value;
            _context = context;

            _blogManager = new BlogManager(appSettings, _context);
        }
        
        [HttpGet]           // GET: Blogs
        public async Task<IActionResult> Get()
        {
            List<Blog> response = await _blogManager.Get();
            return Ok(response);
        }

        [HttpGet("{id}")]   // GET: Blogs/Details/5
        public async Task<ActionResult> Get(int id)
        {
            var response = await _blogManager.Get(id);
            return Ok(response);
        }

        [HttpPost]          // Post: Blogs
        public async Task<ActionResult> Post([FromBody] Blog blog)
        {
            Blog result = await _blogManager.Add(blog);
            return Created("Object Created", result);
        }

        [HttpPut("{id}")]   // GET: Blogs/5
        public async Task<ActionResult> Put(int id, [FromBody] Blog blog)
        {
            blog.BlogId = id;
            Blog result = await _blogManager.Update(blog);
            return Ok(result);
        }

        [HttpDelete("{id}")] // DELETE: Blogs/delete/5
        public ActionResult Delete(int id)
        {
            _blogManager.Delete(id);
            return NoContent();
        }
    }
}