using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerShop : MonoBehaviour
{
    public GameObject prefab;
    public int numeroElementos;
    public int tiempoEntreSpawn;
    public float radio; // Radio del círculo
    private int contador = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", tiempoEntreSpawn, tiempoEntreSpawn);
    }

    // Update is called once per frame
    void Spawn()
    {
        contador++;
        // Generar posición aleatoria dentro del círculo
        Vector2 randomCircle = Random.insideUnitCircle * radio;
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0, randomCircle.y) + transform.position;

        Instantiate(prefab, spawnPosition, Quaternion.identity, transform);

        if (contador >= numeroElementos)
        {
            CancelInvoke("Spawn");
        }
    }

    // Método para dibujar el círculo en el editor de Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
