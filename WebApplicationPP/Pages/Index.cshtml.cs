using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationPP.Pages
{
    public class IndexModel : PageModel
    {
        public int Counter { get; private set; }
        public void OnGet()
        {
            Counter = HttpContext.Session.GetInt32("Counter") ?? 0;
        }
        public void OnPost()
        {
            Counter = HttpContext.Session.GetInt32("Counter") ?? 1;
            Counter++;
            HttpContext.Session.SetInt32("Counter", Counter);
        }
    }
}
