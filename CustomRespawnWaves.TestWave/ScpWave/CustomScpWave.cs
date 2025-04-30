using CustomRespawnWaves.TestWave.ScpWave;
using MapGeneration;
using MEC;
using PlayerRoles;
using Respawning;
using Respawning.Announcements;
using Respawning.Config;
using Respawning.Waves;
using Respawning.Waves.Generic;
using UnityEngine;

namespace CustomRespawnWaves.ScpWave;

public class CustomScpWave : CustomTimeBasedWave, IAnnouncedWave, ILimitedWave, ICustomMilestone
{
    public override string Name => "SCPWAVE";
    public override Faction TargetFaction => Faction.SCP;
    public WaveAnnouncementBase Announcement => new ScpAnnouncement();
    public int InitialRespawnTokens { get; set; } = 1;
    public int RespawnTokens { get; set; }
    public List<RespawnTokensManager.Milestone> Milestones { get; set; } = [];

    public override IWaveConfig Configuration => Main.Instance.Config.ScpWaveConfig;

    public override void Init()
    {
        if (Configuration is not ScpWaveConfig scpWaveConfig)
            return;
        InitialRespawnTokens = scpWaveConfig.InitialTokens;
        foreach (var item in scpWaveConfig.MilestoneValues)
        {
            Milestones.Add(new(item));
        }
    }

    public override void PopulateQueue(Queue<RoleTypeId> queueToFill, int playersToSpawn)
    {
        RoleTypeId roleToSpawn = RoleTypeId.Scp0492; // fallback role;
        if (Configuration is ScpWaveConfig config)
            roleToSpawn = config.RoleToSpawn;
        for (int i = 0; i < playersToSpawn; i++)
        {
            queueToFill.Enqueue(roleToSpawn);
        }
    }

    public void TryAchieveMilestone(float influence)
    {
        foreach (var milestone in Milestones)
        {
            if (!milestone.Achieved && milestone.Threshold <= influence)
            {
                milestone.Achieved = true;
                RespawnTokens++;
                WaveUpdateMessage.ServerSendUpdate(this, UpdateMessageFlags.Tokens);
                break;
            }
        }
    }

    public override void WaveSpawned(List<Player> spawnedPlayers)
    {
        var scps = Player.ReadyList.Where(x => x.IsSCP && !spawnedPlayers.Contains(x) && x.Role != RoleTypeId.Scp079);
        var no_zombies = scps.Where(x=>x.Role != RoleTypeId.Scp0492);
        Vector3 pos = Vector3.zero;
        if (no_zombies.Count() != 0)
        {
            scps = no_zombies;
        }
        if (scps.Count() == 0)
        {
            // TODO: Better spawn when no scp?
            var Surface = Room.Get(FacilityZone.Surface).First();
            var nukeDoor = Surface.Doors.FirstOrDefault(x=>x.DoorName == LabApi.Features.Enums.DoorName.SurfaceNuke);
            pos = nukeDoor.Position;
            pos.y -= 3;
        }
        else
        {
            var randomScp = scps.ToList().RandomItem();
            pos = randomScp.Position;
        }
        
        foreach (var item in spawnedPlayers)
        {
            item.IsGodModeEnabled = true;
            item.Position = pos;
            Timing.CallDelayed(0.3f, () => item.IsGodModeEnabled = false);
        }
    }
}
