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
            if (id == null) return NotFound();
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            if (course == null) return NotFound();
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
            Course course = id == null ? new Course() : await _context.Courses.FindAsync(id);
            if (id != null && course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(Course course)
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

        [HttpGet]
        public async Task<IActionResult> Clone(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            var newCourse = new Course
            {
                Title = course.Title,
                Credits = course.Credits
            };

            return View("CreateEdit", newCourse);
        }

    }
}
