using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleMove : MonoBehaviour
{
    [SerializeField] ObstacleSO obstacleConfig;

    CameraShake cameraShake;
    GameManager gameManager;
    Rigidbody2D myRigidBody;

    float xBound = -13;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        CheckXBound();
    }

    void Move()
    {
        myRigidBody.velocity = new Vector2 (obstacleConfig.GetMoveSpeed(), 0);
    }

    void CheckXBound()
    {
        if (transform.position.x < xBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.isGameActive) return;

        if (collision.gameObject.CompareTag("Player") && !gameObject.CompareTag("Floor"))
        {
            gameManager.Die(collision.gameObject, null);
            Destroy(gameObject);

        }
    }
}
