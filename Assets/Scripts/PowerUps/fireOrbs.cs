using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireOrbs : MonoBehaviour
{
    public GameObject orbePrefab; // Prefab del orbe
    public GameObject player; // GameObject alrededor del cual rotará el orbe
    public List<GameObject> orbes = new List<GameObject>(); // Lista para almacenar los orbes spawnados
    public float distancia = 2f; // Distancia desde el player
    public int maxOrbes = 3; // Máximo número de orbes permitidos

    // Método para spawnear un nuevo orbe
    public void FireOrb()
    {
        if (orbes.Count < maxOrbes)
        {
        // Calcular la posición del nuevo orbe basado en el número de orbes ya generados
        Vector3 newPosition = Vector3.zero; // Posición inicial (0, 0, 0) como referencia

        if (orbes.Count == 0) // Primer orbe
        {
            newPosition = player.transform.position + new Vector3(3f, 1f, 0f); // Posición relativa al jugador: X: 3, Y: 1, Z: 0
        }
        else if (orbes.Count == 1) // Segundo orbe
        {
            newPosition = player.transform.position + new Vector3(-3f, 1f, 0f); // Posición relativa al jugador: X: -3, Y: 1, Z: 0
        }
        else if (orbes.Count == 2) // Tercer orbe
        {
            newPosition = player.transform.position + new Vector3(0f, 1f, 3f); // Posición relativa al jugador: X: 0, Y: 1, Z: 3
        }

        // Instanciar el nuevo orbe en la posición calculada
        GameObject newOrbe = Instantiate(orbePrefab, newPosition, Quaternion.identity);
        newOrbe.transform.parent = player.transform; // Asignar al jugador como padre del orbe
        orbes.Add(newOrbe); // Agregar el orbe a la lista
        }else{
            print("Si hay mas de 3");
        }
    }
}
