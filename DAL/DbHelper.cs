using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DAL
{
    public class DbHelper
    {

        public static string pwd = AppDomain.CurrentDomain.BaseDirectory;
        public static string dbDir = "db";
        public static string dbFile = "item-info.json";

        public static T Read<T>()
        {
            try
            {
                T result;
                using (var r = new StreamReader(Path.Combine(pwd, dbDir, dbFile)))
                {
                    string line = r.ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(line);
                }
                return result;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        public static void Write(Object obj)
        {
            try
            {
                if (Directory.Exists(dbDir) == false)
                {
                    Directory.CreateDirectory(dbDir);
                }
                using (var w = new StreamWriter(Path.Combine(pwd, dbDir, dbFile), false))
                {
                    String value = JsonConvert.SerializeObject(obj);
                    w.Write(value);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void Write(Object obj, FileStream fs)
        {
            try
            {
                String value = JsonConvert.SerializeObject(obj);
                var bytes = Encoding.UTF8.GetBytes(value);
                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
