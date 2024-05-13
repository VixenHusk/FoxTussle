using UnityEngine;

public class RotateAroundTarget : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidad de rotación

    private Transform targetTransform; // Referencia al transform del objeto alrededor del cual queremos rotar

    private Vector3 initialOffset; // Posición inicial del objeto relativa al objetivo

    void Start()
    {
        // Buscar el GameObject con el nombre "transformDMG"
        GameObject targetObject = GameObject.Find("transformDMG");

        if (targetObject != null)
        {
            // Obtener el Transform del objeto alrededor del cual queremos rotar
            targetTransform = targetObject.transform;

            // Calcular la posición inicial relativa del objeto respecto al objetivo
            initialOffset = transform.position - targetTransform.position;
        }
        else
        {
            Debug.LogWarning("No se encontró un GameObject con el nombre 'transformDMG'. Asegúrate de que el nombre sea correcto.");
        }
    }

    void Update()
    {
        if (targetTransform != null)
        {
            // Calcular la rotación alrededor del eje del objetivo
            Quaternion rotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.up);

            // Aplicar la rotación al offset inicial
            initialOffset = rotation * initialOffset;

            // Calcular la nueva posición del objeto
            Vector3 newPosition = targetTransform.position + initialOffset;

            // Asignar la nueva posición al objeto
            transform.position = newPosition;

            // Mantener el objeto mirando hacia el objetivo
            transform.LookAt(targetTransform);
        }
    }
}
