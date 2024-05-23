using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Lista de GameObjects que se pueden instanciar
    private List<int> availableIndices = new List<int>(); // Lista de índices disponibles para instanciar

    public bool dentroCueva = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        InitializeHUD();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeHUD();
    }

    private void InitializeHUD()
    {
        ResetAvailableIndices();
        SpawnRandomObjects();
    }

    public void SpawnRandomObjects()
    {
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

    private int GetUniqueRandomIndex()
    {
        if(dentroCueva == true){
            int randomIndex = Random.Range(0, availableIndices.Count-1);
            print(availableIndices.Count-1);
            return availableIndices[randomIndex];

        }else{
            int randomIndex = Random.Range(0, availableIndices.Count);
            print(availableIndices.Count-1);
            return availableIndices[randomIndex];
        }
    }

    private void ResetAvailableIndices()
    {
        availableIndices.Clear();
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            availableIndices.Add(i);
        }
    }

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
                    case 5: // Regeneration
                        manager.PowerUpRegeneration();
                        break;
                    case 6: // Shop
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

    private void DestroySpawnedObjects()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}