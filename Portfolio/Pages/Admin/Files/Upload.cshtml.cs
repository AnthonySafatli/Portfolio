using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages.Admin.Files;

[Authorize]
public class UploadModel : PageModel
{
    public void OnGet()
    {
        // TODO: Make uploading
    }
}
