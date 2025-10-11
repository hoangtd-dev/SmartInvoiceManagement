using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Enums;
using SIM.Core.Exceptions;
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
            try
            {
                var userId = 1; // TODO: Update when Authen finish
                UserInfo = await _userService.GetUserById(userId);

            }
            catch (NotFoundException ex)
            {
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                TempData["ToastMessage"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                TempData["ToastMessage"] = "System Error !!!";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
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
                TempData["ToastStatus"] = ToastStatusEnum.Success;
                TempData["ToastMessage"] = "User Info updated successfully!";
            }
            catch (NotFoundException ex)
            {
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                TempData["ToastMessage"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["ToastStatus"] = ToastStatusEnum.Fail;
                TempData["ToastMessage"] = "System Error !!!";
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Welcome");
        }
    }
}
