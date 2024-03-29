using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolio.Pages
{
    public class TimeLineModel : PageModel
    {
        public int WeeksLived { get; set; }

        public void OnGet()
        {
            DateTime birthday = new DateTime(2004, 10, 14);
            DateTime currentDate = DateTime.Today;

            TimeSpan timeSpan = currentDate - birthday;
            WeeksLived = (int)(timeSpan.TotalDays / 7);
        }
    }
}
