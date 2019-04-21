using System;
using System.ComponentModel;

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
                var min1 = now.AddMinutes(-1);       //    1分钟前
                var min2 = now.AddMinutes(-2);       //    2分钟前
                var min5 = now.AddMinutes(-5);       //    5分钟前
                var min10 = now.AddMinutes(-10);     //    10分钟前
                var min20 = now.AddMinutes(-20);     //    20分钟前
                var min30 = now.AddMinutes(-30);     //    30分钟前
                var hour1 = now.AddHours(-1);      //    1小时前
                var hour2 = now.AddHours(-2);      //    2小时前
                var hour3 = now.AddHours(-3);      //    3小时前
                var hour4 = now.AddHours(-4);      //    4小时前
                var hour5 = now.AddHours(-5);      //    5小时前
                var hour6 = now.AddHours(-6);      //    6小时前
                var hour7 = now.AddHours(-7);      //    7小时前
                var hour8 = now.AddHours(-8);      //    8小时前
                var hour9 = now.AddHours(-9);      //    9小时前
                var hour10 = now.AddHours(-10);    //    10小时前
                var hour11 = now.AddHours(-11);    //    11小时前
                var hour12 = now.AddHours(-12);    //    12小时前
                var hour13 = now.AddHours(-13);    //    13小时前
                var hour14 = now.AddHours(-14);    //    14小时前
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
        /// 根据出生日期计算年龄
        /// </summary>
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

        /// <summary>
        /// 根据出生日期获得星座信息
        /// </summary>
        public static string GetConstellation(this DateTime birthdate)
        {
            var rtn =Constellation.Acrab;
            float birthdayF = birthdate.Month == 1 && birthdate.Day < 20 ?
                13 + birthdate.Day / 100f :  birthdate.Month + birthdate.Day / 100f;

            float[] bound = { 1.20F, 2.20F, 3.21F, 4.21F, 5.21F, 6.22F, 7.23F, 8.23F, 9.23F, 10.23F, 11.21F, 12.22F, 13.20F };

            var constellations = new Constellation[12];
            for (int i = 0; i < constellations.Length; i++)
            {
                constellations[i] = (Constellation)(i + 1);
            }
               
            for (int i = 0; i < bound.Length - 1; i++)
            {
                float b = bound[i];
                float nextB = bound[i + 1];
                if (birthdayF >= b && birthdayF < nextB)
                {
                    rtn=constellations[i];
                }
            }

            return rtn.ToDescription();
        }

        public enum Constellation
        {
            [Description("水瓶座")]
            Aquarius = 1,//1.20 - 2.18

            [Description("双鱼座")]
            Pisces = 2,//2.19 - 3.20

            [Description("白羊座")]
            Aries = 3,//3.21 - 4.19          

            [Description("金牛座")]
            Taurus = 4, //4.20 - 5.20

            [Description("双子座")]
            Gemini = 5,//5.21 - 6.21

            [Description("巨蟹座")]
            Cancer = 6,//6.22 - 7.22

            [Description("狮子座")]
            Leo = 7,// 7.23 - 8.22

            [Description("处女座")]
            Virgo = 8,//8.23 - 9.22

            [Description("天秤座")]
            Libra = 9,//9.23 - 10.23

            [Description("天蝎座")]
            Acrab = 10,//10.24 - 11.22

            [Description("射手座")]
            Sagittarius = 11,//11.23 - 12.21

            [Description("摩羯座")]
            Capricornus = 12,//12.22 - 1.19
        }
    }
}
