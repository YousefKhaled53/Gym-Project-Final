using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
namespace Gym_Project.Pages
{
    public class profileModel : PageModel
    {
        private readonly GYM_DB db;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting;
        public DataTable specific_user_data_table;
        public DataTable specific_user_bodydata_data_table;

        [BindProperty(SupportsGet =true)]
        public string name { get; set; }
        public user user { get; set; }

        [BindProperty(SupportsGet = true)]

        public IFormFile File { get; set; }
         
        private readonly ILogger<IndexModel> _logger;
        public profileModel(ILogger<IndexModel> logger,GYM_DB db, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
        {
            _logger = logger;
            this.db = db;
            this.hosting = hosting;
        }
        public void OnGet()
        {
            specific_user_data_table = db.return_an_user(name);
            specific_user_bodydata_data_table =db.return_users_bodydata(name);
        }
        [HttpPost]

        public IActionResult OnPostSave() {
            user=new user();
            string filename = string.Empty; // string tha may have the file name 
            if (File != null)
            {
                string images = Path.Combine(hosting.WebRootPath, "images");// getting the path part of images folder 
                filename = File.FileName; 
                string fullpath = Path.Combine(images,filename); // getting the whole path
                File.CopyTo(new FileStream(fullpath,FileMode.Create));
            }

            string birthdate = Request.Form["month"] + "-" + Request.Form["day"] + "-" + Request.Form["year"];
            user.first_name = Request.Form["firstname"];
            user.last_name = Request.Form["lname"];
            user.email = Request.Form["mail"];
            user.password = Request.Form["pass"];
            user.username= Request.Form["username"];
            user.profileurl = filename;
            DateTime add_date = DateTime.Now;
            int day = add_date.Day;
            int month = add_date.Month;
            int year = add_date.Year;
            string date = month + "-" + day + "-" + year;
            db.edituser(user.first_name,user.last_name,birthdate,user.email,user.username,user.password,user.profileurl);
            db.edituserbodydata(int.Parse(Request.Form["height"]), int.Parse(Request.Form["weight"]), int.Parse(Request.Form["muscle"]), int.Parse(Request.Form["fat"]), Request.Form["username"],date);
            if (user.username == "Zee")
            {
                return RedirectToPage("/homepage_3", new { username_coming_from_login = user.username });

            }
            else if (user.username == "sarah")
            {
                return RedirectToPage("/homepage_4", new { username_coming_from_login = user.username });
            }
            else
            {
                return RedirectToPage("/homepage_2", new { username_coming_from_login = user.username });
            }
            
        }
        public IActionResult OnPostCancel()
        {
            if (Request.Form["username"] == "Zee")
            {
                return RedirectToPage("/homepage_3", new { username_coming_from_login = Request.Form["username"] });

            }
            else if (Request.Form["username"] == "sarah")
            {
                return RedirectToPage("/homepage_4", new { username_coming_from_login = Request.Form["username"] });
            }
            else
            {
                return RedirectToPage("/homepage_2", new { username_coming_from_login = Request.Form["username"] });
            }
        }
        public void OnPostDeletemachine()
        {

        }

    }
    

}
