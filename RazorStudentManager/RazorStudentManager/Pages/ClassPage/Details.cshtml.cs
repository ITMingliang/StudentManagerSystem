using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorStudentManager.AppData;
using RazorStudentManager.Model;

namespace RazorStudentManager.Pages.ClassPage
{
    public class DetailsModel : PageModel
    {
        private readonly RazorStudentManager.AppData.AppDbContext _context;

        public DetailsModel(RazorStudentManager.AppData.AppDbContext context)
        {
            _context = context;
        }

        public StudentClass StudentClass { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentClass = await _context.StudentClasses.FirstOrDefaultAsync(m => m.Id == id);

            if (StudentClass == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
