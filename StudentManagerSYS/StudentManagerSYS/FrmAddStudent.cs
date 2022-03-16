using AForge.Video.DirectShow;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagerSYS
{
    /// <summary>
    /// UI界面设计---》实体类设计 ---》数据库表的设计---》数据库服务类----》UI界面后台代码
    /// 
    /// 按正常的流程：数据库表的设计---》实体类设计 ---》数据库服务类----》UI界面后台代码 >>>UI界面设计
    /// </summary>
    public partial class FrmAddStudent : Form
    {
        FilterInfoCollection videoDevices;//获取设备信息对象
        VideoCaptureDevice videoSource;//视频拍摄设置

        StudentService studentService = new StudentService();
        ClassService classService = new ClassService();
        public FrmAddStudent()
        {
            InitializeComponent();
            //在这里加载我们班级列表（一个界面一般初始化数据都放在构造方法里面）
            this.cmbClass.DataSource=classService.GetAllClass();
            this.cmbClass.DisplayMember = "ClassName";
            this.cmbClass.ValueMember = "ClassId";
        }
        #region 学生添加

        private void btnAddStu_Click(object sender, EventArgs e)
        {
            #region 数据验证

            //数据验证
            if (this.txtStuName.Text.Trim().Length==0)
            {
                MessageBox.Show("请输入姓名","信息提示");
                return;
            }

            //正则表达式
             //bool isMathc= Regex.IsMatch(this.txtAge.Text.Trim(), "^[0-9]*$");

            if (this.txtAge.Text.Trim().Length == 0 &&
                (Regex.IsMatch(this.txtAge.Text.Trim(), "^[0-9]*$")!=false))
            {

                MessageBox.Show("请输入姓名", "信息提示");
                return;
            }
            //作业一、修改年龄验证
            //作业二、完善数据验证

            #endregion

            //对象封装（ ? :）
            Students students = new Students()
            {

                StudentName = this.txtStuName.Text.Trim(),
                Gender = this.rdoMale.Checked == true ? "男" : "女",
                Phone = this.txtPhone.Text.Trim(),
                Age = Convert.ToInt32(this.txtAge.Text.Trim()),
                AttendanceNO = this.txtAttendance.Text.Trim(),
                Birthday = this.dtpBirthday.Value,
                StudentAddress = this.txtAdress.Text.Trim(),
                StuImage = this.picPhoto.ImageLocation==null?"": GetFileName(this.picPhoto.ImageLocation),
                 ClassId=Convert.ToInt32(this.cmbClass.SelectedValue),
                 IdentityNO=this.txtIdentity.Text.Trim()
            };

            //添加数据库
            //int num = studentService.AddStudnet(students);
            bool result = studentService.AddStudnet(students);
            if (result)
            {
                //Console.WriteLine("添加成功");//在控制台应用程序里面使用的
                MessageBox.Show("添加成功","信息提示");//WinFrom里使用的显示对话框
            }
            else
            {
                MessageBox.Show("添加失败", "信息提示");
            }
        }

        #endregion

        #region 摄像操作

        //启动摄像头
        private void btnStartVideo_Click(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);//获取所有设备
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);//获取指定设备
            this.videoSourcePlayer.VideoSource = videoSource;//将摄像头加载到控件中
            this.videoSourcePlayer.Start();//启动摄像头
        }
        //关闭摄像头
        private void btnCloseVideo_Click(object sender, EventArgs e)
        {
            this.videoSourcePlayer.SignalToStop();
        }
        //拍照
        private void btnTakePhotos_Click(object sender, EventArgs e)
        {
            //【1】检测设备有没有打开
            if (!this.videoSourcePlayer.IsRunning)
            {
                MessageBox.Show("请先打开摄像头！","信息提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            //【2】检测文件夹有没有创建
            if (!Directory.Exists("Image"))
            {
                Directory.CreateDirectory("Image");
            }
            //【3】定义我们的文件名
            string fileName = Directory.GetCurrentDirectory() + "\\Image\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";

            //【4】获取我们的照片
            Bitmap bitmap = this.videoSourcePlayer.GetCurrentVideoFrame();

            //【5】保存照片
            bitmap.Save(fileName);//保存到文件
            this.picPhoto.ImageLocation = fileName;//保存到控件
        }
        //清除照片
        private void btnClearPhoto_Click(object sender, EventArgs e)
        {
            this.picPhoto.Image = null;
        }
        //选择照片
        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            //【1】检测文件夹有没有创建
            if (!Directory.Exists("Image"))
            {
                Directory.CreateDirectory("Image");
            }
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog()==DialogResult.OK)
            {
                string oldName = openFile.FileName;
                string newName= Directory.GetCurrentDirectory() + "\\Image\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                //把文件复制到程序目录下面
                FileInfo fileInfo = new FileInfo(oldName);
                fileInfo.CopyTo(newName);//复制到新文件夹
                //保存文件到显示控件
                this.picPhoto.ImageLocation = newName;
            }
        }

        #endregion

        #region 其他辅助方法
        /// <summary>
        /// 获取路径的文件名
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetFileName(string filePath)
        {
            string[] stirngArray = filePath.Split('\\');
           return stirngArray[stirngArray.Length - 1];

        }


        #endregion
    }
}
