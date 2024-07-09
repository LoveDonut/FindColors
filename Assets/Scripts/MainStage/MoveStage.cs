using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveStage : MonoBehaviour
{
    [SerializeField] int stageNumber;
    [SerializeField] GameObject hearts;

    GameManager gameManager;

    void Start()
    {
        CheckIfStageClear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(stageNumber);
    }

    void CheckIfStageClear()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null && gameManager.GetStageClearance(stageNumber))
        {
            hearts.SetActive(true);
            Destroy(gameObject);
        }
    }
}
