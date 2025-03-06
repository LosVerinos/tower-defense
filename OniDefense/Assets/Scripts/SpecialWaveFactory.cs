public class SpecialWaveFactory : WaveFactory
{
    public override Wave CreateWave(int waveNumber)
    {
        WaveBuilder builder = new WaveBuilder(waveNumber);

        int specialType = UnityEngine.Random.Range(0, 5);
        switch (specialType)
        {
            case 0: 
                builder.SetSpecialWave("Sprint").AddZombieType(1, 1.0f); 
                break;
            case 1: 
                builder.SetSpecialWave("Tank").AddZombieType(2, 1.0f); 
                break;
            case 2:
                builder.SetSpecialWave("Tsunami").DoubleWaveCount().AddZombieType(0, 0.9f).AddZombieType(1, 0.1f); 
                break;
            case 3: 
                builder.SetSpecialWave("Chaotique");
                builder.AddZombieType(0, UnityEngine.Random.Range(0.3f, 0.5f));
                builder.AddZombieType(1, UnityEngine.Random.Range(0.2f, 0.4f));
                builder.AddZombieType(2, UnityEngine.Random.Range(0.1f, 0.3f));
                builder.AddZombieType(3, UnityEngine.Random.Range(0.1f, 0.2f));
                break;
            case 4: 
                builder.SetSpecialWave("Volants").AddZombieType(3, 1.0f); 
                break;
        }

        return builder.SetBossCount(waveNumber).Build();
    }

}
