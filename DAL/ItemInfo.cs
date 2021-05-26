using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ItemInfo
    {
        public const string DateTimeFormat = "dd/MM/yyyy hh:mm";
        public static System.Globalization.CultureInfo CultureInfoVN = new System.Globalization.CultureInfo("vi-VN", true);

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Project { get; set; } = "New Project";
        public string Directory { get; set; } = @"D:\New folder\New folder 2\New Folder 3";
        public string StartTime { get; set; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
        public string NextTime { get; set; } = (DateTime.Now + TimeSpan.FromMinutes(30)).ToString("dd/MM/yyyy hh:mm");
        public string RunTime { get; set; } = "30m";
        public string Status { get; set; } = "Stoped";
        public string Delete { get; set; } = "X";
    }
}
