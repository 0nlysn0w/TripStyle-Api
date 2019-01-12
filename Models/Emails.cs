using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
 
namespace TripStyle.Api.Models.Emails
{
    class Mail
    {
        public static void Factuur(string email)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("TripStyleService@gmail.com", "TripStyle87!");
 
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress(email),
                    Subject = "TripStyle",
                    Body = "Deze email is gestuurd als voorbeeld"
                };
 
                mail.To.Add(new MailAddress(email));
 
                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };
 
                // Send it...        
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sending email: " + ex.Message);
                //Console.ReadKey();
                return;
            }
 
            //Console.WriteLine("["+x+"] "+"Email sccessfully sent");
            //Console.ReadKey();
        }
    }
}