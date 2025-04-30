using Respawning;
using Respawning.Objectives;
using Respawning.Waves;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CustomRespawnWaves;

public static class CustomWaves
{
    public static ReadOnlyCollection<FactionObjectiveBase> OriginalObjectives { get; private set; }
    public static ReadOnlyCollection<SpawnableWaveBase> OriginalWaves { get; private set; }
    internal static void Init()
    {
        OriginalObjectives = new(FactionInfluenceManager.Objectives);
        OriginalWaves = new(WaveManager.Waves);
        RespawnTokensManager.Milestones.Add(PlayerRoles.Faction.SCP, RespawnTokensManager.DefaultMilestone);
        RespawnTokensManager.Milestones.Add(PlayerRoles.Faction.Flamingos, RespawnTokensManager.DefaultMilestone);
    }
    internal static void UnInit()
    {
        FactionInfluenceManager.Objectives.Clear();
        FactionInfluenceManager.Objectives.AddRange(OriginalObjectives);
        WaveManager.Waves.Clear();
        WaveManager.Waves.AddRange(OriginalWaves);
        OriginalObjectives = new([]);
        OriginalWaves = new([]);
        // This unregister with a shadow copy so should work
        CustomTimeBasedWave.CustomWaves.ToList().ForEach(UnRegisterWave); 
        CustomTimeBasedWave.CustomWaves.Clear();
    }

    public static void RegisterWave()
    {
        Assembly assembly = Assembly.GetCallingAssembly();
        List<Type> types = [.. assembly.GetTypes().
            Where(item =>
                !item.IsAbstract &&
                typeof(CustomTimeBasedWave).IsAssignableFrom(item)
                )];
        RegisterWave([..types]);
    }

    public static void RegisterWave(params Type[] types)
    {
        foreach (var item in types)
        {
            RegisterWave((CustomTimeBasedWave)Activator.CreateInstance(item));
        }
    }

    public static void RegisterWave(this CustomTimeBasedWave wave)
    {
        if (wave == null)
            return;
        wave.Init();
        WaveManager.Waves.Add(wave);
        CustomTimeBasedWave.CustomWaves.Add(wave);
    }

    public static void UnRegisterWave()
    {
        Assembly assembly = Assembly.GetCallingAssembly();
        List<Type> types = [.. assembly.GetTypes().
            Where(item =>
                !item.IsAbstract &&
                typeof(CustomTimeBasedWave).IsAssignableFrom(item)
                )];
        UnRegisterWave([.. types]);
    }

    public static void UnRegisterWave(params Type[] types)
    {
        foreach (var item in types)
        {
            UnRegisterWave(item);
        }
    }

    public static void UnRegisterWave(Type type)
    {
        foreach (var item in CustomTimeBasedWave.CustomWaves.Where(x => x.GetType() == type).ToList())
        {
            UnRegisterWave(item);
        }
    }

    public static void UnRegisterWave(this CustomTimeBasedWave wave)
    {
        if (wave == null) 
            return;
        wave.Destroy();
        WaveManager.Waves.Remove(wave);
        CustomTimeBasedWave.CustomWaves.Remove(wave);
    }
}
