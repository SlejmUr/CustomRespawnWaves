using Respawning.Announcements;
using Subtitles;
using System.Text;
using Utils.Networking;

namespace CustomRespawnWaves.TestWave.ScpWave;

public class ScpAnnouncement : WaveAnnouncementBase
{
    public override void CreateAnnouncementString(StringBuilder builder)
    {
        builder.Append("Security Alert . Substantial SCP Activity Detected . All Personnel is in danger");
    }

    public override void SendSubtitles()
    {
        // Subtitle here. dont know how to do valid one. sorry.
        //new SubtitleMessage([new SubtitlePart(SubtitleType.Custom, ["security alert . substantial scp activity detected . all personnel is in danger."])]).SendToAuthenticated();
    }
}
