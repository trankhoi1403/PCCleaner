using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cleaner.ExtensionMethod;
using DAL;
using System.IO;
using System.Threading;

namespace Cleaner.UserControls
{
    public partial class FileScreen : UserControl
    {
        public ItemInfo itemInfo { get; private set; }
        public FileScreen()
        {
            InitializeComponent();
            ControlConfig();
            EventConfig();
        }

        public FileScreen(ItemInfo itemInfo)
        {
            InitializeComponent();
            ControlConfig();
            EventConfig();
            Set(itemInfo);
        }

        public void Set(ItemInfo itemInfo)
        {
            try
            {
                this.itemInfo = itemInfo;
                if (Helper.TimeSpanTryParseCustom(itemInfo.RunTime, out TimeSpan runTime))
                {
                    timer1.Interval = (int)runTime.TotalMilliseconds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChangeTitleBackColor(Color color)
        {
            lblTitle.BackColor = color;
            BackColor = color;
        }

        public void UpdateTimeLeft()
        {
            if (Helper.DateTimeTryParseExact(itemInfo.NextTime, out DateTime nextTime))
            {
                TimeSpan timeLeft = nextTime - DateTime.Now;
                lblTimeLeft.Text = "- " + timeLeft.ToString(@"dd\.hh\:mm\:ss");
                if (timeLeft < TimeSpan.FromMinutes(1))
                {
                    lblTimeLeft.BackColor = Color.PeachPuff;
                    lblTimeLeft.ForeColor = Color.DarkRed;
                }
                else
                {
                    lblTimeLeft.BackColor = Color.PaleGreen;
                    lblTimeLeft.ForeColor = Color.DarkGreen;
                }
            }
        }

        private void ControlConfig()
        {
            timer1.Tag = this;  // để biết được fileScreen nào giữ timer đó
        }

        private void EventConfig()
        {
            Load += FileScreen_Load;
            btnDel.Click += BtnDel_Click;
            timer1.Tick += Timer1_Tick;
        }

        /// <summary>
        /// Handle event click, xử lý xóa file dựa vào itemInfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //deleteAllFileAndSubFolder();
                //FileScreen_Load(null, null);
                System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("hh:mm:ss")} FileScreen.Timer1_Tick: {itemInfo.Project}");
            }
            catch (Exception ex)
            {
                // ghi log
            }
        }

        public void FileScreen_Load(object sender, EventArgs e)
        {
            try
            {
                // tiêu đề
                lblTitle.Text = itemInfo.Project;
                rtxtRight.Clear();
                rtxtLeft.Clear();

                if (Directory.Exists(itemInfo.Directory))
                {
                    // thông tin thư mục
                    var di = new DirectoryInfo(itemInfo.Directory);
                    var files = di.EnumerateFiles("*", SearchOption.AllDirectories);
                    var directories = di.EnumerateDirectories("*", SearchOption.AllDirectories);

                    double totalSizeByte = files.Sum(fi => fi.Length);
                    var totalSizeMB = totalSizeByte / (1024 * 1024);
                    var totalFile = files.Count();
                    var totalDir = directories.Count();
                    rtxtRight.AppendText(Environment.NewLine);
                    rtxtRight.AppendText($" - Files: {totalFile}", Color.DarkBlue);
                    rtxtRight.AppendText(Environment.NewLine);
                    rtxtRight.AppendText($" - Directories: {totalDir}", Color.DarkOrange);
                    rtxtRight.AppendText(Environment.NewLine);
                    rtxtRight.AppendText($" - Size: {totalSizeMB.ToString("0.00")} MB", Color.DarkRed);

                    // danh sách các file, thư mục trong thư mục
                    var filesName = String.Join(Environment.NewLine, files.Select(f => f.Name));
                    var directoryName = String.Join(Environment.NewLine, directories.Select(d => d.Name));
                    rtxtLeft.AppendText(filesName, Color.DarkBlue);
                    rtxtLeft.AppendText(Environment.NewLine);
                    rtxtLeft.AppendText(directoryName, Color.DarkOrange);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Files load failed!{Environment.NewLine}Error: {ex.Message}", "System Warning Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"Are you sure you want to delete all files and subfolders in this folder?", "System Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;
            try
            {
                deleteAllFileAndSubFolder();
                FileScreen_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"File deletion failed!{Environment.NewLine}Error: {ex.Message}", "System Error Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region function
        private void deleteAllFileAndSubFolder()
        {
            if (Directory.Exists(itemInfo.Directory))
            {
                var di = new DirectoryInfo(itemInfo.Directory);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
        }
        #endregion
    }
}
