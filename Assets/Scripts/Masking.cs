using UnityEngine;
using UnityEngine.EventSystems;

public class Masking : MonoBehaviour
{

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        buildManager.DeselectNode();
    }

}

