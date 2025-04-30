using PlayerRoles;
using PlayerStatsSystem;
using Respawning.Objectives;

namespace CustomRespawnWaves.TestWave.ScpWave.Objectives;

public class ScpKillObjective : CustomScpObjectiveBase<KillObjectiveFootprint>
{
    public override int FakeObjectiveIndex => 0;

    public float ScpTimeReward = -20;
    public float ZombieTimeReward = -5;
    public float FallbackTimeReward = -5;
    public float ScpInfluenceReward = 5;
    public float ZombieInfluenceReward = 3;
    public float FallbackInfluenceReward = 1;

    protected override KillObjectiveFootprint ClientCreateFootprint()
    {
        return new KillObjectiveFootprint();
    }

    public override void OnInstanceCreated()
    {
        base.OnInstanceCreated();
        PlayerStats.OnAnyPlayerDied += PlayerStats_OnAnyPlayerDied;
    }

    private void PlayerStats_OnAnyPlayerDied(ReferenceHub victim, DamageHandlerBase damage)
    {
        if (damage is not AttackerDamageHandler attackerDamage)
            return;
        if (attackerDamage.Attacker.Hub == null)
            return;
        var attacker = attackerDamage.Attacker;
        RoleTypeId role = attacker.Role;
        Faction faction = role.GetFaction();
        if (!IsValidFaction(faction) || !IsValidEnemy(role, victim))
        {
            return;
        }
        float timereward;
        float influence;
        if (victim.IsSCP(false))
        {
            timereward = ScpTimeReward;
            influence = ScpInfluenceReward;
        }
        else if (victim.roleManager.CurrentRole.RoleTypeId == RoleTypeId.Scp0492)
        {
            timereward = ZombieTimeReward;
            influence = ZombieInfluenceReward;
        }
        else
        {
            timereward = FallbackTimeReward;
            influence = FallbackInfluenceReward;
        }
        GrantInfluence(faction, influence);
        ReduceTimer(faction, timereward);
        ObjectiveFootprint = new KillObjectiveFootprint
        {
            InfluenceReward = influence,
            TimeReward = timereward,
            AchievingPlayer = new ObjectiveHubFootprint(attacker),
            VictimFootprint = new ObjectiveHubFootprint(victim, RoleTypeId.None)
        };
        ServerSendUpdate();
    }

    private bool IsValidEnemy(RoleTypeId role, ReferenceHub victim)
    {
        return HitboxIdentity.IsEnemy(role, victim.GetRoleId());
    }

    public override void ParseFakeObject(FactionObjectiveBase factionObjective)
    {
        HumanKillObjective humanKill = (HumanKillObjective)factionObjective;
        humanKill.ObjectiveFootprint = ObjectiveFootprint;
    }
}
