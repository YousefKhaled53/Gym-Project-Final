using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Gym_Project.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly GYM_DB db;

		public IndexModel(ILogger<IndexModel> logger, GYM_DB db)
		{
			_logger = logger;
			this.db = db;
		}
		[BindProperty]
		public string password_from_login { get; set; }
		[BindProperty]
		public string usrname_from_login { get; set; }
		public void OnGet()
		{

		}
		public IActionResult OnPostLogin() {
			usrname_from_login = Request.Form["username"];
			password_from_login += Request.Form["password"];
            
            if (db.getpassword(usrname_from_login) == password_from_login && db.getjob(usrname_from_login) == "student")
			{
				return RedirectToPage("/homepage_2", new { username_coming_from_login = usrname_from_login });
			}
			else if (db.getpassword(usrname_from_login) == password_from_login && db.getjob(usrname_from_login) == "Coach")
			{
				return RedirectToPage("/homepage_3", new { username_coming_from_login = usrname_from_login });
			}
			else if (db.getpassword(usrname_from_login) == password_from_login && db.getjob(usrname_from_login) == "SU")
			{
				return RedirectToPage("/homepage_4", new { username_coming_from_login = usrname_from_login });
			}
			else
			{
				return Page();
			}


		}
		public string first_name { get; set; }
        public string last_name { get; set; }
        public string day_bd{ get; set; }
        public string month_bd { get; set; }
        public string year_bd { get; set; }
        public string Gender { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public IActionResult OnPostSign()
		{
			first_name = Request.Form["first-name"];
			last_name = Request.Form["last-name"];
			day_bd = Request.Form["day"];
			month_bd = Request.Form["month"];
			year_bd = Request.Form["year"];
			Gender = Request.Form["gender"];
			email = Request.Form["email"];
			username = Request.Form["username"];
			password = Request.Form["password"];
			confirmpassword = Request.Form["confirm-password"];
			string birthdate= month_bd+ "-"+ day_bd + "-" +year_bd;
			if (password == confirmpassword)
			{
				db.adduser(first_name,last_name,birthdate,Gender,email,username,password);
                return RedirectToPage("/homepage_2", new { username_coming_from_login = username });
            }
			else { return Page(); }
			
		}
		public void OnPostForgotpassword()
		{
			string from = "abdoaboshareb8@gmail.com";
			string from_pass = "bafpjaukobocrpnq";
			MailMessage msg = new MailMessage();
			msg.From = new MailAddress(from); // Specify the sender's email address
			msg.Subject = "YOUR ZC GYM ACCOUNT PASSWORD ";
			msg.To.Add(new MailAddress("s-abdel-rahman.ahmed@zewailcity.edu.eg"));
			msg.Body = "<html><body> your password is </body></html>";
			msg.IsBodyHtml = true;
			var smtpClient = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential(from, from_pass),
				EnableSsl = true,
			};
			smtpClient.Send(msg);
		}





	}
}