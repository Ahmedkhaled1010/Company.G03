using System.Net;
using System.Net.Mail;
using Company.G03.DAL.Model;

namespace Company.G03.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com",587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("ahmed01020238429@gmail.com", "01118227172");
            Client.Send("ahmed01020238429@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
