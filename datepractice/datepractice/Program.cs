using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace datepractice
{
    //日期的相關處理

    //* 取得今天日期!
    //* 取得現在的時間!
    //* 取得今天是星期幾!
    //* 假設一星期的第一天是星期日, 請寫程式求得本星期的第一天!
    //* 計算出本月一日是哪一天!
    //* 計算本月一共有幾天!
    //* 判斷今年是不是閏年!
    //* 計算出本月最後一天是哪一天!
    //* 計算出下個月一日是哪一天!
    //* 計算本年最後一天是哪一天!
    //* 取得 2022 年裡,每一個星期日的記錄, 傳回型別是 DateTime[]!
    internal class Program
    {
        static void Main(string[] args)
        {
            string year_type = "yyyy/MM/dd";
            
            DateTime Today = DateTime.Today;
            #region 基本判斷
            string today = Today.ToShortDateString();//今天日期
            string time = DateTime.Now.ToShortTimeString();//現在時間
            string week = Today.DayOfWeek.ToString(); //星期幾
            string leapyear = DateTime.IsLeapYear(Today.Year).ToString();//閏年判斷
            #endregion

            #region 判斷日期
            //假設一星期的第一天是星期日, 請寫程式求得本星期的第一天

            int delete_days = ((int)Today.DayOfWeek % 7) * -1;
            string first_day_this_week = Today.AddDays(delete_days).ToString(year_type);

            string days_this_month = DateTime.DaysInMonth(2020, 10).ToString();//計算本月一共有幾天

            string first_day_this_month = (new DateTime(Today.Year, Today.Month, 1)).ToString(year_type);//計算出本月一日是哪一天

            string last_day_this_month = (new DateTime(Today.Year, Today.AddMonths(1).Month, 1).AddDays(-1)).ToString(year_type);//計算出本月最後一天是哪一天

            string first_day_next_month = (new DateTime(Today.Year, Today.AddMonths(1).Month, 1)).ToString(year_type);//計算出下個月一日是哪一天

            string last_day_this_year = (new DateTime(Today.AddYears(1).Year, 1, 1).AddDays(-1)).ToString(year_type);//計算本年最後一天是哪一天

            #endregion





            date_handler date_Handler = new date_handler();

            #region List to Array
            List<DateTime> Dates;

            Dates = date_Handler.list_every_sunday_in_2022(2022);

            DateTime[] dateTimes = Dates.ToArray(); //轉陣列

            #endregion

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("今天日期：" + today);
            sb.AppendLine("現在時間：" + time);
            sb.AppendLine("今天" + date_Handler.week_to_chinese(week));
            sb.AppendLine("是否閏年：" + leapyear);
            sb.AppendLine("本周的第一天：" + first_day_this_week);
            sb.AppendLine("本月天數：" + days_this_month);
            sb.AppendLine("本月第一天：" + first_day_this_month);
            sb.AppendLine("本月最後一天：" + last_day_this_month);
            sb.AppendLine("下個月第一天：" + first_day_next_month);
            sb.AppendLine("今年最後一天：" + last_day_this_year);


            var result = sb.ToString();


            Console.WriteLine(result);

            Console.WriteLine("今年的每個星期日：");
            foreach (var date in dateTimes)
            {
                Console.WriteLine(date);
            }

        }

        //先找一年當中的第一個星期日，在+7
        #region 處理日期的function
        class date_handler
        {
            public string week_to_chinese(string week)//英文轉中文
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("Monday", "星期一");
                dic.Add("Tuesday", "星期二");
                dic.Add("Wednesday", "星期三");
                dic.Add("Thursday", "星期四");
                dic.Add("Friday", "星期五");
                dic.Add("Saturday", "星期六");
                dic.Add("Sunday", "星期日");

                if (dic.ContainsKey(week))
                {
                    week = dic[week];
                }
                return week;
            }

            public List<DateTime> list_every_sunday_in_2022(int year) // 找尋2022的每個星期日
            {
                string year_type = "yyyy/MM/dd";
                List<DateTime> Dates = new List<DateTime>();

                DateTime first_day_this_year = new DateTime(year, 1, 1);//今年第一天
                first_day_this_year = first_sunday_this_year(first_day_this_year);

                DateTime first_day_next_year = new DateTime(year + 1, 1, 1);//明年第一天
                first_day_next_year = first_sunday_this_year(first_day_next_year);

                int Count_sunday = ((int)(first_day_next_year.Subtract(first_day_this_year).TotalDays) / 7);

                while ((Count_sunday-1) > 0)
                {
                    Dates.Add(first_day_this_year);
                    first_day_this_year = first_day_this_year.AddDays(7);
                    Count_sunday--;
                }

                return Dates;
            }
            public DateTime first_sunday_this_year(DateTime first_day_this_year) // 找一年當中的第一個星期日
            {
                int days = 7 - ((int)first_day_this_year.DayOfWeek % 7);
                return first_day_this_year.AddDays(days);
            }



        }
        #endregion





    }
}
