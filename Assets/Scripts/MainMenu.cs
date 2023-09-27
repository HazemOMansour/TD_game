using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";
    public SceneFader scene;
    public void Play()
    {
        scene.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
