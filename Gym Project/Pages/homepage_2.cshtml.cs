using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Gym_Project.Pages
{
    public class homepage_2Model : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

     
        public body_info body_info;
        private readonly GYM_DB db;

        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string username_coming_from_login { get; set; }
        public DateTime start_date { get; set; }
        public homepage_2Model(ILogger<IndexModel> logger, GYM_DB db)
        {
            _logger = logger;
            this.db = db;
        }

        public void OnGet()
        {
            body_info= new body_info(username_coming_from_login,db.getweight(username_coming_from_login),db.getheight(username_coming_from_login),db.getmusclemass(username_coming_from_login),db.getfatpercentage(username_coming_from_login));
        }
        public void OnPostM1()
        {
            int day = start_date.Day;
            int month = start_date.Month;
            int year = start_date.Year;
            string date = month+ "-" + day+ "-" + year;
            Random random = new Random();
            int randomNumber = random.Next(1, 100000);
            db.add_subscribtion_to_user(username_coming_from_login,randomNumber,"150","1 month",date);
            //return Page();
        }
        public void OnPostM3()
        {
            int day = start_date.Day;
            int month = start_date.Month;
            int year = start_date.Year;
            string date = month + "-" + day + "-" + year;
            Random random = new Random();
            int randomNumber = random.Next(1, 100000);
            db.add_subscribtion_to_user(username_coming_from_login, randomNumber, "250", "3 month", date);
            //return Page();
        }
        public void OnPostM6()
        {
            int day = start_date.Day;
            int month = start_date.Month;
            int year = start_date.Year;
            string date = month + "-" + day + "-" + year;
            Random random = new Random();
            int randomNumber = random.Next(1, 100000);
            db.add_subscribtion_to_user(username_coming_from_login, randomNumber, "450", "6 month", date);
            //return Page();

        }
        public void OnPostYear_subs()
        {
            int day = start_date.Day;
            int month = start_date.Month;
            int year = start_date.Year;
            string date = month + "-" + day + "-" + year;
            Random random = new Random();
            int randomNumber = random.Next(1, 100000);
            db.add_subscribtion_to_user(username_coming_from_login, randomNumber, "850", "1 year", date);
            //return Page();
        }
    }
}
