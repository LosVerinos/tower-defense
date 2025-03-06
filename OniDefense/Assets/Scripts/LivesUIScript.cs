using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LivesUIScript : MonoBehaviour
    {
        public TextMeshProUGUI livesText;
        // Start is called before the first frame update
        void Start()
        {
            livesText.text = " ❤ :" + PlayerStats.Lives.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            livesText.text = " ❤ :" + PlayerStats.Lives.ToString();
        }
    }

}

