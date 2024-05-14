using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilAvanzadoEnemigo : MonoBehaviour
{

    //public GameObject efectoImpactoConAudio;
    public int pupa;
    public string tagPlayer;

    public void Start(){
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter(Collider other){
        /*if (efectoImpactoConAudio){
            Instantiate(efectoImpactoConAudio, transform.position, transform.rotation);
        } else {
            Debug.LogWarning("El arma no tiene asociado un prefab de explosión (o similar)");
        }*/
        if (other.CompareTag(tagPlayer))
        {
            if (other.GetComponent<PlayerHealthManager>() != null){
                    other.GetComponent<PlayerHealthManager>().RecibirPupa(pupa);
            } else {
                Debug.LogWarning("El enemigo no tiene el componente PlayerHealthManager");
            }
                  Destroy(gameObject);
        }
    }
}