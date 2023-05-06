using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
			password_from_login+= Request.Form["password"];
			if (db.getpassword(usrname_from_login) == password_from_login && db.getjob(usrname_from_login)== "student")
			{
				return RedirectToPage("/homepage_2", new { username_coming_from_login = usrname_from_login });
            }
			else if (db.getpassword(usrname_from_login) == password_from_login && db.getjob(usrname_from_login) == "Coach")
			{
				return RedirectToPage("/homepage_3");
            }
            else if (db.getpassword(usrname_from_login) == password_from_login && db.getjob(usrname_from_login) == "SU")
            {
                return RedirectToPage("/homepage_4");
            }
            else {
                return Page();
            }
		}
		
	}
}