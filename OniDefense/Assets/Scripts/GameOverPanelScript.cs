using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanelScript : MonoBehaviour
{
    [SerializeField] private StatsUIScript stats;
    public void OnRestartButtonClick()
    {
        // var currentScene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }

    public void Activate(bool activate){
        gameObject.SetActive(activate);
        stats.DisplayStats();
    }
}
