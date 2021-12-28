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

namespace Cleaner.UserControls
{
    public partial class FileScreen : UserControl
    {
        public delegate void ProjectCleanHandler(FileScreen sender);
        public event ProjectCleanHandler ProjectClean;

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
            Set(itemInfo);
            EventConfig();
        }

        public void Set(ItemInfo itemInfo)
        {
            this.itemInfo = itemInfo;
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
            timer1.Interval = 1000; // quét sau 1s
        }

        private void EventConfig()
        {
            Load += FileScreen_Load;
            btnDel.Click += BtnDel_Click;
            timer1.Tick += Timer1_Tick;

            timer1.Start();
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
                if (itemInfo.Status != "Running")
                    return;

                if (Helper.DateTimeTryParseExact(itemInfo.NextTime, out DateTime itemInfoNextTime) == false)
                    return;

                if (Helper.DateTimeTryParseExact(itemInfo.StartTime, out DateTime itemInfoStartTime) == false)
                    return;

                // canh thời điểm xóa
                if (DateTime.Now.ToString(ItemInfo.DateTimeFormat).Equals(itemInfo.NextTime))   // đúng thời điểm NextTime thì sẽ start
                {
                    //tính toán lại thời điểm xóa file tiếp theo 
                    if (CalculateNextTime(itemInfo.StartTime, itemInfo.RunTime, out DateTime newItemInfoNextTime) == false)
                        return;
                    itemInfo.NextTime = newItemInfoNextTime.ToString(ItemInfo.DateTimeFormat);
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("hh:mm:ss")} Da toi thoi diem xoa file: {itemInfo.Project}");

                    ProjectClean?.Invoke(this);
                }

                // cập nhật thời gian còn lại đến lúc bị xóa
                UpdateTimeLeft();


                // canh thòi điểm load lại
                if (itemInfoStartTime.Second.Equals(DateTime.Now.Second))
                {
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("hh:mm:ss")} Reload FileScreen: {itemInfo.Project}");
                }

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

        /// <summary>
        /// Tính toán thời điểm nextTime sẽ xảy ra sự kiện xóa tiếp theo
        /// </summary>
        /// <param name="_startTime"></param>
        /// <param name="_runTime"></param>
        /// <param name="_nextTime"></param>
        /// <returns></returns>
        private bool CalculateNextTime(string _startTime, string _runTime, out DateTime _nextTime)
        {
            if (Helper.DateTimeTryParseExact(_startTime, out DateTime startTime) == false
                       || Helper.TimeSpanTryParseCustom(_runTime, out TimeSpan runTime) == false)
            {
                throw new FormatException($@"The correct structure should be '\d+[d,h,m]'{Environment.NewLine}Example: 24d, 7h, 4233m,...");
            }
            if (runTime > ItemInfo.MaximumRunTime)
            {
                throw new ArgumentOutOfRangeException("_runTime", $"Maximum RunTime circle is '24d'. Let's try again!");
            }

            try
            {
                if (startTime > DateTime.Now)
                {
                    _nextTime = startTime;
                }
                else
                {
                    var duration = (DateTime.Now - startTime).TotalMinutes; // khoảng thời gian chênh lệch
                    var total = (int)(duration / runTime.TotalMinutes); // số lần chênh lệch so với runTime
                    _nextTime = startTime + TimeSpan.FromMinutes((total + 1) * runTime.TotalMinutes);
                }
                return true;
            }
            catch (Exception ex)
            {
                _nextTime = DateTime.MinValue;
                return false;
            }
        }
        #endregion
    }
}
