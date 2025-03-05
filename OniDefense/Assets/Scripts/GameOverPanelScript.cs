using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelScript : MonoBehaviour
{
    [SerializeField] private StatsUIScript stats;
    public GameObject mainMenuUI;
    public TextMeshProUGUI rankingText;

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

            string playerName = PlayerPrefs.GetString("PlayerName");

            int finalScore = PlayerStats.PassedWaves; 
            RankingSystem.AddNewScore(playerName, finalScore);
            UpdateRankingDisplay();
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

    void UpdateRankingDisplay()
    {
        if (rankingText == null) return;

        RankingSystem.ScoreList scoreList = RankingSystem.LoadScores();
        rankingText.text = "Ranking : \n";

        int rank = 1;
        foreach (var entry in scoreList.scores)
        {
            rankingText.text += $"{rank}. {entry.playerName}: {entry.score} waves\n";
            rank++;
        }
    }
}