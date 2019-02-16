using System;

namespace Infrastructure
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// 时间转义
        /// </summary>
        /// <param name="datetime">时间：2018-09-04 0630：30.000</param>
        /// <returns></returns>
        public static string GetDateDesc(this DateTime datetime)
        {
            var rtn = "";

            var now = DateTime.Now;
            var today = DateTime.Now.Date;  //2018-9-4 0:00:00 今天凌晨
            var yestoday = DateTime.Now.AddDays(-1).Date;  //2018-9-3 0:00:00  昨天凌晨
            var beforeyestoday = DateTime.Now.AddDays(-2).Date;  //2018-9-2 0:00:00  前天凌晨
            if (datetime > today)
            {
                var min1 = now.AddMinutes(-1).Date;       //    1分钟前
                var min2 = now.AddMinutes(-2).Date;       //    2分钟前
                var min5 = now.AddMinutes(-5).Date;       //    5分钟前
                var min10 = now.AddMinutes(-10).Date;     //    10分钟前
                var min20 = now.AddMinutes(-20).Date;     //    20分钟前
                var min30 = now.AddMinutes(-30).Date;     //    30分钟前
                var hour1 = now.AddHours(-1).Date;      //    1小时前
                var hour2 = now.AddHours(-2).Date;      //    2小时前
                var hour3 = now.AddHours(-3).Date;      //    3小时前
                var hour4 = now.AddHours(-4).Date;      //    4小时前
                var hour5 = now.AddHours(-5).Date;      //    5小时前
                var hour6 = now.AddHours(-6).Date;      //    6小时前
                var hour7 = now.AddHours(-7).Date;      //    7小时前
                var hour8 = now.AddHours(-8).Date;      //    8小时前
                var hour9 = now.AddHours(-9).Date;      //    9小时前
                var hour10 = now.AddHours(-10).Date;    //    10小时前
                var hour11 = now.AddHours(-11).Date;    //    11小时前
                var hour12 = now.AddHours(-12).Date;    //    12小时前
                var hour13 = now.AddHours(-13).Date;    //    13小时前
                var hour14 = now.AddHours(-14).Date;    //    14小时前
                if (datetime >= min1)
                {
                    rtn = "刚刚";
                }
                else if (datetime >= min2 && datetime < min1)
                {
                    rtn = "1分钟前";
                }
                else if (datetime >= min5 && datetime < min2)
                {
                    rtn = "2分钟前";
                }
                else if (datetime >= min10 && datetime < min5)
                {
                    rtn = "5分钟前";
                }
                else if (datetime >= min20 && datetime < min10)
                {
                    rtn = "10分钟前";
                }
                else if (datetime >= min30 && datetime < min20)
                {
                    rtn = "20分钟前";
                }
                else if (datetime >= hour1 && datetime < min30)
                {
                    rtn = "30分钟前";
                }
                else if (datetime >= hour2 && datetime < hour1)
                {
                    rtn = "1小时前";
                }
                else if (datetime >= hour3 && datetime < hour2)
                {
                    rtn = "2小时前";
                }
                else if (datetime >= hour4 && datetime < hour3)
                {
                    rtn = "3小时前";
                }
                else if (datetime >= hour5 && datetime < hour4)
                {
                    rtn = "4小时前";
                }
                else if (datetime >= hour6 && datetime < hour5)
                {
                    rtn = "5小时前";
                }
                else if (datetime >= hour7 && datetime < hour6)
                {
                    rtn = "6小时前";
                }
                else if (datetime >= hour8 && datetime < hour7)
                {
                    rtn = "8小时前";
                }
                else if (datetime >= hour10 && datetime < hour9)
                {
                    rtn = "9小时前";
                }
                else if (datetime >= hour11 && datetime < hour10)
                {
                    rtn = "10小时前";
                }
                else if (datetime >= hour12 && datetime < hour11)
                {
                    rtn = "11小时前";
                }
                else if (datetime >= hour13 && datetime < hour12)
                {
                    rtn = "12小时前";
                }
                else if (datetime >= hour14 && datetime < hour13)
                {
                    rtn = "13小时前";
                }
                else
                {
                    rtn = "14小时前";
                }
            }
            else if (datetime <= today && datetime > yestoday)
            {
                rtn = "昨天";
            }
            else if (datetime <= yestoday && datetime > beforeyestoday)
            {
                rtn = "前天";
            }
            else
            {
                rtn = datetime.ToShortDateString().ToString();
            }
            return rtn;
        }

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="birthdate">生日</param>
        /// <returns></returns>
        public static int GetAgeByBirthdate(this DateTime birthdate)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - birthdate.Year;
            if (now.Month < birthdate.Month || (now.Month == birthdate.Month && now.Day < birthdate.Day))
            {
                age--;
            }
            return age < 0 ? 0 : age;
        }
    }
}
