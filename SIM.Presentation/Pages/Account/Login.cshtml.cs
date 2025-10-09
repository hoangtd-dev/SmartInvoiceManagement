using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel? Input { get; set; }

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
                return RedirectToPage("/Index", new { userId = user.Id });
            }
            catch (Exception ex)
            {
                // Map domain errors to model state
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Email is required")]
            [EmailAddress (ErrorMessage = "Invalid email address")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            public string? Password { get; set; }
        }
    }
}
