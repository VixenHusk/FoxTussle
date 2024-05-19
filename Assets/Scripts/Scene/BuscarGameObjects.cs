using UnityEngine;
using UnityEngine.SceneManagement;

public class BuscarGameObjects : MonoBehaviour
{
    void Awake()
    {
        // Verifica si objetosPersistentes no es nulo
        if (CargaEscenas.objetosPersistentes != null)
        {
            // Obtén la escena actual
            Scene escenaActual = SceneManager.GetActiveScene();

            // Mueve los objetos persistentes a la escena actual
            foreach (GameObject obj in CargaEscenas.objetosPersistentes)
            {
                SceneManager.MoveGameObjectToScene(obj, escenaActual);
            }
        }
        else
        {
            Debug.LogError("objetosPersistentes es nulo. Asegúrate de que CargaEscenas haya sido inicializado.");
        }
    }
}
