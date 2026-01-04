using Respawning.Announcements;
using Subtitles;
using System.Text;

namespace CustomRespawnWaves.TestWave.ScpWave;

public class ScpAnnouncement : WaveAnnouncementBase
{
    public override void CreateAnnouncement(StringBuilder builder, List<ReferenceHub> spawnedPlayers, out SubtitlePart[] subtitles)
    {
        builder.Append("Security Alert . Substantial SCP Activity Detected . All Personnel is in danger");
        subtitles =
        [
            new SubtitlePart(SubtitleType.Custom, ["security alert . substantial scp activity detected . all personnel is in danger."])
        ];
    }
}
