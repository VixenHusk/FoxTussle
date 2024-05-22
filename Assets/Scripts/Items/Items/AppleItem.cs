using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
            // Encontrar el objeto GameManager en la escena
            GameManager gameManager = FindObjectOfType<GameManager>();

            // Verificar si se encontró el GameManager
            if (gameManager != null)
            {
                // Modificar la variable ratioDefensa del GameManager
                gameManager.multiplicadorEXP = 1.75f;
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("GameManager no encontrado en la escena.");
            }

    }
}