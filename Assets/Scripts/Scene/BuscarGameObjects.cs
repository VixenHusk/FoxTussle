using UnityEngine;

public class BuscarGameObjects : MonoBehaviour
{
    private GameObject player;
    private GameObject hud;
    private GameObject camerasParent;
    private GameObject enemiesParent;
    private GameObject fireOrbsParent;
    private GameObject gameManager;

    private void Start()
    {
        // Buscar el objeto "Player" por etiqueta
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No se encontró el objeto 'Player'.");
        }
        gameManager = GameObject.Find("GameManager");
        if (gameManager == null)
        {
            Debug.LogError("No se encontró el objeto 'GameManager'.");
        }

        // Buscar el objeto "HUD" por nombre
        hud = GameObject.Find("HUD");
        if (hud == null)
        {
            Debug.LogError("No se encontró el objeto 'HUD'.");
        }

        // Buscar el objeto "Cameras" bajo el objeto "HUD"
        camerasParent = hud != null ? hud.transform.Find("Cameras").gameObject : null;
        if (camerasParent == null)
        {
            Debug.LogError("No se encontró el objeto 'Cameras' bajo el objeto 'HUD'.");
        }

        // Buscar el objeto "Enemies" por etiqueta
        enemiesParent = GameObject.FindGameObjectWithTag("Enemies");
        if (enemiesParent == null)
        {
            Debug.LogError("No se encontró el objeto 'Enemies'.");
        }

        // Buscar el objeto "FireOrbs" por etiqueta
        fireOrbsParent = GameObject.FindGameObjectWithTag("FireOrbs");
        if (fireOrbsParent == null)
        {
            Debug.LogError("No se encontró el objeto 'FireOrbs'.");
        }
    }
}
