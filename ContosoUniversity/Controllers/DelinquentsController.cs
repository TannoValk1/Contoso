using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ContosoUniversity.Controllers
{
    public class DelinquentsController : Controller
    {
        private readonly SchoolContext _context;
        public DelinquentsController(SchoolContext context) 
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            return View(await _context.Delinquents.ToListAsync());
        }

        

    }
}
