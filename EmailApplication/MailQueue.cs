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
        public EmailControl ec;
        public MailQueue()
        {
            //setup smtp object with info from database
            smtp = new SmtpClient();
            switch(ec.DeliveryMethod.toLower())
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
            smtp.EnableSsl = ec.smtpEnableSSL;
            smtp.Host = ec.smtpHost;
            smtp.Port = ec.smtpPort;
            smtp.Credentials = new System.Net.NetworkCredential(ec.smtpUser, ec.smtpPass);


        }




        public static void sendMail(string sFrom, string sTo, string sSubject, string sBody, bool bIsHtml)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(sFrom);

            // The important part -- configuring the SMTP client
            SmtpClient smtp = new SmtpClient();
            //smtp.Port = 587;   // [1] You can try with 465 also, I always used 587 and got success
            //smtp.EnableSsl = true;
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
            //smtp.UseDefaultCredentials = false; // [3] Changed this
            //smtp.Credentials = new NetworkCredential(mail.From,  "password_here");  // [4] Added this. Note, first parameter is NOT string.
            smtp.Host = ConfigurationManager.AppSettings["smtphost"].ToString();
            //recipient address
            string[] sTos = sTo.Split(',');

            foreach (string s in sTos)
            {
                mail.To.Add(new MailAddress(s));
            }

            //Formatted mail body
            mail.IsBodyHtml = bIsHtml;
            mail.Subject = sSubject;
            mail.Body = sBody;
            smtp.Send(mail);

        }
    }
}
