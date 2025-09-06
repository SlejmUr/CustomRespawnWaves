using HarmonyLib;
using Respawning;
using Respawning.Waves;
using System.Reflection.Emit;

namespace CustomRespawnWaves.Patches;

[HarmonyPatch(typeof(RespawnTokensManager), nameof(RespawnTokensManager.OnPointsModified))]
internal class RespawnTokensManager_OnPointsModified_Patch
{
    public static IEnumerable<CodeInstruction> OnPointsModified_Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        var insertionIndex = instructions.ToList().FindIndex(x=>x.opcode == OpCodes.Stloc_1) + 2;

        var limitedWaveNullRet =  codes[insertionIndex];

        Label label = new();

        codes.InsertRange(insertionIndex,
        [
            new CodeInstruction(OpCodes.Brtrue_S, label),
            new CodeInstruction(OpCodes.Ret),
            new CodeInstruction(OpCodes.Ldloc_S, 0).WithLabels(label), // spawnableWaveBase
            new CodeInstruction(OpCodes.Ldarg_1), // newValue
            new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(RespawnTokensManager_OnPointsModified_Patch), nameof(CustomOnPointsModified))),
            new CodeInstruction(OpCodes.Brfalse_S, limitedWaveNullRet),
        ]);

        return codes;
    }

    public static bool CustomOnPointsModified(SpawnableWaveBase spawnableWaveBase, float newValue)
    {
        if (spawnableWaveBase is not ICustomMilestone customMilestone)
            return false;
        customMilestone.TryAchieveMilestone(newValue);
        return true;
    }
}
