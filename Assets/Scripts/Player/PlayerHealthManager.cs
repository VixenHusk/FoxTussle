using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private GameManager gameManager;
    private Animator animator;
    void Start(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponentInChildren<Animator>();
    }
    //FALTA PARAR DE ACTIVAR EL TRIGGER DE MORIR
    public void RecibirPupa(int pupa){
        Rigidbody rb = GetComponent<Rigidbody>();
        gameManager.DecrementarSalud(pupa);
        print(gameManager.salud);
        if (gameManager.salud == 0)
        {
            print("La vida del jugador es 0");
            rb.isKinematic = true;
            animator.SetTrigger("Die");
            MonoBehaviour[] mb = GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour m in mb)
            {
                m.enabled = false;
            }
            
        }
    }

}
