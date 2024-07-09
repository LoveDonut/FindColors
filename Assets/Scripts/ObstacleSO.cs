using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Obstacle Config", fileName = "New Obstacle Config")]
public class ObstacleSO : ScriptableObject
{
    [SerializeField] GameObject obstacleObject;
    [SerializeField] float moveSpeed = -5f;
    [SerializeField] float SpawnsRate = 1.5f;
    [SerializeField] float spawnTimeVariance = 0.5f;
    [SerializeField] float minimumSpawnTime = 1f;

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(SpawnsRate - spawnTimeVariance, SpawnsRate + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }

    public GameObject GetObstacleObject()
    {
        return obstacleObject;
    }

    public float GetObstacleSpawnY()
    {
        return obstacleObject.transform.position.y;
    }
}
