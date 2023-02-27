using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyForum.BL.Entities;
using MyForum.DAL;

namespace MyForum.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly MyForumDbContext _context;

        public PostController(MyForumDbContext context)
        {
            _context = context;
        }
        // GET: Post
        public async Task<IActionResult> Index(int id)
        {
            var Result = _context.Posts.Include(x => x.User).OrderByDescending(x => x.PublishedDateTime).Where(p => p.ForumId == id).ToListAsync();
            ViewData["ForumId"] = id;

            return View(await Result);
        }
        // GET: Post/Details/5
        public ActionResult ViewCommentsPost(int id)
        {
            Console.WriteLine(id);
            return RedirectToAction("Index", "Comments", new { id = id });
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.IdPost == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create(int id)
        {
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[Bind("IdPost,Title,Content,PublishedDateTime,UserId,IdForum")] Post post)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("id     " + id);
                post.ForumId = id ;
                post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                post.PublishedDateTime = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewPostsForum", "Forum", new { id = id });
            }
            return View(post);
        }
        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPost,Title,Content,PublishedDateTime,UserId,IdForum")] Post post)
        {
            var existingPost = _context.Posts.Find(id);
            if (existingPost == null)
            {
                return NotFound();
            }
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(existingPost.IdPost))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ViewPostsForum", "Forum", new { id = existingPost.ForumId });
            }
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.IdPost == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'MyForumDbContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("ViewPostsForum", "Forum", new { id = post.ForumId });
        }

        private bool PostExists(int id)
        {
          return _context.Posts.Any(e => e.IdPost == id);
        }
    }
}
