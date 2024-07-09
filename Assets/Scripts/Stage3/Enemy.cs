using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] ObstacleSO enemySO;

    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRigidbody;
    Transform player;
    GameManager gameManager;
    CameraShake cameraShake;

    Color[] colors = new Color[3];

    float xBound = 13;
    Vector2 targetPosition;

    void Awake()
    {
        if(!GameManager.isGameActive)
        {
            Destroy(gameObject);
            return;
        }

        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        gameManager = FindObjectOfType<GameManager>();
        cameraShake = FindObjectOfType<CameraShake>();
    }

    void Start()
    {
       colors[0] = mySpriteRenderer.color; colors[1] = Color.green; colors[2] = Color.blue;
        mySpriteRenderer.color = colors[Random.Range(0, colors.Length)];    
    }
    void Update()
    {
        if (!GameManager.isGameActive) return;
        Move();
        CheckXBound();
    }

    
    void Move() // Move with delay
    {
       if(player != null) 
        {
            targetPosition = new Vector2(player.position.x - 2, player.position.y);
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            myRigidbody.AddForce(direction * enemySO.GetMoveSpeed());
        }
    }
    void CheckXBound()
    {
        if (transform.position.x < -xBound || transform.position.x > xBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManager.Die(collision.gameObject, null);
            gameManager.isSadEnding = false; // if you don't clear the stage, then recover the ending stage.
            Destroy(gameObject);
        }
    }
}
