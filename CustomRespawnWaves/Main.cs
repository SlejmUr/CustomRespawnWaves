using HarmonyLib;
using LabApi.Loader.Features.Plugins;

namespace CustomRespawnWaves;

internal sealed class Main : Plugin
{
    public static Main Instance { get; private set; }
    #region Plugin Info
    public override string Author => "SlejmUr";
    public override string Name => "CustomRespawnWaves";
    public override Version Version => new(0, 1);
    public override string Description => "Adding custom Respawn Wave API to SCP:SL";
    public override Version RequiredApiVersion => LabApi.Features.LabApiProperties.CurrentVersion;
    #endregion

    private Harmony Harmony;

    public override void Enable()
    {
        Instance = this;
        Harmony = new($"{Name}_{DateTime.Now}");
        Harmony.PatchAll();
        CustomWaves.Init();
    }


    public override void Disable()
    {
        Harmony.UnpatchAll(Harmony.Id);
        CustomWaves.UnInit();
        Instance = null;
    }
}
