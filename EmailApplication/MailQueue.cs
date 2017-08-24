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
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ec.FromAddress);
            string[] sTos = eq.MessageTo.Split(',');
            foreach (string s in sTos)
            {
                mail.To.Add(new MailAddress(s));
            }
            //Formatted mail body
            mail.IsBodyHtml = eq.MessageIsHtml;
            mail.Subject = eq.MessageSubject;
            mail.Body = eq.MessageBody;
            smtp.Send(mail);
        }
    }
}
