using CustomRespawnWaves.Configs;
using UnityEngine;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Respawning.Waves;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public abstract class CustomTimeBasedWave : TimeBasedWave
{
    public static List<CustomTimeBasedWave> CustomWaves = [];
    public abstract string Name { get; }
    public override float InitialSpawnInterval => 250;
    public override int MaxWaveSize
    {
        get
        {
            if (Configuration is CustomWaveConfig config)
            {
                if (config.UseSizePercentage)
                    return Mathf.CeilToInt((float)Player.Count * config.SizePercentage);
                return config.MaxWaveSize;
            }
            return 0;
        }
    }

    public override void OnInstanceCreated()
    {
        base.OnInstanceCreated();
        LabApi.Events.Handlers.ServerEvents.WaveRespawned += ServerEvents_WaveRespawned;
    }

    protected void ServerEvents_WaveRespawned(LabApi.Events.Arguments.ServerEvents.WaveRespawnedEventArgs ev)
    {
        if (ev.Wave.Base != this)
            return;
        WaveSpawned([.. ev.Players]);
    }

    public override void OnInstanceDestroyed()
    {
        base.OnInstanceDestroyed();
        LabApi.Events.Handlers.ServerEvents.WaveRespawned -= ServerEvents_WaveRespawned;
    }

    public override void OnAnyWaveSpawned(SpawnableWaveBase wave, List<ReferenceHub> spawnedPlayers)
    {
        base.OnAnyWaveSpawned(wave, spawnedPlayers);
    }

    public virtual void WaveSpawned(List<Player> spawnedPlayers)
    {

    }

    public virtual void Init()
    {

    }
}
