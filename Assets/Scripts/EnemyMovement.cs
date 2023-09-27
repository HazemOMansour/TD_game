using UnityEngine;

[RequireComponent(typeof(Enemy))]    
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int WaypointIndex = 0;

    private Enemy enemy;
    Vector3 offset = new Vector3(0.001f, 0f, 0f);

    private void Start()
    {
        target = Waypoints.points[0];
        enemy = GetComponent<Enemy>();

    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, enemy.speed * Time.deltaTime);
        Vector3 dir = (target.position - transform.position) + offset;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);


        if (transform.position == target.position)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (WaypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        WaypointIndex++;
        target = Waypoints.points[WaypointIndex];
    }

    void EndPath()
    {
        AudioManager.instance.Play("LivesLost");
        if (enemy.isBoss)
        {
            Destroy(gameObject);
            PlayerStats.Lives = 0;
            WaveSpawner.EnemiesAlive--;
            return;
        }
        Destroy(gameObject);
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
    }

    public float GetDistanceToEnd()
    {
        float distance = 0f;
        distance += Vector3.Distance(transform.position, Waypoints.points[WaypointIndex].transform.position);
        for (int i = WaypointIndex; i < Waypoints.points.Length - 1; i++)
        {
            Vector3 start = Waypoints.points[i].transform.position;
            Vector3 end = Waypoints.points[i + 1].transform.position;
            distance += Vector3.Distance(start, end);
        }
        return distance;

    }
}
