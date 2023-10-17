using Project.DataAccessLayer_DAL_.Entities;
using System;
using System.Net;
using System.Net.Mail;

namespace Project.PresentationLayer_PL_.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("aatwan300@gmail.com", "bzjnzupxjthfjqrj");
            client.Send("aatwan300@gmail.com", email.To, email.Tittle, email.Body);
        }
    }
}
