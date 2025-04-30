using Respawning.Config;

namespace CustomRespawnWaves.Configs;

public class CustomWaveConfig : IWaveConfig
{
    public virtual string Name { get; set; }
    public virtual bool IsEnabled { get; set; }
    public virtual int MaxWaveSize { get; set; }
    public virtual bool UseSizePercentage { get; set; }
    public virtual float SizePercentage { get; set; }
}
