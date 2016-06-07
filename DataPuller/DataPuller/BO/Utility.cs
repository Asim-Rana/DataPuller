using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataPuller.BO
{
    public static class Utility
    {
        public static string validateString(string date)
        {
            string[] monthsList = { "jan" , "january" , "feb" , "february" , "mar" , "march" , "apr" , "april" , "may" , "jun" , "june", "jul" , "july" , "aug" ,
            "august" , "sep" , "september" , "oct" , "october" , "nov" , "november" , "dec" , "december" , "pm" , "am"};
            Regex regex = new Regex(@"[a-zA-Z]+");
            MatchCollection match = regex.Matches(date);

            foreach (Match m in match)
            {
                if (!monthsList.Contains(m.Value.ToLower()))
                {
                    date = date.Replace(m.Value, " ");
                }
            }
            return date.Trim();
        }
        public static DateTime ConvertToDateTime(string date , DateTime scrappedDate)
        {
            string oldDate = date.ToLower();
            date = validateString(date.ToLower());
            DateTime datetime;
            DateTime dt;
            try
            {
                DateTime.TryParse(date, out datetime);
                string d = datetime.ToString("yyyy-MM-dd HH:mm:ss tt");
                dt = DateTime.Parse(d, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                dt = DateTime.MinValue;
                Console.WriteLine(e.Message);
            }
            if (dt.CompareTo(DateTime.MinValue) == 0)
            {
                dt = ConvertToSystemDateTime(oldDate , scrappedDate);
            }
            return dt;
        }
        static DateTime ConvertToSystemDateTime(string date , DateTime sysDate)
        {
            Regex re = new Regex(@"\d+");
            Match m = re.Match(date);
            int num = 1;
            try
            {
                if (m.Success)
                {
                    num = int.Parse(m.Value);
                }
                if (date.Contains("day") || date.Contains("days"))
                {
                    sysDate = sysDate.AddDays(-num);
                }
                else if (date.Contains("week") || date.Contains("weeks"))
                {
                    sysDate = sysDate.AddDays(-num*7);
                }
                else if (date.Contains("month") || date.Contains("months"))
                {
                    sysDate = sysDate.AddMonths(-num);
                }
                else if (date.Contains("year") || date.Contains("years"))
                {
                    sysDate = sysDate.AddYears(-num);
                }
                else if (date.Contains("hour") || date.Contains("hours"))
                {
                    sysDate = sysDate.AddHours(-num);
                }
                else if (date.Contains("minute") || date.Contains("minutes"))
                {
                    sysDate = sysDate.AddMinutes(-num);
                }
                else if (date.Contains("second") || date.Contains("seconds"))
                {
                    sysDate = sysDate.AddSeconds(-num);
                }
                else
                {
                    sysDate = DateTime.MinValue;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurs in ConvertToSystemDateTime method and message is : " + e.Message);
                sysDate = DateTime.MinValue;
            }
            return sysDate;
        }
    }
}