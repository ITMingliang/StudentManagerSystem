using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagerSYS
{

    //架构搭建：管理我们的文件和组织我们的代码（我个人的理解），二层（UI层--数据访问层）
    //这个系统做的二层架构（三层架构）
    //winform核心内容就是事件（触发器）+控件
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmStudentMananger());//启动窗口


            //操作登陆窗口
            FrmLogin frmLogin = new FrmLogin();//实例化窗口
            DialogResult dialogResult = frmLogin.ShowDialog();


            //操作主窗口
            if (dialogResult == DialogResult.OK)
            {
                Application.Run(new Main());//启动窗口
            }
            else
            {
                Application.Exit();
            }

        }

        public static SysAdmin currentAdmin = null;
    }


}
