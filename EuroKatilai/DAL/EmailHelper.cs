using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmailHelper
    {
        public static SmtpClient GetSmptClient()
        {
            string smtpServer = "smtp.hostinger.com";
            int smtpPort = 587;

            string smtpUsername = @"info@eurokatilai.lt";
            string smtpPassword = @"Edvinas1#";

            SmtpClient client = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            return client;
        }
    }
}
