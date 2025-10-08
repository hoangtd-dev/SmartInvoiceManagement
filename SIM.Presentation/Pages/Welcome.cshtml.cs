using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SIM.Presentation.Pages
{
    public class WelcomeModel : PageModel
    {
        public string TitleText { get; private set; } = "Welcome";
        public void OnGet()
        {
            ViewData["Title"] = TitleText;
            ViewData["MainContentExists"] = true;
        }
    }
}
