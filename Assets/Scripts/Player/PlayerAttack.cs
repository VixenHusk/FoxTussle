using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private bool canAttack = true;
    public float attackCooldown = 5f; // Tiempo de enfriamiento entre ataques
    public GameObject playerObject; // Referencia al GameObject del jugador
    public GameObject prefabTailSpinDmg;
    private GameObject tailSpinDmgInstance;
    public Transform transformAreaDmg;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Verificar si se puede atacar y si se presionó el botón Fire1
        if (canAttack && Input.GetButtonDown("Fire1"))
        {
            // Activar la animación de ataque
            animator.SetTrigger("Attack");
            canAttack = false; // Desactivar la posibilidad de atacar
            StartCoroutine(EnableAttackAfterCooldown(attackCooldown));
        }
    }

    public void StartAttack()
    {
        tailSpinDmgInstance = Instantiate(prefabTailSpinDmg, transformAreaDmg.position, transformAreaDmg.rotation, playerObject.transform);
    }

    // Método para manejar el evento de animación "EndAttack"
    public void EndAttack()
    {
        // Detener el trigger "Attack"
        animator.ResetTrigger("Attack");
        Destroy(tailSpinDmgInstance);
    }

    IEnumerator EnableAttackAfterCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true; // Activar la posibilidad de atacar nuevamente
    }
}
