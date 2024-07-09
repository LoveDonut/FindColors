using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeGround : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float shakeDuration = 4f;
    [SerializeField] float shakeCoolDown = 2f;


    CameraShake cameraShake;
    float groundRotationAngle = 15f;
    float elapsedTime = 4f;
    float timeForRotation = 0f;
    bool coolTimeTrigger = true;

    void Awake()
    {
        cameraShake = FindObjectOfType<CameraShake>();    
    }

    void Update()
    {
        Shake();
    }

    public bool GetShakeState()
    {
        return coolTimeTrigger;
    }

    void Shake()
    {
        if (elapsedTime < shakeDuration)
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(-groundRotationAngle, groundRotationAngle, Mathf.PingPong(timeForRotation * rotationSpeed, 1)));
            elapsedTime += Time.deltaTime; timeForRotation += Time.deltaTime;
        }
        else if(elapsedTime >= shakeDuration && coolTimeTrigger)
        {
            coolTimeTrigger = false;
            SwitchRainState();
            StartCoroutine(ResetTime());
        }
    }

    IEnumerator ResetTime()
    {
        yield return new WaitForSeconds(shakeCoolDown);
        elapsedTime = 0;
        coolTimeTrigger = true;
        cameraShake.ShakeCameraWhenGroundMove(shakeDuration);
        SwitchRainState();
    }

    void SwitchRainState()
    {
        foreach (Rain rain in FindObjectsOfType<Rain>())
        {
            rain.ChangeColor(coolTimeTrigger);
        }
    }
}
