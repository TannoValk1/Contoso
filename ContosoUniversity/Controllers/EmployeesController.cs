using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly SchoolContext _context;

        public EmployeesController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["JobName"] = new SelectList(_context.Employees, "JobName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstMidName, LastName, JobName, EmploymentStart")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeID == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employeeToEdit = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeToEdit == null)
            {
                return NotFound();
            }
            return View(employeeToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("EmployeeID,LastName,FirstMidName,JobName, EmploymentStart")] Employee modifiedEmployee)
        {
            if (ModelState.IsValid)
            {
                if (modifiedEmployee.EmployeeID == null)
                {
                    return BadRequest();
                }
                _context.Employees.Update(modifiedEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(modifiedEmployee);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeID == id);

            if (employee == null) 
            {
                return NotFound();
            }

            return View(employee);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
