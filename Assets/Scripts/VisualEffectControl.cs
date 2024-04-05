using UnityEngine;

public class VisualEffectController : MonoBehaviour
{
    public GameObject visualEffect; // Asigna el objeto del efecto visual desde el Inspector de Unity
    private bool isVisualEffectActive = false; // Variable para controlar si el efecto visual está activo
    private PlayerController playerController; // Referencia al script PlayerController

    private void Start()
    {
        // Obtener una referencia al script PlayerController
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Verifica si el botón Fire1 (botón izquierdo del ratón) está siendo pulsado
        if (Input.GetButtonDown("Fire1") && !isVisualEffectActive)
        {
            // Activa el efecto visual
            ActivateVisualEffect();
        }

        // Actualiza la rotación del efecto visual si está activo
        if (isVisualEffectActive)
        {
            UpdateVisualEffectRotation();
        }
    }

    private void ActivateVisualEffect()
    {
        // Establece la bandera indicando que el efecto visual está activo
        isVisualEffectActive = true;
        
        // Activa el efecto visual
        visualEffect.SetActive(true);

        // Puedes desactivar el efecto visual manualmente después de un tiempo determinado si lo deseas
        // Por ejemplo, aquí lo desactivaremos después de 2 segundos
        Invoke("DeactivateVisualEffect", 2f);
    }

    private void DeactivateVisualEffect()
    {
        // Desactiva el efecto visual
        visualEffect.SetActive(false);
        
        // Restablece la bandera indicando que el efecto visual ya no está activo
        isVisualEffectActive = false;
    }

    private void UpdateVisualEffectRotation()
    {
        // Obtiene la dirección del movimiento del jugador desde el script PlayerController
        Vector3 moveDirection = playerController.GetMovementDirection();

        // Si la dirección del movimiento no es cero, actualiza la rotación del efecto visual
        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            visualEffect.transform.rotation = newRotation;
        }
    }
}
