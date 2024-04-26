using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Lista de GameObjects que se pueden instanciar

    void OnEnable()
    {
        SpawnRandomObject();
    }

    public void SpawnRandomObject()
    {
        // Verifica que haya al menos un GameObject en la lista
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogError("No hay objetos en la lista para instanciar.");
            return;
        }

        // Escoge un índice aleatorio dentro del rango de la lista
        int randomIndex = Random.Range(0, objectsToSpawn.Length);

        // Instancia el GameObject como hijo de este objeto
        GameObject spawnedObject = Instantiate(objectsToSpawn[randomIndex], transform.position, Quaternion.identity, transform);
    }
}