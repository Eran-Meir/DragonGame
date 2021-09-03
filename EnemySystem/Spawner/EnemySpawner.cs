using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{   
    public Enemy enemy;
    private List<Enemy> enemies;
    private float[] skeletonTransformScale = { 28, 210, 212, 214, 16, 18, 20 };
    public float waitingTime = 60f;
    public Transform player;


    [Range(1, 50)]
    public int numberOfEnemies = 2;
    private float range = 70.0f;

    void Start()
    {
        spawnSkeletons();
        InvokeRepeating("spawnSkeletons", waitingTime, waitingTime);
    }

    private void spawnSkeletons()
    {
        enemies = new List<Enemy>(); // init as type
        for (int index = 0; index < numberOfEnemies; index++)
        {
            Skeleton spawned = Instantiate(enemy, RandomNavmeshLocation(range), Quaternion.identity) as Skeleton;
            enemies.Add(spawned);
            spawned.transform.localScale = new Vector3(skeletonTransformScale[spawned.getEnemyLevel()], skeletonTransformScale[spawned.getEnemyLevel()], skeletonTransformScale[spawned.getEnemyLevel()]);
            Debug.Log("Spawning skeleton level: " + spawned.getEnemyLevel() + " with: " + spawned.getEnemyDamage() + " Damage" + spawned.getEnemyPrize() + " enemy exp prize " + skeletonTransformScale[spawned.getEnemyLevel()] + " size");
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}