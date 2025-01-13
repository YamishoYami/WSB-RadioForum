using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WSB_RadioForum.Data;
using WSB_RadioForum.Models;

namespace WSB_RadioForum.Controllers
{
    [Authorize]
    public class UserPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserPostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: UserPosts
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var userPosts = await _context.UserPost.ToListAsync();
            var userEmails = await _userManager.Users.ToDictionaryAsync(u => u.Id, u => u.Email);
            ViewBag.UserEmails = userEmails;
            return View(userPosts);
        }
        // GET: UserPosts/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPost = await _context.UserPost
                .Include(up => up.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPost == null)
            {
                return NotFound();
            }

            var userEmail = await _userManager.Users
                .Where(u => u.Id == userPost.UserId)
                .Select(u => u.Email)
                .FirstOrDefaultAsync();
            ViewBag.UserEmail = userEmail;

            return View(userPost);
        }
        // GET: UserPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserPosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,DateAdded")] UserPost userPost)
        {
            userPost.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                _context.Add(userPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userPost);
        }

        // GET: UserPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPost = await _context.UserPost.FindAsync(id);
            if (userPost == null)
            {
                return NotFound();
            }
            return View(userPost);
        }

        // POST: UserPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,DateAdded")] UserPost userPost)
        {
            userPost.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != userPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPostExists(userPost.Id))
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
            return View(userPost);
        }

        // GET: UserPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPost = await _context.UserPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPost == null)
            {
                return NotFound();
            }

            return View(userPost);
        }

        // POST: UserPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userPost = await _context.UserPost.FindAsync(id);
            if (userPost != null)
            {
                _context.UserPost.Remove(userPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPostExists(int id)
        {
            return _context.UserPost.Any(e => e.Id == id);
        }
    }
}
