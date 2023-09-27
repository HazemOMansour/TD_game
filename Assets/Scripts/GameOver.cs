using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI roundsText;

    public SceneFader scene;

    string menuSceneName = "MainMenu";

    private void OnEnable()
    {
        roundsText.text = (PlayerStats.Rounds - 1).ToString();
    }

    public void Retry()
    {
        scene.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        scene.FadeTo(menuSceneName);
    }

}
