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
    public partial class ImportExcel : Form
    {
        DAL.Helper.ImportExcel importExcel = new DAL.Helper.ImportExcel();
        List<Students> stuList = null;
        public ImportExcel()
        {
            InitializeComponent();
        }

        //导入Excel
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog()==DialogResult.OK)
            {
                string path = openFile.FileName;
                this.dgvStudentList.AutoGenerateColumns = false;
                stuList= importExcel.GetStudentByExcel(path);
                this.dgvStudentList.DataSource = stuList;

            }
        }
        //添加到数据库
        private void btnAddToDatabase_Click(object sender, EventArgs e)
        {
            if (stuList.Count==0| stuList==null)
            {
                MessageBox.Show("没有要导入的数据","提示信息");
                return;
            }
            try
            {
                if (importExcel.Import(stuList))
                {
                    MessageBox.Show("导入成功", "提示信息");
                    this.dgvStudentList.DataSource = null;
                    stuList.Clear();
                }
                else
                {
                    MessageBox.Show("导入失败", "提示信息");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("失败原因"+ ex.Message, "提示信息");
            }
        }
    }
}
