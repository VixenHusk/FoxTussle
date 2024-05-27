using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // Lista de prefabs de enemigos
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

        // Selecciona aleatoriamente un prefab de la lista
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // Instancia el prefab seleccionado aleatoriamente
        GameObject newEnemy = Instantiate(randomPrefab, transform.position, transform.rotation, enemiesParent.transform);

        if (contador >= numeroElementos)
        {
            CancelInvoke("Spawn");
        }
    }
}