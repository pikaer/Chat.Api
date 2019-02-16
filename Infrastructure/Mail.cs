using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class Mail
    {
        private static string From { get { return ConfigHelper.AppSettings("From"); } }
        private static string DisplayName { get { return ConfigHelper.AppSettings("DisplayName"); } }
        private static string Username { get { return ConfigHelper.AppSettings("Username"); } }
        private static string Password { get { return ConfigHelper.AppSettings("Password"); } }
        private static string Smtp { get { return ConfigHelper.AppSettings("Smtp"); } }
        private static string Port { get { return ConfigHelper.AppSettings("Port"); } }

        //SmtpClient列表，按Smtp+Port存储单例
        private static ConcurrentDictionary<string, SmtpClient> _smtpClients = new ConcurrentDictionary<string, SmtpClient>();

        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="receivers">接收人，如果多人用;分隔</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <returns></returns>
        public static bool Send(string subject, string body, string receivers=null)
        {
            try
            {
                if (string.IsNullOrEmpty(From) || string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Username)
                    || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Smtp) || string.IsNullOrEmpty(Port))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(receivers))
                {
                    receivers = ConfigHelper.AppSettings("DefaultEmailReceivers");
                    if (string.IsNullOrEmpty(receivers))
                    {
                        return false;
                    }
                }

                int mailPost = int.Parse(Port);
                //改为单例模式
                //var smtp = new SmtpClient(Smtp, mailPost);
                var smtp = GetSmtpClientInstance(Smtp, mailPost);
                smtp.UseDefaultCredentials = false; // 是否使用本地验证
                smtp.Credentials = new NetworkCredential { UserName = Username, Password = Password }; //验证信息
                smtp.EnableSsl = false;//是否使用SSL加密传输
                var mail = new MailMessage();
                mail.From = new MailAddress(From, DisplayName); //发送人
                receivers.Split(';').Where(x => !string.IsNullOrEmpty(x)).ToList().ForEach(t => mail.To.Add(new MailAddress(t.Trim()))); //接收人
                mail.SubjectEncoding = Encoding.UTF8; //编码格式
                mail.Subject = subject;
                mail.IsBodyHtml = true; //Body是否HTML格式           
                mail.Body = body;
                mail.BodyEncoding = Encoding.UTF8;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                var msg = string.Format("发送邮件异常。接收人：{0}，标题：{1}，正文：{2},调用堆栈：{3}", receivers, subject, body, ex);
                return false;
            }
        }

        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="receivers">接收人，如果多人用;分隔</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        public static void SendAsync(string subject, string body, string receivers=null)
        {
            Task.Factory.StartNew(() =>
            {
                Send(subject, body, receivers);
            });
        }

        /// <summary>
        /// 获取SmtpClient单例
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static SmtpClient GetSmtpClientInstance(string smtp, int port)
        {
            var key = string.Format("{0}:{1}", smtp, port);
            if (_smtpClients.ContainsKey(key))
            {
                return _smtpClients[key];
            }
            var smtpClient = new SmtpClient(Smtp, port);
            _smtpClients.TryAdd(key, smtpClient);
            return smtpClient;
        }
    }
    
}
