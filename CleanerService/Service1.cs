using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CleanerService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cleaner.exe"),
                },
            };
        }

        private readonly Process process;

        protected override void OnStart(string[] args)
        {
            try
            {
                string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cleaner.exe");
                WriteToFile(DateTime.Now.ToString("HH:mm:ss:ffffff") + " OnStart: " + fileName);
                Process.Start(fileName);
            }
            catch (Exception ex)
            {
                WriteToFile(DateTime.Now.ToString("HH:mm:ss:ffffff") + "ex.Message: " + ex.Message + Environment.NewLine + "ex.StackTrace" + ex.StackTrace);
            }

            //using (var ts = new TaskService())
            //{

            //    var t = ts.Execute("notepad.exe")
            //        .Once()
            //        .Starting(DateTime.Now.AddSeconds(5))
            //        .AsTask("myTask");

            //}
        }

        protected override void OnStop()
        {
            try
            {
                WriteToFile(DateTime.Now.ToString("HH:mm:ss:ffffff") + " OnStop");
                process.Kill();
            }
            catch (Exception ex)
            {
                WriteToFile(DateTime.Now.ToString("HH:mm:ss:ffffff") + "ex.Message: " + ex.Message + Environment.NewLine + "ex.StackTrace" + ex.StackTrace);
            }
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
