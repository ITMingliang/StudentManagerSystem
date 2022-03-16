using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Models;

namespace StudentManagerSYS
{
    public partial class FrmLogin : Form
    {

        AdminService adminService = new AdminService();
        public FrmLogin()
        {
            InitializeComponent();//初始化界面 
        }

        private void btnLogin_Click(object sender, EventArgs e)//事件
        {

            //要做的事件放在事件里

            #region 数据验证
            if (this.txtLoginName.Text.Trim().Length==0)
            {
                MessageBox.Show("请输入用户名！","提示信息");
                return;
            }
            if (this.txtPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入密码！", "提示信息");
                return;
            }
            #endregion

            //封装管理员对象
            SysAdmin sysAdmin = new SysAdmin()
            {
                 LoginId=Convert.ToInt32(this.txtLoginName.Text.Trim()),
                  LoginPwd= this.txtPwd.Text.Trim()
            };

            //去数据库核对管理员帐号密码（帐号密码核对）
            SysAdmin newAdmin = adminService.GetAdmin(sysAdmin);
            Program.currentAdmin = newAdmin;
            //要不要登陆管理后台
            if (newAdmin.AdminName!=null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("帐号或者密码不正确！请重新输入","信息提示");
            }


        }
    }
}
