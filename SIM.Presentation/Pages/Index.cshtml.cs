using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.DTOs.Responses;
using SIM.Core.Interfaces.Services;

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
                Console.WriteLine("User is not authenticated, redirecting to Welcome page.");
                return RedirectToPage("/Welcome");
            }

            // Authenticated users remain on the dashboard (render Index)
            return Page();
        }
    }
}
