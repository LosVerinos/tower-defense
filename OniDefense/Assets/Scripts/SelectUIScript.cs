using UnityEditor;
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
        var next = Instantiate(target.defenseClass.upgradeStates[target.defenseClass.upgradeLevel + 1].prefab);
        // wait to get the AssetPreview of the next defense and set it to the RawImage
        GetComponentInChildren<RawImage>().texture = AssetPreview.GetAssetPreview(next) as Texture;
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
