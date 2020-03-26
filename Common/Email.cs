using HR.CommonUtility.Common;
using HR.WebApi.DAL;
using HR.WebApi.Model;
using HR.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Common
{
    public class Email
    {
        private readonly ApplicationDbContext adbContext;

        public Email()
        {
            adbContext = Startup.applicationDbContext;
        }

        public EmailConfiguration GetEmailConfiguration(int id)
        {
            EmailConfiguration emailConfig = new EmailConfiguration();
            try
            {
                Email_Config lstEmailConfig = adbContext.email_config.Where(x => x.Company_Id == id).FirstOrDefault();

                emailConfig = new EmailConfiguration()
                {
                    Email_Host = lstEmailConfig.Email_Host,
                    Email_Port = lstEmailConfig.Email_Port,
                    Email_UserName = lstEmailConfig.Email_UserName,
                    Email_Password = lstEmailConfig.Email_Password,
                    EnableSSL = lstEmailConfig.EnableSSL,
                };
            }
            catch (Exception ex)
            {
                emailConfig = null;
                throw ex;
            }

            return emailConfig;
        }

        public EmailBody GetEmailBody(string email_Id, string cc, string bcc, string subject, string body)
        {
            var usermodel = new EmailBody()
            {
                To = email_Id,
                CC = cc,
                BCC = bcc,
                Subject = subject,
                Body = body,
            };

            return usermodel;
        }

        public void SendEmail(EmailBody emailBody, EmailConfiguration emailConfig)
        {
            try
            {
                CommonUtility.Common.Email objEmail = new CommonUtility.Common.Email();
                objEmail.SendEmail(emailBody, emailConfig);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
