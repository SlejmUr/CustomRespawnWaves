using HarmonyLib;
using Respawning;
using Respawning.Waves;

namespace CustomRespawnWaves.Patches;

[HarmonyPatch(nameof(WaveUpdateMessage), nameof(WaveUpdateMessage.ServerSendUpdate))]
internal class WaveUpdateMessage_ServerSendUpdate_Patch
{
    public static bool Prefix(SpawnableWaveBase wave)
    {
        return wave is not CustomTimeBasedWave;
    }
}
