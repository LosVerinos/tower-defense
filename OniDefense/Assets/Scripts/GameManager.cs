using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    public static bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.GetComponent<GameOverPanelScript>().Activate(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.Lives <= 0 && isRunning)
        {
            EndGame();
        }
    }

    private void EndGame(){
        Debug.Log("Game Over !");
        isRunning = false;
        gameOverPanel.GetComponent<GameOverPanelScript>().Activate(true);
    }

    public void StartGame(){
        isRunning = true;
    }
}
