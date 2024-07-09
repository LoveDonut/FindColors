using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteCanvas : MonoBehaviour
{
    [SerializeField] Button RestartButton;
    [SerializeField] Button QuitButton;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        RestartButton.onClick.AddListener(gameManager.RestartGame);
        QuitButton.onClick.AddListener(gameManager.Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
