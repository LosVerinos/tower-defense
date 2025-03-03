using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelScript : MonoBehaviour
{
    [SerializeField] private StatsUIScript stats;
    public GameObject mainMenuUI; 

    public void OnRestartButtonClick()
    {
        gameObject.SetActive(false);

        GameManager.StartGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Activate(bool activate)
    {
        gameObject.SetActive(activate);

        if (activate)
        {
            GameManager.PauseGame();
            stats.DisplayStats();
        }
    }

    public void Menu()
    {
        gameObject.SetActive(false);

        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(true);
        }

        GameManager.PauseGame();
    }
}