using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EmailApplication
{
    public class MailQueue
    {
        public SmtpClient smtp;
        public EmailControlModel ec;
        public MailQueue()
        {
            //setup smtp object with info from database
            EmailClassesDataContext dc = new EmailClassesDataContext();
            smtp = new SmtpClient();
            ec = dc.EmailControlModels.Where(x => x.SMTPisActive == true).Single();
            switch(ec.SMTPDeliveryMethod.ToLower())
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
        
        private void sendMail(EmailQueueModel eq)
        {
            EmailClassesDataContext dc = new EmailClassesDataContext();
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
                string[] sCCs = eq.MessageCC.Split(',');
                foreach (string s in sCCs)
                {
                    mail.CC.Add(new MailAddress(s));
                }
                string[] sBCCs = eq.MessageBCC.Split(',');
                foreach (string s in sBCCs)
                {
                    mail.Bcc.Add(new MailAddress(s));
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
            catch(Exception e)
            {
                eq.StatusModel = "Error";
                eq.StatusDate = DateTime.Now;
                dc.SubmitChanges();
            }
        }

        public void ProcessQueue()
        {
            EmailClassesDataContext dc = new EmailClassesDataContext();
            if(!CanSend())
            {
                return;
            }
            List<EmailQueueModel> lseq = dc.EmailQueueModels.Where(x => x.StatusModel == "Ready").OrderBy(x => x.QueueDate).ToList();
            foreach(EmailQueueModel eq in lseq)
            {
                if(CanSend())
                {
                    sendMail(eq);
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
            EmailClassesDataContext dc = new EmailClassesDataContext();
            int iLimit = ec.SMTPSendLimit;
            int iSent = dc.EmailQueueModels.Where(x => x.StatusDate > DateTime.Now.AddHours(-23) && x.StatusModel!="Ready").Sum(x => x.RecipientCount);
            if(iLimit>iSent)
            {
                retVal = true;
            }
            return retVal;
        }
    }
}
