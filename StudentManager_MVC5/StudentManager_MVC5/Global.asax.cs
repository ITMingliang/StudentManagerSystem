using StudentManager_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StudentManager_MVC5
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 应用程序初始化的地方
        /// </summary>
        protected void Application_Start()
        {
            Database.SetInitializer(new StudentDbInitializer());//初始化数据库上下文
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
