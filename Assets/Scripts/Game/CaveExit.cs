using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveExit : MonoBehaviour
{
    public string playerTag = "Player"; // Etiqueta del jugador
    public Vector3 newPosition = new Vector3(-32, 0, 85); // Nueva posición del jugador
    public bool dentroCueva = true;

    // Referencia al script PowerUpSpawner
    private PowerUpSpawner powerUpSpawner;

    private void Start()
    {
        // Buscar el script CargaEscenas por nombre
        CargaEscenas cargaEscenas = FindObjectOfType<CargaEscenas>();
        if (cargaEscenas == null)
        {
            Debug.LogWarning("CargaEscenas no encontrado");
        }

        // Buscar el script PowerUpSpawner por nombre, incluyendo objetos desactivados
        powerUpSpawner = FindObjectOfType<PowerUpSpawner>(true); // true incluye objetos desactivados
        if (powerUpSpawner == null)
        {
            Debug.LogWarning("PowerUpSpawner no encontrado");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag(playerTag))
        {
            dentroCueva = false;

            // Encuentra al jugador y lo mueve a la nueva posición
            GameObject player = GameObject.FindGameObjectWithTag(playerTag);
            if (player != null)
            {
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

            // Modifica la variable dentroCueva en el PowerUpSpawner
            if (powerUpSpawner != null)
            {
                powerUpSpawner.dentroCueva = dentroCueva;
            }

            // Carga la escena "MainScene"
            SceneManager.LoadScene("MainScene");
        }
    }
}