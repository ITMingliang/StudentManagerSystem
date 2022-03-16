using DAL;
using DAL.Helper;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagerSYS
{
    public partial class FrmStudentMananger : Form
    {
        StudentService studentService = new StudentService();
        ClassService classService = new ClassService();
        public FrmStudentMananger()
        {
            InitializeComponent();
            //窗体的初始化的内存可以放在构造方法
            this.dgvStudentList.AutoGenerateColumns = false;//禁止自动加载列
            this.dgvStudentList.DataSource = studentService.GetAllStudent();//填充表格数据

            //加载班级列表
            this.cmbClass.DataSource = classService.GetAllClass();//填充数据
            this.cmbClass.DisplayMember = "ClassName";
            this.cmbClass.ValueMember = "ClassId";
            this.cmbClass.SelectedIndex = -1;
        }
        //设置一个窗口全局变量存储学生ID
        string studentId = null;

        private void dgvStudentList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //在这个事件里就可以写自己想做的事件
            if (e.Button==MouseButtons.Right)
            {
                this.dgvStudentList.ClearSelection();//取消之前选择的单元格
                this.dgvStudentList.Rows[e.RowIndex].Selected = true;//选中单击单元格所在的行
                this.contextMenuStrip.Show(MousePosition.X, MousePosition.Y);
                studentId=this.dgvStudentList.Rows[e.RowIndex].Cells[0].Value.ToString();//存储学生ID备用
            }
           
        }

        //显示详细信息
        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //通过构造 方法跨窗口传递数据
            StudentDetail studentDetail = new StudentDetail(studentId);
            studentDetail.Show();
        }
        /// <summary>
        /// 根据条件查询学生信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //10月15号作业2---增加一个条件 让我们一个条件 都没有输入时，查询所有信息
            if (this.txtStudentId.Text.Trim().Length>0)
            {

                List<Students> list = new List<Students>();
                list.Add(studentService.GetStudentListById(this.txtStudentId.Text.Trim()));
                this.dgvStudentList.DataSource = list;

            }
            else if (this.txtStudentName.Text.Trim().Length>0)
            {
                this.dgvStudentList.DataSource = studentService.GetStudentListByName(this.txtStudentName.Text.Trim());
             }
            else if (this.cmbClass.SelectedIndex !=-1)
            {
                this.dgvStudentList.DataSource = studentService.GetStudentListByClassId(this.cmbClass.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("请输入查询的条件！","信息提示");
            }
        }


        //修改学生信息
        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModifyStudent modifyStudent = new ModifyStudent(studentId);
            modifyStudent.Show();
        }

        //删除学生信息
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          bool result=  studentService.DeleteStudentInfo(studentId);
            if (result)
            {
                MessageBox.Show("删除成功！","提示信息");
            }
            else
            {
                MessageBox.Show("删除失败！", "提示信息");
            }
        }

        //导出Excel
        private void btnImport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter="Excel文件|*.xlsx"};
            saveFileDialog.ShowDialog();
            ExportToExcel exportToExcel = new ExportToExcel();
           bool result=  exportToExcel.ExportDataToExcel(this.dgvStudentList, saveFileDialog.FileName);
            if (result)
            {
                MessageBox.Show("导出成功！","提示信息");
            }
            else
            {
                MessageBox.Show("导出失败！", "提示信息");
            }
        }
    }
}
