using UnityEngine;

public class TeleportBack : MonoBehaviour
{
    public string shrineDoorName = "ShrineDoor"; // Nombre del GameObject que actúa como puerta del santuario
    public GameObject spawners;
    public GameObject calamari;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            
            // Buscar el GameObject con el nombre "-- Enemies"
            GameObject enemiesParent = GameObject.Find("-- Enemies");
            if (enemiesParent == null)
            {
                Debug.LogError("No se encontró un objeto con el nombre: -- Enemies");
                return; // Salir del método si no se encuentra el objeto
            }
            //activa spawners
            spawners.SetActive(true);
            //activa calamari
            calamari.SetActive(true);

            // Buscar el GameObject con el nombre "ShrineDoor"
            GameObject shrineDoor = GameObject.Find(shrineDoorName);
            if (shrineDoor != null)
            {
                // Obtener la posición de la puerta del santuario y desplazarla
                Vector3 teleportPosition = shrineDoor.transform.position;
                Destroy(shrineDoor);
                other.transform.position = teleportPosition;

                // Descongelar los GameObjects hijos del GameObject "-- Enemies"
                foreach (Transform enemy in enemiesParent.transform)
                {
                    UnfreezeGameObject(enemy.gameObject);
                }
            }
            else
            {
                Debug.LogError("No se encontró un objeto con el nombre: " + shrineDoorName);
            }
        }
    }

    private void UnfreezeGameObject(GameObject obj)
    {
        // Reactivar el Rigidbody si existe
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        // Reactivar scripts de movimiento específicos (ejemplo)
        MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }
}