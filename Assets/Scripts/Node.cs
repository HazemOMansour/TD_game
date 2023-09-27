using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Renderer rend;
    public Color startColor;
    public Color hoverColor;
    public Color errorColor;
    public Vector3 offset = new Vector3(0f, 0.5f, 0f); 
    BuildManager buildManager;

    [HideInInspector]
    public GameObject turret;
    public NodeUI canvas;
    public TurretBlueprint turretBlueprint;
    public bool isUpgraded = false;
    bool errorColorRunning;

    void Start()
    {
        rend = GetComponent<Renderer>();
        
        startColor = rend.material.color;

        buildManager = BuildManager.instance;

       
    }

    private void Update()
    {
        if (Input.GetKeyDown("q") || (Shop.buttonClicked == true))
        {
            CancelSelection();
        }

    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            return;
        }
        PlayerStats.Money -= blueprint.cost;

        GameObject turret2 = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = turret2;

        turretBlueprint = blueprint;

        GameObject effect = Instantiate(buildManager.buildEffect, turret.transform.position, Quaternion.identity);
        Destroy(effect, 3f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret);

        GameObject turret2 = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = turret2;

        GameObject effect = Instantiate(buildManager.buildEffect, turret.transform.position, Quaternion.identity);
        Destroy(effect, 3f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        PlayerStats.Money += GetSellAmount();
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;

        GameObject effect = Instantiate(buildManager.sellEffect, turret.transform.position, Quaternion.identity);
        Destroy(effect, 3f);
    }

    public int GetSellAmount()
    {
        if (!isUpgraded)
            return turretBlueprint.cost / 2;
        else
            return (turretBlueprint.cost + turretBlueprint.upgradeCost) / 2;       
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + offset;
    }


    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
        {
            rend.material.color = startColor;
            return;
        }
        if (errorColorRunning)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = errorColor;
        } 

    }

    

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (turret != null)
        {
            if (buildManager.CanBuild)
            {
                StartCoroutine(ErrorColor());
                errorColorRunning = true;
            }  
            else
                buildManager.SelectNode(this);
            return;
        }
        else
        {
            if (!buildManager.CanBuild)
            {

                buildManager.DeselectNode();
                return;
            }
            BuildTurret(buildManager.GetTurretToBuild());

        }
    }

    public float GetTurretRange()
    {
        if (turret == null)
            return 0f;
        float range = 0f;
        range = turret.GetComponent<Turret>().range;
        return range;
    }

    IEnumerator ErrorColor()
    {
        rend.material.color = errorColor;
        yield return new WaitForSeconds(0.5f);
        rend.material.color = startColor;
        errorColorRunning = false;
    }

    public void CancelSelection()
    {
        buildManager.DeselectTurretToBuild();
        rend.material.color = startColor;
        Shop.buttonClicked = false;
    }
}
