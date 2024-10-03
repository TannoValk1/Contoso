using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace ContosoUniversity.Controllers
{
    public class DelinquentsController : Controller
    {
        private readonly SchoolContext _context;
        public DelinquentsController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Delinquents.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, LastName, FirstMidName, Violation ")] Delinquent delinguest)
        {
            if (ModelState.IsValid)
            {
                _context.Delinquents.Add(delinguest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delinguest);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var delinquent = await _context.Delinquents
               .FirstOrDefaultAsync(m => m.ID == id);

            if (delinquent == null)
            {
                return NotFound();
            }
            return View(delinquent);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var DelinquentToEdit = await _context.Delinquents
                .FirstOrDefaultAsync(m => m.ID == id);
            if (DelinquentToEdit == null)
            {
                return NotFound();
            }
            return View(DelinquentToEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit([Bind("ID,LastName,FirstMidName,Violations")] Delinquent modifiedDelinquent)
        {
            if (ModelState.IsValid)
            {
                if (modifiedDelinquent.ID == null)
                {
                    return BadRequest();
                }
                _context.Delinquents.Update(modifiedDelinquent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(modifiedDelinquent);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var delinquent = await _context.Delinquents
                .FirstOrDefaultAsync(m => m.ID == id);

            if (delinquent == null) 
            {
                return NotFound();
            }

            return View(delinquent);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delinquent = await _context.Delinquents.FindAsync(id); 

            _context.Delinquents.Remove(delinquent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Clone()
        {
            return View();
        }
        [HttpPost, ActionName("Clone")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Clone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clonedDelinquent = await _context.Delinquents
                .FirstOrDefaultAsync(m => m.ID == id);

            if (clonedDelinquent == null)
            {
                return NotFound();
            }

            var selectedDelinquent = new Delinquent
            {
                FirstMidName = clonedDelinquent.FirstMidName,
                LastName = clonedDelinquent.LastName,
                Violation = clonedDelinquent.Violation
            };

            _context.Delinquents.Add(selectedDelinquent);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}


