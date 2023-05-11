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
            int[] weights_array = new int[12];
            int[] heights_array = new int[12];
            int[] fp_array = new int[12];
            int[] mm_array = new int[12];
            DateTime dateTime= DateTime.Now;
            int year = dateTime.Year;
            for (int i = 0; i < 12; i++)
            {
                string formatted_i = (i+1).ToString("D2");
                //int int_i = int.Parse(formatted_i);
                weights_array[i] = db.get_specif_atribute_from_bodyinfo("weight_", formatted_i,year.ToString() , username_coming_from_login);
                heights_array[i] = db.get_specif_atribute_from_bodyinfo("height", formatted_i, year.ToString(), username_coming_from_login);
                fp_array[i] = db.get_specif_atribute_from_bodyinfo("Fats_Percentage", formatted_i,year.ToString(), username_coming_from_login);
                mm_array[i] = db.get_specif_atribute_from_bodyinfo("Muscles_Percentage", formatted_i, year.ToString(), username_coming_from_login);
            }
            string chartData = @" 
                
            {
            type: 'line',
            responsive: true,
            data:
            {
            labels: ['January', 'February', 'March', 'April','May','June','July','August','September','October','November','December'],
            datasets: [
            {
            label: 'Weight',
            //data: [90, 2171, 71, 17, 122, 172, 172, 172, 172, 173, 173, 173],
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
            //data: [190, 2171, 71, 17, 122, 172, 172, 172, 172, 173, 173, 173],
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
            //data: [20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9],
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
            //data: [50, 52, 54, 56, 58, 60, 62, 64, 66, 68, 70, 72],
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
            // setting up datasets
            Dataset dt = new Dataset();
            Chart.data.datasets[0].data = weights_array;
            Chart.data.datasets[1].data = heights_array;
            Chart.data.datasets[2].data = fp_array;
            Chart.data.datasets[3].data = mm_array;


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
