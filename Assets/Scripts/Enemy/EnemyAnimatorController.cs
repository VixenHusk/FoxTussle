using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;
    private EnemigoGauntlet enemigoGauntlet;
    private GameObject player;
    public float distanciaAtaque = 1.1f;
    public float distanciaAlejarse = 1.1f;
    public string playerTag = "Player";
    private bool isDead = false; // Variable para controlar si el enemigo está muerto
    private float velocidadActual = 0; // Variable para almacenar la velocidad actual

    void Start()
    {
        // Buscar el Animator en el GameObject padre
        animator = GetComponentInParent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found!");
        }

        // Buscar el componente EnemigoGauntlet en el GameObject padre
        enemigoGauntlet = GetComponentInParent<EnemigoGauntlet>();

        if (enemigoGauntlet == null)
        {
            Debug.LogError("PlayerController not found!");
        }

        // Buscar al jugador por su etiqueta en la escena
        player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null)
        {
            Debug.LogError("Player GameObject not found with tag: " + playerTag);
        }
    }

    void FixedUpdate()
    {
        if (isDead) return; // Si el enemigo está muerto, no hacemos nada

        if (player == null) return;

        float distanciaAlJugador = Vector3.Distance(transform.position, player.transform.position);

        if (distanciaAlJugador <= distanciaAtaque)
        {
            animator.SetTrigger("Attack");
        }
        else if (distanciaAlJugador > distanciaAlejarse)
        {
            animator.ResetTrigger("Attack");
        }

        velocidadActual = enemigoGauntlet.speed;

        animator.SetFloat("Velocity", velocidadActual);
    }

    // Método para manejar la muerte del enemigo
    public void Die()
    {
        isDead = true; // Marcar al enemigo como muerto
        animator.SetTrigger("Death"); // Activar la animación de muerte
        animator.ResetTrigger("Attack"); // Reiniciar el trigger de ataque para detener la animación de ataque
    }

    public void isHitted()
    {
        if (isDead){
            animator.ResetTrigger("isHit");
        }else{
        animator.SetTrigger("isHit");
        }
    }

    // Este método será llamado desde un AnimationEvent
    public void OnDeathAnimationEnd()
    {
        Destroy(transform.parent.gameObject);
    }
}


