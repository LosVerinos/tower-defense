using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanelScript : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        // var currentScene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(currentScene.name);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }

    public void Activate(bool activate){
        gameObject.SetActive(activate);
    }
}
