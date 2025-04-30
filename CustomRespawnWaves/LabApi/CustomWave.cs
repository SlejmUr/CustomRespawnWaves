using CustomRespawnWaves.Configs;
using Respawning.Waves;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace LabApi.Features.Wrappers;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public class CustomWave(CustomTimeBasedWave wave) : RespawnWave(wave)
{
    public new CustomTimeBasedWave Base = wave;

    public override int MaxWaveSize
    {
        get
        {
            return this.Base.MaxWaveSize;
        }
        set
        {
            float num = (float)value / (float)ReferenceHub.AllHubs.Count;
            if (Base.Configuration is CustomWaveConfig config)
            {
                if (config.UseSizePercentage)
                    config.SizePercentage = num;
                else
                    config.MaxWaveSize = value;
            }
        }
    }
}
