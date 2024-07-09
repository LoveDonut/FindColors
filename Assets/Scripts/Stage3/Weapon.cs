using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] Bullet bullet;

    [SerializeField] float moveSpeed = 4f;

    bool isDropped = true;
    float xBound = -13f;

    PlayerController playerController;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        if(isDropped)
        {
            MoveWhileDropped();
        }
    }

    public void SetDropState(bool state)
    {
        isDropped = state;
    }

    void MoveWhileDropped()
    {
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        CheckXBound();
    }
    void CheckXBound()
    {
        if (transform.position.x < xBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerController.EquipWeapon();
            Destroy(gameObject);
        }
    }
}
