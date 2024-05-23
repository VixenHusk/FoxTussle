using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTeleport : MonoBehaviour
{
    public string playerTag = "Player"; // Etiqueta del jugador
    public string teleportDestinationName = "TeleportDestination"; // Nombre del destino del teletransporte

    private Transform teleportDestination; // Destino del teletransporte
    private GameObject enemiesParent; // Referencia al GameObject "-- Enemies"
    private GameObject spawnersObject;
    private GameObject calamariTrap;
    private SpawnerItem spawnerItem;
    private PriceDetector priceDetector;

    private void Start()
    {
        // Buscar y activar todos los GameObjects que tienen el script ItemDetector primero
        ActivateAllItemDetectors();

        // Ahora buscar los otros componentes necesarios
        spawnerItem = FindObjectOfType<SpawnerItem>();
        if (spawnerItem != null)
        {
            spawnerItem.SetShopItems();
            Debug.Log("SpawnerItem encontrado y configurado.");
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el script SpawnerItem en la escena.");
        }

        priceDetector = FindObjectOfType<PriceDetector>();
        if (priceDetector != null)
        {
            priceDetector.StartPrices();
            Debug.Log("PriceDetector encontrado y configurado.");
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el script PriceDetector en la escena.");
        }

        // Buscar el destino del teletransporte por nombre
        GameObject destinationObject = GameObject.Find(teleportDestinationName);
        if (destinationObject != null)
        {
            teleportDestination = destinationObject.transform;
            Debug.Log("Destino de teletransporte encontrado: " + teleportDestinationName);
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el nombre: " + teleportDestinationName);
        }

        // Buscar el GameObject "-- CalamariTrap" por nombre
        calamariTrap = GameObject.Find("-- CalamariTrap");
        if (calamariTrap != null)
        {
            Debug.Log("CalamariTrap encontrado.");
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el nombre: -- CalamariTrap");
        }

        // Buscar el GameObject "-- Spawners" por nombre
        spawnersObject = GameObject.Find("-- Spawners");
        if (spawnersObject != null)
        {
            Debug.Log("-- Spawners encontrado.");
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el nombre: -- Spawners");
        }

        // Buscar el GameObject "-- Enemies" por nombre
        enemiesParent = GameObject.Find("-- Enemies");
        if (enemiesParent != null)
        {
            Debug.Log("-- Enemies encontrado.");
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el nombre: -- Enemies");
        }
    }

    private void ActivateAllItemDetectors()
    {
        // Buscar todos los componentes ItemDetector en la escena, incluyendo objetos desactivados
        ItemDetector[] itemDetectors = Resources.FindObjectsOfTypeAll<ItemDetector>();

        foreach (ItemDetector detector in itemDetectors)
        {
            GameObject obj = detector.gameObject;
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                Debug.Log("Activado GameObject con ItemDetector: " + obj.name);
            }
            // Asegúrate de que los objetos activos también inicien sus referencias si es necesario
            detector.StartReferences();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag(playerTag) && teleportDestination != null)
        {
            // Teletransportar al jugador al primer destino
            other.transform.position = teleportDestination.position;

            // Congelar los GameObjects hijos del GameObject "-- Enemies"
            if (enemiesParent != null)
            {
                foreach (Transform enemy in enemiesParent.transform)
                {
                    FreezeGameObject(enemy.gameObject);
                }
            }
            // Desactivar el GameObject "-- Spawners"
            if (spawnersObject != null)
            {
                spawnersObject.SetActive(false);
            }

            // Desactivar CalamariTrap
            if (calamariTrap != null)
            {
                calamariTrap.SetActive(false);
            } 
            else
            {
                print("Error");
            }
        }
    }

    private void FreezeGameObject(GameObject obj)
    {
        // Desactivar el Rigidbody si existe
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        // Desactivar scripts de movimiento específicos (ejemplo)
        MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }
}
