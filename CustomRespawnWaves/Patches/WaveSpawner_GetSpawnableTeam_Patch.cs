using HarmonyLib;
using PlayerRoles;
using Respawning.Waves;

namespace CustomRespawnWaves.Patches;

[HarmonyPatch(nameof(WaveSpawner), nameof(WaveSpawner.GetSpawnableTeam))]
internal class WaveSpawner_GetSpawnableTeam_Patch
{
    public static void Postfix(Faction faction, ref Team __result)
    {
        if (__result != Team.OtherAlive)
        {
            return;
        }
        if (faction == Faction.SCP)
        {
            __result = Team.SCPs;
            return;
        }
        if (faction == Faction.Flamingos)
        {
            __result = Team.Flamingos;
            return;
        }
    }
}
