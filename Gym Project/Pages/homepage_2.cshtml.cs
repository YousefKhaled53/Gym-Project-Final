using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gym_Project.Pages
{
    public class homepage_2Model : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

     
        public body_info body_info;
        private readonly GYM_DB db;

        [BindProperty(SupportsGet = true)]
        public string username_coming_from_login { get; set; }    
        public homepage_2Model(ILogger<IndexModel> logger, GYM_DB db)
        {
            _logger = logger;
            this.db = db;
        }

        public void OnGet()
        {
            body_info= new body_info(username_coming_from_login,db.getweight(username_coming_from_login),db.getheight(username_coming_from_login),db.getmusclemass(username_coming_from_login),db.getfatpercentage(username_coming_from_login));
        }
    }
}
