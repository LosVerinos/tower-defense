using UnityEngine;

namespace DefaultNamespace
{
    public class LivesManager : MonoBehaviour
    {

        public static LivesManager Instance { get; private set; }
        private int lives = 20;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public void DecreaseLives(int amount)
        {
            lives -= amount;
            if (lives < 0)
            {
                lives = 0;
            }
            Debug.Log("Lives: " + lives);
        }
    }
}