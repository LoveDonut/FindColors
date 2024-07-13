using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PuaseCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PauseText;
    [SerializeField] Button ResumeButton;
    [SerializeField] Button QuitButton;

    static PuaseCanvas instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ToggleCanvas(false);
    }

    private void ToggleCanvas(bool turnOn)
    {
        PauseText.gameObject.SetActive(turnOn);
        ResumeButton.gameObject.SetActive(turnOn);
        QuitButton.gameObject.SetActive(turnOn);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !GameManager.isPuase) 
        {
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && GameManager.isPuase) 
        {
            ResumeGame();
        }
    }
    public void ResumeGame()
    {
        GameManager.isPuase = !GameManager.isPuase;
        Time.timeScale = 1.0f;
        ToggleCanvas(false);
    }

    void PauseGame()
    {
        GameManager.isPuase = !GameManager.isPuase;
        Time.timeScale = 0f;
        ToggleCanvas(true);
    }
}
