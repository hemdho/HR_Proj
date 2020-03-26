using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HR.CommonUtility.Common
{
    public class Email
    {
        public async Task<string> SendEmail(EmailBody emailBody, EmailConfiguration emailConfig)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = Convert.ToBoolean(emailConfig.EnableSSL);
                client.Host = emailConfig.Email_Host;
                client.Port = emailConfig.Email_Port;

                // setup Smtp authentication
                NetworkCredential credentials = new NetworkCredential(emailConfig.Email_UserName, emailConfig.Email_Password);

                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                var vMail = new MailMessage()
                {
                    From = new MailAddress(emailConfig.Email_UserName),
                    Subject = emailBody.Subject,
                    Body = emailBody.Body,
                    IsBodyHtml = true
                };

                if (emailBody.file != null)
                {
                    foreach (string onefile in emailBody.file)
                    {
                        string path = Path.GetFullPath(onefile);
                        string fileName = Path.GetFileName(onefile);
                        var fileSave = Path.GetTempPath();
                        string fileSavePath = Path.Combine(fileSave, fileName);
                        File.Copy(path, fileSavePath, true);

                        Attachment attachment = new Attachment(fileSavePath);

                        vMail.Attachments.Add(attachment);
                    }
                }

                #region recipients
                if (emailBody.To != null)
                {
                    foreach (string _To in emailBody.To.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        vMail.To.Add(new MailAddress(_To));
                    }
                }

                if (emailBody.CC != null)
                {
                    foreach (string _cc in emailBody.CC.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        vMail.CC.Add(new MailAddress(_cc));
                    }
                }

                if (emailBody.BCC != null)
                {
                    foreach (string _bcc in emailBody.BCC.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        vMail.Bcc.Add(new MailAddress(_bcc));
                    }
                }
                #endregion recipients

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                try
                {
                    await client.SendMailAsync(vMail);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
                finally
                {
                    client.Dispose();
                }
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }

    public class EmailBody
    {
        [Required]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$")]
        public string To { get; set; }

        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$")]
        public string CC { get; set; }

        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*$")]
        public string BCC { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        public IList<string> file { get; set; }
    }

    public class EmailConfiguration
    {
        
        [Required]
        public string Email_Host { get; set; }
        [Required]
        public int Email_Port { get; set; }
        [Required]
        public string Email_UserName { get; set; }
        [Required]
        public string Email_Password { get; set; }
        [Required]
        public Int16 EnableSSL { get; set; }
    }
}
