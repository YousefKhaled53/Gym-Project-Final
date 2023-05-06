using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
namespace Gym_Project.Pages
{
    public class profileModel : PageModel
    {
        private readonly GYM_DB db;
        public DataTable specific_user_data_table;
        public DataTable specific_user_bodydata_data_table;

        [BindProperty(SupportsGet =true)]
        public string name { get; set; }
        public user user { get; set; }
        //public body_info body { get; set; } 
        
        private readonly ILogger<IndexModel> _logger;
        public profileModel(ILogger<IndexModel> logger,GYM_DB db)
        {
            _logger = logger;
            this.db = db;
        }
        public void OnGet()
        {
            specific_user_data_table = db.return_an_user(name);
            specific_user_bodydata_data_table =db.return_users_bodydata(name);
        }
        public IActionResult OnPostSave() {
            user=new user();

            string birthdate = Request.Form["day"] + "-" + Request.Form["month"] + "-" + Request.Form["year"];
            user.first_name = Request.Form["firstname"];
            user.last_name = Request.Form["lname"];
            user.email = Request.Form["mail"];
            user.password = Request.Form["pass"];
            user.username= Request.Form["username"];
            db.edituser(user.first_name,user.last_name,birthdate,user.email,user.username,user.password);
            db.edituserbodydata(int.Parse(Request.Form["height"]), int.Parse(Request.Form["weight"]), int.Parse(Request.Form["muscle"]), int.Parse(Request.Form["fat"]), Request.Form["username"]);
            return RedirectToPage("/homepage_2", new { username_coming_from_login = user.username });
            
        }
    }
}
