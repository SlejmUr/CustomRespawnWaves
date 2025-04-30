#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Respawning;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public interface ICustomMilestone
{
    public List<RespawnTokensManager.Milestone> Milestones { get; set; }

    public void TryAchieveMilestone(float influence);
}
