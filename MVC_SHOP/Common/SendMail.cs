using System.Net;
using System.Net.Mail;
using System.Text;

namespace MVC_SHOP.Common
{
    public class SendMail
    {
        public class EmailService
        {
            public bool Send(string toEmail, string subject, string body)
            {
                
                int smtpPort = 25;
                try
                {
                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Host = Notice.SmtpHost;
                        smtpClient.Port = smtpPort;
                        smtpClient.UseDefaultCredentials = true;
                        smtpClient.Credentials = new NetworkCredential(Notice.SmtpUserName, Notice.SmtpPassword);
                        var msg = new MailMessage
                        {
                            IsBodyHtml = true,
                            BodyEncoding = Encoding.UTF8,
                            From = new MailAddress(Notice.SmtpUserName),
                            Subject = subject,
                            Body = body,
                            Priority = MailPriority.Normal,
                        };
                        msg.CC.Add(Notice.MailBrightday);
                        msg.CC.Add(Notice.MailGmailHunghv2);
                        msg.To.Add(toEmail);
                        smtpClient.Send(msg);
                        return true;
                    }
                }
                catch
                {

                    return false;
                }
            }
        }
    }

}