using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public float explosionRadius;
    public GameObject bulletEffect;
    public int damage = 50;

    public void Seek(Transform target1)
    {
        target = target1;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target);
        if(transform.position == target.position) 
        {
            HitTarget();
            return;
        }
    
    
    
    }



    void HitTarget()
    {
        GameObject effect = Instantiate(bulletEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);
        
        if(explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            AudioManager.instance.Play("TurretShoot");
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Explode()
    {
        AudioManager.instance.Play("MissileSound");
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
                PlayerStats.Money += 5;
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy enemyInst = enemy.GetComponent<Enemy>();
        if(enemyInst != null) 
        {
            enemyInst.TakeDamage(damage);
            PlayerStats.Money += 1;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
       
    }
}
