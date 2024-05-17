using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCalamari : MonoBehaviour
{
    public GameObject trapObject; // El objeto que se activará y desactivará
    public GameObject movingObject; // El objeto que se activará y se moverá
    public Transform targetTransform; // La posición a la que se moverá el movingObject
    public float activationInterval = 60f; // Intervalo de tiempo entre activaciones
    public float activeDuration = 30f; // Duración durante la cual el objeto está activado

    void OnEnable()
    {
        StartCoroutine(ControlTrap());
    }
    void OnDisable(){
        StopCoroutine(ControlTrap());
    }

    private IEnumerator ControlTrap()
    {
        while (true)
        {
            yield return new WaitForSeconds(activationInterval);

            // Activar los objetos
            trapObject.SetActive(true);
            movingObject.SetActive(true);

            // Mover el movingObject a la posición del targetTransform
            movingObject.transform.position = targetTransform.position;

            // Esperar durante el tiempo que el objeto está activo
            yield return new WaitForSeconds(activeDuration);

            // Desactivar los objetos
            trapObject.SetActive(false);
            movingObject.SetActive(false);

            GameObject parentObject = GameObject.Find("-- Calamaris");

            if (parentObject != null)
            {
                // Destruir todos los hijos del GameObject encontrado
                foreach (Transform child in parentObject.transform)
                {
                    Destroy(child.gameObject);
                }
            }else{
                print("no ta");
            }

        }
    }
}