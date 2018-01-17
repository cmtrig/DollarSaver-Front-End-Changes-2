using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using System.Net.Mail;

namespace DollarSaver.Core.Common {
    public class Mailer {



        public void Send(MailMessage message) {


            String smtpServer = Convert.ToString(ConfigurationManager.AppSettings["smtp_server"]);


            SmtpClient smtp = new SmtpClient(smtpServer, 25);

            try {

                smtp.Send(message);

            } catch (SmtpException ex) {

                if (ex.StatusCode == SmtpStatusCode.InsufficientStorage) {
                    // retry sending
                    smtp.Send(message);

                } else {
                    throw ex;
                }


            }


        }


    }
}
