using UnityEngine;


namespace Game
{
    public class SelectUIScript : MonoBehaviour
    {
        private Node target;

        public GameObject ui;

        public void SetTarget(Node _target)
        {
            target = _target;

            transform.position = target.transform.position;

            ui.SetActive(true);

        }

        public void Hide()
        {
            ui.SetActive(false);
        }

        public void Upgrade()
        {
            target.UpgradeDefense();
            BuildManager.instance.DeselectNode();
        }

        public void Sell()
        {
            target.SellDefense();
            BuildManager.instance.DeselectNode();
        }
    }
}


