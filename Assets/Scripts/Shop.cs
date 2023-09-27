using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    public GameObject standardTurretCancel;
    public GameObject missileLauncherCancel;
    public GameObject laserBeamerCancel;

    BuildManager buildManager;

    public static bool buttonClicked;

    private void Start()
    {
        buildManager = BuildManager.instance;
        DeactivateAll();
        buttonClicked = false;

    }
    public void ButtonClicked()
    {
        buttonClicked = true;
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
        DeactivateAll();
        standardTurretCancel.SetActive(true);
    }

    public void SelectMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missileLauncher);
        DeactivateAll();
        missileLauncherCancel.SetActive(true);
    }

    public void SelectLaserBeamer()
    {
        buildManager.SelectTurretToBuild(laserBeamer);
        DeactivateAll();
        laserBeamerCancel.SetActive(true);
    }

    public void DeactivateAll()
    {
        standardTurretCancel.SetActive(false);
        missileLauncherCancel.SetActive(false);
        laserBeamerCancel.SetActive(false);
    }
}
