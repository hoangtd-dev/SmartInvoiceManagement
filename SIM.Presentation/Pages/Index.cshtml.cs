using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIM.Core.DTOs.Responses;
using SIM.Core.Interfaces.Services;

namespace SIM.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel() { }

        public async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
