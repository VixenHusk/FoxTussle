using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBreath : MonoBehaviour
{
    // Referencia al GameObject que queremos alternar
    public GameObject targetObject;

    // Variable para activar o desactivar el comportamiento
    public bool IsActivated;

    private void Start()
    {
        // Inicia el ciclo de activación/desactivación si IsActivated es verdadero
        if (IsActivated)
        {
            EmpezarFuego();
        }
    }

    private void Update()
    {
        // Verifica si el estado de IsActivated ha cambiado en tiempo real
        if (IsActivated && !IsInvoking("ActivarDesactivar"))
        {
            EmpezarFuego();
        }
        else if (!IsActivated && IsInvoking("ActivarDesactivar"))
        {
            CancelInvoke("ActivarDesactivar");
        }
    }

    private void EmpezarFuego()
    {
        // Comienza a invocar el método Toggle cada 5 segundos
        InvokeRepeating("ActivarDesactivar", 0f, 5f);
    }

    private void ActivarDesactivar()
    {
        // Alterna el estado activo del GameObject objetivo
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}
