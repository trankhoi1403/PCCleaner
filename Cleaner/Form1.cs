using Cleaner.UserControls;
using Cleaner.ExtensionMethod;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cleaner
{
    public partial class Form1 : Form
    {
        private List<ItemInfo> listItemInfo;
        private BindingSource src;
        public Form1()
        {
            InitializeComponent();
            ControlConfig();
            EventConfig();
        }

        private void ControlConfig()
        {


            // folderBrowserDialog
            folderBrowserDialog1.Description = "Select your project directory to monitor";
            folderBrowserDialog1.ShowNewFolderButton = false;

            // config timer 
            timer1.Interval = 60000;    // cách 1 phút quét 1 lần
            
            //config cột
            dgv.AllowUserToAddRows = false;
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "Id (minute)",
                Visible = false
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Project",
                DataPropertyName = "Project",
                HeaderText = "Project",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Directory",
                DataPropertyName = "Directory",
                HeaderText = "Directory",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ToolTipText = "Right click to browse folder"
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "StartTime",
                DataPropertyName = "StartTime",
                HeaderText = "StartTime",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "RunTime",
                DataPropertyName = "RunTime",
                HeaderText = "RunTime",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "NextTime",
                DataPropertyName = "NextTime",
                HeaderText = "NextTime",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewComboBoxColumn()
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Status",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
                DataSource = new string[] { "Stoped", "Running" }
            });
            dgv.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Delete",
                DataPropertyName = "Delete",
                HeaderText = "Delete",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells,
            });

        }

        private void EventConfig()
        {
            listItemInfo = new List<ItemInfo>();
            src = new BindingSource();
            Load += Form1_Load;
            FormClosing += Form1_FormClosing;

            dgv.CellClick += Dgv_CellClick;
            dgv.CellValueChanged += Dgv_CellValueChanged;
            dgv.CurrentCellDirtyStateChanged += Dgv_CurrentCellDirtyStateChanged;
            dgv.CellMouseClick += Dgv_CellMouseClick;

            toolStripButtonAdd.Click += ToolStripLabelAdd_Click;
            toolStripButtonLoad.Click += ToolStripButtonLoad_Click;
            toolStripButtonSave.Click += ToolStripButtonSave_Click;
            toolStripButtonSaveAs.Click += ToolStripButtonSaveAs_Click;
            toolStripButtonPin.Click += ToolStripButtonPin_Click;

            timer1.Tick += Timer1_Tick;
            ResizeBegin += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"w: {this.Width}, h: {this.Height}");
            };
            timer1.Start();
        }

        private void ToolStripButtonPin_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = splitContainer1.Panel1Collapsed ? false:true;
        }

        private void ToolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Json files (*.json)|*.json";
            saveFileDialog1.Title = "Save an project file";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                DbHelper.Write(listItemInfo, fs);

                fs.Close();
            }
        }

        private void Dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) // do bấm vào hàng header hoặc cột sát bên trái
                return;

            var result = folderBrowserDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var value = cell.Value;
                try
                {
                    cell.Value = folderBrowserDialog1.SelectedPath;
                }
                catch (Exception)
                {
                    cell.Value = value;
                }
            }
        }

        private void Dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv.SelectedCells.Count > 0 && dgv.SelectedCells[0] is DataGridViewComboBoxCell)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dgv.EndEdit();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;

            // duyệt qua các dòng, kiểm tra đúng thời điểm cần thiết thì cho timer trong các fileScreen chạy
            foreach (DataGridViewRow item in dgv.Rows)
            {
                var itemInfo = castToItemInfo(item);
                if (itemInfo == null || itemInfo.Status != "Running")
                    continue;

                if (Helper.DateTimeTryParseExact(itemInfo.StartTime, out DateTime itemInfoStartTime) == false)
                    continue;

                //var duration = itemInfoStartTime > now ? itemInfoStartTime - now : now - itemInfoStartTime;
                //if ((int)duration.TotalMinutes % itemInfo.RunTime == 0)
                //{
                //    var fileScreen = getFileScreenById(itemInfo.Id);
                //    if (fileScreen != null)
                //    {
                //        if (fileScreen.timer1.Enabled == false) // đảm bảo khi timer của fileScreen đang còn chạy thì KHÔNG start lại lần nữa
                //        {
                //            System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("hh:mm:ss")} FileScreen.InvokeTimerStart: {itemInfo.Project}");
                //            fileScreen.timer1.Start();
                //            /*
                //             * Cần phải gọi Timer1_Tick chứ không đợi nhảy vô TickEvent vì khi start (đối với fileScreen) thì hàm xử lý của TickEvent CHƯA được gọi liền mà 
                //             * sẽ ở lần Interval tiếp theo
                //             */
                //            fileScreen.Timer1_Tick(null, null);
                //        }
                //    }
                //}
            }
        }

        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var row = dgv.Rows[e.RowIndex];
                // tránh gọi event đệ quy vô tận do cột NextTime này được cập nhật trong chính event này
                if (e.ColumnIndex == row.Cells.IndexOf(row.Cells["NextTime"]))
                    return;

                // nếu cập nhật cột StartTime hoặc RunTime thì tính toán lại cột NextTime
                if (e.ColumnIndex == row.Cells.IndexOf(row.Cells["StartTime"]) || e.ColumnIndex == row.Cells.IndexOf(row.Cells["RunTime"]))
                {
                    if (Helper.DateTimeTryParseExact(row.Cells["StartTime"].Value, out DateTime startTime) == false
                        || Helper.TimeSpanTryParseCustom(row.Cells["RunTime"].Value, out TimeSpan runTime) == false)
                    {
                        return;
                    }
                    DateTime nextTime;
                    if (startTime > DateTime.Now)
                    {
                        nextTime = startTime;
                    }
                    else
                    {
                        var duration = (DateTime.Now - startTime).TotalMinutes; // khoảng thời gian chênh lệch
                        var total = (int)(duration / runTime.TotalMinutes); // số lần chênh lệch so với runTime
                        nextTime = startTime + TimeSpan.FromMinutes((total + 1) * runTime.TotalMinutes);
                    }
                    row.Cells["NextTime"].Value = nextTime.ToString(ItemInfo.DateTimeFormat);
                }

                var itemInfo = castToItemInfo(row);
                var fileScreen = getFileScreenById(itemInfo?.Id);
                if (fileScreen == null)
                    return;

                // nếu người dùng cập nhật đúng cột status và status = Stoped thì mới dừng
                if (e.ColumnIndex == row.Cells.IndexOf(row.Cells["Status"])
                    && row.Cells["Status"].Value.Equals("Stoped"))
                {
                    fileScreen.timer1.Stop();
                }

                fileScreen.Set(itemInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update cell failed!{Environment.NewLine}Error: {ex.Message}", "System Error Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ToolStripButtonSave_Click("Your work has been save successfully!", null);
            GC.Collect();
        }

        /// <summary>
        /// thực hiện hành động ghi đè file dữ liệu item-info.json
        /// </summary>
        /// <param name="sender">
        /// mặc định hệ thống truyền vào object có Type là ToolStripButton
        /// mình quy định nếu truyền vào biến có kiểu String thì sẽ là nội dung của messageBox
        /// </param>
        /// <param name="e"></param>
        private void ToolStripButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                DbHelper.Write(listItemInfo);
                MessageBox.Show(sender is String? sender.ToString() : "Save successfully!", "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save failed!{Environment.NewLine}Error: {ex.Message}", "System Error Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripButtonLoad_Click(object sender, EventArgs e)
        {
            Form1_Load(null, null);
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) // do bấm vào hàng header hoặc cột sát bên trái
                return;

            var senderGrid = (DataGridView)sender;

            try
            {
                try     // xóa dòng
                {
                    if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                        e.RowIndex >= 0)
                    {
                        var id = dgv.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                        removeFileScreen(id);
                        var dataSource = dgv.DataSource as BindingSource;
                        dataSource.RemoveAt(e.RowIndex);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Delete row failed!{Environment.NewLine}Error: {ex.Message}", "System Error Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // chuyển lại màu cũ
                foreach (var item in flowLayoutPanel1.Controls)
                {
                    if (item is FileScreen fileScreenTmp)
                    {
                        fileScreenTmp.ChangeTitleBackColor(Color.FromKnownColor(KnownColor.Control));
                    }
                }

                // đổi màu fileScreen tương ứng khi người dùng click lên grid
                var row = dgv.Rows[e.RowIndex];
                var itemInfo = castToItemInfo(row);
                var fileScreen = getFileScreenById(itemInfo?.Id);
                if (fileScreen != null)
                {
                    fileScreen.ChangeTitleBackColor(Color.LightSkyBlue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong!{Environment.NewLine}Error: {ex.Message}", "System Error Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripLabelAdd_Click(object sender, EventArgs e)
        {
            var itemInfo = new ItemInfo();
            var dataSource = dgv.DataSource as BindingSource;
            dataSource.Add(itemInfo);
            addFileScreen(itemInfo);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // clear control
                flowLayoutPanel1.Controls.Clear();
                dgv.Rows.Clear();

                // load dữ liệu
                listItemInfo = DbHelper.Read<List<ItemInfo>>();
                listItemInfo = listItemInfo ?? new List<ItemInfo>();
                src = new BindingSource();
                src.DataSource = listItemInfo;
                dgv.DataSource = src;

                // add cac file screen tuong ung
                foreach (var item in listItemInfo)
                {
                    addFileScreen(item);
                }
            }
            catch (Exception ex)
            {
                dgv.DataSource = null;
                MessageBox.Show($"Project load failed{Environment.NewLine}Error: {ex.Message}", "System Warning Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #region function
        private void addFileScreen(ItemInfo itemInfo)
        {
            FileScreen fileScreen = new FileScreen();
            fileScreen = new FileScreen(itemInfo);
            flowLayoutPanel1.Controls.Add(fileScreen);
        }
        private void removeFileScreen(string id)
        {
            var fileScreen = getFileScreenById(id);
            flowLayoutPanel1.Controls.Remove(fileScreen);
        }
        private FileScreen getFileScreenById(string value)
        {
            foreach (var item in flowLayoutPanel1.Controls)
            {
                var fileScreen = (item as FileScreen);
                if (fileScreen != null)
                {
                    if (fileScreen.itemInfo.Id == value)
                    {
                        return fileScreen;
                    }
                }
            }
            return null;
        }

        private ItemInfo castToItemInfo(DataGridViewRow row)
        {
            try
            {
                var itemInfo = new ItemInfo()
                {
                    Id = row.Cells["Id"].Value?.ToString(),
                    Project = row.Cells["Project"].Value?.ToString(),
                    Directory = row.Cells["Directory"]?.Value?.ToString(),
                    StartTime = row.Cells["StartTime"]?.Value?.ToString(),
                    NextTime = row.Cells["NextTime"]?.Value?.ToString(),
                    RunTime = row.Cells["RunTime"]?.Value?.ToString(),
                    Status = row.Cells["Status"]?.Value?.ToString()
                };
                return itemInfo;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        
        private void dgvUpdateRow(ItemInfo itemInfo, int rowIndex = -1)
        {
            DataGridViewRow rowToUpdate = null;
            if (rowIndex != -1)
            {
                rowToUpdate = dgv.Rows[rowIndex];
            }
            else
            {
            }
            rowToUpdate.Cells["Project"].Value = itemInfo.Project;
            rowToUpdate.Cells["Directory"].Value = itemInfo.Directory;
            rowToUpdate.Cells["StartTime"].Value = itemInfo.StartTime;
            rowToUpdate.Cells["RunTime"].Value = itemInfo.RunTime;
            rowToUpdate.Cells["NextTime"].Value = itemInfo.NextTime;
            rowToUpdate.Cells["Status"].Value = itemInfo.Status;
            rowToUpdate.Cells["Delete"].Value = itemInfo.Project;
        }

        #endregion
    }
}
