using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaEscenas : MonoBehaviour
{
    // Variable no estática para asignar desde el Inspector
    public GameObject[] objetosPersistentesInspector;

    // Variable estática para usar en otros scripts
    public static GameObject[] objetosPersistentes;

    private void Awake()
    {
        // Asigna los objetos del Inspector a la variable estática
        objetosPersistentes = objetosPersistentesInspector;

        foreach (GameObject obj in objetosPersistentes)
        {
            DontDestroyOnLoad(obj);
        }
    }

    public void JugadorHaMuerto()
    {
        // Obtén la escena actual
        Scene escenaActual = SceneManager.GetActiveScene();

        // Mueve los objetos persistentes a la escena actual
        foreach (GameObject obj in objetosPersistentes)
        {
            SceneManager.MoveGameObjectToScene(obj, escenaActual);
        }
    }
}