using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapCalamari : MonoBehaviour
{
    public GameObject trapObject; // El objeto que se activará y desactivará
    public GameObject movingObject; // El objeto que se activará y se moverá
    public Transform targetTransform; // La posición a la que se moverá el movingObject
    public float activationInterval = 60f; // Intervalo de tiempo entre activaciones
    public float activeDuration = 30f; // Duración durante la cual el objeto está activado

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(ControlTrap());
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        StopCoroutine(ControlTrap());
    }

    GameObject FindInactiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        foreach (Transform obj in objs)
        {
            if (obj.hideFlags == HideFlags.None && obj.name == name)
            {
                return obj.gameObject;
            }
        }
        return null;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reasigna las referencias aquí
        trapObject = FindInactiveObjectByName("-- SpawnersCalamari"); // Cambia "TrapObjectName" por el nombre real del objeto
        movingObject = FindInactiveObjectByName("-- WallsTrapCalamari"); // Cambia "-- WallsTrapCalamari" por el nombre real del objeto
        targetTransform = GameObject.Find("Player")?.transform; // Cambia "TargetTransformName" por el nombre real del objeto

        if (trapObject == null || movingObject == null || targetTransform == null)
        {
            Debug.LogError("No se pudieron reasignar todas las referencias.");
        }
    }

    private IEnumerator ControlTrap()
    {
        while (true)
        {
            yield return new WaitForSeconds(activationInterval);

            if (trapObject != null && movingObject != null && targetTransform != null)
            {
                // Activar los objetos
                trapObject.SetActive(true);
                movingObject.SetActive(true);

                // Mover el movingObject a la posición del targetTransform
                movingObject.transform.position = targetTransform.position;

                // Esperar durante el tiempo que el objeto está activo
                yield return new WaitForSeconds(activeDuration);

                // Desactivar los objetos
                trapObject.SetActive(false);
                movingObject.SetActive(false);

                GameObject parentObject = GameObject.Find("-- Calamaris");

                if (parentObject != null)
                {
                    // Destruir todos los hijos del GameObject encontrado
                    foreach (Transform child in parentObject.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
                else
                {
                    Debug.Log("El objeto '-- Calamaris' no fue encontrado.");
                }
            }
            else
            {
                Debug.LogError("Las referencias a los objetos no están asignadas correctamente.");
            }
        }
    }
}