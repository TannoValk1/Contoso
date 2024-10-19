using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;
        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> DetailsDelete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsDeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CreateEdit(int? id)
        {
            if (id == null)
            {
                ViewBag.Title = "Create";
                ViewBag.Description = "Tee uus Course";
                return View(new Course());
            }
            var course = await _context.CourseAssignments.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit";
            ViewBag.Description = "Edit Course";
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit([Bind("CourseID,Title,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                if (course.CourseID == 0)
                {
                    _context.Add(course);
                }
                else
                {
                    _context.Update(course);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);

        }
    }
}
