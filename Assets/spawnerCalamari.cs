using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerCalamari : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemiesParent;
    public int numeroElementos;
    public int tiempoEntreSpawn;
    private int contador = 0;

    void OnEnable()
    {
        // Reiniciar el contador cada vez que se activa el objeto
        contador = 0;
        // Comenzar a invocar el método Spawn
        InvokeRepeating("Spawn", tiempoEntreSpawn, tiempoEntreSpawn);
    }

    void OnDisable()
    {
        // Detener la invocación cuando el objeto se desactiva
        CancelInvoke("Spawn");
    }

    // Método para instanciar un enemigo y ajustar su posición
    void Spawn()
    {
        if (contador < numeroElementos)
        {
            contador++;
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation, enemiesParent.transform);
        }
        else
        {
            // Detener la invocación una vez se alcanza el número deseado de enemigos
            CancelInvoke("Spawn");
        }
    }
}
