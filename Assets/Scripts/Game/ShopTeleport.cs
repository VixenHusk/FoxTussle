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

    private void Start()
    {
        // Buscar el destino del teletransporte por nombre
        GameObject destinationObject = GameObject.Find(teleportDestinationName);
        if (destinationObject != null)
        {
            teleportDestination = destinationObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el nombre: " + teleportDestinationName);
        }
        calamariTrap = GameObject.Find("-- CalamariTrap");
        spawnersObject = GameObject.Find("-- Spawners");
        // Buscar el GameObject "-- Enemies" por nombre
        enemiesParent = GameObject.Find("-- Enemies");
        if (enemiesParent == null)
        {
            Debug.LogError("No se encontró un objeto con el nombre: -- Enemies");
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

            //Desactivar CalamariTrap
            if (calamariTrap != null)
            {
                calamariTrap.SetActive(false);
            } else{
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