using System.Collections;
using UnityEngine;

public class ActivateBreath : MonoBehaviour
{
    // Referencia al GameObject que queremos alternar
    public GameObject targetObject;

    // Variable para activar o desactivar el comportamiento
    public bool IsActivated;

    // Variable para almacenar la coroutine
    private Coroutine activacionCoroutine;

    private void Start()
    {
        // Inicia el ciclo de activación/desactivación si IsActivated es verdadero
        if (IsActivated)
        {
            activacionCoroutine = StartCoroutine(CicloActivacion());
        }
    }

    private void Update()
    {
        // Verifica si el estado de IsActivated ha cambiado en tiempo real
        if (IsActivated && activacionCoroutine == null)
        {
            activacionCoroutine = StartCoroutine(CicloActivacion());
        }
        else if (!IsActivated && activacionCoroutine != null)
        {
            StopCoroutine(activacionCoroutine);
            activacionCoroutine = null;
            if (targetObject != null)
            {
                targetObject.SetActive(false);
            }
        }
    }

    private IEnumerator CicloActivacion()
    {
        while (true)
        {
            if (targetObject != null)
            {
                targetObject.SetActive(true); // Activa el GameObject
            }
            yield return new WaitForSeconds(1.5f); // Espera 5 segundos

            if (targetObject != null)
            {
                targetObject.SetActive(false); // Desactiva el GameObject
            }
            yield return new WaitForSeconds(5f); // Espera otros 5 segundos
        }
    }
}