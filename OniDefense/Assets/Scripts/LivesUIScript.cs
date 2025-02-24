using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesUIScript : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = " <3 :" + LivesManager.Instance.Lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = " <3 :" + LivesManager.Instance.Lives.ToString();
    }
}
