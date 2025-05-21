using CustomRespawnWaves.Configs;

namespace CustomRespawnWaves.TestWave.CustomChaos;

internal class CIResponseConfig : CustomWaveConfig
{
    public override string Name { get; set; } = "SCPWAVE";
    public override bool IsEnabled { get; set; } = true;
    public override int MaxWaveSize { get; set; } = 10;

    public int CommandoCount { get; set; } = 3;
    public int ReconCount { get; set; } = 2;
    public int BreacherCount { get; set; } = 2;
    public int AgentCount { get; set; } = 2;
    public int LeaderCount { get; set; } = 1;

    public bool UseCustomNameString { get; set; } = false;

    public string Command { get; set; } = string.Empty;

    public string CommandFormat { get; set; } = string.Empty;

    public Dictionary<string, int> CustomRoleNameToId { get; set; } = new()
    {
        ["Commando"] = 1,
        ["Recon"] = 2,
        ["Breacher"] = 3,
        ["Agent"] = 4,
        ["Leader"] = 5,
    };

    public Dictionary<string, string> CustomRoleNameToStringId { get; set; } = new()
    {
        ["Commando"] = string.Empty,
        ["Commando"] = string.Empty,
        ["Recon"] = string.Empty,
        ["Breacher"] = string.Empty,
        ["Agent"] = string.Empty,
        ["Leader"] = string.Empty,
    };


    // fallback
    public Dictionary<string, List<ItemType>> RoleNameToInventory { get; set; } = new()
    {
        ["Commando"] = [ItemType.GunCrossvec, ItemType.Flashlight, ItemType.Medkit, ItemType.Adrenaline, ItemType.Radio, ItemType.ArmorCombat],
        ["Commando"] = [ItemType.GunCrossvec, ItemType.Flashlight, ItemType.Medkit, ItemType.Adrenaline, ItemType.Radio, ItemType.ArmorCombat],
        ["Recon"] = [ItemType.GunE11SR, ItemType.Flashlight, ItemType.Medkit, ItemType.Adrenaline, ItemType.Radio, ItemType.ArmorCombat],
        ["Breacher"] = [ItemType.GunShotgun, ItemType.Flashlight, ItemType.Medkit, ItemType.Adrenaline, ItemType.Radio, ItemType.ArmorCombat],
        ["Agent"] = [ItemType.GunAK, ItemType.Flashlight, ItemType.Medkit, ItemType.Adrenaline, ItemType.Radio, ItemType.ArmorCombat],
        ["Leader"] = [ItemType.GunAK, ItemType.Flashlight, ItemType.Medkit, ItemType.Adrenaline, ItemType.Radio, ItemType.ArmorCombat],
    };
}
