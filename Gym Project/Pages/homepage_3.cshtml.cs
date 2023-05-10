using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Gym_Project.Pages
{
    public class homepage_3Model : PageModel
    {
        private readonly GYM_DB db;

        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string username_coming_from_login { get; set; }

        public DataTable dt { get; set; }
        private readonly ILogger<IndexModel> _logger;
        public homepage_3Model(ILogger<IndexModel> logger, GYM_DB db)
        {
            _logger = logger;
            this.db = db;
        }
        public void OnGet()
        {
            dt = db.return_users_Feedback();
        }
    }
}
