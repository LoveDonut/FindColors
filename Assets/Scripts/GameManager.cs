using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject titleCanvas;
    [SerializeField] AudioClip playerDeadSound;

    bool[] isStageClears = new bool[3];

    int mainStageIndex = 0;

    AudioManager audioManager;

    public bool isSadEnding;
    static GameManager instance;
    public static bool isGameActive = false;

    void Awake()
    {
        ManageSingleton();
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (titleCanvas != null) 
        {
            titleCanvas.SetActive(true);
        }
        else // for test at specific stage
        {
            isGameActive = true;
        }
    }

    void ManageSingleton()
    {
        if(instance == null) 
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

    public bool GetStageClearance(int index)
    {
        return isStageClears[index-1];
    }

    public void SetStageClearance(int index)
    {
        isStageClears[index - 1] = true;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverCanvas.SetActive(true);
    }

    void StageStart(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void StageClear(int index)
    {
        isStageClears[index] = true;
        SceneManager.LoadScene(index);
    }

    public void Die(GameObject diedObject, AudioClip SFX)
    {
        if (diedObject.CompareTag("Player"))
        {
            SFX = playerDeadSound;
            Camera.main.GetComponent<CameraShake>().ShakeCameraWhenDead();
            GameOver();
        }
        PlayHitEffect(diedObject.transform.position, SFX);
        Destroy(diedObject);
    }
    public void PlayHitEffect(Vector2 position, AudioClip SFX)
    {
        if(SFX != null)
        {
            audioManager.PlaySFX(SFX);
        }

        ParticleSystem instance = Instantiate(explosionFX, position, Quaternion.identity);
        Destroy(instance, instance.main.duration + instance.main.startLifetime.constantMax);
    }

    public void StartGame()
    {
        isGameActive = true;
        titleCanvas.SetActive(false);
    }

    public void Retry()
    {
        isGameActive = true;
        gameOverCanvas.SetActive(false);
        StageStart(mainStageIndex);
    }

    public void RestartGame()
    {
        isGameActive = false;
        Destroy(gameObject);
        Destroy(gameOverCanvas);
        StageStart(mainStageIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
