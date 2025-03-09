using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{

    public class ShopUIScript : MonoBehaviour
    {
        [SerializeField] private GameObject defensesButton;
        [SerializeField] private GameObject supportButton;
        [SerializeField] private GameObject bonusButton;
        [SerializeField] private GameObject closeButton;
        private GameObject currentPanel;
        private bool isPanelDeployed = false;
        public GameObject selectUI;

        void Start()
        {
            closeButton.SetActive(false);
        }

        public void DeployPanel(GameObject panel)
        {
            if (currentPanel == panel) return;
            ShrinkOtherPanels();

            GameObject[] panelsToMove = { defensesButton, supportButton, bonusButton, panel, closeButton };
            MovePanels(panelsToMove, new Vector3(0, 220, 0));

            closeButton.SetActive(true);
            currentPanel = panel;
            isPanelDeployed = true;

            selectUI.GetComponent<SelectUIScript>().Hide();
        }

        void ShrinkOtherPanels()
        {
            if (!isPanelDeployed) return;

            GameObject[] panelsToMove = { defensesButton, supportButton, bonusButton, currentPanel, closeButton };
            MovePanels(panelsToMove, new Vector3(0, -220, 0));

            closeButton.SetActive(false);
            currentPanel = null;
            isPanelDeployed = false;
        }

        public void ShrinkAll()
        {
            ShrinkOtherPanels();
        }

        void MovePanels(GameObject[] panels, Vector3 movement)
        {
            foreach (GameObject panel in panels)
            {
                panel.transform.Translate(movement);
            }
        }
    }

}
