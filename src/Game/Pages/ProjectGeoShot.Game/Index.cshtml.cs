using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectGeoShot.Game.Pages;

public class IndexModel : PageModel
{
    private readonly IBattleStorage storage;

    public IndexModel(IBattleStorage storage)
    {
        this.storage = storage;
    }

    public async Task OnGet()
    {
        await storage.SaveAsync(new Battle
        {
            Id = Guid.NewGuid(),
            Name = "Test Battle"
        });
    }
}