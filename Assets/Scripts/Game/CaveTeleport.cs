using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveTeleport : MonoBehaviour
{
    public string playerTag = "Player"; // Etiqueta del jugador
    public Vector3 newPosition = new Vector3(0, 0, 0); // Nueva posición del jugador
    public CargaEscenas cargaEscenas;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag(playerTag))
        {
            // Encuentra al jugador
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            if (player != null)
            {
                // Mueve al jugador a la nueva posición
                player.transform.position = newPosition;
            }

            // Encuentra todos los enemigos en el objeto "-- Enemies" y los elimina
            GameObject enemiesContainer = GameObject.Find("-- Enemies");
            if (enemiesContainer != null)
            {
                foreach (Transform enemy in enemiesContainer.transform)
                {
                    Destroy(enemy.gameObject);
                }
            }
            // Carga la escena "DungeonScene"
            SceneManager.LoadScene("DungeonScene"); 
        }
    }
}
