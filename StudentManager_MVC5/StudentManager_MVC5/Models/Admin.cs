using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManager_MVC5.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
    }
}