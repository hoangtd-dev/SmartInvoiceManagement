using Microsoft.AspNetCore.Mvc;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages
{
    public class WelcomeModel : BasePageModel
    {
        public string TitleText { get; private set; } = "Welcome";
        public string LoginPage { get; private set; } = "/Login";
        public string RegisterPage { get; private set; } = "/Register";
        public IActionResult OnGet()
        {
            if (IsAuthenticated) return RedirectToPage("/Dashboard/Index");

            ViewData["Title"] = TitleText;
            ViewData["MainContentExists"] = true;
            return Page();
        }
    }
}
