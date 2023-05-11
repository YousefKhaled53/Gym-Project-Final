using ChartExample.Models.Chart;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Reflection.Emit;

namespace Gym_Project.Pages
{
    public class homepage_2Model : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public ChartJs Chart { get; set; }
        public string ChartJson { get; set; }


        public body_info body_info;
        private readonly GYM_DB db;
        string subscribtion { get; set; }
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
            var chartData = @"

{
type: 'line',
responsive: true,
data:
{
labels: ['January', 'February', 'March', 'April','May','June','July','August','September','October','November','December'],
datasets: [
{
label: 'Weight',
data: [60, 65, 63, 65, 62, 63,60, 65, 63, 65, 62, 63],
backgroundColor: [
'rgba(255, 99, 132, 0.2)',
'rgba(54, 162, 235, 0.2)',
'rgba(255, 206, 86, 0.2)',
'rgba(75, 192, 192, 0.2)',
'rgba(153, 102, 255, 0.2)',
'rgba(255, 159, 64, 0.2)'
],
borderColor: [
'rgba(255, 99, 132, 1)',
'rgba(54, 162, 235, 1)',
'rgba(255, 206, 86, 1)',
'rgba(75, 192, 192, 1)',
'rgba(153, 102, 255, 1)',
'rgba(255, 159, 64, 1)'
],
borderWidth: 1
},
{
label: 'Height',
data: [170, 171, 171, 172, 172, 172, 172, 172, 172, 173, 173, 173],
backgroundColor: [
'rgba(255, 99, 132, 0.2)',
'rgba(54, 162, 235, 0.2)',
'rgba(255, 206, 86, 0.2)',
'rgba(75, 192, 192, 0.2)',
'rgba(153, 102, 255, 0.2)',
'rgba(255, 159, 64, 0.2)'
],
borderColor: [
'rgba(255, 99, 132, 1)',
'rgba(54, 162, 235, 1)',
'rgba(255, 206, 86, 1)',
'rgba(75, 192, 192, 1)',
'rgba(153, 102, 255, 1)',
'rgba(255, 159, 64, 1)'
],
borderWidth: 1
},
{
label: 'Fat Percentage',
data: [20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9],
backgroundColor: [
'rgba(255, 99, 132, 0.2)',
'rgba(54, 162, 235, 0.2)',
'rgba(255, 206, 86, 0.2)',
'rgba(75, 192, 192, 0.2)',
'rgba(153, 102, 255, 0.2)',
'rgba(255, 159, 64, 0.2)'
],
borderColor: [
'rgba(255, 99, 132, 1)',
'rgba(54, 162, 235, 1)',
'rgba(255, 206, 86, 1)',
'rgba(75, 192, 192, 1)',
'rgba(153, 102, 255, 1)',
'rgba(255, 159, 64, 1)'
],
borderWidth: 1
},
{
label: 'Muscle Mass',
data: [50, 52, 54, 56, 58, 60, 62, 64, 66, 68, 70, 72],
backgroundColor: [
'rgba(255, 99, 132, 0.2)',
'rgba(54, 162, 235, 0.2)',
'rgba(255, 206, 86, 0.2)',
'rgba(75, 192, 192, 0.2)',
'rgba(153, 102, 255, 0.2)',
'rgba(255, 159, 64, 0.2)'
],
borderColor: [
'rgba(255, 99, 132, 1)',
'rgba(54, 162, 235, 1)',
'rgba(255, 206, 86, 1)',
'rgba(75, 192, 192, 1)',
'rgba(153, 102, 255, 1)',
'rgba(255, 159, 64, 1)'
],
borderWidth: 1
}
]
},
options:
{
scales:
{
y: [{
ticks:
{
beginAtZero: true
}
}]
}
}
}"; //end of chartdata
            Chart = JsonConvert.DeserializeObject<ChartJs>(chartData);
            ChartJson = JsonConvert.SerializeObject(Chart, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });
        } //end of OnGet()
        public IActionResult OnPostM1()
        {

            start_date= DateTime.Now;
            string username = Request.Form["username"];
            int day = start_date.Day;
            int month = start_date.Month;
            int year = start_date.Year;
            string date = month+ "-" + day+ "-" + year;
            Random random = new Random();
            int randomNumber = random.Next(1, 100000);
            db.add_subscribtion_to_user(username, randomNumber,"150","1 month",date);
            return RedirectToPage("/homepage_2", new { username_coming_from_login = username });
        }
        public IActionResult OnPostM3()
        {
            string username = Request.Form["username"];
            start_date = DateTime.Now;
            int day = start_date.Day;
            int month = start_date.Month;
            int year = start_date.Year;
            string date = month + "-" + day + "-" + year;
            Random random = new Random();
            int randomNumber = random.Next(1, 100000);
            db.add_subscribtion_to_user(username, randomNumber, "250", "3 month", date);
            return RedirectToPage("/homepage_2", new { username_coming_from_login = username });
            //return Page();
        }
        public IActionResult OnPostM6()
        {
            string username = Request.Form["username"];

            start_date = DateTime.Now;
            int day = start_date.Day;
            int month = start_date.Month;
            int year = start_date.Year;
            string date = month + "-" + day + "-" + year;
            Random random = new Random();
            int randomNumber = random.Next(1, 100000);
            db.add_subscribtion_to_user(username, randomNumber, "450", "6 month", date);
            return RedirectToPage("/homepage_2", new { username_coming_from_login = username });
            //return Page();

        }
        public IActionResult OnPostYear_subs()
        {
            string username = Request.Form["username"];

            start_date = DateTime.Now;
            int day = start_date.Day;
            int month = start_date.Month;
            int year = start_date.Year;
            string date = month + "-" + day + "-" + year;
            Random random = new Random();
            int randomNumber = random.Next(1, 100000);
            db.add_subscribtion_to_user(username, randomNumber, "850", "1 year", date);
            return RedirectToPage("/homepage_2", new { username_coming_from_login = username });
            //return Page();
        }
        public IActionResult OnPostSubmit()
        {
            int Cap_rate =int.Parse( Request.Form["captains"]);
            int Fac_rate =int.Parse( Request.Form["captains"]);
            string comment = Request.Form["comment"];
            var username = Request.Form["username"];
            db.add_feedback_from_user(username,Fac_rate,Cap_rate,comment);
            return RedirectToPage("/homepage_2", new { username_coming_from_login = username });
        }
    }
}
