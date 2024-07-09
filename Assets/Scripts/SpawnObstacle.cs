using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] List<ObstacleSO> obstacleConfig = new List<ObstacleSO>();

    float spawnX = 13f;

    [Header("Stage3")]
    [SerializeField] float lifeTime = 20f;

    void Start()
    {
        foreach(ObstacleSO obstacle in obstacleConfig)
        {
            StartCoroutine(Spawn(obstacle));
        }
    }

    IEnumerator Spawn(ObstacleSO obstacle)
    {
        do
        {
            yield return new WaitForSeconds(obstacle.GetRandomSpawnTime());
            GameObject spawnedObstacle = Instantiate(obstacle.GetObstacleObject(), new Vector3(spawnX, obstacle.GetObstacleSpawnY(), 0), Quaternion.identity);

            if (spawnedObstacle.CompareTag("Enemy"))
            {
                Destroy(spawnedObstacle, lifeTime);
            }
        } while (GameManager.isGameActive);
    }
}
