using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GeoShot.Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public double Lat { get; set; } = 37.7749; // default lat

        [BindProperty(SupportsGet = true)]
        public double Lon { get; set; } = -122.4194; // default lon

        public void OnGet()
        {
        }
    }
}
