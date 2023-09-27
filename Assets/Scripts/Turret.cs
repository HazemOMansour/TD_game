using UnityEngine; 

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;
    private string enemyTag = "Enemy";
    private float fireCountdown = 0f;

    [Header("General")]
    public float range = 10f;


    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public Light glowLight;
    public int damageOverTime = 30;
    public float slowPct = 0.3f;

    [Header("Setup")]
    public Transform firePoint;
    public Transform partToRotate;
    public float turnSpeed = 5f;
    public ParticleSystem laserEffect;

    
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistanceEnd = Mathf.Infinity;
        GameObject nearestEnemy = null;
        

        foreach(GameObject enemy in enemies)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            float DistanceToEnd = enemy.GetComponent<EnemyMovement>().GetDistanceToEnd();
            if(DistanceToEnemy < range)
            {
                if (DistanceToEnd < shortestDistanceEnd)
                {
                    nearestEnemy = enemy;
                    shortestDistanceEnd = DistanceToEnd;
                }
            }



        }
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }
        else
        {
            if (useLaser)
                AudioManager.instance.Stop("LaserSound");
            target = null;
        }
    }

    private void Update()
    {
        if (Enemy.bossIsDead)
        {
            if (useLaser)
            {
                lineRenderer.enabled = false;
                if (laserEffect.isPlaying)
                    laserEffect.Stop();
                glowLight.enabled = false;
                AudioManager.instance.Stop("LaserSound");
            }
            this.enabled = false;
            return;
        }
            
        fireCountdown -= Time.deltaTime;
        if (target == null)
        {
            if (useLaser)
            {
                lineRenderer.enabled = false;
                if (laserEffect.isPlaying)
                    laserEffect.Stop();
                glowLight.enabled = false;
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f && target != null)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
        
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Slerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        AudioManager.instance.Play("LaserSound");
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);

        lineRenderer.enabled = true;
        if(!laserEffect.isPlaying)
            laserEffect.Play();
        glowLight.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        laserEffect.transform.position = target.position + dir.normalized;
        laserEffect.transform.rotation = Quaternion.LookRotation(dir);

    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if(bullet != null)
            bullet.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        
    }

}
