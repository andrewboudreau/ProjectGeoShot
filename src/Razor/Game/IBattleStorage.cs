namespace ProjectGeoShot.Web.Features.Game;

public interface IBattleStorage
{
    Task SaveAsync(Battle battle, CancellationToken ct = default);
    Task<Battle?> LoadAsync(Guid battleId, CancellationToken ct = default);
}
