using HarmonyLib;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;

namespace CustomRespawnWaves;

internal sealed class Main : Plugin<Config>
{
    public static Main Instance { get; private set; }
    #region Plugin Info
    public override string Author => "SlejmUr";
    public override string Name => "CustomRespawnWaves.TestWave";
    public override Version Version => new(0, 1);
    public override string Description => "CustomRespawnWaves.TestWave";
    public override Version RequiredApiVersion => LabApi.Features.LabApiProperties.CurrentVersion;
    public override LoadPriority Priority => LoadPriority.Lowest;
    #endregion

    private Harmony Harmony;

    public override void Enable()
    {
        Instance = this;
        Harmony = new($"{Name}_{DateTime.Now}");
        Harmony.PatchAll();
        CustomWaves.RegisterWave();
    }


    public override void Disable()
    {
        Harmony.UnpatchAll(Harmony.Id);
        CustomWaves.UnRegisterWave();
        Instance = null;
    }
}
