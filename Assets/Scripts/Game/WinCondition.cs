using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarPuerta : MonoBehaviour
{
    public string nombreEscenaACargar;
    public bool playerEntered = false;
    public int price = 888;
    private GameManager gameManager;
    public GameObject textShrine;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    {
           if (other.CompareTag("Player"))
            {
                playerEntered = true;

                if (textShrine != null)
                {
                    textShrine.SetActive(true);
                }
                
            }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEntered = false;

            // Desactivamos el TextShrine cuando el jugador sale del trigger
            if (textShrine != null)
            {
                textShrine.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (playerEntered && Input.GetKeyDown(KeyCode.E))
        {
            // Verificar si el jugador tiene suficientes monedas para comprar el objeto
            if (gameManager != null && gameManager.CheckMonedas(price))
            {
                // Sustraer las monedas del GameManager
                gameManager.Monedas(-price); // Pasar el costo del objeto como valor negativo
                SceneManager.LoadScene(nombreEscenaACargar);
                Debug.Log("Se cargó la escena: " + nombreEscenaACargar);   
            }
            else
            {
                Debug.Log("No tienes suficientes monedas para comprar este objeto");
            }
        }
    }
}