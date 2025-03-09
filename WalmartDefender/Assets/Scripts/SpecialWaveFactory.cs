
namespace Game
{
    public class SpecialWaveFactory : WaveFactory
    {
        public override Wave CreateWave(int waveNumber)
        {
            WaveBuilder builder = new WaveBuilder(waveNumber);

            int specialType = UnityEngine.Random.Range(0, 5);
            switch (specialType)
            {
                case 0:
                    builder.SetSpecialWave("Sprint").AddZombieType(new FastZombie().index, 1.0f);
                    break;
                case 1:
                    builder.SetSpecialWave("Tank").AddZombieType(new HeavyZombie().index, 1.0f);
                    break;
                case 2:
                    builder.SetSpecialWave("Tsunami").DoubleWaveCount().AddZombieType(new ClassicZombie().index, 0.9f).AddZombieType(new FastZombie().index, 0.1f);
                    break;
                case 3:
                    builder.SetSpecialWave("Chaotique");
                    builder.AddZombieType(new ClassicZombie().index, UnityEngine.Random.Range(0.3f, 0.5f));
                    builder.AddZombieType(new FastZombie().index, UnityEngine.Random.Range(0.2f, 0.4f));
                    builder.AddZombieType(new HeavyZombie().index, UnityEngine.Random.Range(0.1f, 0.3f));
                    builder.AddZombieType(new ExplosiveZombie().index, UnityEngine.Random.Range(0.1f, 0.2f));
                    break;
                case 4:
                    builder.SetSpecialWave("Volants").AddZombieType(3, 1.0f);
                    break;
            }

            return builder.SetBossCount(waveNumber).Build();
        }

    }
}


