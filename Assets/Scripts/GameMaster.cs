using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static bool hasEnded;
    public GameObject gameOverUI;
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;
    public SceneFader scene;
    public GameObject levelWonUI;
    public TextMeshProUGUI waveCount;

    private void Start()
    {
        hasEnded = false;
        gameOverUI.SetActive(false);
        levelWonUI.SetActive(false);
    }

    void Update()
    {
        if(PlayerStats.Lives <= 0 && (hasEnded == false))
        {
            EndGame();
        }

        waveCount.text = "Wave: " + PlayerStats.Rounds + "/15";
        
    }

    void EndGame()
    {
        AudioManager.instance.Stop();
        AudioManager.instance.Play("GameOver");
        gameOverUI.SetActive(true);
        hasEnded = true;
    }

    public void WinLevel()
    {
        AudioManager.instance.Stop();
        AudioManager.instance.Play("LevelWon");
        levelWonUI.SetActive(true);
        hasEnded = true;
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }

}
