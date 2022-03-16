using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorStudentManager.Model
{
    public class StudentClass
    {
      
        public int Id { get; set; }
        [Required]
        [Display(Name ="班级名称")]
        public string ClassName { get; set; }

    }
}
