using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Heart : MonoBehaviour
{
    [SerializeField] int stageIndex;
    [SerializeField] float fallRate;

    Action[] fallHeartFunctions = new Action[3];

    GameManager gameManager;
    GameObject player;

    int mainstageIndex = 0;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.Find("Player");
        fallHeartFunctions[0] = FallAtStageOne; fallHeartFunctions[2] = FallAtStageThree;
    }

    void Update()
    {
        if (transform.position.y < 0) return;

        if (stageIndex != 2) 
        {
            fallHeartFunctions[stageIndex-1].Invoke();
        }
    }

    void FallAtStageOne()
    {
        if(Input.GetKeyDown(KeyCode.Space) && player.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            transform.position -= new Vector3(0, fallRate, 0);
        }
    }
    public void FallAtStageTwo(Vector3 direction)
    {
        transform.position += direction * fallRate;
    }
    void FallAtStageThree()
    {
        transform.position -= new Vector3(0, fallRate * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.SetStageClearance(stageIndex);
            SceneManager.LoadScene(mainstageIndex);
        }
    }
}
