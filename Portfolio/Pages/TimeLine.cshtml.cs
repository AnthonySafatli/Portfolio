using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Portfolio.Pages
{
    public class TimeLineModel : PageModel
    {
        public int WeeksLived { get; set; }
        public int Weeks { get; set; } = 4693;
        public string[]? ClassList { get; set; }

        public void OnGet()
        {
            ClassList = new string[Weeks];

            DateTime birthday = new DateTime(2004, 10, 14);
            DateTime currentDate = DateTime.Today;

            TimeSpan timeSpan = currentDate - birthday;
            WeeksLived = (int)(timeSpan.TotalDays / 7);

            for (int i = 0; i < Weeks; i++)
            {
                ClassList[i] = "grid-item";
                if (i > WeeksLived)
                {
                    ClassList[i] += " future-week";
                }
                //if (i > 10 && i < 50)
                //{
                //    ClassList[i] += " elem";
                //}
                // more if's
            }

            // ClassList[10] += " birthday";
            // TODO: Add javascript for this
        }
    }
}
