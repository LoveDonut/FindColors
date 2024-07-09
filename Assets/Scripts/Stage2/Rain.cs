using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField] float spawnY = 6f;
    [SerializeField] float spawnXBound = 8.5f;
    [SerializeField] float fallSpeed = 5f;
    [SerializeField] float positionYForDestroy = -6f;
    [SerializeField] AudioClip yellowBallSound;
    [SerializeField] AudioClip whiteBallSound;

    ShakeGround ground;
    Heart heart;
    SpriteRenderer mySpriteRenderer;
    AudioManager audioManager;

    void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        ground = FindObjectOfType<ShakeGround>();
        heart = FindObjectOfType<Heart>();
    }
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        float spawnX = Random.Range(-spawnXBound, spawnXBound);
        transform.position = new Vector2(spawnX, spawnY);

        ChangeColor(ground.GetShakeState());
    }
    void Update()
    {
        Fall();
    }

    public void ChangeColor(bool isShaked)
    {
        if (isShaked)
        {
            mySpriteRenderer.color = Color.yellow;
        }
        else
        {
            mySpriteRenderer.color = Color.white;
        }
    }

    void Fall()
    {
        transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);

        if(transform.position.y < positionYForDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(mySpriteRenderer.color == Color.yellow)
            {
                PlayRainTriggerEffect(Vector3.up, yellowBallSound);
            }
            else 
            {
                PlayRainTriggerEffect(Vector3.down, whiteBallSound);
            }

            Destroy(gameObject);
        }
    }

    private void PlayRainTriggerEffect(Vector3 direction, AudioClip SFX)
    {
        heart.FallAtStageTwo(direction);
        audioManager.PlaySFX(SFX);
    }
}
