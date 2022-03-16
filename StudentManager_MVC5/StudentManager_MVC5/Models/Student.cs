using StudentManager_MVC5.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManager_MVC5.Models
{
    public class Student
    {
        [Display(Name = "学生ID")]
        public int StudentId { get; set; }
        [Display(Name = "课程ID")]
        public int CourseId { get; set; }//课程表的外键
        [Display(Name = "班级ID")]
        public int StudentClassId { get; set; }//班级表的外键
        [Display(Name = "姓名")]
        [Required]//不为空
        public string StudentName { get; set; }
        [Display(Name = "年龄")]
        public int Age { get; set; }
        [Display(Name = "性别")]
        public Gender Gender { get; set; }
        [Display(Name = "电话")]
        [Required]
        public string Phone { get; set; }
        [Display(Name = "地址")]
        [Required]
        [MyMaxStringLength(5)]
        public string Address { get; set; }

        //1、EF的一个约定。2、这样可以通过.号来进行其他表属性的访问
        public virtual Course Course { get; set; }//课程导航属性
        public virtual StudentClass StudentClass { get; set; }//班级导航属性
    }

  
}