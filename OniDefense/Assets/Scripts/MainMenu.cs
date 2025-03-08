using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject ui;
        public TextMeshProUGUI rankingText;
        public TMP_InputField playerNameInput;

        private string playerName = "Player";

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
                DisplayRanking();

                playerName = PlayerPrefs.GetString("PlayerName");
                playerNameInput.text = playerName;
            }
        }

        public void StartButton()
        {
            if (!string.IsNullOrEmpty(playerNameInput.text))
            {
                playerName = playerNameInput.text;
            }

            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.Save();

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

        void DisplayRanking()
        {
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
}

