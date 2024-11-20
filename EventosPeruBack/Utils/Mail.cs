using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace EventosPeruBack.Utils
{
    public class Mail
    {
        public void Send(string strUser, string strPassword, string strFrom, string strTo, string strSubject, string strTextBody, string strAdjunto)
        {
            var message = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            try
            {
                message.From.Add(new MailboxAddress(strFrom, strUser));
                message.To.Add(new MailboxAddress(strTo, strTo));
                message.Subject = strSubject;
                bodyBuilder.HtmlBody = strTextBody;
                if (strAdjunto.Length > 0)
                {
                    bodyBuilder.Attachments.Add(strAdjunto);
                }
                message.Body = bodyBuilder.ToMessageBody();

                var client = new SmtpClient();

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.Auto);
                client.Authenticate(strUser, strPassword);
                client.Send(message);
                client.Disconnect(true);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}