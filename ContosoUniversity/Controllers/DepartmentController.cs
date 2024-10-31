using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ContosoUniversity.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SchoolContext _context;

        public DepartmentsController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index() 
        {
            var schoolContext = _context.Departments.Include(d => d.Administrator);
            return View(await schoolContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Budget, StartDate, RowVersion, InstructorID, StudentHeight")]Department Department)
        {
           
            if (ModelState.IsValid)
            {
                _context.Add(Department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", Department.InstructorID);
            
            return View(Department);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (Department == null)
            {
                return NotFound();
            }

            return View(Department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Department = await _context.Departments.FindAsync(id);

            _context.Departments.Remove(Department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departmentToEdit = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentToEdit == null)
            {
                return NotFound();
            }
            return View(departmentToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind] Department modifiedDepartment)
        {
            if (ModelState.IsValid)
            {
                if (modifiedDepartment.DepartmentID == null)
                {
                    return BadRequest();
                }
                _context.Departments.Update(modifiedDepartment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(modifiedDepartment);
        }
        [HttpGet]
        public async Task<IActionResult> BaseOn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.Administrator)
                .FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (department == null)
            {
                return NotFound();
            }

            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BaseOn(int id,
            [Bind("DepartmentID, Name, Budget, StartDate, RowVersion, InstructorID, DepartmentOwner")] Department department,
            string actionType)
        {
            if (ModelState.IsValid)
            {
                var existingDepartment = await _context.Departments
                    .FirstOrDefaultAsync(m => m.DepartmentID == id);

                if (existingDepartment == null)
                {
                    return NotFound();
                }

                if (actionType == "Make")
                {
                    _context.Add(existingDepartment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else if (actionType == "Make & delete")
                {
                    _context.Departments.Remove(existingDepartment);
                    _context.Add(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }


    }
}
