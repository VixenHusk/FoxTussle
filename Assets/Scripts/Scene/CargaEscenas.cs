using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaEscenas : MonoBehaviour
{
    public GameObject[] objetosPersistentes;

    private void Awake()
    {
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