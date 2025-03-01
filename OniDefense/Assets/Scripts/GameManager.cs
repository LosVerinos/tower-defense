using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    private bool gameEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.Lives <= 0 && !gameEnded)
        {
            EndGame();
        }
    }

    private void EndGame(){
        Debug.Log("Game Over !");
        gameEnded = true;
        gameOverPanel.GetComponent<GameOverPanelScript>().Activate(true);
    }
}
