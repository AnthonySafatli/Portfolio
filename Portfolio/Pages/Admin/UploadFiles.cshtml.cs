using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages.Admin;

[Authorize]
public class UploadFilesModel : PageModel
{
    public void OnGet()
    {
    }
}
