using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour
{

private Transform centerObject; // Objeto alrededor del cual quieres rotar
private Vector3 initialOffset; // Posición inicial del objeto rotado en relación con el centroObject

void Start()
{
    // Asignar el objeto alrededor del cual quieres rotar
    centerObject = GameObject.Find("Fox").transform; // Reemplaza "NombreDelObjetoCentro" con el nombre real del objeto

    // Calcular el desplazamiento inicial del objeto rotado en relación con el centroObject
    initialOffset = transform.position - centerObject.position;
}

void Update()
{
    // Calcular la nueva posición del objeto rotado sumando la posición del centroObject con el desplazamiento inicial
    Vector3 targetPosition = centerObject.position + initialOffset;

    // Aplicar la rotación alrededor del centroObject
    transform.RotateAround(centerObject.position, Vector3.up, 100 * Time.deltaTime);

    // Mantener la posición relativa constante
    transform.position = targetPosition;
}

}
