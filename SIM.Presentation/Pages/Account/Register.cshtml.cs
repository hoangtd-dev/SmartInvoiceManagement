using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SIM.Presentation.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Dashboard");
        }
    }
}
