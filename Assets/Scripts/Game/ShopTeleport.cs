using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTeleport : MonoBehaviour
{
    public string playerTag = "Player"; // Etiqueta del jugador
    public Transform teleportDestination; // Destino del teletransporte la primera vez
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag(playerTag))
                // Teletransportar al jugador al primer destino
                other.transform.position = teleportDestination.position;

    }
}