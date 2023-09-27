using UnityEngine;

public class LevelWon : MonoBehaviour
{
    public SceneFader scene;
    public string nextLevel = "Level02";
    public string menuSceneName = "MainMenu";


    public void Continue()
    {
        scene.FadeTo(nextLevel);
        WaveSpawner.EnemiesAlive = 0;
    }

    public void Menu()
    {
        scene.FadeTo(menuSceneName);
        WaveSpawner.EnemiesAlive = 0;
    }
}
