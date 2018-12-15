using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



using TripStyle.Api.Models;

namespace TripStyle.Api.Controllers
{
    //TODO doesnt work yet
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly TripStyleContext _context;

        public UserController(TripStyleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.Users.ToList();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> GetById(int id)
        {
            User user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (user == null)
            {
                return NoContent();
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { UserId = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]User user)
        {
            var todo = _context.Users.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Firstname = user.Firstname;
            todo.Lastname = user.Lastname;
            todo.Gender = user.Gender;
            todo.Email = user.Email;
            todo.Phonenumber = user.Phonenumber;
            todo.Password = user.Password;
            todo.Birthdate = user.Birthdate;

            _context.Users.Update(todo);
            _context.SaveChanges();
            return CreatedAtRoute("GetUser", new { id = todo.UserId }, todo);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Users.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Users.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }

         [HttpGet("confirmation/{token}")]
        public IActionResult CheckRegisterConfirmationToken(string token)
        {
            //Check whether confirmation token exists && that token is not yet turned in
            ConfirmationMail result = _context.ConfirmationMails.FirstOrDefault(cMail => cMail.ConfirmationToken == token && cMail.AccountStatus == 0);
            if (result != null)
            {
                result.AccountStatus = 1;
                _context.SaveChanges();

                return new OkObjectResult(true);
            }
            
            return new OkObjectResult(false);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Address a)//Check whether email exists, how send your email(smtp client)
        {
            if (a != null)
            {
                string confirmationTokenGuid = Guid.NewGuid().ToString();

                var emailAlreadyExists = _context.Users.Any(user => user.Email.ToLower() == a.User.Email.ToLower());
                if (!emailAlreadyExists)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<html><head><title>Confirmation mail:</title></head><body>");
                    sb.Append("<p>TO: " + a.User.Firstname + " " + a.User.Lastname + "/" + "</p><br/>");

                    sb.Append("<p>Here is the link:</p><br/>");
                    sb.Append("<p>" + "<a href=" + "https://localhost:5001/confirmation/" + confirmationTokenGuid + ""+">" + "Confirmation link" +"</a></p><br/>");  
                    sb.Append("PLEASE DO NOT REPLY TO THIS MESSAGE AS IT IS FROM AN UNATTENDED MAILBOX. ANY REPLIES TO THIS EMAIL WILL NOT BE RESPONDED TO OR FORWARDED. THIS SERVICE IS USED FOR OUTGOING EMAILS ONLY AND CANNOT RESPOND TO INQUIRIES.");

                    SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                    var mail = new MailMessage();
                    mail.From = new MailAddress("TripStylewebshop@hotmail.com");
                    mail.To.Add("0932309@hr.nl");
                    mail.Subject = "confirmation mail";
                    mail.IsBodyHtml = true;
                    string htmlBody;
                    htmlBody = "@ Hello! " + a.User.Firstname + "\n your TripStyle account is created, please click to following activation link: \n " + "<a href=\"www.google.com\">login</a>";
                    mail.Body = sb.ToString();
                    SmtpServer.Port = 587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("TripStylewebshop@hotmail.com", "TripStyle!");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);


                    ConfirmationMail confirmationMail = new ConfirmationMail();
                    confirmationMail.User = a.User;
                    confirmationMail.AccountStatus = 0;// first 0, if he kliks in email link then 1
                    confirmationMail.ConfirmationToken = confirmationTokenGuid;

                    a.User.ConfirmationMail = confirmationMail;

                    a.Current = 1;
                    _context.Addresses.Add(a);
                    
                    _context.SaveChanges();
                    
           
                   return Ok();
                }

                //User already exists

            }
            
            return NoContent();
        }
    }
}