using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Portfolio.Pages;

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

        Range[] ranges = {
            // Education
            new Range(306, 611, "elem"),
            new Range(620, 767, "junior"),
            new Range(776, 924, "high"),
            new Range(933, 1132, "dal"),

            // Career
            new Range(711, 933, "jakes"),
            new Range(937, WeeksLived, "medit"),

            // Hobbies
            //new Range("coding"), // TODO: Finish weeks
        };

        for (int i = 0; i < Weeks; i++)
        {
            ClassList[i] = "week";
            if (i > WeeksLived)
            {
                ClassList[i] += " future-week";
            }

            foreach (Range range in ranges)
            {
                if (range.inRange(i)) 
                {
                    ClassList[i] += " " + range;
                }
            }
        }
    }
}

struct Range
{
    private int start;
    private int end;
    private string cssClass;

    public Range(int start, int end, string cssClass)
    {
        this.start = start;
        this.end = end;
        this.cssClass = cssClass;
    }

    public Range(int num, string cssClass)
    {
        this.start = num;
        this.end = num;
        this.cssClass = cssClass;
    }

    public bool inRange(int i) => start <= i && i <= end;

    public override string ToString()
    {
        return cssClass;
    }
}
