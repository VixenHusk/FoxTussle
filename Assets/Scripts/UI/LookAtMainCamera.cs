using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCamera : MonoBehaviour
{
    Transform transformCamera;
    void Start()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera == null){
            Debug.LogError("Se debe etiquetar la cámara como MainCamera");
        } else {
            transformCamera = mainCamera.transform;
        }
    }

    void LateUpdate()
    {
        if (transformCamera==null) return;  
        transform.LookAt(transformCamera);  
    }
}
