using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gym_Project.Pages
{
    public class homepage_3Model : PageModel
    {
        [BindProperty(SupportsGet = true)]
        [ViewData]
        public string username_coming_from_login { get; set; }
        public void OnGet()
        {
        }
    }
}
