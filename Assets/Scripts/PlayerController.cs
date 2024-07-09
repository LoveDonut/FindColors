using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioClip jumpSound;

    [Header("Move")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    [Header("Weapon")]
    [SerializeField] Weapon gun;
    [SerializeField] Bullet bullet;
    [SerializeField] float weaponXOffset = 1f;

    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBoxCollider;
    AudioManager audioManager;

    bool isEquipped = false;
    Vector2 moveInput;
    Vector2 minXBound;
    Vector2 maxXBound;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        InitBounds();
    }

    private void InitBounds()
    {
        Camera camera = Camera.main;
        minXBound = camera.ViewportToWorldPoint(new Vector2(0, 0));
        maxXBound = camera.ViewportToWorldPoint(new Vector2(1, 0));
    }

    void Update()
    {
        if (!GameManager.isGameActive) return;

        Move();

        if(isEquipped)
        {
            FlipGun();
        }
    }

    void Move()
    {
        Vector2 moveOnlyX = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);

        myRigidbody.velocity = moveOnlyX;

        if(transform.position.x < minXBound.x)
        {
            transform.position = new Vector2(minXBound.x, transform.position.y);
        }
        else if(transform.position.x > maxXBound.x)
        {
            transform.position = new Vector2(maxXBound.x, transform.position.y);
        }

    }

    void FlipGun()
    {
        bool isMoving = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (isMoving) 
        {
            float direction = Mathf.Sign(myRigidbody.velocity.x);
            gun.gameObject.transform.position = transform.position + new Vector3(direction*weaponXOffset, 0, 0);
            gun.gameObject.transform.localScale = new Vector3(direction, 1, 1);
        }
    }



    public void EquipWeapon()
    {
        gun.gameObject.SetActive(true);
        gun.SetDropState(false);
        isEquipped = true;
    }

    public void RemoveWeapon()
    {
        gun.gameObject.SetActive(false);
        isEquipped = false;
    }

    void OnMove(InputValue value)
    {
        if (!GameManager.isGameActive) return;

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) 
    {
        if (!GameManager.isGameActive) return;

        if (value.isPressed && myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
            audioManager.PlaySFX(jumpSound);
        }
    }
    void OnFire(InputValue value)
    {
        if (!isEquipped) return;

        Instantiate(bullet, (Vector2)gun.transform.position, Quaternion.identity);
    }

}
