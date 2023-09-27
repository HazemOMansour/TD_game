using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    public float startHealth = 100f;
    public float speed;
    public float health;
    public GameObject deathEffect;
    public int coinsGained = 60;

    private bool enemyDied = false;

    [Header("Boss")]
    public bool isBoss = false;
    public static bool bossIsDead;
    public Animator animator;
    public EnemyMovement enemyMovement;
    public GameMaster gameMaster;
    public static Vector3 bossPos;
    Vector3 bossEffectOffset = new Vector3(0, 0, -1.5f);

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {
        bossPos = new Vector3 (0, 0, 0);
        bossIsDead = false;
        speed = startSpeed;
        health = startHealth;
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
    }


    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            if(enemyDied == false)
                Die();
            
        }
    }

    public void Slow(float slowPct)
    {
        speed = startSpeed * (1 - slowPct);
    }

    void Die()
    {
        GameObject effect;
        if (isBoss)
        {
            bossPos = transform.position;
            bossIsDead = true;
            enemyMovement.enabled = false;
            animator.Play("PlayAnim");
            effect = Instantiate(deathEffect, transform.position - (transform.forward * 2f), Quaternion.identity);
        }
        else
        {
            effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
            
            
        enemyDied = true;
        PlayerStats.Money += coinsGained; 

        Destroy(effect, 5f);
        if (isBoss)
            Destroy(gameObject, 6f);
        else
            Destroy(gameObject);

        WaveSpawner.EnemiesAlive--;
    }


}

