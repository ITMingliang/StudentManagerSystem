using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentManager_MVC5.Models;

namespace StudentManager_MVC5.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();//实例化数据库上下文
        // GET: Students
        public ActionResult Index()
        {
            //IQueryable是有延迟查询的功能
            var students = db.Students.Include(s => s.Course).Include(s => s.StudentClass);
            return View(students.ToList());//ToList后会把数据查询到内存
        }


        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()//两个方法的作用：第一个方法打开页面，第二个方法是具体操作
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewBag.StudentClassId = new SelectList(db.StudentClasses, "StudentClassId", "ClassName");
            return View();
        }

        // POST: Students/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,CourseId,StudentClassId,StudentName,Age,Gender,Phone,Address")] Student student)
        {
            
            if (ModelState.IsValid)//验证model错误
            {
                db.Students.Add(student);//把对像添加到上下文
                db.SaveChanges();//保存到数据库
                return RedirectToAction("Index");//跳转到管理页
                
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", student.CourseId);
            ViewBag.StudentClassId = new SelectList(db.StudentClasses, "StudentClassId", "ClassName", student.StudentClassId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", student.CourseId);
            ViewBag.StudentClassId = new SelectList(db.StudentClasses, "StudentClassId", "ClassName", student.StudentClassId);
            return View(student);
        }

        // POST: Students/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,CourseId,StudentClassId,StudentName,Age,Gender,Phone,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;//修改
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", student.CourseId);
            ViewBag.StudentClassId = new SelectList(db.StudentClasses, "StudentClassId", "ClassName", student.StudentClassId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
