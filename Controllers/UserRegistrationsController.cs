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
    public class UserRegistrationsController : Controller
    {
        private readonly AppDbContext _context;

        public UserRegistrationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserRegistrations
        public async Task<IActionResult> Index(string search)
        {
            var data = await _context.userRegistrations.ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                data= await _context.userRegistrations.Where(s=> s.Name.Contains(search) || s.NameId.Contains(search)).ToListAsync();
            }
            return View(data);
        }

        // GET: UserRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRegistration = await _context.userRegistrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRegistration == null)
            {
                return NotFound();
            }

            return View(userRegistration);
        }

        
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NameId,DateOfBirth,BloodGroup,Email,Phone")] UserRegistration userRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userRegistration);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRegistration = await _context.userRegistrations.FindAsync(id);
            if (userRegistration == null)
            {
                return NotFound();
            }
            return View(userRegistration);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NameId,DateOfBirth,BloodGroup,Email,Phone")] UserRegistration userRegistration)
        {
            if (id != userRegistration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRegistrationExists(userRegistration.Id))
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
            return View(userRegistration);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRegistration = await _context.userRegistrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRegistration == null)
            {
                return NotFound();
            }

            return View(userRegistration);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRegistration = await _context.userRegistrations.FindAsync(id);
            if (userRegistration != null)
            {
                _context.userRegistrations.Remove(userRegistration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRegistrationExists(int id)
        {
            return _context.userRegistrations.Any(e => e.Id == id);
        }
    }
}
