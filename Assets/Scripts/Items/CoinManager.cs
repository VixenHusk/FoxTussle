using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public float tiempoVida = 10f; // Tiempo en segundos antes de que la moneda se destruya automáticamente
    public AudioClip audioClip; // Clip de sonido que se reproducirá al recoger la moneda
    public int valor = 1;

    // Nuevas variables para la atracción de monedas
    public float attractionForce = 5f; // Fuerza de atracción reducida
    public float attractionDistance; // Distancia a la que se inicia la atracción
    public GameManager gameManager;

    private GameObject player; // Referencia al jugador

    void Start()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("El GameObject GameManager no tiene el componente GameManager adjunto.");
            }
        }
        else
        {
            Debug.LogError("No se ha encontrado el GameObject GameManager en la escena.");
        }
        
        attractionDistance = gameManager.distanciaAtraccion;
        //Debug.Log("El valor de distancia atraccion es: " + attractionDistance);
        Destroy(gameObject, tiempoVida);

        // Encontrar el objeto del jugador al comienzo del juego
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que ha entrado en contacto tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            GameObject.Find("GameManager")?.GetComponent<GameManager>()?.Monedas(valor);
            // Destruir la moneda
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        // Atracción de monedas hacia el jugador
        AttractCoins();
    }

    void AttractCoins()
    {
        // Encontrar todas las monedas en el escenario
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        // Atraer las monedas hacia el jugador si están dentro de la distancia de atracción
        foreach (GameObject coin in coins)
        {
            Rigidbody coinRigidbody = coin.GetComponent<Rigidbody>();
            if (coinRigidbody != null && player != null)
            {
                float distanceToPlayer = Vector3.Distance(player.transform.position, coin.transform.position);
                if (distanceToPlayer <= attractionDistance)
                {
                    print("Distancia de atraccion dentro del rango");
                    Vector3 direction = (player.transform.position - coin.transform.position).normalized;
                    coinRigidbody.AddForce(direction * attractionForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
                }
            }
        }
    }
}