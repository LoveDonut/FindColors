using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeMagnitude = 0.1f;

    [Header("Hit")]
    [SerializeField] float shakeDuration = 1f;


    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    public void ShakeCameraWhenGroundMove(float shakeDuration)
    {
        StartCoroutine(Shake(shakeDuration));
    }

    public void ShakeCameraWhenDead()
    {
        StartCoroutine(Shake(shakeDuration));
    }

    IEnumerator Shake(float shakeDuration)
    {
        float elapsedTime = 0f;
        while(elapsedTime < shakeDuration) 
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
