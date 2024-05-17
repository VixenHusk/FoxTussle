using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Lista de GameObjects que se pueden instanciar
    private List<int> availableIndices = new List<int>(); // Lista de índices disponibles para instanciar

    void OnEnable()
    {
        ResetAvailableIndices();
        SpawnRandomObjects();
    }

    public void SpawnRandomObjects()
    {
        // Verifica que haya al menos un GameObject en la lista
        if (objectsToSpawn.Length < 3)
        {
            Debug.LogError("No hay suficientes objetos en la lista para instanciar.");
            return;
        }

        if (availableIndices.Count < 3)
        {
            Debug.LogWarning("No hay suficientes índices disponibles para instanciar tres prefabs sin repetición. Reiniciando la lista de índices disponibles.");
            ResetAvailableIndices();
        }

        DestroySpawnedObjects();

        // Instancia tres prefabs aleatorios sin repetición
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = GetUniqueRandomIndex();
            availableIndices.Remove(randomIndex);
            GameObject spawnedObject = Instantiate(objectsToSpawn[randomIndex], transform.position, Quaternion.identity, transform);
            Button buttonComponent = spawnedObject.GetComponentInChildren<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(delegate { ApplyPowerUp(randomIndex); });
            }
            else
            {
                Debug.LogWarning("No se encontró ningún botón en el objeto instanciado.");
            }
        }
        

    }
    
     // Método para obtener un índice único aleatorio
    private int GetUniqueRandomIndex()
    {
        int randomIndex = Random.Range(0, availableIndices.Count);
        int index = availableIndices[randomIndex];
        return index;
    }

    // Método para reiniciar la lista de índices disponibles
    private void ResetAvailableIndices()
    {
        availableIndices.Clear();
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            availableIndices.Add(i);
        }
    }

    // Método para aplicar un power-up según el índice seleccionado
    private void ApplyPowerUp(int index)
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            GameManager manager = gameManager.GetComponent<GameManager>();
            if (manager != null)
            {
                
                switch (index)
                {
                    case 0: // PowerUpHealthMax
                        manager.PowerUpHealthMax();
                        break;
                    case 1: // PowerUpHealing
                        manager.PowerUpHealing();
                        break;
                    case 2: // PowerUpSpeed
                        manager.PowerUpSpeed();
                        break;
                    case 3: // PowerUpAttraction
                        manager.PowerUpAttraction();
                        break;
                    case 4: // FireOrb
                        manager.PowerUpFireOrb();
                        break;
                    case 5: // Shop
                        manager.PowerUpShop();
                        break;
                    default:
                        Debug.LogWarning("Índice de power-up no válido.");
                        break;
                }
                
                
            }
            else
            {
                Debug.LogWarning("GameManager component not found on GameManager GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("GameManager GameObject not found in the scene.");
        }
    }

    // Método para destruir los objetos instanciados anteriores
    private void DestroySpawnedObjects()
    {
        // Destruir todos los hijos del objeto PowerUpSpawner
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}