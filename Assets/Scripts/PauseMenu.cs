using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public SceneFader scene;


    string menuSceneName = "MainMenu";
    private void Start()
    {
        ui.SetActive(false);
    }
    void Update()
    {
        if (GameMaster.hasEnded == true)
        {
            ui.SetActive(false);
            return;
        }
            
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleState();

        }
    }

    public void ToggleState()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            AudioListener.pause = true;
            Time.timeScale = 0f;
        }
        else
        {
            AudioListener.pause = false;
            Time.timeScale = 1f;
        }
            
    }

    public void Retry()
    {
        ToggleState();
        scene.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        ToggleState();
        scene.FadeTo(menuSceneName);
        
    }
}
