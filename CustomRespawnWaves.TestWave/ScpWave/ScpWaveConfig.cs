using CustomRespawnWaves.Configs;
using PlayerRoles;

namespace CustomRespawnWaves.ScpWave;

public sealed class ScpWaveConfig : CustomWaveConfig
{
    public override string Name { get; set; } = "SCPWAVE";
    public override bool IsEnabled { get; set; } = true;
    public RoleTypeId RoleToSpawn { get; set; } = RoleTypeId.Scp0492;
    public override int MaxWaveSize { get; set; } = 5;
    public int InitialTokens { get; set; } = 2;

    public List<int> MilestoneValues { get; set; } = 
    [
        40,
        70,
        90,
        130,
        160,
        200
    ];
}
