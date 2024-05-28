using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    // Método público para iniciar la carga de la escena
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private IEnumerator LoadAsynchronously(string sceneName)
    {
        // Activa la pantalla de carga
        loadingScreen.SetActive(true);

        // Inicia la carga asíncrona de la escena
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        // Evita que la escena se active automáticamente cuando termine la carga
        operation.allowSceneActivation = false;

        // Mientras la escena se está cargando
        while (!operation.isDone)
        {
            // El progreso varía entre 0 y 0.9, así que lo escalamos a 0 y 1
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            // Actualiza la barra de progreso y el texto
            progressBar.value = progress;
            progressText.text = (progress * 100f).ToString("F2") + "%";

            // Si la carga está completa (99%), permitimos que la escena se active
            if (operation.progress >= 0.9f)
            {
                    operation.allowSceneActivation = true;
                    
            }

            yield return null;
        }
    }
}