using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
    public class ForumController : Controller
    {
        private readonly MyForumDbContext _context;

        public ForumController(MyForumDbContext context)
        {
            _context = context;
        }

        // GET: Forum
        public async Task<IActionResult> Index()
        {
            var Result = _context.Forums.Include(x => x.User).OrderByDescending(x => x.PublishedDateTime).ToListAsync();
            return View(await Result);
        }
        public ActionResult ViewPostsForum(int id)
        {
            Console.WriteLine(id);
            return RedirectToAction("Index", "Post", new { id = id });
        }

        // GET: Forum/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }
            var forum = await _context.Forums.Include(x => x.User)
                .FirstOrDefaultAsync(m => m.IdForum == id);
            if (forum == null)
            {
                return NotFound();
            }
            return View(forum);
        }

        // GET: Forum/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Forum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdForum,NameForum,PictureForum,UserId,PublishedDateTime")] Forum forum)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                FileStream fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName),FileMode.Create);
                file[0].CopyTo(fileStream);
                forum.PictureForum = ImageName;
            }
            else if(forum.PictureForum == null)
            {
                forum.PictureForum = "deafaultforum.jpg";
            }
            else
            {
                forum.PictureForum = forum.PictureForum;
            }
            if (ModelState.IsValid)
            {
                forum.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                forum.PublishedDateTime = DateTime.Now;
                _context.Add(forum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forum);
        }

        // GET: Forum/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }
            var forum = await _context.Forums.FindAsync(id);
            if (forum == null)
            {
                return NotFound();
            }
            return View(forum);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("IdForum,NameForum,PictureForum,IdUsercreated,PublishedDateTime")] Forum forum)
        {
            var existingForum = _context.Forums.Find(id);
            if (existingForum == null)
            {
                return NotFound();
            }
            Console.WriteLine(" \n\n\n\n"+existingForum.PictureForum+ " \n\n\n\n");
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                FileStream fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName), FileMode.Create);
                file[0].CopyTo(fileStream);
                Console.WriteLine(" \n\n\n\n"+ImageName +" \n\n\n\n");
                //forum.PictureForum = ImageName;
                existingForum.PictureForum = ImageName;
            }
            existingForum.NameForum = forum.NameForum;
            existingForum.PictureForum = forum.PictureForum;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingForum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumExists(existingForum.IdForum))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(existingForum);
        }

        // GET: Forum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums.Include(x => x.User)
                .FirstOrDefaultAsync(m => m.IdForum == id);
            if (forum == null)
            {
                return NotFound();
            }

            return View(forum);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Forums == null)
            {
                return Problem("Entity set 'MyForumDbContext.Forums'  is null.");
            }
            var forum = await _context.Forums.Include(f => f.Posts).FirstOrDefaultAsync(f => f.IdForum == id);
            if (forum != null)
            {
                _context.Posts.RemoveRange(forum.Posts);
                _context.Forums.Remove(forum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumExists(int id)
        {
          return _context.Forums.Any(e => e.IdForum == id);
        }
    }
}
