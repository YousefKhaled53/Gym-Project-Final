using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;

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
        public DataTable dt_for_machines { get; set; }
        public DataTable dt_Time_slots { get; set; }
        public DataTable dt_Plans{ get; set; }
       
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
            dt_for_machines = db.gym_machines();

            dt_Time_slots = db.Time_slots();
           dt_Plans=db.plans();
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
        public IActionResult OnPostDeletemachine()
        {
            db.deletemachine(Request.Form["machineno"]);
            return RedirectToPage("/homepage_3", new { username_coming_from_login = Request.Form["un"] });

        }
        public IActionResult OnPostSaveplan()
        {
            string vs = Request.Form["vs"];
            string vw = Request.Form["vw"];
            string n = Request.Form["n"];
            db.editplan(vs,vw,n);
            return RedirectToPage("/homepage_3", new { username_coming_from_login = Request.Form["cname"] });
        }
        public IActionResult OnPostSaveschedule()
        {
            string day = Request.Form["day"];
            string[] dayArray = day.Split(',');

            string holiday = Request.Form["holiday"];
            string[] holidayArray = holiday.Split(',');
            string wh_from = Request.Form["wh_from"];
            string[] wh_fromArray = wh_from.Split(',');
            string wh_to = Request.Form["wh_to"];
            string[] wh_toArray = wh_to.Split(',');
            string w_slots_from = Request.Form["w_slots_from"];
            string[] w_slots_fromArray = w_slots_from.Split(',');
            string w_slots_to = Request.Form["w_slots_to"];
            string[] w_slots_toArray = w_slots_to.Split(',');
            DataTable schedule = new DataTable();

            // Create columns
            schedule.Columns.Add("Day", typeof(string));
            schedule.Columns.Add("Holiday", typeof(string));
            schedule.Columns.Add("Work Hours From", typeof(string));
            schedule.Columns.Add("Work Hours To", typeof(string));
            schedule.Columns.Add("Slot From", typeof(string));
            schedule.Columns.Add("Slot To", typeof(string));

            // Add rows 
            for (int i=0;i<5; i++)
            {
                schedule.Rows.Add(dayArray[i], holidayArray[i], wh_fromArray[i], wh_toArray[i],  w_slots_fromArray[i], w_slots_toArray[i]);

            }
            
            db.editslots(schedule);
            return RedirectToPage("/homepage_3", new { username_coming_from_login = Request.Form["capname"] });
        }
        public IActionResult OnPostEditmachhine()
        {
            return RedirectToPage("/homepage_3", new { username_coming_from_login = Request.Form["capname"] });
        }
    }
}
