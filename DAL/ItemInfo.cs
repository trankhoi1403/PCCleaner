using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ItemInfo
    {
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        public const string DefaultProject = "New Project";
        public const string DefaultDirectory = @"D:\New folder\New folder 2\New Folder 3";
        public const string DefaultRunTime = "30m";
        public const string DefaultStatus = "Stoped";
        public const string RegexRunTime = @"\d+[d,h,m]";
        public static TimeSpan MaximumRunTime { get; private set; } = TimeSpan.FromDays(24);
        public static System.Globalization.CultureInfo CultureInfoVN { get; private set; } = new System.Globalization.CultureInfo("vi-VN", true);

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Project { get; set; } = "New Project";
        public string Directory { get; set; } = @"D:\New folder\New folder 2\New Folder 3";
        public string StartTime { get; set; } = DateTime.Now.ToString(DateTimeFormat);
        public string NextTime { get; set; } = (DateTime.Now + TimeSpan.FromMinutes(30)).ToString(DateTimeFormat);
        public string RunTime { get; set; } = DefaultRunTime;
        public string Status { get; set; } = DefaultStatus;
        public string Delete { get; set; } = "X";
    }
}
