using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject ui;
        public GameObject mainMenuUI;

        void Update()
        {
            if (mainMenuUI != null && mainMenuUI.activeSelf)
            {
                ui.SetActive(false);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            if (mainMenuUI != null && mainMenuUI.activeSelf) return;
            ui.SetActive(!ui.activeSelf);

            if (ui.activeSelf)
            {
                GameManager.PauseGame();
            }
            else
            {
                GameManager.StartGame();
            }
        }

        public void Retry()
        {
            ui.SetActive(false);
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Menu()
        {
            ui.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            mainMenuUI.SetActive(true);

            Time.timeScale = 0f;
        }

    }
}

