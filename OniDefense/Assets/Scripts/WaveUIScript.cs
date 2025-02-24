using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class WaveUIScript : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    [SerializeField] public WaveManagerScript waveManager;
    // Start is called before the first frame update
    void Start()
    {
        waveText.text = " Wave:" + waveManager.waveIndex.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = " Wave:" + waveManager.waveIndex.ToString();
    }
}
