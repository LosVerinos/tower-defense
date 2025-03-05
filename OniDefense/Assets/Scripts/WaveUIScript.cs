using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class WaveUIScript : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    [SerializeField] public WaveSpawner waveManager;
    // Start is called before the first frame update
    void Start()
    {
        waveText.text = " Vague :" + WaveSpawner.waveIndex.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = " Vague :" + WaveSpawner.waveIndex.ToString();
    }
}
