namespace BattleGame.Api.Models;

public class Asset
{
    public Guid AssetId { get; set; }
    public string AssetName { get; set; } = default!;
    public int LevelRequire { get; set; }

    public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
namespace BattleGame.Api.Models;

public class Asset
{
    public Guid AssetId { get; set; }
    public string AssetName { get; set; } = default!;
    public int LevelRequire { get; set; }

    public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
