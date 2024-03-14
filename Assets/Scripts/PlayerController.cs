using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [Range(1, 100)]
    public float velocidad = 8f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        // Obtener la c�mara principal para ajustar el movimiento a la perspectiva isom�trica
        Camera mainCamera = Camera.main;
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f; // Asegurar que no haya movimiento vertical
        cameraRight.y = 0f; // Asegurar que no haya movimiento vertical
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calcular el vector de movimiento en funci�n de la vista isom�trica de la c�mara
        Vector3 movimiento = cameraForward * movimientoV + cameraRight * movimientoH;

        // Verificar si no se est� presionando ninguna tecla de movimiento
        if (Mathf.Approximately(movimiento.magnitude, 0.0f))
        {
            // Detener el movimiento estableciendo la velocidad a cero
            rb.velocity = Vector3.zero;
        }
        else
        {
            // Aplicar la velocidad directamente sin inercia
            rb.velocity = movimiento * velocidad;
        }
    }
}

