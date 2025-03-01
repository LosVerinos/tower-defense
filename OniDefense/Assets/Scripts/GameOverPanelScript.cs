using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Activate(false);
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

    public void Activate(bool activate){
        gameObject.SetActive(activate);
    }
}
