using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset = new Vector3(2f, -1f, 1f); // Offset en XYZ
    public string TagBuscar;

    // Actualiza la posición del sistema de partículas para que siga al jugador con el offset
    void FixedUpdate()
    {
        GameObject player = GameObject.FindWithTag(TagBuscar); // Busca el jugador por su etiqueta
        if (player != null)
        {
            transform.position = player.transform.position + offset; // Aplica el offset a la posición del jugador
        }
    }
}
