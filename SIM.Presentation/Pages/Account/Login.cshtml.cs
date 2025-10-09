using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginInputModel Input { get; set; }
        private readonly IAuthService _authService;

        public LoginModel(IAuthService authService)
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

                var user = await _authService.LoginAsync(Input.Email!, Input.Password!);

                // create claims and sign in
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Redirect to Index. Include userId in query string so the client can store it if desired.
                return RedirectToPage("/Dashboard", new { userId = user.Id });
            }
            catch (ArgumentException ex)
            {
                // Map domain error codes from service
                switch (ex.Message)
                {
                    case "EmailNotFound":
                        ModelState.AddModelError(string.Empty, "Email address not found.");
                        break;
                    case "PasswordIncorrect":
                        ModelState.AddModelError(string.Empty, "Password is incorrect.");
                        break;
                    default:
                        ModelState.AddModelError(string.Empty, "Authentication failed.");
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
