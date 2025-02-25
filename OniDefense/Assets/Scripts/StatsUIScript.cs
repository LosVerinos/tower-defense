using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIScript : MonoBehaviour
{
    public TextMeshProUGUI statsText;
    [SerializeField] public WaveManagerScript waveManager;
    // Start is called before the first frame update
    void Start()
    {
        statsText.text = $"Wave: {waveManager.waveIndex} \n" +
                         $"Money: {PlayerStats.Money} \n" +
                         $"Health: {LivesManager.Instance.Lives.ToString()}";
    }

    // Update is called once per frame
    void Update()
    {
        statsText.text = $"Wave: {waveManager.waveIndex} \n" +
                         $"Money: {PlayerStats.Money} \n" +
                         $"Health: {LivesManager.Instance.Lives.ToString()}";
    }
}
