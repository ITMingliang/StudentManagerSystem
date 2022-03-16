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
    public class EditModel : PageModel
    {
        private AppDbContext _db;

        public EditModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Student Student{ get; set; }


        public SelectList ClassList { get; set; }

        public async Task OnGet(int id)
        {
            //加载班级列表
            var items = await _db.StudentClasses.ToListAsync();
            ClassList = new SelectList(items, "Id", "ClassName");
            //加载学生信息
            Student = await _db.Students.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var studentObj = await _db.Students.FindAsync(Student.Id);
                studentObj.Name = Student.Name;
                studentObj.Age = Student.Age;
                studentObj.Phone = Student.Phone;
                studentObj.Address = Student.Address;
                studentObj.Gender = Student.Gender;
                studentObj.StudentClassId = Student.StudentClassId;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}
