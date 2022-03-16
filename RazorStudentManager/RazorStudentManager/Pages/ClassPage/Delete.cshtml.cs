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
    public class DeleteModel : PageModel
    {
        private readonly RazorStudentManager.AppData.AppDbContext _context;

        public DeleteModel(RazorStudentManager.AppData.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentClass = await _context.StudentClasses.FindAsync(id);

            if (StudentClass != null)
            {
                _context.StudentClasses.Remove(StudentClass);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
