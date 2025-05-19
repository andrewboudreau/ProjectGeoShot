using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectGeoShot.Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        // Default to 3647 N Saint Louis Ave, Chicago IL 60618
        public double Lat { get; set; } = 41.9464;

        [BindProperty(SupportsGet = true)]
        public double Lon { get; set; } = -87.7159;

        public void OnGet()
        {
        }
    }
}
