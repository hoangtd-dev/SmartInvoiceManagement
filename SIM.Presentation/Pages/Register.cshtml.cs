using Microsoft.AspNetCore.Mvc;
using SIM.Core.Interfaces.Services;
using SIM.Presentation.Pages.Base;

namespace SIM.Presentation.Pages
{
    public class RegisterModel : BasePageModel
    {
        [BindProperty]

        public RegisterInputModel Input { get; set; }
        private readonly IAuthService _authService;

        public RegisterModel(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult OnGet()
        {
            if (IsAuthenticated) return RedirectToPage("/Dashboard/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (Input is null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid input.");
                    return Page();
                }

                await _authService.RegisterAsync(Input.FirstName!, Input.LastName!, Input.Email!, Input.Password!);
                return RedirectToPage("/Login");
            }
            catch (ArgumentException ex)
            {
                switch (ex.Message)
                {
                    case "EmailAlreadyExists":
                        ModelState.AddModelError(string.Empty, "Email already exists.");
                        break;
                    default:
                        ModelState.AddModelError(string.Empty, "Registration failed.");
                        break;
                }

                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An internal error occurred. Please try again later.");
                return Page();
            }
        }
    }
}
