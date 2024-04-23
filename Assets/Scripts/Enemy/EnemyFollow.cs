using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//testing
public class EnemyFollow : MonoBehaviour
{
    public float velocidad = 5f;
    private Transform jugador; // Referencia al jugador en la escena

    void Start()
    {
        // Buscar al jugador en la escena por etiqueta (asegúrate de asignar la etiqueta "Jugador" al objeto del jugador)
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        if (jugador == null)
        {
            Debug.LogError("No se encontró el objeto del jugador en la escena.");
        }
    }

    void FixedUpdate()
    {
        if (jugador != null)
        {
            Vector3 direccion = jugador.position - transform.position;
            direccion.Normalize();

            // Mueve al objeto enemigo hacia el jugador usando Rigidbody
            GetComponent<Rigidbody>().MovePosition(transform.position + direccion * velocidad * Time.fixedDeltaTime);
        }
    }
}

