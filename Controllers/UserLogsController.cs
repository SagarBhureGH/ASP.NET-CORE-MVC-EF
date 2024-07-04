using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project2024.Data;
using MVC_Project2024.Models;

namespace MVC_Project2024.Controllers
{
    public class UserLogsController : Controller
    {
        private readonly AppDbContext _context;

        public UserLogsController(AppDbContext context)
        {
            _context = context;
        }


        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(UserLog user)//in my user log model i make email prop thats why in my view ia addd email also
         {
            if (ModelState.IsValid)
            {
                var data= _context.userLogs.FirstOrDefault(s=> user.Name==s.Name && user.Password==s.Password);
                if (data != null)
                {
                   // HttpContext.Session.SetString("UserName", user.Name);
                return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ModelState.AddModelError("", "Username or Password is incorrect.");
            }
            return RedirectToAction("LogIn", "UserLogs");
        }

        // GET: UserLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.userLogs.ToListAsync());
        }

        // GET: UserLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLog = await _context.userLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLog == null)
            {
                return NotFound();
            }

            return View(userLog);
        }

        // GET: UserLogs/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password")] UserLog userLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userLog);
        }

        // GET: UserLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLog = await _context.userLogs.FindAsync(id);
            if (userLog == null)
            {
                return NotFound();
            }
            return View(userLog);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password")] UserLog userLog)
        {
            if (id != userLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLogExists(userLog.Id))
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
            return View(userLog);
        }

        // GET: UserLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLog = await _context.userLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLog == null)
            {
                return NotFound();
            }

            return View(userLog);
        }

        // POST: UserLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userLog = await _context.userLogs.FindAsync(id);
            if (userLog != null)
            {
                _context.userLogs.Remove(userLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLogExists(int id)
        {
            return _context.userLogs.Any(e => e.Id == id);
        }

     /*  
      IN WEb.Config file
      *  <connectionStrings>
                <add name = "SagarContext" 
                connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=SagarApp;Integrated Security=True"
                providerName="System.Data.SqlClient" />
            </connectionStrings>*/
    }
}
