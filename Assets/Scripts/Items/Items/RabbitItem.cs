using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitItem : MonoBehaviour
{

    public void Activar()
    {
            // Encontrar el objeto GameManager en la escena
            GameManager gameManager = FindObjectOfType<GameManager>();

            // Verificar si se encontró el GameManager
            if (gameManager != null)
            {
                // Modificar la variable ratioDefensa del GameManager
                gameManager.GetItemRabbit();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("GameManager no encontrado en la escena.");
            }
        
    }
}