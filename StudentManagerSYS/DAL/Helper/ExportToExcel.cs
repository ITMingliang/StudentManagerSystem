using Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL.Helper
{
    /// <summary>
    /// 导出
    /// </summary>
   public class ExportToExcel
    {
        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <param name="dgv">Dgv控件</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public bool ExportDataToExcel(DataGridView dgv,string filePath)
        {
            Excel.Application excel = new Excel.Application();//创建excel对象   
            if (excel==null)
            {
                MessageBox.Show("创建Excel失败，可能你的电脑没有安装Excel");
                return false;
            }
            Workbook workbook = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);//创建工作薄
            Worksheet worksheet = workbook.Worksheets[1];//创建工作表
            int colIndex = 0;
            //导出标题列
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                colIndex++;
                worksheet.Cells[1, colIndex] = dgv.Columns[i].HeaderText;
            }
            //导出内容
            for (int r = 0; r < dgv.Rows.Count; r++)
            {
                colIndex = 0;
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    colIndex++;
                    worksheet.Cells[r+2, colIndex] = dgv.Rows[i].Cells[i].Value;
                }
            }

            workbook.SaveAs(filePath);
            excel.Quit();
            return true;
        } 


    }
}
