using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;

    public GameObject UI;
    public TextMeshProUGUI upgradeCost;
    public Button upgradeButton;
    public TextMeshProUGUI sellCost;

    public GameObject cylinder;
    public void SetTarget(Node target2)
    {
        target = target2;
        float range = target.GetTurretRange();
        Vector3 rangeScale = new Vector3(range *2, 0f, range *2);

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
            cylinder.transform.localScale = rangeScale;
        }
        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
            cylinder.transform.localScale = rangeScale;
        }
        
        sellCost.text = "$" + target.GetSellAmount();
        
        cylinder.SetActive(true);
        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
        cylinder.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}
