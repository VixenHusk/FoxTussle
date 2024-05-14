using System.Collections;
using UnityEngine;

public class DesactivadorTexto : MonoBehaviour
{
    void Start()
    {
        // Inicia la corrutina que desactiva el GameObject después de 5 segundos
        StartCoroutine(DeactivateAfterTime(5f));
    }

    IEnumerator DeactivateAfterTime(float time)
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(time);
        // Desactiva el GameObject
        gameObject.SetActive(false);
    }
}