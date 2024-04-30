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
            if (!muerteReproducida) // Verifica si la animación de muerte aún no se ha reproducido
            {
                // Buscar un GameObject hijo por su nombre
                Transform childTransform = transform.Find("AreaDMG(Clone)");

                // Verificar si se encontró el GameObject hijo
                if (childTransform != null)
                {
                    // Destruir el GameObject hijo
                    Destroy(childTransform.gameObject);
                    Debug.Log("Se elimino el GameObject hijo: " + childTransform.name);
                }
                else
                {
                    // El GameObject hijo no fue encontrado
                    Debug.LogWarning("No estaba atacando mientras moria");
                }

                rb.isKinematic = true;
                animator.SetTrigger("Die");
                muerteReproducida = true; // Marca la animación de muerte como reproducida
                
                MonoBehaviour[] mb = GetComponentsInChildren<MonoBehaviour>();  // En teoria, desabilita todos los componentes del jugador
                foreach (MonoBehaviour m in mb)
                {
                    m.enabled = false;
                }
            }
        }
    }
}
