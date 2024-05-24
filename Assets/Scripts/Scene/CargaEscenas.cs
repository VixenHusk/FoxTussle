using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaEscenas : MonoBehaviour
{
    // Variable no estática para asignar desde el Inspector
    public GameObject[] objetosPersistentesInspector;

    // Variable estática para usar en otros scripts
    public static GameObject[] objetosPersistentes;

    private static CargaEscenas instance;

    private void Awake()
    {
        // Implementación del patrón Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Asigna los objetos del Inspector a la variable estática solo la primera vez
            objetosPersistentes = objetosPersistentesInspector;

            foreach (GameObject obj in objetosPersistentes)
            {
                DontDestroyOnLoad(obj);
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Suscribirse al evento SceneManager.sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
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

    // Se llama cuando se destruye el objeto
    private void OnDestroy()
    {
        // Desuscribirse del evento SceneManager.sceneLoaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Se llama cuando se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Si se carga la escena principal (MainScene)
        if (scene.name == "MainScene")
        {
            // Encuentra y elimina los objetos de la escena principal (MainScene) que coinciden con los nombres de los objetos persistentes
            GameObject[] objetosEnMainScene = scene.GetRootGameObjects();
            foreach (GameObject obj in objetosEnMainScene)
            {
                // Comprueba si el nombre del objeto de MainScene coincide con alguno de los objetos persistentes
                if (System.Array.Exists(objetosPersistentes, x => x.name == obj.name))
                {
                    Destroy(obj);
                }
            }
        }
    }
}