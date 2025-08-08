namespace BattleGame.Api.Models;

public class Player
{
    public Guid PlayerId { get; set; }
    public string PlayerName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public int Level { get; set; }
    public string? Email { get; set; }

    public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
