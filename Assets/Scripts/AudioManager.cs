using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    AudioSource myAudioSource;

    private void Awake()
    {
        ManageSingleton();
        myAudioSource = GetComponent<AudioSource>();
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
    public void PlaySFX(AudioClip clip)
    {
        myAudioSource.PlayOneShot(clip);
    }
}
