using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float shootSpeed = 5f;
    [SerializeField] AudioClip deadSound;

    GameManager gameManager;
    Weapon gun;
    Rigidbody2D myRigidbody;
    float xBound = 13f;


    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gun = GameObject.Find("Player").GetComponentInChildren<Weapon>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        shootSpeed *= gun.transform.localScale.x;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        myRigidbody.velocity = new Vector2(shootSpeed, 0);
        CheckXBound();
    }

    void CheckXBound()
    {
        if(transform.position.x > xBound || transform.position.x < -xBound)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Floor"))
        {
            if(collision.CompareTag("Enemy"))
            {
                gameManager.isSadEnding = true;
            }
            gameManager.Die(collision.gameObject, deadSound);
            Destroy(gameObject);
        }
    }
}
