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
    public partial class ScoreManager : Form
    {
        
        ScoreService scoreService = new ScoreService();
        ClassService classService = new ClassService();
        public ScoreManager()
        {
            InitializeComponent();
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = scoreService.GetScoreLists(0,0,"");

            //加载主管级列表
            this.cmbClass.DataSource = classService.GetAllClass();
            this.cmbClass.DisplayMember = "ClassName";
            this.cmbClass.ValueMember = "ClassId";
            this.cmbClass.SelectedIndex = -1;
            //初始化获取考试统计信息
            Dictionary<string,string> Dic= scoreService.GetScoreInfo();
            this.txtAttendCount.Text = Dic["stuCount"];
            this.txtAbsentCount.Text = Dic["absentCount"];
            this.txtHtmlGPA.Text = Dic["avgHTML"];
            this.txtSQLGPA.Text = Dic["avgSQL"];
            //初始化缺考人员名单
             this.listBoxAbsent.Items.AddRange(scoreService.GetAbsentList().ToArray())  ;

        }

        //按条件查询
        private void btnQuery_Click(object sender, EventArgs e)
        {

            int studentId = this.txtStuId.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(this.txtStuId.Text.Trim());
            int classtId = Convert.ToInt32(this.cmbClass.SelectedValue);
            string stuName = this.txtStuName.Text.Trim();
            this.dgvScoreList.DataSource = scoreService.GetScoreLists(studentId, classtId, stuName);
        }
    }
}
