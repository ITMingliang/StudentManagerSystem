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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加学生事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddStu_Click(object sender, EventArgs e)
        {
            FrmAddStudent frmAddStudent = new FrmAddStudent();
            OpenForm(frmAddStudent);
            //frmAddStudent.Show();
        }


        //变化的设置为参数，不变设置方法体
        private void OpenForm(Form form)
        {
            this.splitContainer1.Panel2.Controls.Clear();//清除容器里的控件
            form.TopLevel = false;//将窗体设置为非顶级控件
            form.FormBorderStyle = FormBorderStyle.None;//去掉窗体 的边框
            form.Parent = this.splitContainer1.Panel2;//设置窗体的父容器
            form.Dock = DockStyle.Fill; //随着窗口大小自动调整窗体大小
            form.Show();//显示窗体 
        }

        //学生管理
        private void btnStuManage_Click(object sender, EventArgs e)
        {
            FrmStudentMananger frmStudent = new FrmStudentMananger();
            OpenForm(frmStudent);
        }
        //成绩管理
        private void btnScoreManager_Click(object sender, EventArgs e)
        {
            ScoreManager scoreManager = new ScoreManager();
            OpenForm(scoreManager);
        }
        //学生信息导入
        private void button1_Click(object sender, EventArgs e)
        {
            ImportExcel importExcel = new ImportExcel();
            OpenForm(importExcel);
        }

        //官网
        private void btnOfficial_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            OpenForm(frmAbout);
        }

        //密码修改
        private void btnModifyPwd_Click(object sender, EventArgs e)
        {
            FrmModifyPwd frmModifyPwd = new FrmModifyPwd();
            OpenForm(frmModifyPwd);
        }

        //切帐号
        private void btnAccoutSwithch_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            DialogResult result = frmLogin.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.tslCurrentUser.Text = Program.currentAdmin.AdminName;
            }
        }
        //退出系统
        private void btnExitSYS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
          DialogResult result=  MessageBox.Show("确定退出程序吗","提示信息",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result!=DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 添加学生AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddStu_Click(null, null);
        }
    }
}
