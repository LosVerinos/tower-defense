using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
        public GameObject ui;

    private void Start()
    {
        if (GameManager.isRunning)
        {
            ui.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            ui.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void StartButton()
    {
        GameManager.StartGame();
        ui.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}