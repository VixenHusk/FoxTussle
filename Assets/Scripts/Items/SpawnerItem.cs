using System.Collections.Generic;
using UnityEngine;

public class SpawnerItem : MonoBehaviour
{
    public List<GameObject> prefabs; // Lista de prefabs que quieres instanciar
    public List<Transform> spawnPoints; // Lista de posiciones donde quieres instanciar los prefabs

    private List<GameObject> spawnedPrefabs = new List<GameObject>(); // Lista de prefabs ya instanciados

    public void SetShopItems()
    {
        SpawnPrefabsRandomly();
        Debug.Log("Se han repuesto los objetos de la tienda");
    }

    void SpawnPrefabsRandomly()
    {
        // Lista para almacenar los índices de prefabs que aún no se han utilizado
        List<int> availablePrefabIndices = new List<int>();
        for (int i = 0; i < prefabs.Count; i++)
        {
            availablePrefabIndices.Add(i);
        }

        // Instanciar prefabs en posiciones aleatorias
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (availablePrefabIndices.Count == 0)
            {
                Debug.LogWarning("No hay más prefabs disponibles para instanciar.");
                return;
            }

            // Elegir aleatoriamente un índice de prefab disponible
            int randomIndex = Random.Range(0, availablePrefabIndices.Count);
            int prefabIndex = availablePrefabIndices[randomIndex];

            // Instanciar el prefab como hijo del spawnPoint
            GameObject spawnedPrefab = Instantiate(prefabs[prefabIndex], spawnPoint);
            spawnedPrefabs.Add(spawnedPrefab);

            // Ajustar la posición local del prefab instanciado
            spawnedPrefab.transform.localPosition = new Vector3(0, 1f, 0);
            spawnedPrefab.transform.localRotation = Quaternion.identity;

            // Eliminar el índice utilizado de la lista de índices disponibles
            availablePrefabIndices.RemoveAt(randomIndex);
        }
    }
}