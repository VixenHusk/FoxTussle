using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found!");
        }

        playerController = GetComponentInChildren<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found!");
        }
    }

    void FixedUpdate()
    {
        // Obtener la velocidad actual del PlayerController
        float velocidadActual = playerController.VelocidadActual;

        // Actualizar el parámetro "Velocity" del Animator con la velocidad actual
        animator.SetFloat("Velocity", velocidadActual);
    }

    
}

