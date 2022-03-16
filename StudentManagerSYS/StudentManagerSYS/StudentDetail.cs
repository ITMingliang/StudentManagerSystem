using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagerSYS
{
    public partial class StudentDetail : Form
    {
        StudentService studentService = new StudentService();
        public StudentDetail(string studentId)
        {

            InitializeComponent();
            //关闭控件可操作功能
            this.txtStuName.Enabled = false;
            this.txtIdentity.Enabled = false;
            this.txtClass.Enabled = false;
            this.txtAttendance.Enabled = false;
            this.txtAdress.Enabled = false;
            this.txtPhone.Enabled = false;
            this.txtAge.Enabled = false;
            this.dtpBirthday.Enabled = false;
            this.rdoMale.Enabled = false;
            this.rdoFemale.Enabled = false;
            //初始化学生详细信息
            Students students = studentService.GetStudentDetail(studentId);
            this.txtStuName.Text = students.StudentName;
            this.txtIdentity.Text = students.IdentityNO;
            this.txtClass.Text = students.ClassName;
            this.txtAttendance.Text = students.AttendanceNO;
            this.txtAdress.Text = students.StudentAddress;
            this.txtPhone.Text = students.Phone;
            this.txtAge.Text = students.Age.ToString();
            this.dtpBirthday.Value = students.Birthday;
            this.picPhoto.ImageLocation = students.StuImage == "" ? students.StuImage : Directory.GetCurrentDirectory() + "\\Image\\" + students.StuImage;
            if (students.Gender == "男")
                this.rdoMale.Checked = true;
            else 
                this.rdoFemale.Checked = true;
            
        }
    }
}
