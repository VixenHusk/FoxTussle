using UnityEngine;

public class ActivadorInventario : MonoBehaviour
{
    public GameObject panelInventario;
    private bool inventarioActivo = false;
    private bool tiempoPausado = false;
    public GameObject panelLevel;

    void Update()
    {
        // Verificar si se presionó la tecla 'I' y el panel de nivel está desactivado
        if (Input.GetKeyDown(KeyCode.I) && !panelLevel.activeSelf)
        {
            // Pausar o reanudar el tiempo
            tiempoPausado = !tiempoPausado;
            Time.timeScale = tiempoPausado ? 0 : 1;

            // Mostrar u ocultar el inventario
            inventarioActivo = tiempoPausado; // El inventario se muestra solo cuando el tiempo está pausado
            panelInventario.SetActive(inventarioActivo);
        }
    }
}