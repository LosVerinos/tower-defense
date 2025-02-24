using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnRestartButtonClick()
    {
        // var currentScene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(currentScene.name);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }
}
