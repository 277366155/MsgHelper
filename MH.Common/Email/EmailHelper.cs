using System.Text;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using MH.Core;
using System;

namespace MH.Common
{
    public  class EmailHelper
    {


        private static void GetConfigValue()
        {
            var mailConfig = BaseCore.Configuration.GetSection("AppSettings").GetSection("MailConfig");
            foreach (var config in mailConfig.GetChildren())
            {
                Console.WriteLine(config.Value + "——" + DEncrypt.Decrypt(config.Value));
            }
        }

        /// <summary>
        /// 读取配置文件后，异步发送邮件
        /// </summary>
        /// <param name="toMail">收件地址</param>
        /// <param name="subj">主题</param>
        /// <param name="bodys">正文</param>
        /// <param name="enableSsl">是否启用ssl</param>
        /// <param name="isEncrypted">配置是否加密</param>
        public static async Task SendMailAsync(string toMail, string subj, string bodys, bool enableSsl = false, bool isEncrypted = true)
        {
            var mailConfig = BaseCore.Configuration.GetSection("AppSettings").GetSection("MailConfig");
            var smtpServer = mailConfig["smtp"];
            var userName = mailConfig["account"];
            var pwd = mailConfig["pwd"];
            var nickName = mailConfig["nickName"];
            var fromMail = mailConfig["fromMail"];
            if (isEncrypted)
            {
                smtpServer = DEncrypt.Decrypt(smtpServer);
                userName = DEncrypt.Decrypt(userName);
                pwd = DEncrypt.Decrypt(pwd);
                nickName = DEncrypt.Decrypt(nickName);
                fromMail = DEncrypt.Decrypt(fromMail);
            }

         await Task.Run(() => {
                SendMail(smtpServer, enableSsl, userName, pwd, nickName, fromMail, toMail, subj, bodys);
            });
            
        }

        /// <summary>
        /// 自定义配置发送邮件
        /// </summary>
        /// <param name="smtpServer">smtp服务地址</param>
        /// <param name="enableSsl">是否开启ssl</param>
        /// <param name="userName">登录用户名</param>
        /// <param name="pwd">登录密码</param>
        /// <param name="nickName">发件人昵称</param>
        /// <param name="fromMail">发送地址</param>
        /// <param name="toMail">接收地址</param>
        /// <param name="subj">主题</param>
        /// <param name="bodys">邮件内容</param>
        /// <returns></returns>
        public static void SendMail(string smtpServer, bool enableSsl, string userName, string pwd, string nickName, string fromMail, string toMail, string subj, string bodys)
        { 
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer;//指定SMTP服务器
            smtpClient.Credentials = new NetworkCredential(userName, pwd);//用户名和密码
            smtpClient.EnableSsl = enableSsl;

            MailAddress fromAddress = new MailAddress(fromMail, nickName);
            MailAddress toAddress = new MailAddress(toMail);
            MailMessage mailMessage = new MailMessage(fromAddress, toAddress);

            mailMessage.Subject = subj;//主题
            mailMessage.Body = bodys;//内容
            mailMessage.BodyEncoding = Encoding.Default;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Normal;//优先级

           smtpClient.Send(mailMessage);
        }
    }
}
