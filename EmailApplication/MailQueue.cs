using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using NLog;
using NLog.Targets;
using NLog.Config;
using System.Threading;

namespace EmailApplication
{
    public class MailQueue
    {
        private SmtpClient smtp;
        private EmailControlModel ec;
        private Logger logger = LogManager.GetCurrentClassLogger();
        public MailQueue()
        {
            try
            {
                //setup smtp object with info from database
                string sConnString = ConfigurationManager.ConnectionStrings["PRAWaitlistConnection"].ConnectionString.ToString();
                EmailClassesDataContext dc = new EmailClassesDataContext(sConnString);
                smtp = new SmtpClient();
                ec = dc.EmailControlModels.Where(x => x.SMTPisActive == true).Single();
                switch (ec.SMTPDeliveryMethod.ToLower())
                {
                    case "network":
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        break;
                    case "pickupdirectoryfromiis":
                        smtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                        break;
                    case "specifiedpickupdirectory":
                        smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        break;
                }
                smtp.EnableSsl = ec.SMTPEnableSSL;
                smtp.Host = ec.SMTPHost;
                smtp.Port = ec.SMTPPort;
                smtp.Credentials = new System.Net.NetworkCredential(ec.SMTPUser, ec.SMTPPassword);

            }
            catch(Exception e)
            {
                log(e);
            }
        }
        
        private void sendMail(EmailQueueModel eq)
        {
            try
            {
                string sConnString = ConfigurationManager.ConnectionStrings["PRAWaitlistConnection"].ConnectionString.ToString();
                EmailClassesDataContext dc = new EmailClassesDataContext(sConnString);
                eq = dc.EmailQueueModels.Where(x => x.ID == eq.ID).Single();
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(ec.FromAddress);
                    string[] sTos = eq.MessageTo.Split(',');
                    foreach (string s in sTos)
                    {
                        mail.To.Add(new MailAddress(s));
                    }
                    if (eq.MessageCC != string.Empty)
                    {
                        string[] sCCs = eq.MessageCC.Split(',');
                        foreach (string s in sCCs)
                        {
                            mail.CC.Add(new MailAddress(s));
                        }
                    }
                    if (eq.MessageBCC != string.Empty)
                    {
                        string[] sBCCs = eq.MessageBCC.Split(',');
                        foreach (string s in sBCCs)
                        {
                            mail.Bcc.Add(new MailAddress(s));
                        }
                    }
                    //Formatted mail body
                    mail.IsBodyHtml = eq.MessageIsHtml;
                    mail.Subject = eq.MessageSubject;
                    mail.Body = eq.MessageBody;
                    smtp.Send(mail);
                    eq.StatusModel = "Sent";
                    eq.StatusDate = DateTime.Now;
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {
                    log(e);
                    eq.ErrorMessage = e.Message;
                    if (!e.Message.StartsWith("Service not available,"))
                    {
                        eq.StatusModel = "Error";
                        eq.StatusDate = DateTime.Now;
                    }
                    dc.SubmitChanges();
                }
            }
            catch(Exception e)
            {
                log(e);
            }
        }

        public void ProcessQueue()
        {
            string sConnString = ConfigurationManager.ConnectionStrings["PRAWaitlistConnection"].ConnectionString.ToString();
            EmailClassesDataContext dc = new EmailClassesDataContext(sConnString);
            if (!CanSend())
            {
                return;
            }
            List<EmailQueueModel> lseq = dc.EmailQueueModels.Where(x => x.StatusModel == "Ready").OrderBy(x => x.QueueDate).ToList();
            foreach(EmailQueueModel eq in lseq)
            {
                if(CanSend())
                {
                    sendMail(eq);
                    Thread.Sleep(2000);
                }
                else
                {
                    return;
                }
            }


        }

        private bool CanSend()
        {
            bool retVal = false;
            try
            {
                string sConnString = ConfigurationManager.ConnectionStrings["PRAWaitlistConnection"].ConnectionString.ToString();
                EmailClassesDataContext dc = new EmailClassesDataContext(sConnString);
                int iLimit = ec.SMTPSendLimit;
                int iSent = 0;
//                if (dc.EmailQueueModels.Where(x => x.StatusDate > DateTime.Now.AddHours(-24) && x.StatusModel != "Ready").Any())
                if (dc.EmailQueueModels.Where(x => x.StatusDate > DateTime.Now.AddMinutes(-1) && x.StatusModel != "Ready").Any())
                {
                    iSent = dc.EmailQueueModels.Where(x => x.StatusDate > DateTime.Now.AddMinutes(-1) && x.StatusModel != "Ready").Sum(x => x.RecipientCount);
                    //iSent = dc.EmailQueueModels.Where(x => x.StatusDate > DateTime.Now.AddHours(-24) && x.StatusModel != "Ready").Sum(x => x.RecipientCount);
                }
                if (iLimit > iSent)
                {
                    retVal = true;
                }
            }
            catch(Exception e)
            {
                log(e);
            }
            return retVal;
        }

        private void log(string message, string level)
        {
            switch (level.ToLower())
            {
                case "error":
                    logger.Error(message);
                    break;

                case "info":
                    logger.Info(message);
                    break;

            }
        }
        private void log(Exception e)
        {
            logger.Error(e);
        }
    }
}
