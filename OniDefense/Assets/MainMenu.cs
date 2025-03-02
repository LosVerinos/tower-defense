using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool gameStarted = false;
    public GameObject ui;

    private void Start()
    {
        if (gameStarted)
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
        gameStarted = true;
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