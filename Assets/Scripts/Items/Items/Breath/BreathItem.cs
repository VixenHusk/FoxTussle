using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathItem : MonoBehaviour
{
    // Referencia al script ActivateBreath
    public ActivateBreath activateBreath;
    void Start ()
    {
        // Busca y asigna el ActivateBreath en la escena
        activateBreath = FindObjectOfType<ActivateBreath>();

        // Verifica si ActivateBreath fue encontrado
        if (activateBreath == null)
        {
            Debug.LogError("ActivateBreath no encontrado en la escena.");
        }
    }

    public void Activar()
    {

            // Cambia el valor de IsActivated
            activateBreath.IsActivated = !activateBreath.IsActivated;
        
    }
}