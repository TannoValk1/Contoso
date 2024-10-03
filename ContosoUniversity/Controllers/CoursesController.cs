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
        public async Task<IActionResult> DetailsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Clone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var clonedCourse = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id);
            if (clonedCourse == null)
            {
                return NotFound();
            }
            int lastID = _context.Courses.OrderBy(u => u.CourseID).Last().CourseID;
            lastID++;
            var selectedCourse = new Course();
            selectedCourse.CourseID = clonedCourse.CourseID;
            selectedCourse.Title = clonedCourse.Title;
            selectedCourse.Credits = clonedCourse.Credits;
            selectedCourse.Enrollments = clonedCourse.Enrollments;
            _context.Courses.Add(selectedCourse);
            await _context.SaveChangesAsync(true);
            return RedirectToAction("Index");
        }
    }
}