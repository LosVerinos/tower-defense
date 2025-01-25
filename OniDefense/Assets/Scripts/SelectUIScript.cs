using UnityEngine;

public class SelectUIScript : MonoBehaviour
{
    private NodeScript target;

    public GameObject ui;

    public void SetTarget(NodeScript _target)
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

}
