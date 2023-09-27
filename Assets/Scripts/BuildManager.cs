using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;


    public GameObject buildEffect;
    public GameObject sellEffect;
    Shop shop;

    private void Awake()
    {
        instance = this; 
        shop = FindObjectOfType<Shop>();
        nodeUI.Hide();
    }


    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }


    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(selectedNode);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public void DeselectTurretToBuild()
    {
        turretToBuild = null;
        shop.DeactivateAll();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
