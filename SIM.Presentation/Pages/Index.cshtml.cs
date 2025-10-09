using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SIM.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel() { }

        public IActionResult OnGet()
        {
            // If the user is not authenticated, send them to the public welcome page.
            if (!(User?.Identity?.IsAuthenticated ?? false))
            {
                return RedirectToPage("/Welcome");
            }

            // Authenticated users remain on the dashboard (render Index)
            return Page();
        }
    }
}
