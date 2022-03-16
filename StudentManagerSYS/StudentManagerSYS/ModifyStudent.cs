using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagerSYS
{
    public partial class ModifyStudent : Form
    {
        StudentService studentService = new StudentService();
        ClassService classService = new ClassService();
        //保存学生ID
        string stuId = null;
        public ModifyStudent(string studentId)
        {
            stuId = studentId;
            InitializeComponent();
            //班级下接列表初始化
            this.cmbClass.DataSource= classService.GetAllClass();
            this.cmbClass.DisplayMember = "ClassName";
            this.cmbClass.ValueMember = "ClassId";
            //初始化学生信息
            Students students=studentService.GetStudentDetail(studentId);
            this.txtStuName.Text = students.StudentName;
            this.txtAdress.Text = students.StudentAddress;
            this.txtIdentity.Text = students.IdentityNO;
            this.txtPhone.Text = students.Phone;
            this.picPhoto.Text = students.StuImage;
            this.cmbClass.SelectedValue = students.ClassId;
            this.dtpBirthday.Value = students.Birthday;
            this.txtAttendance.Text = students.AttendanceNO;
            this.txtAge.Text = students.Age.ToString();
            if (students.Gender == "男")
                this.rdoMale.Checked = true;
            else
                this.rdoFemale.Checked = true;

        }
        

        //保存要修改数据到数据
        private void btnModifyStu_Click(object sender, EventArgs e)
        {

            //数据验证（作业1）


            //封装对象
            Students students = new Students()
            {
                StudentId = Convert.ToInt32(stuId),
                StudentName = this.txtStuName.Text.Trim(),
                Gender = this.rdoMale.Checked == true ? "男" : "女",
                Phone = this.txtPhone.Text.Trim(),
                Age = Convert.ToInt32(this.txtAge.Text.Trim()),
                AttendanceNO = this.txtAttendance.Text.Trim(),
                Birthday = this.dtpBirthday.Value,
                StudentAddress = this.txtAdress.Text.Trim(),
                StuImage = this.picPhoto.ImageLocation,
                ClassId = Convert.ToInt32(this.cmbClass.SelectedValue),
                IdentityNO=this.txtIdentity.Text.Trim()
            };

            bool result= studentService.UpdateStudentInfo(students);
            if (result)
            {
                MessageBox.Show("修改成功！","信息提示");
            }
            else
            {
                MessageBox.Show("修改失败！", "信息提示");
            }
        }
    }
}
