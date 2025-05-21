using PlayerRoles;
using Respawning.Config;
using Respawning.Waves;

namespace CustomRespawnWaves.TestWave.CustomChaos;

public class CIResponse : CustomTimeBasedWave
{
    // Hopefully this is acurately 2 Minutes.
    public override float InitialSpawnInterval => 2 * 60 * 60;
    public override string Name => "CIResponse";

    public override Faction TargetFaction => Faction.FoundationEnemy;

    public override IWaveConfig Configuration => Main.Instance.Config.CIResponseConfig;

    public override void PopulateQueue(Queue<RoleTypeId> queueToFill, int playersToSpawn)
    {
        // We dont do anything here.
        for (int i = 0; i < playersToSpawn; i++)
        {
            queueToFill.Enqueue(RoleTypeId.ChaosRepressor);
        }
    }

    public override void WaveSpawned(List<Player> spawnedPlayers)
    {
        int CommandoCount = Main.Instance.Config.CIResponseConfig.CommandoCount;
        int ReconCount = Main.Instance.Config.CIResponseConfig.ReconCount;
        int BreacherCount = Main.Instance.Config.CIResponseConfig.BreacherCount;
        int AgentCount = Main.Instance.Config.CIResponseConfig.AgentCount;
        int LeaderCount = Main.Instance.Config.CIResponseConfig.LeaderCount;
        foreach (var item in spawnedPlayers)
        {
            if (LeaderCount != 0)
            {
                SetPlayerToRole(item, "Leader");
                LeaderCount--;
                continue;
            }
            if (AgentCount != 0)
            {
                SetPlayerToRole(item, "Agent");
                AgentCount--;
                continue;
            }
            if (BreacherCount != 0)
            {
                SetPlayerToRole(item, "Breacher");
                BreacherCount--;
                continue;
            }
            if (ReconCount != 0)
            {
                SetPlayerToRole(item, "Recon");
                ReconCount--;
                continue;
            }
            if (CommandoCount != 0)
            {
                SetPlayerToRole(item, "Commando");
                CommandoCount--;
                continue;
            }
        }
    }

    protected void SetPlayerToRole(Player player, string role)
    {
        if (!string.IsNullOrEmpty(Main.Instance.Config.CIResponseConfig.Command))
        {
            // This can run different commands,
            // example 1: cr 1 1 | exiled custom role set userid 1 to roleid 1
            // example 2: scr_seto CIR_Commando 1 | SimpleCustomRole set userid 1 to CIR_Commando role
            Server.RunCommand(string.Format(Main.Instance.Config.CIResponseConfig.CommandFormat,
                Main.Instance.Config.CIResponseConfig.Command,
                player.PlayerId,
                Main.Instance.Config.CIResponseConfig.UseCustomNameString ?
                Main.Instance.Config.CIResponseConfig.CustomRoleNameToStringId[role] :
                Main.Instance.Config.CIResponseConfig.CustomRoleNameToId[role]
            ));
            return;
        }
        var inv = Main.Instance.Config.CIResponseConfig.RoleNameToInventory[role];
        player.ClearInventory();
        foreach (var item in inv)
        {
            player.AddItem(item, InventorySystem.Items.ItemAddReason.StartingItem);
        }
        

    }
}
