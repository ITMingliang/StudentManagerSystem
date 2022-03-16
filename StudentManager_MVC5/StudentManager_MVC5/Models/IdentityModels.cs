using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentManager_MVC5.Models
{

    #region ApplicationUser

    // 可以通过将更多属性添加到 ApplicationUser 类来为用户添加配置文件数据，请访问 https://go.microsoft.com/fwlink/?LinkID=317594 了解详细信息。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    #endregion

    /// <summary>
    /// 数据库上下文类(作用是：操作EF)
    /// 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// 数据库上下文的构造函数
        /// 参数1:数据库链接，参数二:这个可以忽略，他其实是一个底版本Identity1.0.0校验，抛出异常
        /// 
        /// 什么是Identity--权限管理插件
        /// </summary>
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        /// <summary>
        /// 创建上下文类的实例
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
            
        }

        /// <summary>
        /// 学生表
        /// </summary>
        public DbSet<Student> Students { get; set; }
        /// <summary>
        /// 课程表
        /// </summary>
        public DbSet<Course> Courses { get; set; }
        /// <summary>
        /// 班级表
        /// </summary>
        public DbSet<StudentClass> StudentClasses { get; set; }
        /// <summary>
        /// 管理员类
        /// </summary>
        public DbSet<Admin>  Admins { get; set; }
    }
   
}