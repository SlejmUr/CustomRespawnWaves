using HarmonyLib;
using Respawning.Waves;

namespace CustomRespawnWaves.Patches;

[HarmonyPatch(nameof(RespawnWaves), nameof(RespawnWaves.Get))]
internal class RespawnWaves_Get_Patch
{
    public static void Postfix(SpawnableWaveBase baseWave, ref RespawnWave __result)
    {
        if (__result != null)
            return;
        var customWave = CustomTimeBasedWave.CustomWaves.FirstOrDefault(x=> x == baseWave);
        if (customWave == null)
            return;
        if (string.IsNullOrEmpty(customWave.Name))
            return;
        __result = new CustomWave(customWave);
    }
}
