using UnityEngine;
using UnityEngine.UI;

public class SelectUIScript : MonoBehaviour
{
    private NodeScript target;

    public GameObject ui;

    public void SetTarget(NodeScript _target)
    {
        target = _target;

        transform.position = target.transform.position;

        ui.SetActive(true);
        RawImage targetRawImage = target.GetComponentInChildren<RawImage>();
        string current_name = targetRawImage.name;
        var next = Instantiate(target.defenseClass.upgradeStates[target.defenseClass.upgradeLevel + 1].prefab);
        string next_name = next.GetComponentInChildren<RawImage>().name;

        if (current_name == next_name)
        {
            targetRawImage.GetComponent<RawImage>().texture = targetRawImage.texture;
        }
        else
        {
            targetRawImage.GetComponent<RawImage>().texture = next.GetComponentInChildren<RawImage>().texture;
        }
        Destroy(next);
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
