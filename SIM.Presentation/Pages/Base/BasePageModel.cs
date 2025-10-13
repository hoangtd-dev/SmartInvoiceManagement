using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace SIM.Presentation.Pages.Base
{
    public class BasePageModel : PageModel
    {
        public BasePageModel()
        {
        }

        public bool IsAuthenticated => User.Identity!.IsAuthenticated;
        public int CurrentUserId => int.Parse(User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
    }
}
