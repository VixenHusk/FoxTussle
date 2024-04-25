using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageTailspin : MonoBehaviour
{
    public int pupa;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            {
                if (other.GetComponent<EnemigoHealthManager>() != null){
                        other.GetComponent<EnemigoHealthManager>().HacerPupa(pupa);
                } else {
                    Debug.LogWarning("El enemigo no tiene el componente EnemigoHealthManager");
                }
            }

    }

}