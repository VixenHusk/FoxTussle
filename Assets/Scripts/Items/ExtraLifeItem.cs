using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifeItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entró en el trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Encontrar el objeto GameManager en la escena
            GameManager gameManager = FindObjectOfType<GameManager>();

            // Verificar si se encontró el GameManager
            if (gameManager != null)
            {
                // Modificar la variable ratioDefensa del GameManager
                gameManager.vidaExtra += 1;
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("GameManager no encontrado en la escena.");
            }
        }
    }
}