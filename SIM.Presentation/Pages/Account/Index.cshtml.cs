using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages.Account
{
    public class AccountModel : PageModel
    {
        [BindProperty]
        public UserModel? UserInfo { get; set; }

        private readonly IUserService _userService;
        public AccountModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = 1; // TODO: Update when Authen finish
            UserInfo = await _userService.GetUserById(userId);
            return Page();
        }

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UserInfo == null)
            {
                ModelState.AddModelError(string.Empty, "User info is missing.");
                return Page();
            }

            var updateUser = new UpdateUserRequest
            {
                Id = 1, // TODO: Update when Authen finish
                Address = UserInfo.Address,
                Phone = UserInfo.Phone,
                Email = UserInfo.Email,
                Lastname = UserInfo.LastName,
                Firstname = UserInfo.FirstName,
            };
            await _userService.UpdateUser(updateUser);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Welcome");
        }
    }
}
