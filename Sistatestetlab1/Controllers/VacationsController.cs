using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistatestetlab1.Data;
using Sistatestetlab1.Models;

namespace Sistatestetlab1.Controllers
{
    public class VacationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VacationsController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Index(string SearchString)
        {
            ViewData["CurrentFilter"] = SearchString;
            var vacation = from v in _context.Vacation
                           select v;
            if (!String.IsNullOrEmpty(SearchString))
            {
                vacation = vacation.Where(v => v.Name.Contains(SearchString) ||
                                               v.StartDate.ToString().Contains(SearchString) ||
                                               v.EndDate.ToString().Contains(SearchString));
            }

            return View(vacation);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Vacation == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacation == null)
            {
                return NotFound();
            }

            return View(vacation);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Reason,StartDate,EndDate, CreatedTime")] Vacation vacation)
        {
            if (ModelState.IsValid)
            {
                vacation.Id = Guid.NewGuid();
                _context.Add(vacation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacation);
        }


        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Vacation == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacation.FindAsync(id);
            if (vacation == null)
            {
                return NotFound();
            }
            return View(vacation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Reason,StartDate,EndDate,CreatedTime")] Vacation vacation)
        {
            if (id != vacation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationExists(vacation.Id))
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
            return View(vacation);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Vacation == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacation == null)
            {
                return NotFound();
            }

            return View(vacation);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Vacation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vacation'  is null.");
            }
            var vacation = await _context.Vacation.FindAsync(id);
            if (vacation != null)
            {
                _context.Vacation.Remove(vacation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationExists(Guid id)
        {
          return (_context.Vacation?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
