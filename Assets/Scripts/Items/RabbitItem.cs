using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitItem : MonoBehaviour
{
    // Referencia al GameManager
    public GameManager gameManager;

    // Bandera para verificar si el jugador está en el trigger
    private bool playerInTrigger = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado en la escena.");
        }
    }
    // Método que se ejecuta cuando otro objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger tiene una etiqueta específica
        if (other.CompareTag("Player"))
        {
            // Marca que el jugador está en el trigger
            playerInTrigger = true;
        }
    }

    // Método que se ejecuta cuando otro objeto sale del trigger
    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale del trigger tiene una etiqueta específica
        if (other.CompareTag("Player"))
        {
            // Marca que el jugador ha salido del trigger
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        // Verifica si el jugador está en el trigger y si se presiona la tecla E
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Llama al método del GameManager
            gameManager.GetItemRabbit();
            Destroy(gameObject);
        }
    }
}