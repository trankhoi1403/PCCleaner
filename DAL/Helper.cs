using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Helper
    {
        public static bool DateTimeTryParseExact(object obj, out DateTime dateTime)
        {
            return DateTime.TryParseExact(obj.ToString(), ItemInfo.DateTimeFormat, ItemInfo.CultureInfoVN, System.Globalization.DateTimeStyles.None, out dateTime);
        }
        /// <summary>
        /// Convert thành timespan object phải có định dạng như 3d, 4h, 5m, nếu không sẽ return false :)
        /// </summary>
        /// <param name="obj">vd: 3d, 4h, 5m</param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static bool TimeSpanTryParseCustom(object obj, out TimeSpan timeSpan)
        {
            var str = obj.ToString();
            var format = str.Last();
            if (double.TryParse(str.Substring(0, str.Length - 1), out double number) == false)
            {
                timeSpan = TimeSpan.Zero;   // buộc phải assign giá trị cho timeSpan vì từ khóa out
                return false;
            }
            switch (format)
            {
                case 'd':
                    timeSpan = TimeSpan.FromDays(number);
                    break;
                case 'h':
                    timeSpan = TimeSpan.FromHours(number);
                    break;
                case 'm':
                    timeSpan = TimeSpan.FromMinutes(number);
                    break;
                default:
                    timeSpan = TimeSpan.Zero;
                    break;
            }
            return timeSpan.Equals(TimeSpan.Zero) ? false : true;
        }
    }
}
