using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Image timerImage;
    [SerializeField] float timeLimit = 60f;

    GameManager gameManager;
    float elapsedTime;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        elapsedTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isGameActive) return;

        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (elapsedTime < 0)
        {
            gameManager.Die(GameObject.Find("Player"), null);
            elapsedTime = timeLimit;
        }
        else
        {
            elapsedTime -= Time.deltaTime;
            timerImage.fillAmount = elapsedTime / timeLimit;
        }
    }
}
