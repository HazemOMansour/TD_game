using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    
    public static int EnemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;

    public TextMeshProUGUI waveTimer;

    private int waveNumber = 0;
    private float countdown = 11f;
    public float timeBetweenWaves = 5.5f;

    public GameMaster gameMaster;
    public GameObject boss;
    public GameObject finalWaveUI;
 
    private void Start()
    {
        EnemiesAlive = 0;
        finalWaveUI.SetActive(false);
    }
    void Update()
    {
        
        if (EnemiesAlive > 0)
            return;
        
            
        
        if (waveNumber == waves.Length)
        {
            if (PlayerStats.Lives > 0 && (Enemy.bossIsDead == false))
            {
                if (countdown <= 0f)
                {
                    SpawnBoss();
                    return;
                }
            }
            else if (PlayerStats.Lives > 0 && Enemy.bossIsDead)
            {
                
                this.enabled = false;
                return;
            }
            else
                return;

        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveTimer.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveNumber];

        EnemiesAlive = 0;

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            EnemiesAlive += wave.enemyCount[i];
        }
        for (int i = 0;i < wave.enemies.Length; i++) 
        { 
            for (int j = 0; j < wave.enemyCount[i]; j++)
            {
                SpawnEnemy(wave.enemies[i]);
                yield return new WaitForSeconds(1 / wave.spawnRate);
            }

        }

        waveNumber++;

    }
    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

    }

    private void SpawnBoss()
    {
        finalWaveUI.SetActive(true);
        PlayerStats.Rounds++;
        EnemiesAlive = 1;
        AudioManager.instance.Stop();
        AudioManager.instance.Play("BossWave");
    }


}
