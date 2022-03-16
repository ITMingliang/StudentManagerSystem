using DAL;
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
    public partial class FrmModifyPwd : Form
    {
        public FrmModifyPwd()
        {
            InitializeComponent();
        }

        //修改密码
        private void btnModifyPwd_Click(object sender, EventArgs e)
        {
            //验证老密码的两个方式
            //一、先去数据库去查询一下管理员的密码
            //二、登陆时候就把管理员的密码保存起来，要验证的时候就直接拿出来去验证
            #region 数据验证
            if (this.txtOldPwd.Text.Trim().Length==0)
            {
                MessageBox.Show("请输入原密码！","信息提示");
                return;
            }
            if (this.txtOldPwd.Text.Trim()!=Program.currentAdmin.LoginPwd)
            {
                MessageBox.Show("输入的原密码不正确！", "信息提示");
                return;
            }
            if (this.txtNewPwd.Text.Trim().Length==0)
            {
                MessageBox.Show("请输入新密码！", "信息提示");
                return;
            }
            if (this.txtConfirmPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入确认新密码！", "信息提示");
                return;
            }
            if (this.txtConfirmPwd.Text.Trim()!= this.txtNewPwd.Text.Trim())
            {
                MessageBox.Show("两次输入新密码不相同！", "信息提示");
                return;
            }

            #endregion

            #region 提交修改
            try
            {
                 Program.currentAdmin.LoginPwd=this.txtConfirmPwd.Text.Trim();
               bool result= new AdminService().ModifyPwd(Program.currentAdmin);
                if (result)
                {
                    MessageBox.Show("修改成功！", "信息提示");
                }
                else
                {
                    MessageBox.Show("修改失败！", "信息提示");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("修改失败！"+ex.Message, "信息提示");
            }

            #endregion
        }
    }
}
