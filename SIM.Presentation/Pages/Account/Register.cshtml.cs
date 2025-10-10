using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]

        public RegisterInputModel Input { get; set; }
        private readonly IAuthService _authService;

        public RegisterModel(IAuthService authService)
        {
            _authService = authService;
        }

        public void OnGet()
        {
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

                var user = await _authService.RegisterAsync(Input.FirstName!, Input.LastName!, Input.Email!, Input.Password!);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToPage("/Dashboard");
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
