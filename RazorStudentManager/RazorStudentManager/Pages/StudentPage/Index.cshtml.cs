using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorStudentManager.AppData;
using RazorStudentManager.Model;

namespace RazorStudentManager.Pages.StudentPage
{
    public class IndexModel : PageModel
    {

        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public SelectList ClassList { get; set; }

        public IEnumerable<Student> Students { get; set; }

        //根据姓名搜索
        [BindProperty(SupportsGet =true)]
        public string Search { get; set; }

        //根据班级搜索
        [BindProperty(SupportsGet = true)]
        public int? ClassId { get; set; }

        public async Task OnGet()
        {

            //加载班级列表
            var items = await _db.StudentClasses.ToListAsync();
            ClassList = new SelectList(items, "Id", "ClassName");



            var query = _db.Students
                .Include(c=>c.StudentClass)
                .AsNoTracking();//AsNoTracking方法不跟踪

            if (!string.IsNullOrEmpty(Search))
            {
                query = query.Where(s => s.Name.Contains(Search));
            }

            if (ClassId!=null)
            {
                query = query.Where(s => s.StudentClassId == ClassId);
            }

            Students =await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {

            var student = await _db.Students.FindAsync(id);
            if (student==null)
            {
                return NotFound();
            }
            _db.Students.Remove(student);//把student状态设置删除
            await _db.SaveChangesAsync();

            return RedirectToPage();
        }

    }
}
