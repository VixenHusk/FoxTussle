using UnityEngine;

public class TeleportBack : MonoBehaviour
{
    public string shrineDoorName = "ShrineDoor"; // Nombre del GameObject que actúa como puerta del santuario
    public GameObject playerCamera; // Referencia a la cámara que sigue al jugador

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Buscar el GameObject con el nombre "ShrineDoor"
            GameObject shrineDoor = GameObject.Find(shrineDoorName);
            
            // Verificar si se encontró el objeto "ShrineDoor"
            if (shrineDoor != null)
            {
                // Obtener la posición de la puerta del santuario y desplazarla
                Vector3 teleportPosition = shrineDoor.transform.position;
                Destroy(shrineDoor);
                other.transform.position = teleportPosition;
        }
    }
}
}