namespace BattleGame.Api.Models;

public class PlayerAsset
{
    public Guid PlayerId { get; set; }
    public Player Player { get; set; } = default!;
    public Guid AssetId { get; set; }
    public Asset Asset { get; set; } = default!;
}
