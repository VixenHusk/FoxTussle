using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject configPanel;
    public GameObject introPanel;

    public GameObject creditPanel;

    public void IniciarJuego()
    {
        SceneManager.LoadScene(1);
    }

        public void VolverInicio()
    {
        SceneManager.LoadScene(0);
    }

    public void MostrarInstrucciones()
    {
                // Verifica si el GameObject es válido
        if (configPanel != null)
        {
            // Activa el GameObject
            configPanel.SetActive(true);
            introPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("No se ha asignado ningún GameObject para activar.");
        }
    }

        public void NoMostrarInstrucciones()
    {
                // Verifica si el GameObject es válido
        if (configPanel != null)
        {
            // Activa el GameObject
            configPanel.SetActive(false);
            introPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("No se ha asignado ningún GameObject para activar.");
        }
    }
    
        public void MostrarCreditos()
    {
                // Verifica si el GameObject es válido
        if (creditPanel != null)
        {
            // Activa el GameObject
            creditPanel.SetActive(true);
            introPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("No se ha asignado ningún GameObject para activar.");
        }
    }

        public void NoMostrarCreditos()
    {
                // Verifica si el GameObject es válido
        if (creditPanel != null)
        {
            // Activa el GameObject
            creditPanel.SetActive(false);
            introPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("No se ha asignado ningún GameObject para activar.");
        }
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
