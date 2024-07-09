using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDescription : MonoBehaviour
{
    float destroyTimer = 5f;
    void Start()
    {
        Destroy(gameObject, destroyTimer);        
    }

}
