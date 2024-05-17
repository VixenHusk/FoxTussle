using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemiesParent;
    public int numeroElementos;
    public int tiempoEntreSpawn;
    private int contador = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        InvokeRepeating("Spawn", tiempoEntreSpawn, tiempoEntreSpawn);
    }
    void OnDisable() {
        CancelInvoke("Spawn");
    }

    // Método para instanciar un enemigo y ajustar su posición
    void Spawn()
    {
        contador++;
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation, enemiesParent.transform);

        if (contador >= numeroElementos)
        {
            CancelInvoke("Spawn");
        }
    }
}
