using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTeleport : MonoBehaviour
{
    public string playerTag = "Player"; // Etiqueta del jugador
    public string teleportDestinationName = "TeleportDestination"; // Nombre del destino del teletransporte

    private Transform teleportDestination; // Destino del teletransporte

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
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag(playerTag) && teleportDestination != null)
        {
            // Teletransportar al jugador al primer destino
            other.transform.position = teleportDestination.position;
        }
    }
}