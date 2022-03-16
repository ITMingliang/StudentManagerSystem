using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorStudentManager.Model
{
    public class Student
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Display(Name = "年龄")]
        public int Age { get; set; }
        [Display(Name = "电话")]
        public string Phone { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "性别")]
        public Gender Gender { get; set; }

        public int? StudentClassId { get; set; }

        [ForeignKey("StudentClassId")]
        public virtual StudentClass StudentClass { get; set; }
    }
}
