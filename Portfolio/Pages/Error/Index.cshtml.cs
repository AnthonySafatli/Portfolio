using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages.Error
{
    public class IndexModel : PageModel
    {
        public int StatusCode { get; private set; }

        public void OnGet(int? code)
        {
            StatusCode = code ?? 0;
        }

    }
}
