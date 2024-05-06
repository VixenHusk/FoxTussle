using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveTeleport : MonoBehaviour
{
    public string playerTag = "Player"; // Etiqueta del jugador
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag(playerTag))
                SceneManager.LoadScene("DungeonScene");
    }
}