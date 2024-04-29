using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private GameManager gameManager;
    private Animator animator;
    private bool muerteReproducida = false;
    void Start(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponentInChildren<Animator>();
    }
    public void RecibirPupa(int pupa)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        gameManager.DecrementarSalud(pupa);
        if (gameManager.salud == 0)
        {
            print("La vida del jugador es 0");
            if (!muerteReproducida) // Verifica si la animación de muerte aún no se ha reproducido
            {
                rb.isKinematic = true;
                animator.SetTrigger("Die");
                muerteReproducida = true; // Marca la animación de muerte como reproducida
                MonoBehaviour[] mb = GetComponentsInChildren<MonoBehaviour>();
                foreach (MonoBehaviour m in mb)
                {
                    m.enabled = false;
                }
            }
        }
    }
}
