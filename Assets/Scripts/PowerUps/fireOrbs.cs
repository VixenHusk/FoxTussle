using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireOrbs : MonoBehaviour
{
    public List<GameObject> orbes = new List<GameObject>(); // Lista para almacenar los orbes disponibles
    public int maxOrbes = 4; // Máximo número de orbes permitidos
    private int currentOrbIndex = 0; // Índice del orbe actualmente activo

    // Método para activar el siguiente orbe
    public void FireOrb()
    {
        if (currentOrbIndex < maxOrbes)
        {
            // Activar el próximo orbe
            orbes[currentOrbIndex].SetActive(true);

            // Incrementar el índice para el próximo orbe
            currentOrbIndex++;
        }
        else
        {
            Debug.Log("Ya hay tres orbes activos.");
        }
    }
}
