using PlayerRoles;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Respawning.Objectives;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public abstract class CustomScpObjectiveBase<T> : CustomObjectiveBase<T> where T : ObjectiveFootprintBase
{
    public override bool IsValidFaction(Faction faction)
    {
        return faction == Faction.SCP;
    }
}
