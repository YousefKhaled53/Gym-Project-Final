using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace Gym_Project.Pages
{
    public class homepage_3Model : PageModel
    {
        private readonly GYM_DB db;

        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string username_coming_from_login { get; set; }
        [ViewData]
        public string picurl { get; set; }
        public DataTable dt { get; set; }
        private readonly ILogger<IndexModel> _logger;
        public homepage_3Model(ILogger<IndexModel> logger, GYM_DB db)
        {
            _logger = logger;
            this.db = db;
        }
        public void OnGet()
        {
            if (db.getprofilepiclink(username_coming_from_login) != null)
            {
                picurl = db.getprofilepiclink(username_coming_from_login);
            }
            dt = db.return_users_Feedback();
        }
        public IActionResult OnPostSubmitrequest()
        {
            string from = "abdoaboshareb8@gmail.com";
            string from_pass = "bafpjaukobocrpnq";
            string to = "s-abdel-rahman.ahmed@zewailcity.edu.eg";
            string password = db.getpassword(Request.Form["username"]);
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(from); // Specify the sender's email address
            msg.Subject =  Request.Form["maintenance-type"] +"";
            msg.To.Add(new MailAddress(to));
            msg.Body = "<html><body> "+ Request.Form["comment"] + "</body></html>";
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(from, from_pass),
                EnableSsl = true,
            };
            smtpClient.Send(msg);
            return RedirectToPage("/homepage_3", new { username_coming_from_login = Request.Form["un"] });
        }
    }
}
