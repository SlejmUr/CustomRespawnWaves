# CustomRespawnWaves
Customizable respawn waves for SCP:SL.

## How do I add new custom wave?
Use base class as `CustomTimeBasedWave`.

## How can I add my own already existing wave to the game?
Run `CustomWaves.RegisterWave();` on `Enabled` inside plugin loader.
Or run `myWaveInstanciated.RegisterWave();` works too.

## Custom Milestones?
Check CustomScpWave.