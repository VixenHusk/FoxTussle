using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveTeleport : MonoBehaviour
{
    public string playerTag = "Player"; // Etiqueta del jugador
    public Vector3 newPosition = new Vector3(0, 0, 0); // Nueva posición del jugador
    public CargaEscenas cargaEscenas;
    public bool dentroCueva = false;
    
    public LoadingScreen loadingScreen;

    // Referencia al script PowerUpSpawner
    public PowerUpSpawner powerUpSpawner;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    GameObject FindInactiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        foreach (Transform obj in objs)
        {
            if (obj.hideFlags == HideFlags.None && obj.name == name)
            {
                return obj.gameObject;
            }
        }
        return null;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cargaEscenas = GameObject.Find("GameManager").GetComponent<CargaEscenas>();
        if (cargaEscenas == null)
        {
            Debug.LogError("El objeto GameManager no tiene el componente CargaEscenas.");
        }

        powerUpSpawner = FindInactiveObjectByName("PanelLevelUp").GetComponent<PowerUpSpawner>();
        if (powerUpSpawner == null)
        {
            Debug.LogError("El objeto PanelLevelUp no tiene el componente PowerUpSpawner.");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag(playerTag))
        {
            dentroCueva = true;

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

            // Carga la escena "DungeonScene"
            loadingScreen.LoadSceneAsync("DungeonScene");
        }
    }
}