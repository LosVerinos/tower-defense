public class SpecialWaveFactory : WaveFactory
{
    public override Wave CreateWave(int waveNumber)
    {
        Wave wave = new StandardWaveFactory().CreateWave(waveNumber);
        wave.isSpecialWave = true;
        
        int specialType = UnityEngine.Random.Range(0, 5);
        switch (specialType)
        {
            case 0: wave.specialWaveType = "Sprint"; wave.zombieRatios.Clear(); wave.zombieRatios.Add(1, 1.0f); break;
            case 1: wave.specialWaveType = "Tank"; wave.zombieRatios.Clear(); wave.zombieRatios.Add(2, 1.0f); break;
            case 2: wave.specialWaveType = "Tsunami"; wave.count *= 2; break;
            case 3: wave.specialWaveType = "Chaotique"; RandomizeWave(wave); break;
            case 4: wave.specialWaveType = "Volants"; wave.zombieRatios.Clear(); wave.zombieRatios.Add(3, 1.0f); break;
        }

        return wave;
    }

    private void RandomizeWave(Wave wave)
    {
        wave.zombieRatios.Clear();
        wave.zombieRatios.Add(0, UnityEngine.Random.Range(0.3f, 0.5f));
        wave.zombieRatios.Add(1, UnityEngine.Random.Range(0.2f, 0.4f));
        wave.zombieRatios.Add(2, UnityEngine.Random.Range(0.1f, 0.3f));
        wave.zombieRatios.Add(3, UnityEngine.Random.Range(0.1f, 0.2f));
    }
}
