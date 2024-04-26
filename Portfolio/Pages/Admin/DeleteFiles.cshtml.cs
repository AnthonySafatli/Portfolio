using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Data;

namespace Portfolio.Pages.Admin;

[Authorize]
public class DeleteFilesModel : PageModel
{
    public void OnGet()
    {
        // Get all files in 'projects' folder
        // see if they are used in projects db
        // display all items with a delete button
        // post
        // delete file
    }
}
