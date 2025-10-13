using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SIM.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel() { }

        public IActionResult OnGet()
        {
            if (!(User?.Identity?.IsAuthenticated ?? false))
            {
               return RedirectToPage("/Welcome");
            }
            else
            {
               return RedirectToPage("/Dashboard/Index");
            }
        }
    }
}
