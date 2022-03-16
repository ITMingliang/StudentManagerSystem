using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentManager_MVC5.Models
{
    /// <summary>
    /// 数据库初始化策略类
    /// 1--需求我们需要在数据库生成时自动添加一些数据
    /// 2--修改了实体类后链接数据库报错（在默认策略下）
    /// 办法：传递写一个数据初始化类继承自他的各种策略类
    /// DropCreateDatabaseIfModelChanges策略：模型改变时，自动 重新创建一个新的数据库、就可以继承他，在开过程序中非常有用。
    /// CreateDatabaseIfNotExists策略：默认策略，他只是在没有数据库的时候创建一个。
    /// DropCreateDatabaseAlways策略：注意慎重使用、这个策略每次运行都会重新生成数据库。（不小心就是删除 库跑路）
    /// 
    /// </summary>
    public class StudentDbInitializer: DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ApplicationDbContext context)
        {

            //这里面写要添加的数据库
            //1创建对象
            StudentClass studentClass = new StudentClass { ClassName = "Ant基础班" };
            Course course = new Course { CourseName = "ASP.NET Mvc5" };
            Student student = new Student
            {
                  Course= course,
                  StudentClass= studentClass,
                   Address="浙江杭州",
                    Age=20,
                     Gender=Gender.男,
                      Phone= "13246578900",
                       StudentName="Ant编程"
            };
            //2保存到数据库
            context.StudentClasses.Add(studentClass);
            context.Courses.Add(course);
            context.Students.Add(student);
            context.SaveChanges();
            //base.Seed(context);
        }
    }
}