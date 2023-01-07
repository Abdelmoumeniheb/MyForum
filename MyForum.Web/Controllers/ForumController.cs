using System;
using System.Collections.Generic;
using System.Linq;
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
              return View(await _context.Forums.ToListAsync());
        }

        // GET: Forum/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums
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
        public async Task<IActionResult> Create([Bind("IdForum,NameForum,PictureForum,IdUsercreated,PublishedDateTime")] Forum forum)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("IdForum,NameForum,PictureForum,IdUsercreated,PublishedDateTime")] Forum forum)
        {
            if (id != forum.IdForum)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumExists(forum.IdForum))
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
            return View(forum);
        }

        // GET: Forum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forum = await _context.Forums
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
            var forum = await _context.Forums.FindAsync(id);
            if (forum != null)
            {
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
