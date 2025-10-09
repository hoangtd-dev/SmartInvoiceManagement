using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SIM.Presentation.Pages
{
    public class WelcomeModel : PageModel
    {
        public string TitleText { get; private set; } = "Welcome";
        // Page links used by the Welcome.cshtml view
        public string LoginPage { get; private set; } = "/Account/Login";
        public string RegisterPage { get; private set; } = "/Account/Register";
        public void OnGet()
        {
            ViewData["Title"] = TitleText;
            ViewData["MainContentExists"] = true;
        }
    }
}
