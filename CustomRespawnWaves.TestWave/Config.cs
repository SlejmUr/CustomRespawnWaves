using CustomRespawnWaves.ScpWave;
using CustomRespawnWaves.TestWave.CustomChaos;

namespace CustomRespawnWaves;

internal class Config
{
    public ScpWaveConfig ScpWaveConfig { get; set; } = new();

    public CIResponseConfig CIResponseConfig { get; set; } = new();
}
